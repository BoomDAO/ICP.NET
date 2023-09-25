using EdjCase.ICP.Candid.Mapping;
using Sample.Shared.ICRC1Ledger.Models;
using BlockIndex = EdjCase.ICP.Candid.Models.UnboundedUInt;

namespace Sample.Shared.ICRC1Ledger.Models
{
	[Variant()]
	public class TransferResult
	{
		[VariantTagProperty()]
		public TransferResultTag Tag { get; set; }

		[VariantValueProperty()]
		public object? Value { get; set; }

		public BlockIndex? Ok { get => this.Tag == TransferResultTag.Ok ? (BlockIndex)this.Value : default; set => (this.Tag, this.Value) = (TransferResultTag.Ok, value); }

		public TransferError? Err { get => this.Tag == TransferResultTag.Err ? (TransferError)this.Value : default; set => (this.Tag, this.Value) = (TransferResultTag.Err, value); }

		public TransferResult(TransferResultTag tag, object? value)
		{
			this.Tag = tag;
			this.Value = value;
		}

		protected TransferResult()
		{
		}
	}

	public enum TransferResultTag
	{
		Ok,
		Err
	}
}