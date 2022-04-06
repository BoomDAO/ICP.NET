namespace ICP.Common.Candid
{
	public class CandidFunc : CandidToken
	{
		public override CandidTokenType Type { get; } = CandidTokenType.Func;

		public byte[] CanisterId { get; }
		public string Name { get; }

		public CandidFunc(byte[] canisterId, string name)
		{
			this.CanisterId = canisterId;
			this.Name = name;
		}

		public override byte[] EncodeType()
		{
			return
		}

		public override byte[] EncodeValue()
		{
			throw new NotImplementedException();
		}
	}

}
