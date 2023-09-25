using EdjCase.ICP.Candid.Mapping;
using Sample.Shared.ICRC1Ledger.Models;
using BlockIndex = EdjCase.ICP.Candid.Models.UnboundedUInt;

namespace Sample.Shared.ICRC1Ledger.Models
{
	[Variant()]
	public class ApproveResult
	{
		[VariantTagProperty()]
		public ApproveResultTag Tag { get; set; }

		[VariantValueProperty()]
		public object? Value { get; set; }

		public BlockIndex? Ok { get => this.Tag == ApproveResultTag.Ok ? (BlockIndex)this.Value : default; set => (this.Tag, this.Value) = (ApproveResultTag.Ok, value); }

		public ApproveError? Err { get => this.Tag == ApproveResultTag.Err ? (ApproveError)this.Value : default; set => (this.Tag, this.Value) = (ApproveResultTag.Err, value); }

		public ApproveResult(ApproveResultTag tag, object? value)
		{
			this.Tag = tag;
			this.Value = value;
		}

		protected ApproveResult()
		{
		}
	}

	public enum ApproveResultTag
	{
		Ok,
		Err
	}
}