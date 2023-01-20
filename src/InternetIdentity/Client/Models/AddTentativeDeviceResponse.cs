using System;
using EdjCase.ICP.Candid.Mapping;
using Timestamp = System.UInt64;

namespace EdjCase.ICP.InternetIdentity.Models
{
	[Variant(typeof(AddTentativeDeviceResponseTag))]
	internal class AddTentativeDeviceResponse
	{
		[VariantTagProperty]
		public AddTentativeDeviceResponseTag Tag { get; set; }
		[VariantValueProperty]
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
			[CandidName("verification_code")]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
			public string VerificationCode { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
			
			[CandidName("device_registration_timeout")]
			public Timestamp DeviceRegistrationTimeout { get; set; }
			
		}
		public static AddTentativeDeviceResponse AddedTentatively(O0 info)
		{
			return new AddTentativeDeviceResponse(AddTentativeDeviceResponseTag.AddedTentatively, info);
		}
		
		public O0 AsAddedTentatively()
		{
			this.ValidateTag(AddTentativeDeviceResponseTag.AddedTentatively);
			return (O0)this.Value!;
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
		[CandidName("added_tentatively")]
		[VariantOptionType(typeof(AddTentativeDeviceResponse.O0))]
		AddedTentatively,
		[CandidName("device_registration_mode_off")]
		DeviceRegistrationModeOff,
		[CandidName("another_device_tentatively_added")]
		AnotherDeviceTentativelyAdded,
	}
}

