using EdjCase.ICP.Agent;
using EdjCase.ICP.Candid.Models;
using EdjCase.ICP.Candid.Utilities;
using Xunit;

namespace EdjCase.ICP.Candid.Tests
{
	public class PrincipalTests
	{
		[Theory]
		[InlineData("2chl6-4hpzw-vqaaa-aaaaa-c", "EFCDAB000000000001")]
		[InlineData("aaaaa-aa", "")]
		[InlineData("2vxsx-fae", "04")]
		[InlineData("g4gvl-fx6s5-epgv4-vhyqk-zkiny-ja4gn-zo7zf-dv3va-26u3t-ijwrx-tqe", "FE9748F357953E20ACA90DC241C3372EFE4A3AEEA0D7A9B9A1368DE702")]
		public void Principal_Text_Conversion(string text, string hex)
		{
			Principal textPrincipal = Principal.FromText(text);
			string textHex = textPrincipal.ToHex();
			Assert.Equal(hex, textHex);
			string textText = textPrincipal.ToText();
			Assert.Equal(text, textText);

			Principal hexPrincipal = Principal.FromHex(hex);
			Assert.Equal(textPrincipal, hexPrincipal);
			string hexHex = hexPrincipal.ToHex();
			Assert.Equal(hex, hexHex);
			string hexText = hexPrincipal.ToText();
			Assert.Equal(text, hexText);
		}

		[Theory]
		[InlineData("3sw4h-rtfno-q76l5-h22ed-hfyqb-xazad-dsxgq-ixxzo-zk7yq-tohf4-nqe", "3036300506032B6570032D00302A300506032B6570032100D179DF2F612600C20ABB9EACA0528593432CA7D7BE7E32170D48EDD9BE1C2C35")]
		[InlineData("ztyjw-vpmm4-w2hqh-wensq-ypf7n-dcjo7-utdp3-vs4gx-pcw4a-llatq-dae", "303c300c060a2b0601040183b8430102032c000a00000000000000070101a451d1829b843e2aabdd49ea590668978a73612067bdde0b8502f844452a7558")]
		public void Principal_Match_PublicKey(string principalText, string publicKeyHex)
		{
			var principal = Principal.FromText(principalText);

			Assert.Equal(principalText, principal.ToText());

			byte[] publicKeyBytes = ByteUtil.FromHexString(publicKeyHex);

			var publicKey = SubjectPublicKeyInfo.FromDerEncoding(publicKeyBytes);
			var publicKeyPrincipal = publicKey.ToPrincipal();
			Assert.Equal(principal, publicKeyPrincipal);


			Assert.Equal(publicKeyPrincipal.ToHex(), principal.ToHex());
		}
	}
}
