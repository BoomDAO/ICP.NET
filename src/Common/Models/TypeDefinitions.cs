using ICP.Common.Candid.Constants;
using ICP.Common.Encodings;
using ICP.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{

	public abstract class CandidTypeDefinition
	{
		public abstract IDLTypeCode TypeCode { get; }

		public abstract override bool Equals(object? obj);

		public abstract override int GetHashCode();

		public abstract byte[] Encode(CompoundTypeTable compoundTypeTable);
	}


	public class PrimitiveCandidTypeDefinition : CandidTypeDefinition
	{
		public override IDLTypeCode TypeCode { get; }

		public PrimitiveCandidTypeDefinition(IDLTypeCode typeCode)
		{
			this.TypeCode = typeCode;
		}

		public override byte[] Encode(CompoundTypeTable compoundTypeTable)
		{
			return SLEB128.FromInt64((long)this.TypeCode).Raw;
		}

		public override bool Equals(object? obj)
		{
			if (obj is PrimitiveCandidTypeDefinition pDef)
			{
				return this.TypeCode == pDef.TypeCode;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return (int)this.TypeCode;
		}
	}

	public abstract class CompoundCandidTypeDefinition : CandidTypeDefinition
	{
		protected abstract byte[] EncodeInnerType(CompoundTypeTable compoundTypeTable);

		public override byte[] Encode(CompoundTypeTable compoundTypeTable)
		{
			return compoundTypeTable.GetOrAdd(this, this.EncodeInternal);
		}

		private byte[] EncodeInternal(CompoundTypeTable compoundTypeTable)
		{
			byte[] encodedInnerValue = this.EncodeInnerType(compoundTypeTable);
			return SLEB128.FromInt64((long)this.TypeCode).Raw
				.Concat(encodedInnerValue)
				.ToArray();
		}
	}

	public class OptCandidTypeDefinition : CompoundCandidTypeDefinition
	{
		public override IDLTypeCode TypeCode { get; } = IDLTypeCode.Opt;
		public CandidTypeDefinition Value { get; }

		public OptCandidTypeDefinition(CandidTypeDefinition value)
		{
			this.Value = value ?? throw new ArgumentNullException(nameof(value));
		}

		protected override byte[] EncodeInnerType(CompoundTypeTable compoundTypeTable)
		{
			return this.Value.Encode(compoundTypeTable);
		}

		public override bool Equals(object? obj)
		{
			if (obj is OptCandidTypeDefinition def)
			{
				return this.Value == def.Value;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(IDLTypeCode.Opt, this.Value);
		}
	}

	public class VectorCandidTypeDefinition : CompoundCandidTypeDefinition
	{
		public override IDLTypeCode TypeCode { get; } = IDLTypeCode.Vector;

		public CandidTypeDefinition Value { get; }

		public VectorCandidTypeDefinition(CandidTypeDefinition value)
		{
			this.Value = value ?? throw new ArgumentNullException(nameof(value));
		}

		protected override byte[] EncodeInnerType(CompoundTypeTable compoundTypeTable)
		{
			return this.Value.Encode(compoundTypeTable);
		}

		public override bool Equals(object? obj)
		{
			if (obj is VectorCandidTypeDefinition def)
			{
				return this.Value == def.Value;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(IDLTypeCode.Vector, this.Value);
		}
	}

	public abstract class RecordOrVariantCandidTypeDefinition : CompoundCandidTypeDefinition
	{
		public override abstract IDLTypeCode TypeCode { get; }

		protected abstract string ArgumentExceptionMessage { get; }

		public IReadOnlyDictionary<Label, CandidTypeDefinition> Fields { get; }

		protected RecordOrVariantCandidTypeDefinition(Dictionary<Label, CandidTypeDefinition> fields)
		{
			if(fields?.Any() != true)
			{
				throw new ArgumentException(this.ArgumentExceptionMessage, nameof(fields));
			}
			this.Fields = fields;
		}

		protected override byte[] EncodeInnerType(CompoundTypeTable compoundTypeTable)
		{
			byte[] fieldCount = LEB128.FromUInt64((ulong)this.Fields.Count).Raw;
			IEnumerable<byte> fieldTypes = this.Fields
				.SelectMany(f =>
				{
					return LEB128.FromUInt64(f.Key.Id).Raw
						.Concat(f.Value.Encode(compoundTypeTable));
				});
			return fieldCount
				.Concat(fieldTypes)
				.ToArray(); ;
		}

		public override bool Equals(object? obj)
		{
			bool exactType = this.GetType() == obj?.GetType();
			if (exactType && obj is RecordOrVariantCandidTypeDefinition def)
			{
				if(this.Fields.Count != def.Fields.Count)
				{
					return false;
				}
				foreach((Label fLabel, CandidTypeDefinition fDef) in this.Fields)
				{
					if(!def.Fields.TryGetValue(fLabel, out CandidTypeDefinition? otherFDef))
					{
						return false;
					}
					if(fDef != otherFDef)
					{
						return false;
					}
				}
				return true;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(this.TypeCode, this.Fields);
		}
	}

	public class Label
	{
		public string? Name { get; }
		public uint Id { get; }

		private Label(uint id, string? name)
		{
			this.Id = id;
			this.Name = name;
		}

		public Label(uint id) : this(id, null)
		{

		}

		public override bool Equals(object? obj)
		{
			if(obj is Label l)
			{
				return this.Id == l.Id;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(this.Id);
		}


		/// <summary>
		/// Hashes the name to get the proper id 
		/// hash(name) = ( Sum_(i=0..k) utf8(name)[i] * 223^(k-i) ) mod 2^32 where k = |utf8(name)|-1
		/// </summary>
		/// <param name="name">Name to hash</param>
		/// <returns>Unsigned 32 byte integer hash</returns>
		public static uint HashName(string name)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(name);
			int k = bytes.Length - 1;
			return (uint)bytes
				.Select((v, i) => Math.Pow(v * 223, k - i) % Math.Pow(2, 32))
				.Sum();
		}

		public static Label FromName(string name)
		{
			uint id = Label.HashName(name);

			return new Label(id, name);
		}
	}

	public class RecordCandidTypeDefinition : RecordOrVariantCandidTypeDefinition
	{
		public override IDLTypeCode TypeCode { get; } = IDLTypeCode.Record;

		protected override string ArgumentExceptionMessage { get; } = "At least one record field must be specified";

		public RecordCandidTypeDefinition(Dictionary<Label, CandidTypeDefinition> fields) : base(fields)
		{
		}

	}

	public class VariantCandidTypeDefinition : RecordOrVariantCandidTypeDefinition
	{
		public override IDLTypeCode TypeCode { get; } = IDLTypeCode.Variant;

		protected override string ArgumentExceptionMessage { get; } = "At least one variant option must be specified";

		public VariantCandidTypeDefinition(Dictionary<Label, CandidTypeDefinition> options) : base(options)
		{
		}

	}

	public class ServiceCandidTypeDefinition : CompoundCandidTypeDefinition
	{
		public override IDLTypeCode TypeCode { get; } = IDLTypeCode.Service;

		public IReadOnlyList<(string MethodName, FuncCandidTypeDefinition Type)> Methods { get; }

		public ServiceCandidTypeDefinition(List<(string MethodName, CandidTypeDefinition Type)> methods)
		{
			this.Methods = methods;
		}

		protected override byte[] EncodeInnerType(CompoundTypeTable compoundTypeTable)
		{
			byte[] methodCount = LEB128.FromUInt64((ulong)this.Methods.Count).Raw;
			IEnumerable<byte> methodTypes = this.Methods
				.SelectMany(m =>
				{
					byte[] encodedName = Encoding.UTF8.GetBytes(m.MethodName);
					byte[] encodedNameLength = LEB128.FromUInt64((ulong)encodedName.Length).Raw;
					return encodedNameLength
					.Concat(encodedName)
					.Concat(m.Type.Encode(compoundTypeTable));
				});
			return methodCount
				.Concat(methodTypes)
				.ToArray();
		}

		public override bool Equals(object? obj)
		{
			if(obj is ServiceCandidTypeDefinition sDef)
			{
				return sDef.Methods
					.OrderBy(s => s.MethodName)
					.SequenceEqual(sDef.Methods.OrderBy(s => s.MethodName));
			}
			return false;
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(this.TypeCode, this.Methods);
		}
	}


	public class FuncCandidTypeDefinition : CompoundCandidTypeDefinition
	{
		public override IDLTypeCode TypeCode { get; } = IDLTypeCode.Func;

		public IReadOnlyList<FuncMode> Modes { get; }
		public IReadOnlyList<CandidTypeDefinition> ArgTypes { get; }
		public IReadOnlyList<CandidTypeDefinition> ReturnTypes { get; }

		public FuncCandidTypeDefinition(
			List<FuncMode> modes,
			List<CandidTypeDefinition> argTypes,
			List<CandidTypeDefinition> returnTypes)
		{
			this.Modes = modes.Distinct().ToList();
			this.ArgTypes = argTypes;
			this.ReturnTypes = returnTypes;
		}

		protected override byte[] EncodeInnerType(CompoundTypeTable compoundTypeTable)
		{
			byte[] argsCount = LEB128.FromUInt64((ulong)this.ArgTypes.Count).Raw;

			IEnumerable<byte> argTypes = this.ArgTypes
				.SelectMany(a => a.Encode(compoundTypeTable));

			byte[] returnsCount = LEB128.FromUInt64((ulong)this.ReturnTypes.Count).Raw;

			IEnumerable<byte> returnTypes = this.ReturnTypes
				.SelectMany(a => a.Encode(compoundTypeTable));

			byte[] modesCount = LEB128.FromUInt64((ulong)this.Modes.Count).Raw;

			IEnumerable<byte> modeTypes = this.Modes
				.SelectMany(m => SLEB128.FromInt64((long)m).Raw);

			return argsCount
				.Concat(argTypes)
				.Concat(returnsCount)
				.Concat(returnTypes)
				.Concat(modesCount)
				.Concat(modeTypes)
				.ToArray();
		}

		public override bool Equals(object? obj)
		{
			if (obj is ServiceCandidTypeDefinition sDef)
			{
				return sDef.Methods
					.OrderBy(s => s.MethodName)
					.SequenceEqual(sDef.Methods.OrderBy(s => s.MethodName));
			}
			return false;
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(this.TypeCode, this.Modes, this.ArgTypes, this.ReturnTypes);
		}
	}
}


public enum FuncMode
{
	Oneway, // No response
	Query // Response
}