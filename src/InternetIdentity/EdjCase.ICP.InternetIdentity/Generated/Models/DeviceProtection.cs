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
	[EdjCase.ICP.Candid.Mapping.VariantAttribute(typeof(DeviceProtectionTag))]
	public class DeviceProtection
	{
		[EdjCase.ICP.Candid.Mapping.VariantTagPropertyAttribute]
		public DeviceProtectionTag Tag { get; set; }
		[EdjCase.ICP.Candid.Mapping.VariantValuePropertyAttribute]
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
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("protected")]
		Protected,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("unprotected")]
		Unprotected,
	}
}

