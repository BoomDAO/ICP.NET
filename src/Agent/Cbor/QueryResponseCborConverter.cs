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
#if DEBUG
                    string argHex = Convert.ToHexString(context.ReplyArg!);
#endif
                    var arg = CandidArg.FromBytes(context.ReplyArg!);
                    var reply = new QueryReply(arg);
					return QueryResponse.Replied(reply);
				case "rejected":
                    ReplicaRejectCode code = (ReplicaRejectCode)(ulong)context.RejectCode!;
					return QueryResponse.Rejected(code, context.RejectMessage);
				default:
					throw new NotImplementedException($"Cannot deserialize query response with status '{context.Status}'");
            }
		}

		public override void Write(ref CborWriter writer, QueryResponse value)
		{
            // Never write
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
            bool lastItem = false;
            bool read = true;
            while(read)
            {
                string? field = reader.ReadString();
                reader.MoveNextMapItem();
                switch (field)
                {
                    case "status":
                        string? status = reader.ReadString();
                        context.Status = status;
                        break;
                    case "reply":
                        byte[]? replyContext = null;
                        reader.ReadMap(new ReplyCborMapReader(), ref replyContext);
                        context.ReplyArg = replyContext;
                        break;
                    case "reject_code":
                        CborDataItemType codeType = reader.GetCurrentDataItemType();
                        switch (codeType)
                        {
                            case CborDataItemType.Unsigned:
                                context.RejectCode = reader.ReadUInt64();
                                break;
                            case CborDataItemType.ByteString:
                                byte[] codeBytes = reader.ReadByteString().ToArray();
                                context.RejectCode = LEB128.DecodeUnsigned(codeBytes);
                                break;
                            default:
                                throw new NotImplementedException($"Cannot deserialize query reject_code of type '{codeType}'");
                        }
                        break;
                    case "reject_message":
                        context.RejectMessage = reader.ReadString();
                        break;
                    default:
                        throw new NotImplementedException($"Cannot deserialize query response. Unknown field '{field}'");
                }
                bool isNext = reader.MoveNextMapItem();
                read = isNext || !lastItem;
                if (!isNext)
                {
                    lastItem = true;
                }
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
            string? field = reader.ReadString();
            reader.MoveNextMapItem();
            switch (field)
            {
                case "arg":
                    context = reader.ReadByteString().ToArray();
                    break;
                default:
                    throw new NotImplementedException($"Cannot deserialize query response. Unknown field '{field}'");
            }
        }
    }
    
    internal class QueryReponseContext
    {
        public string? Status { get; set; }
        public byte[]? ReplyArg { get; set; }
        public UnboundedUInt? RejectCode { get; set; }
        public string? RejectMessage { get; set; }
    }
}
