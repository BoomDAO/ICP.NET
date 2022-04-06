namespace Candid
{
	public class CandidVector : CandidToken
	{
		public override CandidTokenType Type { get; } = CandidTokenType.Vector;

		public CandidToken[] Values { get; }

		public CandidVector(CandidToken[] values)
		{
			CandidTokenType? valueType = values.FirstOrDefault()?.Type;
			if(valueType != null && values.Any(v => v.Type != valueType))
			{
				throw new ArgumentException("All vector values must be the same type");
			}
			this.Values = values;
		}
	}

}
