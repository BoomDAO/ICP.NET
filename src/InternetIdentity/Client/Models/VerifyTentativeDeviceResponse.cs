using System;
using EdjCase.ICP.Candid.Mapping;

namespace EdjCase.ICP.InternetIdentity.Models
{
	[Variant(typeof(VerifyTentativeDeviceResponseTag))]
	public class VerifyTentativeDeviceResponse
	{
		[VariantTagProperty]
		public VerifyTentativeDeviceResponseTag Tag { get; set; }
		[VariantValueProperty]
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
			[CandidName("retries_left")]
			public byte RetriesLeft { get; set; }
			
		}
		public static VerifyTentativeDeviceResponse WrongCode(O1 info)
		{
			return new VerifyTentativeDeviceResponse(VerifyTentativeDeviceResponseTag.WrongCode, info);
		}
		
		public O1 AsWrongCode()
		{
			this.ValidateTag(VerifyTentativeDeviceResponseTag.WrongCode);
			return (O1)this.Value!;
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
		[CandidName("verified")]
		Verified,
		[CandidName("wrong_code")]
		[VariantOptionType(typeof(VerifyTentativeDeviceResponse.O1))]
		WrongCode,
		[CandidName("device_registration_mode_off")]
		DeviceRegistrationModeOff,
		[CandidName("no_device_to_verify")]
		NoDeviceToVerify,
	}
}

