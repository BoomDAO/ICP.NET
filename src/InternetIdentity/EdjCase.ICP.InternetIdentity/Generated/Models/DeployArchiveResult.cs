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
	[EdjCase.ICP.Candid.Mapping.VariantAttribute(typeof(DeployArchiveResultTag))]
	public class DeployArchiveResult
	{
		[EdjCase.ICP.Candid.Mapping.VariantTagPropertyAttribute]
		public DeployArchiveResultTag Tag { get; set; }
		[EdjCase.ICP.Candid.Mapping.VariantValuePropertyAttribute]
		public object? Value { get; set; }
		private DeployArchiveResult(DeployArchiveResultTag tag, System.Object? value)
		{
			this.Tag = tag;
			this.Value = value;
		}
		
		protected DeployArchiveResult()
		{
		}
		
		public static DeployArchiveResult Success(EdjCase.ICP.Candid.Models.Principal info)
		{
			return new DeployArchiveResult(DeployArchiveResultTag.Success, info);
		}
		
		public EdjCase.ICP.Candid.Models.Principal AsSuccess()
		{
			this.ValidateTag(DeployArchiveResultTag.Success);
			return (EdjCase.ICP.Candid.Models.Principal)this.Value!;
		}
		
		public static DeployArchiveResult CreationInProgress()
		{
			return new DeployArchiveResult(DeployArchiveResultTag.CreationInProgress, null);
		}
		
		public static DeployArchiveResult Failed(string info)
		{
			return new DeployArchiveResult(DeployArchiveResultTag.Failed, info);
		}
		
		public string AsFailed()
		{
			this.ValidateTag(DeployArchiveResultTag.Failed);
			return (string)this.Value!;
		}
		
		private void ValidateTag(DeployArchiveResultTag tag)
		{
			if (!this.Tag.Equals(tag))
			{
				throw new InvalidOperationException($"Cannot cast '{this.Tag}' to type '{tag}'");
			}
		}
	}
	public enum DeployArchiveResultTag
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("success")]
		[EdjCase.ICP.Candid.Mapping.VariantOptionTypeAttribute(typeof(EdjCase.ICP.Candid.Models.Principal))]
		Success,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("creation_in_progress")]
		CreationInProgress,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("failed")]
		[EdjCase.ICP.Candid.Mapping.VariantOptionTypeAttribute(typeof(string))]
		Failed,
	}
}

