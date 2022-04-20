using Dahomey.Cbor.ObjectModel;
using Dahomey.Cbor.Serialization;
using Dahomey.Cbor.Serialization.Converters;
using ICP.Agent;
using ICP.Agent.Responses;
using ICP.Common.Candid;
using ICP.Common.Encodings;
using ICP.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agent.Cbor
{
	internal class QueryResponseCborConverter : CborConverterBase<QueryResponse>
	{
		public override QueryResponse Read(ref CborReader reader)
		{
            var context = new QueryReponseContext();
            reader.ReadMap(new QueryResponseCborMapReader(), ref context);
            switch (context.Status)
            {
				case "replied":
                    var reply = new QueryReply(context.ReplyArg!);
					return QueryResponse.Replied(reply);
				case "rejected":
                    ReplicaRejectCode code = (ReplicaRejectCode)context.RejectCode!.ToUInt64();
					return QueryResponse.Rejected(code, context.RejectMessage);
				default:
					throw new NotImplementedException($"Cannot deserialize query response with status '{context.Status}'");
            }
		}

		public override void Write(ref CborWriter writer, QueryResponse value)
		{
			throw new NotImplementedException();
		}
	}

    internal class QueryResponseCborMapReader : ICborMapReader<QueryReponseContext>
    {
        public void ReadBeginMap(int size, ref QueryReponseContext context)
        {

        }

        public void ReadMapItem(ref CborReader reader, ref QueryReponseContext context)
        {
            string? field = reader.ReadString();
            reader.MoveNextMapItem();
            switch (field)
            {
                case "status":
                    string? status = reader.ReadString();
                    context.Status = status;
                    return;
                case "reply":
                    byte[]? replyContext = null;
                    reader.ReadMap(new ReplyCborMapReader(), ref replyContext);
                    context.ReplyArg = replyContext;
                    return;
                case "reject_code":
                    CborDataItemType codeType = reader.GetCurrentDataItemType();
                    switch (codeType)
                    {
                        case CborDataItemType.Unsigned:
                            ulong code = reader.ReadUInt64();
                            context.RejectCode = LEB128.FromUInt64(code);
                            return;
                        case CborDataItemType.ByteString:
                            byte[] codeBytes = reader.ReadByteString().ToArray();
                            context.RejectCode = LEB128.FromRaw(codeBytes);
                            return;
                        default:
                            throw new NotImplementedException($"Cannot deserialize query reject_code of type '{codeType}'");
                    }
                case "reject_message":
                    context.RejectMessage = reader.ReadString();
                    return;
                default:
                    throw new NotImplementedException($"Cannot deserialize query response. Unknown field '{field}'");
            }
        }
    }

    internal class ReplyCborMapReader : ICborMapReader<byte[]?>
    {
        public void ReadBeginMap(int size, ref byte[]? context)
        {

        }

        public void ReadMapItem(ref CborReader reader, ref byte[]? context)
        {
            context = reader.ReadByteString().ToArray();
        }
    }
    
    internal class QueryReponseContext
    {
        public string? Status { get; set; }
        public byte[]? ReplyArg { get; set; }
        public LEB128? RejectCode { get; set; }
        public string? RejectMessage { get; set; }
    }
}
