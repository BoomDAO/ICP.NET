using BlockIndex = EdjCase.ICP.Candid.Models.UnboundedUInt;
using Subaccount = System.Collections.Generic.List<System.Byte>;
using Timestamp = System.UInt64;
using Duration = System.UInt64;
using Tokens = EdjCase.ICP.Candid.Models.UnboundedUInt;
using TxIndex = EdjCase.ICP.Candid.Models.UnboundedUInt;
using QueryArchiveFn = EdjCase.ICP.Candid.Models.Values.CandidFunc;
using Map = System.Collections.Generic.List<Sample.Shared.ICRC1Ledger.Models.MapItem>;
using Block = Sample.Shared.ICRC1Ledger.Models.Value;
using QueryBlockArchiveFn = EdjCase.ICP.Candid.Models.Values.CandidFunc;
using EdjCase.ICP.Candid.Mapping;
using Sample.Shared.ICRC1Ledger.Models;
using System;

namespace Sample.Shared.ICRC1Ledger.Models
{
	[Variant(typeof(ChangeFeeCollectorTag))]
	public class ChangeFeeCollector
	{
		[VariantTagProperty()]
		public ChangeFeeCollectorTag Tag { get; set; }

		[VariantValueProperty()]
		public System.Object? Value { get; set; }

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
		[VariantOptionType(typeof(Account))]
		SetTo
	}
}