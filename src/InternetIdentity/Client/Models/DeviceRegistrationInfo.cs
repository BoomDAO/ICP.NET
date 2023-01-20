using EdjCase.ICP.Candid.Mapping;
using Timestamp = System.UInt64;

namespace EdjCase.ICP.InternetIdentity.Models
{
	internal class DeviceRegistrationInfo
	{
		[CandidName("tentative_device")]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public Candid.Models.OptionalValue<DeviceData> TentativeDevice { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		
		[CandidName("expiration")]
		public Timestamp Expiration { get; set; }
		
	}
}

