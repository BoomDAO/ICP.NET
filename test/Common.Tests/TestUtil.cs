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
			var builder = new IDLBuilder();
			builder.Add(value, typeDef);
			byte[] actualValue = builder.Encode();
			string actualHex = Convert.ToHexString(actualValue);
			string didlPrefix = "4449444C";
			Assert.StartsWith(didlPrefix, actualHex);
			actualHex = actualHex[8..];
			Assert.StartsWith(expectedPrefix, actualHex);
			actualHex = actualHex[expectedPrefix.Length..];
			Assert.Equal(expectedHex, actualHex);
		}
	}
}
