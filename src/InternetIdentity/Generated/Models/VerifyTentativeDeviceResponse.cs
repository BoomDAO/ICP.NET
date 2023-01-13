using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid;
using UserNumber = System.UInt64;
using PublicKey = System.Collections.Generic.List<System.Byte>;
using CredentialId = System.Collections.Generic.List<System.Byte>;
using DeviceKey = System.Collections.Generic.List<System.Byte>;
using UserKey = System.Collections.Generic.List<System.Byte>;
using SessionKey = System.Collections.Generic.List<System.Byte>;
using FrontendHostname = System.String;
using Timestamp = System.UInt64;
using ChallengeKey = System.String;

namespace EdjCase.ICP.InternetIdentity.Models
{
	[EdjCase.ICP.Candid.Mapping.VariantAttribute(typeof(VerifyTentativeDeviceResponseTag))]
	public class VerifyTentativeDeviceResponse
	{
		[EdjCase.ICP.Candid.Mapping.VariantTagPropertyAttribute]
		public VerifyTentativeDeviceResponseTag Tag { get; set; }
		[EdjCase.ICP.Candid.Mapping.VariantValuePropertyAttribute]
		public object? Value { get; set; }
		private VerifyTentativeDeviceResponse(VerifyTentativeDeviceResponseTag tag, System.Object? value)
		{
			this.Tag = tag;
			this.Value = value;
		}
		
		protected VerifyTentativeDeviceResponse()
		{
		}
		
		public static VerifyTentativeDeviceResponse Verified()
		{
			return new VerifyTentativeDeviceResponse(VerifyTentativeDeviceResponseTag.Verified, null);
		}
		
		public class O1
		{
			[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("retries_left")]
			public byte RetriesLeft { get; set; }
			
		}
		public static VerifyTentativeDeviceResponse WrongCode(VerifyTentativeDeviceResponse.O1 info)
		{
			return new VerifyTentativeDeviceResponse(VerifyTentativeDeviceResponseTag.WrongCode, info);
		}
		
		public VerifyTentativeDeviceResponse.O1 AsWrongCode()
		{
			this.ValidateTag(VerifyTentativeDeviceResponseTag.WrongCode);
			return (VerifyTentativeDeviceResponse.O1)this.Value!;
		}
		
		public static VerifyTentativeDeviceResponse DeviceRegistrationModeOff()
		{
			return new VerifyTentativeDeviceResponse(VerifyTentativeDeviceResponseTag.DeviceRegistrationModeOff, null);
		}
		
		public static VerifyTentativeDeviceResponse NoDeviceToVerify()
		{
			return new VerifyTentativeDeviceResponse(VerifyTentativeDeviceResponseTag.NoDeviceToVerify, null);
		}
		
		private void ValidateTag(VerifyTentativeDeviceResponseTag tag)
		{
			if (!this.Tag.Equals(tag))
			{
				throw new InvalidOperationException($"Cannot cast '{this.Tag}' to type '{tag}'");
			}
		}
	}
	public enum VerifyTentativeDeviceResponseTag
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("verified")]
		Verified,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("wrong_code")]
		[EdjCase.ICP.Candid.Mapping.VariantOptionTypeAttribute(typeof(VerifyTentativeDeviceResponse.O1))]
		WrongCode,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("device_registration_mode_off")]
		DeviceRegistrationModeOff,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("no_device_to_verify")]
		NoDeviceToVerify,
	}
}

