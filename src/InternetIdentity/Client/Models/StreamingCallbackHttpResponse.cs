using EdjCase.ICP.Candid.Mapping;
using PublicKey = System.Collections.Generic.List<System.Byte>;

namespace EdjCase.ICP.InternetIdentity.Models
{
	public class StreamingCallbackHttpResponse
	{
		[CandidName("body")]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public PublicKey Body { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		
		[CandidName("token")]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public Candid.Models.OptionalValue<Token> Token { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		
	}
}

