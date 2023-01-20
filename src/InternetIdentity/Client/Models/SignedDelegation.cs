using EdjCase.ICP.Candid.Mapping;

namespace EdjCase.ICP.InternetIdentity.Models
{
	public class SignedDelegation
	{
		[CandidName("delegation")]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public Delegation Delegation { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		
		[CandidName("signature")]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public byte[] Signature { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		
	}
}

