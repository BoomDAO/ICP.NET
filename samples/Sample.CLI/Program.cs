using EdjCase.ICP.Agent.Agents;
using EdjCase.ICP.Agent.Identity;
using EdjCase.ICP.Candid.Models;
using System;
using System.Threading.Tasks;
using EdjCase.ICP.Candid.Utilities;
using EdjCase.ICP.InternetIdentity;
using Dahomey.Cbor;
using Dahomey.Cbor.Util;
using McMaster.Extensions.CommandLineUtils;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;


[HelpOption("-h|--help")]
public class App
{
	[Option("-u|--user-number", Description = "Authenticate as this user")]
	public ulong UserNumber { get; set; }

	public static void Main(string[] args) => CommandLineApplication.Execute<App>(args);

	public async Task OnExecute()
	{
		var run = this.Run();
		await Polling(run);
	}

	public static async Task Polling(Task t)
	{
		var sw = System.Diagnostics.Stopwatch.StartNew();

		while (!t.IsCompleted)
		{
			Console.WriteLine($"Waiting for {sw.Elapsed}...");
			await Task.Delay(1000);
		}
	}


	public static DelegationIdentity? GetDelegationIdentityFromFile()
	{
		try
		{
			var raw = Convert.FromBase64String(System.IO.File.ReadAllText("test_SessionIdentity.json"));
			var ms = new System.IO.MemoryStream(raw);
			using (var reader = new BsonDataReader(ms))
			{
				JsonSerializer serializer = new JsonSerializer();
				return serializer.Deserialize<DelegationIdentity>(reader);
			}
		}
		catch
		{
			return null;
		}
	}

	public static async Task<DelegationIdentity> CreateDelegationIdentity(ulong userNumber, string hostname)
	{
		// authenticate to II
		var anonConn = new IIConnection();
		var authenticatedConn = await anonConn.LoginToConn(userNumber);

		// get delegation identity for sessions
		ED25519Identity sessionKey = ED25519Identity.Generate();
		var sessionDelegationIdentity = await authenticatedConn.PrepareAndGetDelegation(hostname, sessionKey);
		return sessionDelegationIdentity;
	}

	public static async Task<DelegationIdentity> GetOrCreateDelegationIdentity(ulong userNumber, string hostname)
	{
		var identity = GetDelegationIdentityFromFile();
		if (identity != null) return identity;

		identity = await CreateDelegationIdentity(userNumber, hostname);

		var ms = new System.IO.MemoryStream();
		using (var writer = new BsonDataWriter(ms))
		{
			JsonSerializer serializer = new JsonSerializer();
			serializer.Serialize(writer, identity);
		}
		System.IO.File.WriteAllText("test_SessionIdentity.json", Convert.ToBase64String(ms.ToArray()));

		return identity;
	}

	public async Task Run()
	{
		// this is the frontend canister to which we want to authenticate.
		// we can pretend to to be this host for now (to get the same anonymized identity as in the browser)...
		// in practice we should use derivation origins.
		var hostname = "https://6nx2y-qiaaa-aaaal-qa6wq-cai.ic0.app";

		var sessionDelegationIdentity = await GetOrCreateDelegationIdentity(this.UserNumber, hostname);

		// this is the backend canister with which we want to communicate.
		var canisterId = Principal.FromText("6eure-gaaaa-aaaal-qa6xa-cai");

		// make a call to the canister
		Uri url = new Uri($"https://ic0.app");
		IAgent agent = new HttpAgent(sessionDelegationIdentity, url);

		var response = await agent.CallAndWaitAsync(
			canisterId,
			"get_account_data",
			CandidArg.Empty());
		Console.WriteLine(response.ToString());

		var response2 = await agent.CallAndWaitAsync(
			canisterId,
			"get_owned_objects",
			CandidArg.Empty());
		Console.WriteLine(response2.ToString());
	}
}