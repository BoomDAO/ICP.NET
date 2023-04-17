using EdjCase.ICP.Agent;
using EdjCase.ICP.Agent.Agents;
using EdjCase.ICP.Agent.Identities;
using EdjCase.ICP.Agent.Models;
using EdjCase.ICP.Agent.Responses;
using EdjCase.ICP.Candid.Models;
using EdjCase.ICP.Candid.Models.Types;
using EdjCase.ICP.Candid.Models.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdjCase.ICP.InternetIdentity
{

	internal class AgentInternetIdentityClient : IInternetIdentityClient
	{
		private IAgent agent { get; }
		private Principal identityCanisterId { get; }
		public AgentInternetIdentityClient(IAgent agent, Principal? identityCanisterOverride = null)
		{
			this.agent = agent;
			this.identityCanisterId = identityCanisterOverride ?? Principal.FromText("rdmx6-jaaaa-aaaaa-aaadq-cai");
		}

		public IIdentity? Identity { get; set; }

		public Principal GetCanisterId()
		{
			return this.identityCanisterId;
		}

		public async Task<List<DeviceInfo>> LookupAsync(ulong anchor)
		{
			CandidArg arg = CandidArg.FromCandid(
				CandidTypedValue.Nat64(anchor)
			);
			QueryResponse response = await this.agent.QueryAsync(this.identityCanisterId, "lookup", arg);
			CandidArg reply = response.ThrowOrGetReply();
			return reply.Values[0].Value.AsVector().Values
				.Select(d => d.AsRecord())
				// Only authentication devices
				.Where(d => d["purpose"].AsVariant().Tag == CandidTag.FromName("authentication"))
				.Select(d => new DeviceInfo(
					publicKey: SubjectPublicKeyInfo.FromDerEncoding(d["pubkey"].AsVectorAsArray(v => v.AsNat8())),
					credentialId: d["credential_id"].AsOptional(o => o.AsVectorAsArray(v => v.AsNat8())).GetValueOrDefault()
				))
				.ToList();
		}


		public async Task<DelegationIdentity> PrepareAndGetDelegationAsync(
			ulong anchor,
			string hostname,
			IIdentity deviceIdentity,
			IIdentity sessionIdentity,
			TimeSpan? maxTimeToLive = null
		)
		{
			SubjectPublicKeyInfo sesionPublicKey = sessionIdentity.GetPublicKey();

			var anchorArg = CandidTypedValue.Nat64(anchor);
			var hostnameArg = CandidTypedValue.Text(hostname);
			var publicKeyArg = CandidTypedValue.Vector(
				CandidType.Nat8(),
				sesionPublicKey.ToDerEncoding(),
				v => CandidValue.Nat8(v)
			);
			CandidArg prepareArg = CandidArg.FromCandid(
				anchorArg,
				hostnameArg,
				publicKeyArg,
				CandidTypedValue.Opt(new CandidTypedValue(
					maxTimeToLive.HasValue
						? CandidValue.Nat64((ulong)maxTimeToLive.Value.Ticks * 100UL)
						: CandidValue.Null(),
					CandidType.Nat64()
				))
			);
			// Authenticate as the device identity
			this.agent.Identity = deviceIdentity;


			// Prepare the delegation to be gotten
			CandidArg responseArg = await this.agent.CallAndWaitAsync(this.identityCanisterId, "prepare_delegation", prepareArg);
			byte[] userKey = responseArg.Values[0].Value.AsVectorAsArray(v => v.AsNat8());
			ulong expiration = responseArg.Values[1].Value.AsNat64();

			CandidArg getArg = CandidArg.FromCandid(
				anchorArg,
				hostnameArg,
				publicKeyArg,
				CandidTypedValue.Nat64(expiration)
			);
			// Get the delegation from the preperation
			QueryResponse response = await this.agent.QueryAsync(this.identityCanisterId, "get_delegation", getArg);
			CandidArg reply = response.ThrowOrGetReply();
			CandidVariant responseVariant = reply.Values[0].Value.AsVariant();

			switch (responseVariant.Tag.Id)
			{
				case 1500769643: // signed_delegation
					CandidRecord signedDelegationRecord = responseVariant.Value.AsRecord();
					CandidRecord delegationRecord = signedDelegationRecord["delegation"].AsRecord();

					byte[] publicKey = delegationRecord["pubkey"].AsVectorAsArray(v => v.AsNat8());
					ICTimestamp exp = ICTimestamp.FromNanoSeconds(delegationRecord["expiration"].AsNat64());
					List<Principal>? targets = delegationRecord["targets"]
						.AsOptional(v => v
							.AsVectorAsList(i => i
								.AsPrincipal()
							)
						)
						.GetValueOrDefault();

					Delegation delegation = new Delegation(SubjectPublicKeyInfo.FromDerEncoding(publicKey), exp, targets);
					var signedDelegation = new SignedDelegation(
						delegation,
						signature: signedDelegationRecord["signature"]
							.AsVectorAsArray(v => v.AsNat8())
					);

					var chain = new DelegationChain(
						SubjectPublicKeyInfo.FromDerEncoding(userKey),
						new List<SignedDelegation> { signedDelegation }
					);
					return new DelegationIdentity(sessionIdentity, chain);
				case 918105634: // no_such_delegation
					throw new Exception("Failed to find delegation. Delegation preparation must have failed.");
				default:
					throw new NotImplementedException();
			}
		}
	}
}
