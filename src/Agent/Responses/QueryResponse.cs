using Dahomey.Cbor.ObjectModel;
using ICP.Common.Models;
using System;

namespace ICP.Agent.Responses
{
    public class QueryResponse
    {
        public QueryResponseType Type { get; }
        private readonly object value;

        private QueryResponse(QueryResponseType type, object value)
        {
            this.Type = type;
            this.value = value;
        }

        public QueryReply AsReplied()
        {
            this.ThrowIfWrongType(QueryResponseType.Replied);
            return (QueryReply)this.value;
        }

        public (ReplicaRejectCode Code, string? Message) AsRejected()
        {
            this.ThrowIfWrongType(QueryResponseType.Rejected);
            return (ValueTuple<ReplicaRejectCode, string>)this.value;
        }

        private void ThrowIfWrongType(QueryResponseType type)
        {
            if (this.Type != type)
            {
                throw new InvalidOperationException($"Unable to parse '{this.Type}' query response as type '{type}'");
            }
        }

        public static QueryResponse Rejected(ReplicaRejectCode code, string? message)
        {
            return new QueryResponse(QueryResponseType.Rejected, (code, message));
        }

        public static QueryResponse Replied(QueryReply reply)
        {
            return new QueryResponse(QueryResponseType.Replied, reply);
        }
    }

    public enum QueryResponseType
    {
        Replied,
        Rejected
    }

    public class QueryReply
    {
        public CandidArg Arg { get; }

        public QueryReply(CandidArg arg)
        {
            this.Arg = arg;
        }
    }

    public enum ReplicaRejectCode
    {
        SysFatal = 1,
        SysTransient = 2,
        DestinationInvalid = 3,
        CanisterReject = 4,
        CanisterError = 5
    }
}