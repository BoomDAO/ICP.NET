using Common.Models;
using ICP.Common.Models;
using System;

namespace ICP.Common.Candid
{
	public enum CandidValueType
	{
		Primitive,
		Vector,
		Record,
		Variant,
		Func,
		Service,
		Reserved,
		Empty,
		Null,
		Optional
	}

	public abstract class CandidValue
	{
		public abstract CandidValueType Type { get; }

		public static CandidValue Null { get; } = new CandidNull();

		public static CandidValue Reserved { get; } = new CandidReserved();

		public static CandidValue Empty { get; } = new CandidEmpty();

		public abstract CandidTypeDefinition BuildTypeDefinition();

		public abstract byte[] EncodeValue();

		public CandidPrimitive AsPrimitive()
		{
			this.ValidateType(CandidValueType.Primitive);
			return (CandidPrimitive)this;
		}

		public CandidVector AsVector()
		{
			this.ValidateType(CandidValueType.Vector);
			return (CandidVector)this;
		}

		public CandidRecord AsRecord()
		{
			this.ValidateType(CandidValueType.Record);
			return (CandidRecord)this;
		}

		public CandidVariant AsVariant()
		{
			this.ValidateType(CandidValueType.Variant);
			return (CandidVariant)this;
		}

		public CandidFunc AsFunc()
		{
			this.ValidateType(CandidValueType.Func);
			return (CandidFunc)this;
		}

		public CandidService AsService()
		{
			this.ValidateType(CandidValueType.Service);
			return (CandidService)this;
		}

		public CandidOptional AsOptional()
		{
			this.ValidateType(CandidValueType.Optional);
			return (CandidOptional)this;
		}

		protected void ValidateType(CandidValueType type)
		{
			if (this.Type != type)
			{
				throw new InvalidOperationException($"Cannot convert candid type '{this.Type}' to candid type '{type}'");
			}
		}
	}
}
