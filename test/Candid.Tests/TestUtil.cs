using EdjCase.ICP.Candid.Models;
using System;
using Xunit;
using EdjCase.ICP.Candid.Models.Values;
using EdjCase.ICP.Candid.Models.Types;

namespace EdjCase.ICP.Candid.Tests
{
	public static class TestUtil
	{
		public static void AssertEncodedCandid(string expectedHex, string expectedPrefix, CandidValue value, CandidType typeDef)
		{
			// Encode test
			CandidArg arg = CandidArg.FromCandid(
				CandidTypedValue.FromValueAndType(value, typeDef)
			);
			byte[] actualBytes = arg.Encode();
			string actualHex = Convert.ToHexString(actualBytes);
			const string didlPrefix = "4449444C";
			Assert.StartsWith(didlPrefix, actualHex);
			actualHex = actualHex[8..];
			Assert.StartsWith(expectedPrefix, actualHex);
			actualHex = actualHex[expectedPrefix.Length..];
			Assert.Equal(expectedHex, actualHex);

			// Decode test
			CandidArg args = CandidArg.FromBytes(actualBytes);
			CandidTypedValue actual = Assert.Single(args.Values);

			Assert.Equal(value, actual.Value);
			Assert.Equal(typeDef, actual.Type);
		}
	}
}
