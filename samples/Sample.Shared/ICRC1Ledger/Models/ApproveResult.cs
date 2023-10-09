using EdjCase.ICP.Candid.Mapping;
using Sample.Shared.ICRC1Ledger.Models;
using System;
using BlockIndex = EdjCase.ICP.Candid.Models.UnboundedUInt;

namespace Sample.Shared.ICRC1Ledger.Models
{
	[Variant]
	public class ApproveResult
	{
		[VariantTagProperty]
		public ApproveResultTag Tag { get; set; }

		[VariantValueProperty]
		public object? Value { get; set; }

		public ApproveResult(ApproveResultTag tag, object? value)
		{
			this.Tag = tag;
			this.Value = value;
		}

		protected ApproveResult()
		{
		}

		public static ApproveResult Ok(BlockIndex info)
		{
			return new ApproveResult(ApproveResultTag.Ok, info);
		}

		public static ApproveResult Err(ApproveError info)
		{
			return new ApproveResult(ApproveResultTag.Err, info);
		}

		public BlockIndex AsOk()
		{
			this.ValidateTag(ApproveResultTag.Ok);
			return (BlockIndex)this.Value!;
		}

		public ApproveError AsErr()
		{
			this.ValidateTag(ApproveResultTag.Err);
			return (ApproveError)this.Value!;
		}

		private void ValidateTag(ApproveResultTag tag)
		{
			if (!this.Tag.Equals(tag))
			{
				throw new InvalidOperationException($"Cannot cast '{this.Tag}' to type '{tag}'");
			}
		}
	}

	public enum ApproveResultTag
	{
		Ok,
		Err
	}
}