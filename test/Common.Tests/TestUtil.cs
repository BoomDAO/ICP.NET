using ICP.Candid.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using ICP.Candid.Models.Values;
using ICP.Candid.Models.Types;
using ICP.Candid;

namespace ICP.Candid.Tests
{
    public static class TestUtil
	{
		public static void AssertEncodedCandid(string expectedHex, string expectedPrefix, CandidValue value, CandidTypeDefinition typeDef)
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
			Assert.Empty(args.OpaqueReferenceBytes);

			Assert.Equal(value, actual.Value);
			Assert.Equal(typeDef, actual.Type);
		}
	}
}
