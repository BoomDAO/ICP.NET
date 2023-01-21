using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using UserNumber = System.UInt64;
using PublicKey = System.Collections.Generic.List<System.Byte>;
using DeviceKey = System.Collections.Generic.List<System.Byte>;
using UserKey = System.Collections.Generic.List<System.Byte>;
using SessionKey = System.Collections.Generic.List<System.Byte>;
using FrontendHostname = System.String;
using Timestamp = System.UInt64;
using EdjCase.ICP.Agent.Agents;
using EdjCase.ICP.Agent.Responses;
using EdjCase.ICP.Candid.Models;
using EdjCase.ICP.InternetIdentity.Models;
using EdjCase.ICP.Agent.Identities;

namespace EdjCase.ICP.InternetIdentity
{
	internal class InternetIdentityApiClient
	{
		public IAgent Agent { get; }
		public Principal CanisterId { get; }

		public InternetIdentityApiClient(IAgent agent, Principal canisterId)
		{
			this.Agent = agent ?? throw new ArgumentNullException(nameof(agent));
			this.CanisterId = canisterId ?? throw new ArgumentNullException(nameof(canisterId));
		}


		public async Task InitSalt()
		{
			string method = "init_salt";
			var candidArgs = new List<CandidTypedValue>
			{
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			CandidArg responseArg = await this.Agent.CallAndWaitAsync(this.CanisterId, method, arg, null, null);
		}
		public async Task<Challenge> CreateChallenge()
		{
			string method = "create_challenge";
			var candidArgs = new List<CandidTypedValue>
			{
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			CandidArg responseArg = await this.Agent.CallAndWaitAsync(this.CanisterId, method, arg, null, null);
			Challenge r0 = responseArg.Values[0].ToObject<Challenge>();
			return (r0);
		}
		public async Task<RegisterResponse> Register(DeviceData arg0, ChallengeResult arg1)
		{
			string method = "register";
			CandidTypedValue p0 = CandidTypedValue.FromObject(arg0);
			CandidTypedValue p1 = CandidTypedValue.FromObject(arg1);
			var candidArgs = new List<CandidTypedValue>
			{
				p0,
				p1,
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			CandidArg responseArg = await this.Agent.CallAndWaitAsync(this.CanisterId, method, arg, null, null);
			RegisterResponse r0 = responseArg.Values[0].ToObject<RegisterResponse>();
			return (r0);
		}
		public async Task Add(UserNumber arg0, DeviceData arg1)
		{
			string method = "add";
			CandidTypedValue p0 = CandidTypedValue.FromObject(arg0);
			CandidTypedValue p1 = CandidTypedValue.FromObject(arg1);
			var candidArgs = new List<CandidTypedValue>
			{
				p0,
				p1,
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			CandidArg responseArg = await this.Agent.CallAndWaitAsync(this.CanisterId, method, arg, null, null);
		}
		public async Task Update(UserNumber arg0, DeviceKey arg1, DeviceData arg2)
		{
			string method = "update";
			CandidTypedValue p0 = CandidTypedValue.FromObject(arg0);
			CandidTypedValue p1 = CandidTypedValue.FromObject(arg1);
			CandidTypedValue p2 = CandidTypedValue.FromObject(arg2);
			var candidArgs = new List<CandidTypedValue>
			{
				p0,
				p1,
				p2,
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			CandidArg responseArg = await this.Agent.CallAndWaitAsync(this.CanisterId, method, arg, null, null);
		}
		public async Task Remove(UserNumber arg0, DeviceKey arg1)
		{
			string method = "remove";
			CandidTypedValue p0 = CandidTypedValue.FromObject(arg0);
			CandidTypedValue p1 = CandidTypedValue.FromObject(arg1);
			var candidArgs = new List<CandidTypedValue>
			{
				p0,
				p1,
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			CandidArg responseArg = await this.Agent.CallAndWaitAsync(this.CanisterId, method, arg, null);
		}
		public async Task<List<DeviceData>> Lookup(UserNumber arg0)
		{
			string method = "lookup";
			CandidTypedValue p0 = CandidTypedValue.FromObject(arg0);
			var candidArgs = new List<CandidTypedValue>
			{
				p0,
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, method, arg);
			QueryReply reply = response.ThrowOrGetReply();
			List<DeviceData> r0 = reply.Arg.Values[0].ToObject<List<DeviceData>>();
			return (r0);
		}
		public async Task<IdentityAnchorInfo> GetAnchorInfo(UserNumber arg0)
		{
			string method = "get_anchor_info";
			CandidTypedValue p0 = CandidTypedValue.FromObject(arg0);
			var candidArgs = new List<CandidTypedValue>
			{
				p0,
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			CandidArg responseArg = await this.Agent.CallAndWaitAsync(this.CanisterId, method, arg, null);
			IdentityAnchorInfo r0 = responseArg.Values[0].ToObject<IdentityAnchorInfo>();
			return (r0);
		}
		public async Task<Principal> GetPrincipal(UserNumber arg0, FrontendHostname arg1)
		{
			string method = "get_principal";
			CandidTypedValue p0 = CandidTypedValue.FromObject(arg0);
			CandidTypedValue p1 = CandidTypedValue.FromObject(arg1);
			var candidArgs = new List<CandidTypedValue>
			{
				p0,
				p1,
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, method, arg);
			QueryReply reply = response.ThrowOrGetReply();
			Principal r0 = reply.Arg.Values[0].ToObject<Principal>();
			return (r0);
		}
		public async Task<InternetIdentityStats> Stats()
		{
			string method = "stats";
			var candidArgs = new List<CandidTypedValue>
			{
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, method, arg);
			QueryReply reply = response.ThrowOrGetReply();
			InternetIdentityStats r0 = reply.Arg.Values[0].ToObject<InternetIdentityStats>();
			return (r0);
		}
		public async Task<Timestamp> EnterDeviceRegistrationMode(UserNumber arg0)
		{
			string method = "enter_device_registration_mode";
			CandidTypedValue p0 = CandidTypedValue.FromObject(arg0);
			var candidArgs = new List<CandidTypedValue>
			{
				p0,
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			CandidArg responseArg = await this.Agent.CallAndWaitAsync(this.CanisterId, method, arg, null);
			Timestamp r0 = responseArg.Values[0].ToObject<Timestamp>();
			return (r0);
		}
		public async Task ExitDeviceRegistrationMode(UserNumber arg0)
		{
			string method = "exit_device_registration_mode";
			CandidTypedValue p0 = CandidTypedValue.FromObject(arg0);
			var candidArgs = new List<CandidTypedValue>
			{
				p0,
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			CandidArg responseArg = await this.Agent.CallAndWaitAsync(this.CanisterId, method, arg, null);
		}
		public async Task<AddTentativeDeviceResponse> AddTentativeDevice(UserNumber arg0, DeviceData arg1)
		{
			string method = "add_tentative_device";
			CandidTypedValue p0 = CandidTypedValue.FromObject(arg0);
			CandidTypedValue p1 = CandidTypedValue.FromObject(arg1);
			var candidArgs = new List<CandidTypedValue>
			{
				p0,
				p1,
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			CandidArg responseArg = await this.Agent.CallAndWaitAsync(this.CanisterId, method, arg, null);
			AddTentativeDeviceResponse r0 = responseArg.Values[0].ToObject<AddTentativeDeviceResponse>();
			return (r0);
		}
		public async Task<VerifyTentativeDeviceResponse> VerifyTentativeDevice(UserNumber arg0, string verificationCode)
		{
			string method = "verify_tentative_device";
			CandidTypedValue p0 = CandidTypedValue.FromObject(arg0);
			CandidTypedValue p1 = CandidTypedValue.FromObject(verificationCode);
			var candidArgs = new List<CandidTypedValue>
			{
				p0,
				p1,
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			CandidArg responseArg = await this.Agent.CallAndWaitAsync(this.CanisterId, method, arg, null);
			VerifyTentativeDeviceResponse r0 = responseArg.Values[0].ToObject<VerifyTentativeDeviceResponse>();
			return (r0);
		}
		public async Task<(byte[] UserKey, Timestamp Expiration)> PrepareDelegation(UserNumber arg0, FrontendHostname arg1, byte[] arg2, OptionalValue<ulong> maxTimeToLive)
		{
			string method = "prepare_delegation";
			CandidTypedValue p0 = CandidTypedValue.FromObject(arg0);
			CandidTypedValue p1 = CandidTypedValue.FromObject(arg1);
			CandidTypedValue p2 = CandidTypedValue.FromObject(arg2);
			CandidTypedValue p3 = CandidTypedValue.FromObject(maxTimeToLive);
			var candidArgs = new List<CandidTypedValue>
			{
				p0,
				p1,
				p2,
				p3,
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			CandidArg responseArg = await this.Agent.CallAndWaitAsync(this.CanisterId, method, arg, null);
			byte[] r0 = responseArg.Values[0].ToObject<byte[]>();
			Timestamp r1 = responseArg.Values[1].ToObject<Timestamp>();
			return (r0, r1);
		}
		public async Task<GetDelegationResponse> GetDelegation(UserNumber arg0, FrontendHostname arg1, byte[] arg2, Timestamp arg3)
		{
			string method = "get_delegation";
			CandidTypedValue p0 = CandidTypedValue.FromObject(arg0);
			CandidTypedValue p1 = CandidTypedValue.FromObject(arg1);
			CandidTypedValue p2 = CandidTypedValue.FromObject(arg2);
			CandidTypedValue p3 = CandidTypedValue.FromObject(arg3);
			var candidArgs = new List<CandidTypedValue>
			{
				p0,
				p1,
				p2,
				p3,
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, method, arg);
			QueryReply reply = response.ThrowOrGetReply();
			GetDelegationResponse r0 = reply.Arg.Values[0].ToObject<GetDelegationResponse>();
			return (r0);
		}
		public async Task<HttpResponse> HttpRequest(HttpRequest request)
		{
			string method = "http_request";
			CandidTypedValue p0 = CandidTypedValue.FromObject(request);
			var candidArgs = new List<CandidTypedValue>
			{
				p0,
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, method, arg);
			QueryReply reply = response.ThrowOrGetReply();
			HttpResponse r0 = reply.Arg.Values[0].ToObject<HttpResponse>();
			return (r0);
		}
		public async Task<DeployArchiveResult> DeployArchive(PublicKey wasm)
		{
			string method = "deploy_archive";
			CandidTypedValue p0 = CandidTypedValue.FromObject(wasm);
			var candidArgs = new List<CandidTypedValue>
			{
				p0,
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			CandidArg responseArg = await this.Agent.CallAndWaitAsync(this.CanisterId, method, arg, null);
			DeployArchiveResult r0 = responseArg.Values[0].ToObject<DeployArchiveResult>();
			return (r0);
		}
	}
}

