using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using EdjCase.ICP.Agent.Agents;
using EdjCase.ICP.Agent.Agents.Http;
using EdjCase.ICP.Agent.Identities;
using EdjCase.ICP.Candid.Models;
using EdjCase.ICP.Candid.Utilities;
using Performance.Tests.Benchmarks;

//var identity = Ed25519Identity.Create();
//var httpClient = new FakeHttpClient();
//var agent = new HttpAgent(httpClient, identity);
//Principal canisterId = Principal.FromText("rrkah-fqaaa-aaaaa-aaaaq-cai");
//string method = "get_proposal_info";
//CandidArg arg = CandidArg.FromCandid(CandidTypedValue.Nat64(1999));

//const int interationCount = 10_000;
//for (int i = 0; i < interationCount; i++)
//{
//	await agent.QueryAsync(canisterId, method, arg);
//}




//BenchmarkRunner.Run<HashingBenchmarks>();
//BenchmarkRunner.Run<SignatureBenchmarks>();
//BenchmarkRunner.Run<SignatureBenchmarks>();
BenchmarkRunner.Run(typeof(Program).Assembly);







internal class FakeHttpClient : IHttpClient
{
	private readonly HttpResponseMessage response;

	public FakeHttpClient()
	{
		string hexValue = "D9D9F7A266737461747573677265706C696564657265706C79A1636172675904CA4449444C616E016C11DBB70102B2CEEF2F75AFA3BDA10175D9DCF28E02048F8ECFD60206C9C0999604789996F39E0478AFBC879205099491A1B50578CE89BE970678818DB0DE070AF494EDE10717C2F286FC0775D3A4A3A20A78B2B8D4960B19B4BFD4960B02E687FEB20F786E036C01DBB701786E056C0290C6C1960571D19BC28F0E756D076C02007801086C02EA99CFF20475AD9E83B60E786E786E0B6C01C7A1E7F90A0C6E0D6C08F1E79D9D020EC1CBDFC70213DF9AB4900516E4C0D6DC090EBAE8FACC0A0ECBC9C7F90C0ECBFFD3A80D0EAE8AABE90D166E0F6C02B2CEEF2F10B3C4B1F204156E116C07B2CEEF2F12C0CFF27109D7E09B900213FFDB81F703098DAACD940809B0E4D2970A0981CFAEF40A146E756D686D7B6E686D0F6E186C04C1C00178A7D2F00278C4A7C9A10178D6D5DAC60F786E1A6C04EFD6E4027198ABEC81011BB6F798B2011CA696A48708716E716E1D6B0C93A7E09D021ED881C9C40321D69CE79D0AC20082FFCFAA0CC3009E9598A00DCA009DFA94A40DCF00E3C3C5990ED400B1A5AEA10ED800F5D9D7A50EDB00FAD5DDF40EDC00DB9CEBF70EDD00D6F4C7FF0FE0006C02DBB70102BAC7A7FA0D1F6E206C02CBE4FDC70471FC91F4F8051B6C03DBB70102CBE2B58B0822F1BB8B880DC0006E236B0C9B9CD0A40124BAB5F1A40126918BACF10227FC9FC6830529C6B3BB91062E8DB2D592093698A5D0C7093791B2FAB80A38E0F8FFFD0B1A8BF3AFAC0D3989B8B3B30E3AA3F3C0AD0F3B6C03BC949D820325DBE2BE950915EF9999FE09096E796C01B9EF938008786C02AFA3BDA10175C2CEE0D80C286D036C01D7AB012A6E2B6B039EF5CC0F2C9992CCD0012DDAE1C99903786C006C029CB1FA2515BA89E5C204786C01A78882820A2F6E306B0996A7F71531F381D4AB02328CB2F18C0733B09B9BA4072CD0FB87AF072C90F29AFE0734E4AC938D0C2CF7AACFD80D2CC3A2F6C90E356C01F6B0989A08156C018EDDC3A60D156C01D0E1E9F60C7E6C018DC3B2B303796C01C88ECAD50A786C02EA99CFF20475B2B8D4960B026C01C38FBBD10B026C05F5BBE3900178D2BBF0D9017EB9EF93800878DBE2BE950915EF9999FE09786C01BBB4B09703256C01B99D9DA50B796C02A9DDF49B073CD8A38CA80D3E6E3D6C01CEDFA0A804146E3F6C01E0A9B302786EC1006B02CD8E8EB90414CEBEE1D308036C02E4D7BEE905758EFFD6E90E146C03CE9CA6CE01C400F382CCB307C600B9EF938008786EC5006C02DBB701159DF1AFE7073C6EC7006B02FDF59AEC0BC800E3B586FF0CC9006C01F5BBE39001786C01A9DDF49B073C6C03D889BEA60D09B5F6F9E90E15C6F6EBEB0ECB006ECC006C08FED391BD0178ABE1B1D001CD00DCD0A0AB0378DFBCB4D80478EDC6ECAB087993E5E481097890B090AF0E78CA9AB7D20F786ECE006C028FA0804178CF898DD304786C02CFBE93A404D000C796CDBE0B156ED1006C01EDBB85F901D2006ED3006C02F9889A5778B2CC99E705786C0184AEAD33D5006DD6006C02007501D7006C01C2CEE0D80C286C02F8B9B6C904D900A4CCF7DD0ADA006E7E6DC3006C089EB493CF0378BEFA8DD40479BE8FE6E30478CE89BE97067886F998BC0978C5CAE3D40A7893A190E00C78F5E1D0E70D786C018594E2C50B136C01F0A2CABB0BDE006EDF006B02BF80E42BC500C6A6E4B90AC5006C0196BDB4E90471010000";
		byte[] content = ByteUtil.FromHexString(hexValue);
		this.response = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
		{
			Content = new ByteArrayContent(content)
		};
	}
	public Task<HttpResponseMessage> SendAsync(HttpRequestMessage message)
	{
		return Task.FromResult(this.response);
	}
}