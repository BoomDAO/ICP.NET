using EdjCase.ICP.Candid.Mapping;
using ChallengeKey = System.String;

namespace EdjCase.ICP.InternetIdentity.Models
{
	internal class Challenge
	{
		[CandidName("png_base64")]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public string PngBase64 { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		
		[CandidName("challenge_key")]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public ChallengeKey ChallengeKey { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		
	}
}

