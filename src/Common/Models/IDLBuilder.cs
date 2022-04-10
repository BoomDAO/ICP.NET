using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Models;
using ICP.Common.Candid;
using ICP.Common.Candid.Constants;
using ICP.Common.Encodings;

namespace ICP.Common.Models
{
	public class CompoundTypeTable
	{
		/// <summary>
		/// Bytes of compound types. The types are referenced by index such as in in `EncodedTypes`
		/// </summary>
		private readonly List<byte[]> EncodedCompoundTypes = new List<byte[]>();

		/// <summary>
		/// A mapping of compound type definition to `EncodedCompoundTypes` index to be used as reference
		/// </summary>
		private readonly Dictionary<CompoundCandidTypeDefinition, int> CompoundTypeIndexMap = new Dictionary<CompoundCandidTypeDefinition, int>();

		public byte[] GetOrAdd(CompoundCandidTypeDefinition def, Func<CompoundTypeTable, byte[]> encodeFunc)
		{
			if (!this.CompoundTypeIndexMap.TryGetValue(def, out int index))
			{
				byte[] encodedType = encodeFunc(this);
				this.EncodedCompoundTypes.Add(encodedType);
				index = this.EncodedCompoundTypes.Count - 1;
				this.CompoundTypeIndexMap.Add(def, index);
			}
			return this.EncodedCompoundTypes[index];
		}

		public IEnumerable<byte> Encode()
		{
			byte[] compoundTypesCount = LEB128.FromUInt64((ulong)this.EncodedCompoundTypes.Count).Raw;
			return compoundTypesCount
				.Concat(this.EncodedCompoundTypes.SelectMany(t => t));
		}
	}

	public class IDLBuilder
	{
		/// <summary>
		/// Helper to capture compound types
		/// </summary>
		private readonly CompoundTypeTable compoundTypeTable = new CompoundTypeTable();
		/// <summary>
		/// Ordered list of encoded types (encoded with SLEB128).
		/// If SLEB value is positive, it is an index for `EncodedCompoundTypes` for a compound type
		/// If SLEB value is negative, it is type code for a primitive value
		/// </summary>
		private readonly List<byte[]> EncodedTypes = new List<byte[]>();
		/// <summary>
		/// Ordered list of encoded values
		/// </summary>
		private readonly List<byte[]> EncodedValues = new List<byte[]>();


		public void Add(CandidValue value, CandidTypeDefinition def)
		{
			byte[] encodedType = def.Encode(this.compoundTypeTable);
			this.EncodedTypes.Add(encodedType);
			byte[] encodedValue = value.EncodeValue();
			this.EncodedValues.Add(encodedValue);
		}

		//private void EncodeType(CandidValue value)
		//{

		//	IDLTypeCode type = value.Type switch
		//	{
		//		CandidValueType.Primitive => this.GetIDLType(((CandidPrimitive)value).ValueType),
		//		CandidValueType.Vector => IDLTypeCode.Vector,
		//		CandidValueType.Record => IDLTypeCode.Record,
		//		CandidValueType.Variant => IDLTypeCode.Variant,
		//		CandidValueType.Func => IDLTypeCode.Func,
		//		CandidValueType.Service => IDLTypeCode.Service,
		//		CandidValueType.Reserved => IDLTypeCode.Reserved,
		//		CandidValueType.Empty => IDLTypeCode.Empty,
		//		CandidValueType.Null => IDLTypeCode.Null,
		//		CandidValueType.Optional => IDLTypeCode.Opt,
		//		_ => throw new NotImplementedException(),
		//	};
		//	long typeCodeOrIndex;
		//	switch (type)
		//	{
		//		case IDLTypeCode.Opt:
		//		case IDLTypeCode.Vector:
		//		case IDLTypeCode.Record:
		//		case IDLTypeCode.Variant:
		//		case IDLTypeCode.Service:
		//		case IDLTypeCode.Func:
		//			// Compound types definitions are put into the 'type table'
		//			// then references by index (indices are positive, type codes are negative)
		//			CompoundCandidTypeDefinition typeDef = value.BuildTypeDefinition();
		//			if (!this.CompoundTypeIndexMap.TryGetValue(typeDef, out int index))
		//			{
		//				byte[] encodedType = typeDef.Encode();
		//				this.EncodedCompoundTypes.Add(encodedType);
		//				index = this.EncodedCompoundTypes.Count - 1;
		//				this.CompoundTypeIndexMap[typeDef] = index;
		//			}
		//			typeCodeOrIndex = index;
		//			break;
		//		default:
		//			// Primitive types are just indicated by their code (all negative values to differentiate between compound types)
		//			typeCodeOrIndex = (long)type;
		//			break;
		//	}
		//	// Encode code or index as a SLEB128
		//	byte[] bytes = SLEB128.FromInt64(typeCodeOrIndex).Raw;
		//	this.EncodedTypes.Add(bytes);
		//}

		private IDLTypeCode GetIDLType(CandidPrimitiveType type)
		{
			return type switch
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
				CandidPrimitiveType.Principal => IDLTypeCode.Principal,
				_ => throw new NotImplementedException()
			};
		}

		public static IDLBuilder FromArgs(IEnumerable<(CandidValue, CandidTypeDefinition)> values)
		{
			var builder = new IDLBuilder();
			foreach ((CandidValue value, CandidTypeDefinition def) in values)
			{
				builder.Add(value, def);
			}
			return builder;
		}

		public byte[] Encode()
		{
			byte[] encodedPrefix = Encoding.UTF8.GetBytes("DIDL");

			IEnumerable<byte> encodedTypes = this.GenerateTypeEncoding();
			IEnumerable<byte> encodedValues = this.GenerateValueEncoding();
			return encodedPrefix
				.Concat(encodedTypes)
				.Concat(encodedValues)
				.ToArray();
		}

		private IEnumerable<byte> GenerateTypeEncoding()
		{
			byte[] encodedTypesCount = LEB128.FromUInt64((ulong)this.EncodedTypes.Count).Raw;

			return this.compoundTypeTable.Encode()
				.Concat(encodedTypesCount)
				.Concat(this.EncodedTypes.SelectMany(t => t));
		}

		private IEnumerable<byte> GenerateValueEncoding()
		{
			return this.EncodedValues.SelectMany(v => v);
		}
	}
}