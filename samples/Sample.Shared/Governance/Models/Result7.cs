using EdjCase.ICP.Candid.Mapping;
using Sample.Shared.Governance.Models;
using System;

namespace Sample.Shared.Governance.Models
{
	[Variant]
	public class Result7
	{
		[VariantTagProperty]
		public Result7Tag Tag { get; set; }

		[VariantValueProperty]
		public object? Value { get; set; }

		public Result7(Result7Tag tag, object? value)
		{
			this.Tag = tag;
			this.Value = value;
		}

		protected Result7()
		{
		}

		public static Result7 Committed(Committed info)
		{
			return new Result7(Result7Tag.Committed, info);
		}

		public static Result7 Aborted(Result7.AbortedInfo info)
		{
			return new Result7(Result7Tag.Aborted, info);
		}

		public Committed AsCommitted()
		{
			this.ValidateTag(Result7Tag.Committed);
			return (Committed)this.Value!;
		}

		public Result7.AbortedInfo AsAborted()
		{
			this.ValidateTag(Result7Tag.Aborted);
			return (Result7.AbortedInfo)this.Value!;
		}

		private void ValidateTag(Result7Tag tag)
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

	public enum Result7Tag
	{
		Committed,
		Aborted
	}
}