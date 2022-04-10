using Common.Models;
using System;

namespace ICP.Common.Candid
{
	public class CandidEmpty : CandidValue
	{
		public override CandidValueType Type { get; } = CandidValueType.Empty;

		public override byte[] EncodeValue()
		{
			throw new InvalidOperationException("Emtpy values cannot appear as a function argument");
		}
	}
}