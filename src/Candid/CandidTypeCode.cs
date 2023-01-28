namespace EdjCase.ICP.Candid
{
	/// <summary>
	/// Specifies all the possible candid types and their corresponding codes
	/// </summary>
	public enum CandidTypeCode
	{
		/// <summary>
		/// A null value
		/// </summary>
		Null = -1,
		/// <summary>
		/// A bool value
		/// </summary>
		Bool = -2,
		/// <summary>
		/// An unbounded uint value
		/// </summary>
		Nat = -3,
		/// <summary>
		/// An unbounded int value
		/// </summary>
		Int = -4,
		/// <summary>
		/// An UInt8 value
		/// </summary>
		Nat8 = -5,
		/// <summary>
		/// An UInt16 value
		/// </summary>
		Nat16 = -6,
		/// <summary>
		/// An UInt32 value
		/// </summary>
		Nat32 = -7,
		/// <summary>
		/// An UInt64 value
		/// </summary>
		Nat64 = -8,
		/// <summary>
		/// An Int8 value
		/// </summary>
		Int8 = -9,
		/// <summary>
		/// An Int16 value
		/// </summary>
		Int16 = -10,
		/// <summary>
		/// An Int32 value
		/// </summary>
		Int32 = -11,
		/// <summary>
		/// An Int64 value
		/// </summary>
		Int64 = -12,
		/// <summary>
		/// A float32 value
		/// </summary>
		Float32 = -13,
		/// <summary>
		/// A float64 value
		/// </summary>
		Float64 = -14,
		/// <summary>
		/// A string value
		/// </summary>
		Text = -15,
		/// <summary>
		/// A reserved value
		/// </summary>
		Reserved = -16,
		/// <summary>
		/// An empty value
		/// </summary>
		Empty = -17,
		/// <summary>
		/// An optional value
		/// </summary>
		Opt = -18,
		/// <summary>
		/// An vector value
		/// </summary>
		Vector = -19,
		/// <summary>
		/// An record value
		/// </summary>
		Record = -20,
		/// <summary>
		/// An variant value
		/// </summary>
		Variant = -21,
		/// <summary>
		/// An func value
		/// </summary>
		Func = -22,
		/// <summary>
		/// An service value
		/// </summary>
		Service = -23,
		/// <summary>
		/// An principal value
		/// </summary>
		Principal = -24,
	}
}
