using EdjCase.ICP.Candid.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using EdjCase.ICP.Candid.Models.Values;
using EdjCase.ICP.Candid.Models.Types;
using EdjCase.ICP.Candid;

namespace EdjCase.ICP.Candid.Tests
{
    public static class TestUtil
	{
		public static void AssertEncodedCandid(string expectedHex, string expectedPrefix, CandidValue value, CandidType typeDef)
		{
			// Encode test
			var builder = new CandidArgBuilder();
			builder.Add(CandidValueWithType.FromValueAndType(value, typeDef));
			byte[] actualBytes = builder.Encode();
			string actualHex = Convert.ToHexString(actualBytes);
			const string didlPrefix = "4449444C";
			Assert.StartsWith(didlPrefix, actualHex);
			actualHex = actualHex[8..];
			Assert.StartsWith(expectedPrefix, actualHex);
			actualHex = actualHex[expectedPrefix.Length..];
			Assert.Equal(expectedHex, actualHex);

			// Decode test
            CandidArg args = CandidArg.FromBytes(actualBytes);
			CandidValueWithType actual = Assert.Single(args.Values);

			Assert.Equal(value, actual.Value);
			Assert.Equal(typeDef, actual.Type);
		}
	}
}
