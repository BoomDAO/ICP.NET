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
	[EdjCase.ICP.Candid.Mapping.VariantAttribute(typeof(PurposeTag))]
	public class Purpose
	{
		[EdjCase.ICP.Candid.Mapping.VariantTagPropertyAttribute]
		public PurposeTag Tag { get; set; }
		[EdjCase.ICP.Candid.Mapping.VariantValuePropertyAttribute]
		public object? Value { get; set; }
		private Purpose(PurposeTag tag, System.Object? value)
		{
			this.Tag = tag;
			this.Value = value;
		}
		
		protected Purpose()
		{
		}
		
		public static Purpose Recovery()
		{
			return new Purpose(PurposeTag.Recovery, null);
		}
		
		public static Purpose Authentication()
		{
			return new Purpose(PurposeTag.Authentication, null);
		}
		
	}
	public enum PurposeTag
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("recovery")]
		Recovery,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("authentication")]
		Authentication,
	}
}

