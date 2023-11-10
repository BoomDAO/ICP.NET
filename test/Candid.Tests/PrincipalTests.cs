using EdjCase.ICP.Agent;
using EdjCase.ICP.Candid.Models;
using EdjCase.ICP.Candid.Models.Values;
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
		[Theory]
		[InlineData("un4fu-tqaaa-aaaab-qadjq-cai", "4A8D3F2B6E01C87D9E03B4567CF89A01D23456789ABCDEF0123456789ABCDEF0", "8C5C20C6153F7F51E20D0F0FB508515B476563A962B4A9915F4F02708AED4F82")]
		[InlineData("aaaaa-aa", null, "2D0E897F7E862D2B57D9BC9EA5C65F9A24AC6C074575F47898314B8D6CB0929D")]
		[InlineData("2vxsx-fae", "5A8D3F2B6E01C87D9C03B4567CF89A01D23456789ABCDEF0123456789ABCDEF0", "2A215AD241E757C707EC3163503C1D91DCB72B2415603273BD05029E42ECEA6A")]
		[InlineData("g4gvl-fx6s5-epgv4-vhyqk-zkiny-ja4gn-zo7zf-dv3va-26u3t-ijwrx-tqe", "6A8D3F2B6E01C87D9C03B4567CF89A01D23456789ABCDEF0123456789ABCDEF0", "8365061F9C5ADA294975282C81606E87C4D2DDE0C93AA41AC2C2C784623370E6")]

		public void Principal_ToLedgerAccount(string principal, string? subAccountHex, string expectedHex)
		{
			byte[]? subAccount = subAccountHex == null ? null : ByteUtil.FromHexString(subAccountHex);
			byte[] actual = Principal.FromText(principal).ToLedgerAccount(subAccount);
			string actualHex = ByteUtil.ToHexString(actual);
			Assert.Equal(expectedHex, actualHex);
		}
	}
}
