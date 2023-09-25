using EdjCase.ICP.Candid.Mapping;
using Sample.Shared.ICRC1Ledger.Models;
using BlockIndex = EdjCase.ICP.Candid.Models.UnboundedUInt;

namespace Sample.Shared.ICRC1Ledger.Models
{
	[Variant()]
	public class TransferFromResult
	{
		[VariantTagProperty()]
		public TransferFromResultTag Tag { get; set; }

		[VariantValueProperty()]
		public object? Value { get; set; }

		public BlockIndex? Ok { get => this.Tag == TransferFromResultTag.Ok ? (BlockIndex)this.Value : default; set => (this.Tag, this.Value) = (TransferFromResultTag.Ok, value); }

		public TransferFromError? Err { get => this.Tag == TransferFromResultTag.Err ? (TransferFromError)this.Value : default; set => (this.Tag, this.Value) = (TransferFromResultTag.Err, value); }

		public TransferFromResult(TransferFromResultTag tag, object? value)
		{
			this.Tag = tag;
			this.Value = value;
		}

		protected TransferFromResult()
		{
		}
	}

	public enum TransferFromResultTag
	{
		Ok,
		Err
	}
}