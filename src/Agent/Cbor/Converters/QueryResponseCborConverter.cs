using Dahomey.Cbor.Serialization;
using Dahomey.Cbor.Serialization.Converters;
using EdjCase.ICP.Agent.Cbor;
using EdjCase.ICP.Agent.Responses;
using EdjCase.ICP.Candid.Encodings;
using EdjCase.ICP.Candid.Models;
using EdjCase.ICP.Candid.Utilities;
using System;

namespace EdjCase.ICP.Agent.Cbor.Converters
{
	internal class QueryResponseCborConverter : CborConverterBase<QueryResponse>
	{
		public override QueryResponse Read(ref CborReader reader)
		{
			var context = new QueryReponseContext();
			CborUtil.ReadMap(ref reader, ref context, this.SetQueryResponseField);
			switch (context.Status)
			{
				case "replied":
#if DEBUG
					string argHex = ByteUtil.ToHexString(context.ReplyArg!);
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

		private void SetQueryResponseField(string name, ref CborReader reader, ref QueryReponseContext context)
		{
			switch (name)
			{
				case "status":
					string? status = reader.ReadString();
					context.Status = status;
					break;
				case "reply":
					byte[]? replyContext = null;
					CborUtil.ReadMap(ref reader, ref replyContext, this.SetQueryResponseReply);
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
					throw new NotImplementedException($"Cannot deserialize query response. Unknown field '{name}'");
			}
		}

		private void SetQueryResponseReply(string field, ref CborReader reader, ref byte[]? context)
		{
			switch (field)
			{
				case "arg":
					context = reader.ReadByteString().ToArray();
					break;
				default:
					throw new NotImplementedException($"Cannot deserialize query response. Unknown field '{field}'");
			}
		}

		public override void Write(ref CborWriter writer, QueryResponse value)
		{
			// Never write
			throw new NotImplementedException();
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
