using Dahomey.Cbor.ObjectModel;
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

        public (ReplicaRejectCode Code, string Message) AsRejected()
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

        public static QueryResponse FromCbor(CborObject cbor)
        {
            string status = cbor["status"].Value<string>();
            QueryResponseType type;
            object value;
            switch (status)
            {
                case "replied":
                    type = QueryResponseType.Replied;
                    QueryReply reply = QueryReply.FromCbor(cbor["reply"].Value<CborObject>());
                    value = QueryResponse.Replied(reply);
                    break;
                case "rejected":
                    type = QueryResponseType.Rejected;
                    ReplicaRejectCode code = cbor["reject_code"].Value<ReplicaRejectCode>();
                    string message = cbor["reject_message"].Value<string>();
                    value = QueryResponse.Rejected(code, message);
                    break;
                default:
                    throw new Exception($"Invalid query response status: {status}");
            }
            return new QueryResponse(type, value);
        }

        public static QueryResponse Rejected(ReplicaRejectCode code, string message)
        {
            return new QueryResponse(QueryResponseType.Rejected, (code, message));
        }

        private static QueryResponse Replied(QueryReply reply)
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
        public byte[] Arg { get; }

        public QueryReply(byte[] arg)
        {
            this.Arg = arg;
        }

        public static QueryReply FromCbor(CborObject cborObject)
        {
            byte[] arg = cborObject.Value<byte[]>();
            return new QueryReply(arg);
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