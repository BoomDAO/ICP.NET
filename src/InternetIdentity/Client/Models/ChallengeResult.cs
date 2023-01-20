using EdjCase.ICP.Candid.Mapping;
using ChallengeKey = System.String;

namespace EdjCase.ICP.InternetIdentity.Models
{
	internal class ChallengeResult
	{
		[CandidName("key")]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public ChallengeKey Key { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		
		[CandidName("chars")]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public string Chars { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		
	}
}

