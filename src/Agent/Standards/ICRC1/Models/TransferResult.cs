using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EdjCase.ICP.Agent.Standards.ICRC1.Models
{

	[Variant(typeof(TransferResultTag))]
	public class TransferResult
	{
		[VariantTagProperty]
		public TransferResultTag Tag { get; set; }

		[VariantValueProperty]
		private object? Value { get; set; }

		private TransferResult(TransferResultTag tag, object? value)
		{
			this.Tag = tag;
			this.Value = value;
		}

		private TransferResult()
		{
		}

		public static TransferResult Ok(UnboundedUInt info)
		{
			return new TransferResult(TransferResultTag.Ok, info);
		}

		public static TransferResult Err(TransferError info)
		{
			return new TransferResult(TransferResultTag.Err, info);
		}

		public UnboundedUInt AsOk()
		{
			this.ValidateTag(TransferResultTag.Ok);
			return (UnboundedUInt)this.Value!;
		}

		public TransferError AsErr()
		{
			this.ValidateTag(TransferResultTag.Err);
			return (TransferError)this.Value!;
		}

		private void ValidateTag(TransferResultTag tag)
		{
			if (!this.Tag.Equals(tag))
			{
				throw new InvalidOperationException($"Cannot cast '{this.Tag}' to type '{tag}'");
			}
		}
	}

	public enum TransferResultTag
	{
		[CandidName("Ok")]
		[VariantOptionType(typeof(UnboundedUInt))]
		Ok,
		[CandidName("Err")]
		[VariantOptionType(typeof(TransferError))]
		Err
	}


}
