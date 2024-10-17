using EdjCase.ICP.Candid.Models;
using EdjCase.ICP.PocketIC;
using EdjCase.ICP.PocketIC.Client;
using EdjCase.ICP.PocketIC.Models;

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
			await using (PocketIc pocketIc = await PocketIc.CreateAsync(url))
			{
				CreateCanisterResponse response = await pocketIc.CreateCanisterAsync();

				Assert.NotNull(response);
				Assert.NotNull(response.CanisterId);
			}
		}
	}
}
