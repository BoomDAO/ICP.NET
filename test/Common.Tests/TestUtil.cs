using Common.Candid;
using Common.Models;
using ICP.Common.Candid;
using ICP.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Common.Tests
{
    public static class TestUtil
	{
		public static void AssertEncodedCandid(string expectedHex, string expectedPrefix, CandidValue value, CandidTypeDefinition typeDef)
		{
			// Encode test
			var builder = new IDLBuilder();
			builder.Add(value, typeDef);
			byte[] actualBytes = builder.Encode();
			string actualHex = Convert.ToHexString(actualBytes);
			const string didlPrefix = "4449444C";
			Assert.StartsWith(didlPrefix, actualHex);
			actualHex = actualHex[8..];
			Assert.StartsWith(expectedPrefix, actualHex);
			actualHex = actualHex[expectedPrefix.Length..];
			Assert.Equal(expectedHex, actualHex);

			// Decode test
            List<(CandidValue, CandidTypeDefinition)> args = CandidReader.Read(actualBytes);
            (CandidValue actualValue, CandidTypeDefinition actualTypeDef) = Assert.Single(args);

			Assert.Equal(value, actualValue);
			Assert.Equal(typeDef, actualTypeDef);
		}
	}
}
