using EdjCase.ICP.PocketIC;

namespace Sample.PocketIC
{
	public class Tests
	{
		private Server? server;
		[SetUp]
		public async Task Setup()
		{
			this.server = await Server.Start();
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
		public void Test1()
		{
			Console.WriteLine(this.server!.GetUrl());
			Assert.Pass();
		}
	}
}
