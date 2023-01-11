using EdjCase.ICP.Agent.Agents;
using EdjCase.ICP.Agent.Identity;
using EdjCase.ICP.Candid.Models;
using System;
using System.Threading.Tasks;
using EdjCase.ICP.Candid.Utilities;
using System.Numerics;
using Candid = EdjCase.ICP.Candid;
using System.Linq;
using Newtonsoft.Json;

using EdjCase.ICP.InternetIdentity;

namespace Jsonnable
{
	public class Delegation
	{
		public string pubkey = "";
		public string expiration = "";
		public string[]? targets;
	}

	public class SignedDelegation
	{
		public string signature = "";
		public Delegation delegation = new();
	}

	public class DelegationChain
	{
		public string publicKey = "";
		public SignedDelegation[] delegations = System.Array.Empty<SignedDelegation>();
	}

	public class ED25519Identity
	{
		public string privateKey = "";
		public string publicKey = "";
	}

	public class DelegationIdentity
	{
		public ED25519Identity sessionKey = new();
		public DelegationChain identity = new();
	}
}


public class App
{
	public static Delegation FromJSON(Jsonnable.Delegation x)
	{
		return new Delegation(
			publicKey: ByteUtil.FromHexString(x.pubkey),
			expiration: new (new Candid.UnboundedUInt(BigInteger.Parse(x.expiration, System.Globalization.NumberStyles.AllowHexSpecifier))),
			targets: x.targets?.Select(Principal.FromHex).ToList()
		);
	}

	public static SignedDelegation FromJSON(Jsonnable.SignedDelegation x)
	{
		return new SignedDelegation(FromJSON(x.delegation), new(ByteUtil.FromHexString(x.signature)));
	}
	public static DelegationChain FromJSON(Jsonnable.DelegationChain x)
	{
		return new DelegationChain(
			new DerEncodedPublicKey(ByteUtil.FromHexString(x.publicKey)),
			x.delegations.Select(FromJSON).ToList());
	}

	public static ED25519Identity FromJSON(Jsonnable.ED25519Identity x)
	{
		return new ED25519Identity(
			ED25519PublicKey.FromDer(ByteUtil.FromHexString(x.publicKey)),
			ByteUtil.FromHexString(x.privateKey)
		);
	}

	public static DelegationIdentity FromJSON(Jsonnable.DelegationIdentity x)
	{
		return new DelegationIdentity(
			FromJSON(x.sessionKey),
			FromJSON(x.identity));
	}

	public static DelegationIdentity GetIdentity()
	{
		string identityStr = System.IO.File.ReadAllText("test_SessionIdentity.json");
		var identity = JsonConvert.DeserializeObject<Jsonnable.DelegationIdentity>(identityStr);
		return FromJSON(identity);
	}

	public static async Task Main(string[] args)
	{
		var identitySaved = GetIdentity();

		var userNumber = 1980705ul;

		// this is the frontend canister to which we want to authenticate.
		// we can pretend to to be this host for now (to get the same anonymized identity as in the browser)...
		// in practice we should use derivation origins.
		var hostname = "https://6nx2y-qiaaa-aaaal-qa6wq-cai.ic0.app";

		// this is the backend canister with which we want to communicate.
		var canisterId = Principal.FromText("6eure-gaaaa-aaaal-qa6xa-cai");

		// authenticate to II
		var anonConn = new IIConnection();
		var authenticatedConn = await anonConn.LoginToConn(userNumber);
		
		// get delegation identity for sessions
		ED25519Identity sessionKey = ED25519Identity.Generate();
		var sessionDelegationIdentity = await authenticatedConn.PrepareAndGetDelegation(hostname, sessionKey);

		// make a call to the canister
		Uri url = new Uri($"https://ic0.app");
		IAgent agent = new HttpAgent(sessionDelegationIdentity, url);

		var response = await agent.CallAndWaitAsync(
			canisterId,
			"get_account_data",
			CandidArg.Empty());

		Console.WriteLine(response.ToString());
	}
}