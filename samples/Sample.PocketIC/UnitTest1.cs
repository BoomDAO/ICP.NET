using EdjCase.ICP.Candid.Models;
using EdjCase.ICP.PocketIC;

namespace Sample.PocketIC
{
	public class Tests
	{
		private PocketIcServer server;
		[SetUp]
		public async Task Setup()
		{
			this.server = await PocketIcServer.Start();
		}

		[TearDown]
		public async Task Teardown()
		{
			if (this.server != null)
			{
				await this.server.Stop();
				this.server.Dispose();
			}
		}

		[Test]
		public async Task Test1()
		{
			string url = this.server.GetUrl();
			PocketIcClient client = await PocketIcClient.CreateAsync(url);
			Principal canisterId = await client.CreateCanisterAsync();
			Assert.Pass();
		}
	}
}
