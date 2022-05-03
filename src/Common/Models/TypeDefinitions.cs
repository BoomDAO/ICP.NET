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

	public abstract class CandidTypeDefinition : IEquatable<CandidTypeDefinition>
	{
		public abstract IDLTypeCode Type { get; }

		public abstract override bool Equals(object? obj);

		public abstract override int GetHashCode();
		public abstract override string ToString();

		public abstract byte[] Encode(CompoundTypeTable compoundTypeTable);

        public bool Equals(CandidTypeDefinition? other)
        {
			return this.Equals(other as object);
		}

		public static bool operator ==(CandidTypeDefinition? def1, CandidTypeDefinition? def2)
		{
			if(object.ReferenceEquals(def1, null))
            {
				return object.ReferenceEquals(def2, null);
            }
			return def1.Equals(def2);
		}

		public static bool operator !=(CandidTypeDefinition? def1, CandidTypeDefinition? def2)
		{
			if (object.ReferenceEquals(def1, null))
			{
				return !object.ReferenceEquals(def2, null);
			}
			return !def1.Equals(def2);
		}
	}


	public class PrimitiveCandidTypeDefinition : CandidTypeDefinition
	{
		public override IDLTypeCode Type { get; }
		public CandidPrimitiveType PrimitiveType { get; }

		public PrimitiveCandidTypeDefinition(CandidPrimitiveType type)
		{
			this.PrimitiveType = type;
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
			return LEB128.EncodeSigned((long)this.Type);
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

        public override string ToString()
        {
			return this.PrimitiveType switch
            {
                CandidPrimitiveType.Text => "text",
                CandidPrimitiveType.Nat => "nat",
                CandidPrimitiveType.Nat8 => "nat8",
                CandidPrimitiveType.Nat16 => "nat16",
                CandidPrimitiveType.Nat32 => "nat32",
                CandidPrimitiveType.Nat64 => "nat64",
                CandidPrimitiveType.Int => "int",
                CandidPrimitiveType.Int8 => "int8",
                CandidPrimitiveType.Int16 => "int16",
                CandidPrimitiveType.Int32 => "int32",
                CandidPrimitiveType.Int64 => "int64",
                CandidPrimitiveType.Float32 => "float32",
                CandidPrimitiveType.Float64 => "float64",
                CandidPrimitiveType.Bool => "bool",
                CandidPrimitiveType.Principal => "principal",
                CandidPrimitiveType.Reserved => "reserved",
                CandidPrimitiveType.Empty => "empty",
                CandidPrimitiveType.Null => "null",
                _ => throw new NotImplementedException(),
            };
        }
    }

	public abstract class CompoundCandidTypeDefinition : CandidTypeDefinition
	{
		public string? RecursiveId { get; set; }
		internal abstract byte[] EncodeInnerType(CompoundTypeTable compoundTypeTable);
		protected abstract string ToStringInternal();

		public override byte[] Encode(CompoundTypeTable compoundTypeTable)
		{
			UnboundedUInt index =  compoundTypeTable.GetOrAdd(this);
			return LEB128.EncodeSigned(index);
		}

        public override string ToString()
        {
			string value = this.ToStringInternal();
			if(this.RecursiveId != null)
            {
				value = $"{this.RecursiveId}.{value}";
            }
			return value;
        }
    }

    public class RecursiveReferenceCandidTypeDefinition : CandidTypeDefinition
    {
		public override IDLTypeCode Type => IDLTypeCode.Func; // TODO
		public string RecursiveId { get; }

		public RecursiveReferenceCandidTypeDefinition(string recursiveId)
		{
			this.RecursiveId = recursiveId;
		}

		public override byte[] Encode(CompoundTypeTable compoundTypeTable)
        {
			// Never will be encoded
			throw new NotImplementedException();
        }

        public override bool Equals(object? obj)
		{
			if (obj is RecursiveReferenceCandidTypeDefinition r)
			{
				return this.RecursiveId == r.RecursiveId;
			}
			if (obj is CompoundCandidTypeDefinition c)
			{
				return this.RecursiveId == c.RecursiveId;
			}
			return false;
        }

        public override int GetHashCode()
        {
			return HashCode.Combine(this.Type, this.RecursiveId);
        }

        public override string ToString()
        {
			return $"rec {this.RecursiveId}";
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

		internal override byte[] EncodeInnerType(CompoundTypeTable compoundTypeTable)
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

        protected override string ToStringInternal()
        {
			return $"opt {this.Value}";
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

		internal override byte[] EncodeInnerType(CompoundTypeTable compoundTypeTable)
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

		protected override string ToStringInternal()
		{
			return $"vec {this.Value}";
        }
    }

	public abstract class RecordOrVariantCandidTypeDefinition : CompoundCandidTypeDefinition
	{
		public override abstract IDLTypeCode Type { get; }
		protected abstract string TypeString { get; }

		public IReadOnlyDictionary<Label, CandidTypeDefinition> Fields { get; }

		protected RecordOrVariantCandidTypeDefinition(Dictionary<Label, CandidTypeDefinition> fields)
		{
			this.Fields = fields;
		}

		internal override byte[] EncodeInnerType(CompoundTypeTable compoundTypeTable)
		{
			byte[] fieldCount = LEB128.EncodeSigned(this.Fields.Count);
			IEnumerable<byte> fieldTypes = this.Fields
				.SelectMany(f =>
				{
					return LEB128.EncodeUnsigned(f.Key.Id)
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

		protected override string ToStringInternal()
		{
			IEnumerable<string> fields = this.Fields.Select(f => $"{f.Key}:{f.Value}");
			return $"{this.TypeString} {{{string.Join("; ", fields)}}}";
		}
	}

	public class Label : IComparable<Label>, IComparable, IEquatable<Label>
	{
		public string? Name { get; }
		public UnboundedUInt Id { get; }

		private Label(UnboundedUInt id, string? name)
		{
			this.Id = id;
			this.Name = name;
		}

		public Label(UnboundedUInt id) : this(id, null)
		{

		}


		public bool Equals(Label? other)
		{
			return this.CompareTo(other) == 0;
		}

		public override bool Equals(object? obj)
		{
			return this.Equals(obj as Label);
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

		public static Label FromId(UnboundedUInt id)
		{
			return new Label(id, null);
		}

		public static implicit operator Label(string value)
		{
			return Label.FromName(value);
		}

		public static implicit operator Label(UnboundedUInt value)
		{
			return Label.FromId(value);
		}

		public static implicit operator UnboundedUInt(Label value)
		{
			return value.Id;
		}

        public override string ToString()
        {
			string value = this.Id.ToString();
			if(this.Name != null)
            {
				value += $"({this.Name})";
            }
			return value;
        }
    }

	public class RecordCandidTypeDefinition : RecordOrVariantCandidTypeDefinition
	{
		public override IDLTypeCode Type { get; } = IDLTypeCode.Record;
		protected override string TypeString { get; } = "record";


		public RecordCandidTypeDefinition(Dictionary<Label, CandidTypeDefinition> fields) : base(fields)
		{
		}

	}

	public class VariantCandidTypeDefinition : RecordOrVariantCandidTypeDefinition
	{
		public override IDLTypeCode Type { get; } = IDLTypeCode.Variant;

		protected override string TypeString { get; } = "variant";

		public VariantCandidTypeDefinition(Dictionary<Label, CandidTypeDefinition> options) : base(options)
		{
			if (options?.Any() != true)
			{
				throw new ArgumentException("At least one variant option must be specified", nameof(options));
			}
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

		internal override byte[] EncodeInnerType(CompoundTypeTable compoundTypeTable)
		{
			byte[] methodCount = LEB128.EncodeSigned(this.Methods.Count);
			IEnumerable<byte> methodTypes = this.Methods
				.OrderBy(m => m.Key) // Ordered by method name
				.SelectMany(m =>
				{
					byte[] encodedName = Encoding.UTF8.GetBytes(m.Key);
					byte[] encodedNameLength = LEB128.EncodeSigned(encodedName.Length);
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

		protected override string ToStringInternal()
		{
			// TODO
            throw new NotImplementedException();
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

		internal override byte[] EncodeInnerType(CompoundTypeTable compoundTypeTable)
		{
			byte[] argsCount = LEB128.EncodeSigned(this.ArgTypes.Count);

			IEnumerable<byte> argTypes = this.ArgTypes
				.SelectMany(a => a.Encode(compoundTypeTable));

			byte[] returnsCount = LEB128.EncodeSigned(this.ReturnTypes.Count);

			IEnumerable<byte> returnTypes = this.ReturnTypes
				.SelectMany(a => a.Encode(compoundTypeTable));

			byte[] modesCount = LEB128.EncodeSigned(this.Modes.Count);

			IEnumerable<byte> modeTypes = this.Modes
				.SelectMany(m => LEB128.EncodeSigned((UnboundedInt)(long)m));

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

		protected override string ToStringInternal()
		{
			// TODO
            throw new NotImplementedException();
        }
    }
}


public enum FuncMode
{
	Oneway = 2, // No response
	Query = 1 // Response
}