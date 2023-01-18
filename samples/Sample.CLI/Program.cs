using EdjCase.ICP.Agent.Agents;
using EdjCase.ICP.Candid.Models;
using System;
using System.Threading.Tasks;
using EdjCase.ICP.InternetIdentity;
using McMaster.Extensions.CommandLineUtils;
using EdjCase.ICP.Serialization;
using EdjCase.ICP.Agent.Identities;

[HelpOption("-h|--help")]
public class App
{

	[Option("-u|--user-number", Description = "Authenticate as this user")]
	public ulong UserNumber { get; set; }


	[Option("--write-format", Description = "Write session data in this format")]
	public SerializedFormat WriteFormat { get; set; }


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
			var raw = System.IO.File.ReadAllBytes("test_SessionIdentity.json");
			return SerializationUtil.Deserialize<DelegationIdentity>(SerializationUtil.DefaultSerializer, raw);
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
		Ed25519Identity sessionKey = Ed25519Identity.Generate();
		var sessionDelegationIdentity = await authenticatedConn.PrepareAndGetDelegation(hostname, sessionKey);
		return sessionDelegationIdentity;
	}

	public async Task<DelegationIdentity> GetOrCreateDelegationIdentity(ulong userNumber, string hostname)
	{
		var identity = GetDelegationIdentityFromFile();
		if (identity != null && identity.Chain.IsExpirationValid(ICTimestamp.Now())) return identity;

		identity = await CreateDelegationIdentity(userNumber, hostname);

		System.IO.File.WriteAllBytes(
			"test_SessionIdentity.json",
			SerializationUtil.Serialize(SerializationUtil.DefaultSerializer, identity, this.WriteFormat));

		return identity;
	}

	public async Task Run()
	{
		// this is the frontend canister to which we want to authenticate.
		// we can pretend to to be this host for now (to get the same anonymized identity as in the browser)...
		// in practice we should use derivation origins.
		var hostname = "https://6nx2y-qiaaa-aaaal-qa6wq-cai.ic0.app";

		var sessionDelegationIdentity = await this.GetOrCreateDelegationIdentity(this.UserNumber, hostname);

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