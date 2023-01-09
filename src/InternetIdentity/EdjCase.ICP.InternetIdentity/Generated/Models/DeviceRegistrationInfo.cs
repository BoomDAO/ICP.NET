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
	public class DeviceRegistrationInfo
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("tentative_device")]
		public EdjCase.ICP.Candid.Models.OptionalValue<DeviceData> TentativeDevice { get; set; }
		
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("expiration")]
		public Timestamp Expiration { get; set; }
		
	}
}

