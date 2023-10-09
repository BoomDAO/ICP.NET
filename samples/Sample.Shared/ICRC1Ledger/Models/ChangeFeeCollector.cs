using EdjCase.ICP.Candid.Mapping;
using Sample.Shared.ICRC1Ledger.Models;
using System;

namespace Sample.Shared.ICRC1Ledger.Models
{
	[Variant]
	public class ChangeFeeCollector
	{
		[VariantTagProperty]
		public ChangeFeeCollectorTag Tag { get; set; }

		[VariantValueProperty]
		public object? Value { get; set; }

		public ChangeFeeCollector(ChangeFeeCollectorTag tag, object? value)
		{
			this.Tag = tag;
			this.Value = value;
		}

		protected ChangeFeeCollector()
		{
		}

		public static ChangeFeeCollector Unset()
		{
			return new ChangeFeeCollector(ChangeFeeCollectorTag.Unset, null);
		}

		public static ChangeFeeCollector SetTo(Account info)
		{
			return new ChangeFeeCollector(ChangeFeeCollectorTag.SetTo, info);
		}

		public Account AsSetTo()
		{
			this.ValidateTag(ChangeFeeCollectorTag.SetTo);
			return (Account)this.Value!;
		}

		private void ValidateTag(ChangeFeeCollectorTag tag)
		{
			if (!this.Tag.Equals(tag))
			{
				throw new InvalidOperationException($"Cannot cast '{this.Tag}' to type '{tag}'");
			}
		}
	}

	public enum ChangeFeeCollectorTag
	{
		Unset,
		SetTo
	}
}