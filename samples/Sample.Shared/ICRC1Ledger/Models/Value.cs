using EdjCase.ICP.Candid.Mapping;
using Sample.Shared.ICRC1Ledger.Models;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Models;
using Map = System.Collections.Generic.Dictionary<System.String, Sample.Shared.ICRC1Ledger.Models.Value>;

namespace Sample.Shared.ICRC1Ledger.Models
{
	[Variant()]
	public class Value
	{
		[VariantTagProperty()]
		public ValueTag Tag { get; set; }

		[VariantValueProperty()]
		public object? Value_ { get; set; }

		public List<byte>? Blob { get => this.Tag == ValueTag.Blob ? (List<byte>)this.Value_ : default; set => (this.Tag, this.Value_) = (ValueTag.Blob, value); }

		public string? Text { get => this.Tag == ValueTag.Text ? (string)this.Value_ : default; set => (this.Tag, this.Value_) = (ValueTag.Text, value); }

		public UnboundedUInt? Nat { get => this.Tag == ValueTag.Nat ? (UnboundedUInt)this.Value_ : default; set => (this.Tag, this.Value_) = (ValueTag.Nat, value); }

		public ulong? Nat64 { get => this.Tag == ValueTag.Nat64 ? (ulong)this.Value_ : default; set => (this.Tag, this.Value_) = (ValueTag.Nat64, value); }

		public UnboundedInt? Int { get => this.Tag == ValueTag.Int ? (UnboundedInt)this.Value_ : default; set => (this.Tag, this.Value_) = (ValueTag.Int, value); }

		public List<Value>? Array { get => this.Tag == ValueTag.Array ? (List<Value>)this.Value_ : default; set => (this.Tag, this.Value_) = (ValueTag.Array, value); }

		public Map? Map { get => this.Tag == ValueTag.Map ? (Map)this.Value_ : default; set => (this.Tag, this.Value_) = (ValueTag.Map, value); }

		public Value(ValueTag tag, object? value)
		{
			this.Tag = tag;
			this.Value_ = value;
		}

		protected Value()
		{
		}
	}

	public enum ValueTag
	{
		Blob,
		Text,
		Nat,
		Nat64,
		Int,
		Array,
		Map
	}
}