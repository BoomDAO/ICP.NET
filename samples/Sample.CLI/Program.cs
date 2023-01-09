using EdjCase.ICP.Agent.Agents;
using EdjCase.ICP.Agent.Auth;
using EdjCase.ICP.Agent.Identity;
using EdjCase.ICP.Agent.Responses;
using EdjCase.ICP.Candid.Models;
using Sample.Shared.Governance;
using Sample.Shared.Governance.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Path = EdjCase.ICP.Candid.Models.Path;
using Newtonsoft.Json.Linq;
using System.Diagnostics.Contracts;
using EdjCase.ICP.Candid.Utilities;
using System.Numerics;
using Candid = EdjCase.ICP.Candid;
using System.Linq;
using EdjCase.ICP.Agent;
using Newtonsoft.Json;

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
			new ED25519PublicKey(ByteUtil.FromHexString(x.publicKey)), // Note: assumes that the public key has the correct encoding
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

	public static IIdentity GetIdentity()
	{
		string identityStr = "";
		var identity = JsonConvert.DeserializeObject<Jsonnable.DelegationIdentity>(identityStr);
		return FromJSON(identity);
	}

	public static async Task Main(string[] args)
	{
		Uri url = new Uri($"https://ic0.app");

		var identity = GetIdentity();
		IAgent agent = new HttpAgent(identity, url);

		Principal canisterId = Principal.FromText("6eure-gaaaa-aaaal-qa6xa-cai");

		var response = await agent.CallAndWaitAsync(
			canisterId,
			"get_account_data",
			CandidArg.Empty());

		Console.WriteLine(response.ToString());
	}
}