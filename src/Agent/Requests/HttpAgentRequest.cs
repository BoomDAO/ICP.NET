using System;

namespace Dfinity.Agent.Requests
{
	public class HttpAgentRequest
	{
		public AgentEndpoint Endpoint { get; }
		private readonly object value;
		private HttpAgentRequest(AgentEndpoint endpoint, object value)
		{
			this.Endpoint = endpoint;
			this.value = value ?? throw new ArgumentNullException(nameof(value));
		}

		public QueryRequest AsReadState()
		{
			this.ThrowIfWrongType(AgentEndpoint.ReadState);
			return (QueryRequest)this.value;
		}

		public QueryRequest AsQuery()
		{
			this.ThrowIfWrongType(AgentEndpoint.ReadState);
			return (QueryRequest)this.value;
		}

		public CallRequest AsCall()
		{
			this.ThrowIfWrongType(AgentEndpoint.ReadState);
			return (CallRequest)this.value;
		}


		public static HttpAgentRequest ReadState(QueryRequest request)
		{
			return new HttpAgentRequest(AgentEndpoint.ReadState, request);
		}

		public static HttpAgentRequest Query(QueryRequest request)
		{
			return new HttpAgentRequest(AgentEndpoint.ReadState, request);
		}

		public static HttpAgentRequest Call(CallRequest request)
		{
			return new HttpAgentRequest(AgentEndpoint.ReadState, request);
		}

		private void ThrowIfWrongType(AgentEndpoint endpoint)
		{
			if (this.Endpoint != endpoint)
			{
				throw new InvalidOperationException($"Unable to parse '{this.Endpoint}' agent endpoint response as type '{endpoint}'");
			}
		}
	}

	public enum AgentEndpoint
	{
		ReadState,
		Query,
		Call
	}
}