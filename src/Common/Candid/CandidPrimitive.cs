using Candid.Constants;
using ICP.Common.Candid.Constants;
using ICP.Common.Encodings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICP.Common.Candid
{
	public enum CandidPrimitiveType
	{
		Text,
		Nat,
		Nat8,
		Nat16,
		Nat32,
		Nat64,
		Int,
		Int8,
		Int16,
		Int32,
		Int64,
		Float32,
		Float64,
		Bool,
		Principal
	}

	public class CandidPrimitive : CandidValue
	{
		public override CandidValueType Type { get; } = CandidValueType.Primitive;
		public CandidPrimitiveType ValueType { get; }
		private readonly object value;

		private CandidPrimitive(CandidPrimitiveType valueType, object value)
		{
			this.ValueType = valueType;
			this.value = value;
		}


		public override byte[] BuildTypeDefinition()
		{
		}

		public override byte[] EncodeValue()
		{
			return this.ValueType switch
			{
				CandidPrimitiveType.Text => LEB128.FromUInt64((ulong)this.AsText().Length).Raw
					.Concat(raw),
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

		public string AsText()
		{
			this.ValidateType(CandidPrimitiveType.Text);
			return (string)this.value;
		}

		public UnboundedUInt AsNat()
		{
			this.ValidateType(CandidPrimitiveType.Nat);
			return (UnboundedUInt)this.value;
		}

		public byte AsNat8()
		{
			this.ValidateType(CandidPrimitiveType.Nat8);
			return (byte)this.value;
		}

		public ushort AsNat16()
		{
			this.ValidateType(CandidPrimitiveType.Nat16);
			return (ushort)this.value;
		}

		public uint AsNat32()
		{
			this.ValidateType(CandidPrimitiveType.Nat32);
			return (uint)this.value;
		}

		public ulong AsNat64()
		{
			this.ValidateType(CandidPrimitiveType.Nat64);
			return (uint)this.value;
		}

		public UnboundedInt AsInt()
		{
			this.ValidateType(CandidPrimitiveType.Int);
			return (UnboundedInt)this.value;
		}

		public byte AsInt8()
		{
			this.ValidateType(CandidPrimitiveType.Int8);
			return (byte)this.value;
		}

		public short AsInt16()
		{
			this.ValidateType(CandidPrimitiveType.Int16);
			return (short)this.value;
		}

		public int AsInt32()
		{
			this.ValidateType(CandidPrimitiveType.Int32);
			return (int)this.value;
		}

		public long AsInt64()
		{
			this.ValidateType(CandidPrimitiveType.Int64);
			return (long)this.value;
		}

		public float AsFloat32()
		{
			this.ValidateType(CandidPrimitiveType.Float32);
			return (float)this.value;
		}

		public double AsFloat64()
		{
			this.ValidateType(CandidPrimitiveType.Float64);
			return (double)this.value;
		}

		public bool AsBool()
		{
			this.ValidateType(CandidPrimitiveType.Bool);
			return (bool)this.value;
		}

		public byte[] AsPrincipal()
		{
			this.ValidateType(CandidPrimitiveType.Principal);
			return (byte[])this.value;
		}



		public static CandidValue Blob(byte[] value)
		{
			return new CandidPrimitive(CandidPrimitiveType.Blob, value);
		}

		public static CandidPrimitive Nat(UnboundedUInt value)
		{
			return new CandidPrimitive(CandidPrimitiveType.Nat, value);
		}

		public static CandidValue Text(string value)
		{
			return new CandidPrimitive(CandidPrimitiveType.Text, value);
		}





		protected void ValidateType(CandidPrimitiveType type)
		{
			if (this.ValueType != type)
			{
				throw new InvalidOperationException($"Cannot convert candid type '{this.Type}' to candid type '{type}'");
			}
		}
	}
}
