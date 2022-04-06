using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Candid
{
	public enum CandidTokenType
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

	public abstract class CandidToken
	{
		public abstract CandidTokenType Type { get; }

		public static CandidToken Null { get; } = new CandidNull();

		public static CandidToken Reserved { get; } = new CandidReserved();

		public static CandidToken Empty { get; } = new CandidEmpty();

		public abstract byte[] EncodeType();
		public abstract byte[] EncodeValue();

		public CandidPrimitive AsPrimitive()
		{
			this.ValidateType(CandidTokenType.Primitive);
			return (CandidPrimitive)this;
		}

		public CandidVector AsVector()
		{
			this.ValidateType(CandidTokenType.Vector);
			return (CandidVector)this;
		}

		public CandidRecord AsRecord()
		{
			this.ValidateType(CandidTokenType.Record);
			return (CandidRecord)this;
		}

		public CandidVariant AsVariant()
		{
			this.ValidateType(CandidTokenType.Variant);
			return (CandidVariant)this;
		}

		public CandidFunc AsFunc()
		{
			this.ValidateType(CandidTokenType.Func);
			return (CandidFunc)this;
		}

		public CandidService AsService()
		{
			this.ValidateType(CandidTokenType.Service);
			return (CandidService)this;
		}

		public CandidOptional AsOptional()
		{
			this.ValidateType(CandidTokenType.Optional);
			return (CandidOptional)this;
		}

		protected void ValidateType(CandidTokenType type)
		{
			if (this.Type != type)
			{
				throw new InvalidOperationException($"Cannot convert candid type '{this.Type}' to candid type '{type}'");
			}
		}
	}
}
