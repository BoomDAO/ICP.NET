using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid.Models;
using System;
using System.Collections.Generic;
using System.Formats.Cbor;
using System.Text;

namespace EdjCase.ICP.WebSockets.Models
{
	internal class WebSocketMessageWrapper
	{
		[CandidName("msg")]
		public WebSocketMessage Message { get; set; }

		public WebSocketMessageWrapper(WebSocketMessage message)
		{
			this.Message = message ?? throw new ArgumentNullException(nameof(message));
		}
	}
	internal class WebSocketMessage
	{
		[CandidName("sequence_num")]
		public ulong SequenceNumber { get; }
		[CandidName("content")]
		public byte[] Content { get; }
		[CandidName("client_key")]
		public ClientKey ClientKey { get; }
		[CandidName("timestamp")]
		public ulong Timestamp { get; }
		[CandidName("is_service_message")]
		public bool IsServiceMessage { get; }

		public WebSocketMessage(
			ulong sequenceNumber,
			byte[] content,
			ClientKey clientKey,
			ulong timestamp,
			bool isServiceMessage
		)
		{
			this.SequenceNumber = sequenceNumber;
			this.Content = content ?? throw new ArgumentNullException(nameof(content));
			this.ClientKey = clientKey ?? throw new ArgumentNullException(nameof(clientKey));
			this.Timestamp = timestamp;
			this.IsServiceMessage = isServiceMessage;
		}

		internal static WebSocketMessage FromCbor(CborReader reader)
		{
			ulong? sequenceNumber = null;
			byte[]? content = null;
			ClientKey? clientKey = null;
			ulong? timestamp = null;
			bool? isServiceMessage = null;

			reader.ReadStartMap();
			while (reader.PeekState() != CborReaderState.EndMap)
			{
				switch (reader.ReadTextString())
				{
					case "sequence_num":
						sequenceNumber = reader.ReadUInt64();
						break;
					case "content":
						content = reader.ReadByteString();
						break;
					case "client_key":
						clientKey = ClientKey.FromCbor(reader);
						break;
					case "timestamp":
						timestamp = reader.ReadUInt64();
						break;
					case "is_service_message":
						isServiceMessage = reader.ReadBoolean();
						break;
				}
			}
			reader.ReadEndMap();
			if (sequenceNumber == null)
			{
				throw new CborContentException("Missing field from incoming client message: sequence_num");
			}
			if (content == null)
			{
				throw new CborContentException("Missing field from incoming client message: content");
			}
			if (clientKey == null)
			{
				throw new CborContentException("Missing field from incoming client message: client_key");
			}
			if (timestamp == null)
			{
				throw new CborContentException("Missing field from incoming client message: timestamp");
			}
			if (isServiceMessage == null)
			{
				throw new CborContentException("Missing field from incoming client message: is_service_message");
			}
			return new WebSocketMessage(
				sequenceNumber.Value,
				content,
				clientKey,
				timestamp.Value,
				isServiceMessage.Value
			);
		}

		internal void ToCbor(CborWriter writer)
		{
			writer.WriteStartMap(5); // Assuming there are 5 fields to write

			// Write "sequence_num"
			writer.WriteTextString("sequence_num");
			writer.WriteUInt64(this.SequenceNumber);

			// Write "content"
			writer.WriteTextString("content");
			writer.WriteByteString(this.Content);

			// Write "client_key"
			writer.WriteTextString("client_key");
			this.ClientKey.ToCbor(writer);

			// Write "timestamp"
			writer.WriteTextString("timestamp");
			writer.WriteUInt64(this.Timestamp);

			// Write "is_service_message"
			writer.WriteTextString("is_service_message");
			writer.WriteBoolean(this.IsServiceMessage);

			writer.WriteEndMap();
		}
	}
}
