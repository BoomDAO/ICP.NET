using ICP.Agent.Auth;
using ICP.Agent.Responses;
using ICP.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ICP.Agent.Agents
{
    public interface IAgent
    {
        /// <summary>
        /// Returns the principal ID associated with this agent (by default). It only shows
        /// the principal of the default identity in the agent, which is the principal used
        /// when calls don't specify it.
        /// </summary>
        /// <returns>Principal for agent</returns>
        PrincipalId GetPrincipal();

        /**
		* Send a read state query to the replica. This includes a list of paths to return,
		* and will return a Certificate. This will only reject on communication errors,
		* but the certificate might contain less information than requested.
		* @param effectiveCanisterId A Canister ID related to this call.
		* @param options The options for this call.
		*/
        Task<ReadStateResponse> ReadStateAsync(PrincipalId canisterId, List<Path> paths, IIdentity? identityOverride = null);

        Task CallAsync(PrincipalId canisterId, string method, CandidArg encodedArgument, PrincipalId? effectiveCanisterId = null, IIdentity? identityOverride = null);

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
        Task<QueryResponse> QueryAsync(PrincipalId canisterId, string method, CandidArg encodedArgument, IIdentity? identityOverride = null);
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
        Task<Key> GetRootKeyAsync();
    }
}
