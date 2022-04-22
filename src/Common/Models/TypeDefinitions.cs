using ICP.Common.Candid;
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
		public abstract IDLTypeCode Type { get; }

		public abstract override bool Equals(object? obj);

		public abstract override int GetHashCode();

		public abstract byte[] Encode(CompoundTypeTable compoundTypeTable);
	}


	public class PrimitiveCandidTypeDefinition : CandidTypeDefinition
	{
		public override IDLTypeCode Type { get; }

		public PrimitiveCandidTypeDefinition(CandidPrimitiveType type)
		{
			this.Type = type switch
            {
                CandidPrimitiveType.Text => IDLTypeCode.Text,
                CandidPrimitiveType.Nat => IDLTypeCode.Nat,
                CandidPrimitiveType.Nat8 => IDLTypeCode.Nat8,
                CandidPrimitiveType.Nat16 => IDLTypeCode.Nat16,
                CandidPrimitiveType.Nat32 => IDLTypeCode.Nat32,
                CandidPrimitiveType.Nat64 => IDLTypeCode.Nat64,
                CandidPrimitiveType.Int => IDLTypeCode.Int,
                CandidPrimitiveType.Int8 => IDLTypeCode.Int8,
                CandidPrimitiveType.Int16 => IDLTypeCode.Int16,
                CandidPrimitiveType.Int32 => IDLTypeCode.Int32,
                CandidPrimitiveType.Int64 => IDLTypeCode.Int64,
                CandidPrimitiveType.Float32 => IDLTypeCode.Float32,
                CandidPrimitiveType.Float64 => IDLTypeCode.Float64,
                CandidPrimitiveType.Bool => IDLTypeCode.Bool,
				CandidPrimitiveType.Null => IDLTypeCode.Null,
				CandidPrimitiveType.Empty => IDLTypeCode.Empty,
				CandidPrimitiveType.Reserved => IDLTypeCode.Reserved,
				CandidPrimitiveType.Principal => IDLTypeCode.Principal,
				_ => throw new NotImplementedException(),
            };
		}

		public override byte[] Encode(CompoundTypeTable compoundTypeTable)
		{
			return SLEB128.FromInt64((long)this.Type).Raw;
		}

		public override bool Equals(object? obj)
		{
			if (obj is PrimitiveCandidTypeDefinition pDef)
			{
				return this.Type == pDef.Type;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return (int)this.Type;
		}
	}

	public abstract class CompoundCandidTypeDefinition : CandidTypeDefinition
	{
		protected abstract byte[] EncodeInnerType(CompoundTypeTable compoundTypeTable);

		public override byte[] Encode(CompoundTypeTable compoundTypeTable)
		{
			int index =  compoundTypeTable.GetOrAdd(this, this.EncodeInternal);
			return SLEB128.FromInt64(index).Raw;
		}

		private byte[] EncodeInternal(CompoundTypeTable compoundTypeTable)
		{
			byte[] encodedInnerValue = this.EncodeInnerType(compoundTypeTable);
			return SLEB128.FromInt64((long)this.Type).Raw
				.Concat(encodedInnerValue)
				.ToArray();
		}
	}

	public class OptCandidTypeDefinition : CompoundCandidTypeDefinition
	{
		public override IDLTypeCode Type { get; } = IDLTypeCode.Opt;
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
		public override IDLTypeCode Type { get; } = IDLTypeCode.Vector;

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
		public override abstract IDLTypeCode Type { get; }

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
			return HashCode.Combine(this.Type, this.Fields);
		}
	}

	public class Label : IComparable<Label>, IComparable
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
			return this.CompareTo(obj) == 0;
		}

		public int CompareTo(object? obj)
		{
			return this.CompareTo(obj as Label);
		}

		public int CompareTo(Label? other)
		{
			if(other == null)
            {
				return 1;
            }
			return this.Id.CompareTo(other.Id);
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
			uint digest = 0;
			foreach(byte b in bytes)
            {
				digest = (digest * 223) + b;
            }
			return digest;
		}

		public static Label FromName(string name)
		{
			uint id = Label.HashName(name);

			return new Label(id, name);
		}
    }

	public class RecordCandidTypeDefinition : RecordOrVariantCandidTypeDefinition
	{
		public override IDLTypeCode Type { get; } = IDLTypeCode.Record;

		protected override string ArgumentExceptionMessage { get; } = "At least one record field must be specified";

		public RecordCandidTypeDefinition(Dictionary<Label, CandidTypeDefinition> fields) : base(fields)
		{
		}

	}

	public class VariantCandidTypeDefinition : RecordOrVariantCandidTypeDefinition
	{
		public override IDLTypeCode Type { get; } = IDLTypeCode.Variant;

		protected override string ArgumentExceptionMessage { get; } = "At least one variant option must be specified";

		public VariantCandidTypeDefinition(Dictionary<Label, CandidTypeDefinition> options) : base(options)
		{
		}

	}

	public class ServiceCandidTypeDefinition : CompoundCandidTypeDefinition
	{
		public override IDLTypeCode Type { get; } = IDLTypeCode.Service;

		public IReadOnlyDictionary<string, FuncCandidTypeDefinition> Methods { get; }

		public ServiceCandidTypeDefinition(IReadOnlyDictionary<string, FuncCandidTypeDefinition> methods)
		{
			this.Methods = methods;
		}

		protected override byte[] EncodeInnerType(CompoundTypeTable compoundTypeTable)
		{
			byte[] methodCount = LEB128.FromUInt64((ulong)this.Methods.Count).Raw;
			IEnumerable<byte> methodTypes = this.Methods
				.OrderBy(m => m.Key) // Ordered by method name
				.SelectMany(m =>
				{
					byte[] encodedName = Encoding.UTF8.GetBytes(m.Key);
					byte[] encodedNameLength = LEB128.FromUInt64((ulong)encodedName.Length).Raw;
					return encodedNameLength
					.Concat(encodedName)
					.Concat(m.Value.Encode(compoundTypeTable));
				});
			return methodCount
				.Concat(methodTypes)
				.ToArray();
		}

		public override bool Equals(object? obj)
		{
			if(obj is ServiceCandidTypeDefinition sDef)
			{
				return this.Methods
					.OrderBy(s => s.Key)
					.SequenceEqual(sDef.Methods.OrderBy(s => s.Key));
			}
			return false;
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(this.Type, this.Methods);
		}
	}


	public class FuncCandidTypeDefinition : CompoundCandidTypeDefinition
	{
		public override IDLTypeCode Type { get; } = IDLTypeCode.Func;

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
			if (obj is FuncCandidTypeDefinition sDef)
			{
				return this.Modes
					.OrderBy(s => s)
					.SequenceEqual(sDef.Modes.OrderBy(s => s))
					&& this.ArgTypes.SequenceEqual(sDef.ArgTypes)
					&& this.ReturnTypes.SequenceEqual(sDef.ArgTypes);
			}
			return false;
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(this.Type, this.Modes, this.ArgTypes, this.ReturnTypes);
		}
	}
}


public enum FuncMode
{
	Oneway, // No response
	Query // Response
}