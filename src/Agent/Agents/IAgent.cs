using EdjCase.ICP.Agent.Auth;
using EdjCase.ICP.Agent.Requests;
using EdjCase.ICP.Agent.Responses;
using EdjCase.ICP.Candid;
using EdjCase.ICP.Candid.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EdjCase.ICP.Agent.Agents
{
	public interface IAgent
	{
		/**
		* Send a read state query to the replica. This includes a list of paths to return,
		* and will return a Certificate. This will only reject on communication errors,
		* but the certificate might contain less information than requested.
		* @param effectiveCanisterId A Canister ID related to this call.
		* @param options The options for this call.
		*/
		Task<ReadStateResponse> ReadStateAsync(Principal canisterId, List<Path> paths, IIdentity? identityOverride = null);

		Task<RequestStatus?> GetRequestStatusAsync(Principal canisterId, RequestId id);

		Task<RequestId> CallAsync(Principal canisterId, string method, CandidArg encodedArgument, Principal? effectiveCanisterId = null, IIdentity? identityOverride = null);

		/**
		* Query the status endpoint of the replica. This normally has a few fields that
		* corresponds to the version of the replica, its root public key, and any other
		* information made public.
		* @returns A JsonObject that is essentially a record of fields from the status
		*     endpoint.
		*/
		Task<StatusResponse> GetStatusAsync();

		/**
		* Send a query call to a canister. See
		* {@link https://sdk.ICP.org/docs/interface-spec/#http-query | the interface spec}.
		* @param canisterId The Principal of the Canister to send the query to. Sending a query to
		*     the management canister is not supported (as it has no meaning from an agent).
		* @param options Options to use to create and send the query.
		* @returns The response from the replica. The Promise will only reject when the communication
		*     failed. If the query itself failed but no protocol errors happened, the response will
		*     be of type QueryResponseRejected.
		*/
		Task<QueryResponse> QueryAsync(Principal canisterId, string method, CandidArg arg, IIdentity? identityOverride = null);
		/**
		* By default, the agent is configured to talk to the main Internet Computer,
		* and verifies responses using a hard-coded public key.
		*
		* This function will instruct the agent to ask the endpoint for its public
		* key, and use that instead. This is required when talking to a local test
		* instance, for example.
		*
		* Only use this when you are  _not_ talking to the main Internet Computer,
		* otherwise you are prone to man-in-the-middle attacks! Do not call this
		* function by default.
		*/
		Task<byte[]> GetRootKeyAsync();


	}

	public static class IAgentExtensions
	{
		public static async Task<CandidArg> CallAndWaitAsync(
			this IAgent agent,
			Principal canisterId,
			string method,
			CandidArg encodedArgument,
			Principal? effectiveCanisterId = null,
			IIdentity? identityOverride = null,
			CancellationToken? cancellationToken = null)
		{
			RequestId id = await agent.CallAsync(canisterId, method, encodedArgument, effectiveCanisterId, identityOverride);

			while (true)
			{
				await Task.Delay(100, cancellationToken ?? CancellationToken.None);

				cancellationToken?.ThrowIfCancellationRequested();

				RequestStatus? requestStatus = await agent.GetRequestStatusAsync(canisterId, id);

				cancellationToken?.ThrowIfCancellationRequested();

				switch (requestStatus?.Type)
				{
					case null:
					case RequestStatus.StatusType.Received:
					case RequestStatus.StatusType.Processing:
						continue; // Still processing
					case RequestStatus.StatusType.Replied:
						return requestStatus.AsReplied();
					case RequestStatus.StatusType.Rejected:
						(UnboundedUInt code, string message, string? errorCode) = requestStatus.AsRejected();
						throw new CallRejectedException(code, message, errorCode);
					case RequestStatus.StatusType.Done:
						throw new RequestCleanedUpException();
				}
			}
		}
	}
}
