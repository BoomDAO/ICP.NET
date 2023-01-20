using EdjCase.ICP.Candid.Mapping;

namespace EdjCase.ICP.InternetIdentity.Models
{
	[Variant(typeof(DeviceProtectionTag))]
	internal class DeviceProtection
	{
		[VariantTagProperty]
		public DeviceProtectionTag Tag { get; set; }
		[VariantValueProperty]
		public object? Value { get; set; }
		private DeviceProtection(DeviceProtectionTag tag, System.Object? value)
		{
			this.Tag = tag;
			this.Value = value;
		}
		
		protected DeviceProtection()
		{
		}
		
		public static DeviceProtection Protected()
		{
			return new DeviceProtection(DeviceProtectionTag.Protected, null);
		}
		
		public static DeviceProtection Unprotected()
		{
			return new DeviceProtection(DeviceProtectionTag.Unprotected, null);
		}
		
	}
	public enum DeviceProtectionTag
	{
		[CandidName("protected")]
		Protected,
		[CandidName("unprotected")]
		Unprotected,
	}
}

