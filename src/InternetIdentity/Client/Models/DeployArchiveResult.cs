using System;
using EdjCase.ICP.Candid.Mapping;

namespace EdjCase.ICP.InternetIdentity.Models
{
	[Variant(typeof(DeployArchiveResultTag))]
	internal class DeployArchiveResult
	{
		[VariantTagProperty]
		public DeployArchiveResultTag Tag { get; set; }
		[VariantValueProperty]
		public object? Value { get; set; }
		private DeployArchiveResult(DeployArchiveResultTag tag, System.Object? value)
		{
			this.Tag = tag;
			this.Value = value;
		}
		
		protected DeployArchiveResult()
		{
		}
		
		public static DeployArchiveResult Success(Candid.Models.Principal info)
		{
			return new DeployArchiveResult(DeployArchiveResultTag.Success, info);
		}
		
		public Candid.Models.Principal AsSuccess()
		{
			this.ValidateTag(DeployArchiveResultTag.Success);
			return (Candid.Models.Principal)this.Value!;
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
		[CandidName("success")]
		[VariantOptionType(typeof(Candid.Models.Principal))]
		Success,
		[CandidName("creation_in_progress")]
		CreationInProgress,
		[CandidName("failed")]
		[VariantOptionType(typeof(string))]
		Failed,
	}
}

