using EdjCase.ICP.Candid.Mapping;
using Sample.Shared.Governance.Models;

namespace Sample.Shared.Governance.Models
{
	[Variant()]
	public class Result7
	{
		[VariantTagProperty()]
		public Result7Tag Tag { get; set; }

		[VariantValueProperty()]
		public object? Value { get; set; }

		public Committed? Committed { get => this.Tag == Result7Tag.Committed ? (Committed)this.Value : default; set => (this.Tag, this.Value) = (Result7Tag.Committed, value); }

		public Result7.AbortedInfo? Aborted { get => this.Tag == Result7Tag.Aborted ? (Result7.AbortedInfo)this.Value : default; set => (this.Tag, this.Value) = (Result7Tag.Aborted, value); }

		public Result7(Result7Tag tag, object? value)
		{
			this.Tag = tag;
			this.Value = value;
		}

		protected Result7()
		{
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