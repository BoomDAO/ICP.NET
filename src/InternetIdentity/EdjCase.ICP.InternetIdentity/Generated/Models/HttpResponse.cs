using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid;
using UserNumber = System.UInt64;
using PublicKey = System.Collections.Generic.List<System.Byte>;
using CredentialId = System.Collections.Generic.List<System.Byte>;
using DeviceKey = PublicKey;
using UserKey = PublicKey;
using SessionKey = PublicKey;
using FrontendHostname = System.String;
using Timestamp = System.UInt64;
using ChallengeKey = System.String;

namespace EdjCase.ICP.InternetIdentity.Models
{
	public class HttpResponse
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("status_code")]
		public ushort StatusCode { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("headers")]
		public System.Collections.Generic.List<HeaderField> Headers { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("body")]
		public System.Collections.Generic.List<byte> Body { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("streaming_strategy")]
		public EdjCase.ICP.Candid.Models.OptionalValue<StreamingStrategy> StreamingStrategy { get; set; }
		
	}
}

