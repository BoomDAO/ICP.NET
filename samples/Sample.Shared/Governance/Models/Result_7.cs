using EdjCase.ICP.Candid.Mapping;
using Sample.Shared.Governance.Models;
using System;

namespace Sample.Shared.Governance.Models
{
	[Variant(typeof(Result_7Tag))]
	public class Result_7
	{
		[VariantTagProperty()]
		public Result_7Tag Tag { get; set; }

		[VariantValueProperty()]
		public System.Object? Value { get; set; }

		public Result_7(Result_7Tag tag, object? value)
		{
			this.Tag = tag;
			this.Value = value;
		}

		protected Result_7()
		{
		}

		public static Result_7 Committed(Committed info)
		{
			return new Result_7(Result_7Tag.Committed, info);
		}

		public static Result_7 Aborted(Result_7.AbortedInfo info)
		{
			return new Result_7(Result_7Tag.Aborted, info);
		}

		public Committed AsCommitted()
		{
			this.ValidateTag(Result_7Tag.Committed);
			return (Committed)this.Value!;
		}

		public Result_7.AbortedInfo AsAborted()
		{
			this.ValidateTag(Result_7Tag.Aborted);
			return (Result_7.AbortedInfo)this.Value!;
		}

		private void ValidateTag(Result_7Tag tag)
		{
			if (!this.Tag.Equals(tag))
			{
				throw new InvalidOperationException($"Cannot cast '{this.Tag}' to type '{tag}'");
			}
		}

		public class AbortedInfo
		{
			public AbortedInfo()
			{
			}
		}
	}

	public enum Result_7Tag
	{
		[VariantOptionType(typeof(Committed))]
		Committed,
		[VariantOptionType(typeof(Result_7.AbortedInfo))]
		Aborted
	}
}