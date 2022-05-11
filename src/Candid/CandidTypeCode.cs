using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICP.Candid
{
	public enum CandidTypeCode
	{
		Null = -1,
		Bool = -2,
		Nat = -3,
		Int = -4,
		Nat8 = -5,
		Nat16 = -6,
		Nat32 = -7,
		Nat64 = -8,
		Int8 = -9,
		Int16 = -10,
		Int32 = -11,
		Int64 = -12,
		Float32 = -13,
		Float64 = -14,
		Text = -15,
		Reserved = -16,
		Empty = -17,
		Opt = -18,
		Vector = -19,
		Record = -20,
		Variant = -21,
		Func = -22,
		Service = -23,
		Principal = -24,
	}
}
