using Common.Models;
using System;

namespace ICP.Common.Candid
{
	public class CandidFunc : CandidValue
	{
		public override CandidValueType Type { get; } = CandidValueType.Func;

		public byte[] CanisterId { get; }
		public string Name { get; }

		public CandidFunc(byte[] canisterId, string name)
		{
			this.CanisterId = canisterId;
			this.Name = name;
		}

		public override byte[] EncodeValue()
		{

		}
	}

}
