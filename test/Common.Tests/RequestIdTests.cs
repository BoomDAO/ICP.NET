using EdjCase.ICP.Candid.Crypto;
using EdjCase.ICP.Candid.Models;
using System.Collections.Generic;
using Xunit;

namespace EdjCase.ICP.Candid.Tests
{
	public class RequestIdTests
	{
		private IHashFunction sha256 = SHA256HashFunction.Create();

		[Fact]
		public void RequestId_Dict()
		{
			RequestId requestId = RequestId.FromObject(new Dictionary<string, IHashable>
			{
				{"arg", new byte[]{68, 73, 68, 76, 0, 1, 121, 54, 215, 245, 143}.ToHashable() },
				{"canister_id", Principal.FromBytes(new byte[]{0, 0, 0, 0, 0, 0, 0, 12, 1, 1}) },
				{"ingress_expiry", ICTimestamp.FromNanoSeconds(1669488594991000000) },
				{"method_name", "deleteSavedItem".ToHashable() },
				{"nonce", new byte[]{0, 0, 1, 132, 181, 66, 179, 129, 253, 30, 229, 15, 18, 153, 227, 38}.ToHashable() },
				{"request_type", "call".ToHashable() },
				{"sender", Principal.FromBytes(new byte[]{ 1, 21, 182, 196, 80, 130, 54, 35, 242, 253, 87, 201, 224, 138, 44, 199, 161, 21, 101, 92, 106, 37, 214, 170, 254, 173, 248, 228, 2}) }
			}, this.sha256);
			var expected = new byte[] { 26, 131, 195, 163, 207, 0, 163, 105, 137, 221, 27, 79, 139, 36, 141, 141, 161, 176, 173, 222, 165, 213, 153, 232, 98, 36, 205, 164, 71, 128, 210, 221 };
			Assert.Equal(expected, requestId.RawValue);
		}
	}
}
