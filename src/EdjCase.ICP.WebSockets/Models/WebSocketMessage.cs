using System;
using System.Collections.Generic;
using System.Text;

namespace EdjCase.ICP.WebSockets.Models
{
	internal class WebSocketMessage
	{
		public ulong SequenceNumber { get; }
		public byte[] Content { get; }
		public ClientKey ClientKey { get; }
		public ulong Timestamp { get; }
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
	}
}
