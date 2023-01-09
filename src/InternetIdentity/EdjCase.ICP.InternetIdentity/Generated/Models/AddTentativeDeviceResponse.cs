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
	[EdjCase.ICP.Candid.Mapping.VariantAttribute(typeof(AddTentativeDeviceResponseTag))]
	public class AddTentativeDeviceResponse
	{
		[EdjCase.ICP.Candid.Mapping.VariantTagPropertyAttribute]
		public AddTentativeDeviceResponseTag Tag { get; set; }
		[EdjCase.ICP.Candid.Mapping.VariantValuePropertyAttribute]
		public object? Value { get; set; }
		private AddTentativeDeviceResponse(AddTentativeDeviceResponseTag tag, System.Object? value)
		{
			this.Tag = tag;
			this.Value = value;
		}
		
		protected AddTentativeDeviceResponse()
		{
		}
		
		public class O0
		{
			[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("verification_code")]
			public string VerificationCode { get; set; }
			
			[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("device_registration_timeout")]
			public Timestamp DeviceRegistrationTimeout { get; set; }
			
		}
		public static AddTentativeDeviceResponse AddedTentatively(AddTentativeDeviceResponse.O0 info)
		{
			return new AddTentativeDeviceResponse(AddTentativeDeviceResponseTag.AddedTentatively, info);
		}
		
		public AddTentativeDeviceResponse.O0 AsAddedTentatively()
		{
			this.ValidateTag(AddTentativeDeviceResponseTag.AddedTentatively);
			return (AddTentativeDeviceResponse.O0)this.Value!;
		}
		
		public static AddTentativeDeviceResponse DeviceRegistrationModeOff()
		{
			return new AddTentativeDeviceResponse(AddTentativeDeviceResponseTag.DeviceRegistrationModeOff, null);
		}
		
		public static AddTentativeDeviceResponse AnotherDeviceTentativelyAdded()
		{
			return new AddTentativeDeviceResponse(AddTentativeDeviceResponseTag.AnotherDeviceTentativelyAdded, null);
		}
		
		private void ValidateTag(AddTentativeDeviceResponseTag tag)
		{
			if (!this.Tag.Equals(tag))
			{
				throw new InvalidOperationException($"Cannot cast '{this.Tag}' to type '{tag}'");
			}
		}
	}
	public enum AddTentativeDeviceResponseTag
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("added_tentatively")]
		[EdjCase.ICP.Candid.Mapping.VariantOptionTypeAttribute(typeof(AddTentativeDeviceResponse.O0))]
		AddedTentatively,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("device_registration_mode_off")]
		DeviceRegistrationModeOff,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("another_device_tentatively_added")]
		AnotherDeviceTentativelyAdded,
	}
}

