using System;
using System.Linq;

namespace ICP.Common.Candid
{
	public class CandidOptional : CandidValue
	{
		public override CandidValueType Type { get; } = CandidValueType.Optional;
		public CandidValue Value { get; }

		public CandidOptional(CandidValue value)
		{
			this.Value = value ?? throw new ArgumentNullException(nameof(value));
		}

		public override byte[] EncodeValue()
		{
			if(this.Value.Type == CandidValueType.Null)
			{
				return new byte[] { 0 };
			}
			return new byte[] { 1 }
				.Concat(this.Value.EncodeValue())
				.ToArray();
		}
	}
}