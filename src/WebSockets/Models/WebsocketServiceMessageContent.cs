using EdjCase.ICP.Candid.Mapping;
using System;
using System.Collections.Generic;
using System.Formats.Cbor;
using System.Text;

namespace EdjCase.ICP.WebSockets.Models
{
	internal enum ServiceMessageTag
	{
		OpenMessage,
		AckMessage,
		KeepAliveMessage
	}

	[Variant]
	internal class ServiceMessage
	{

		[VariantTagProperty]
		public ServiceMessageTag Tag { get; set; }

		[VariantValueProperty]
		public object? Value { get; set; }

		public ServiceMessage(ServiceMessageTag tag, object? value)
		{
			this.Tag = tag;
			this.Value = value;
		}

		protected ServiceMessage()
		{
		}


		public OpenMessage AsOpenMessage()
		{
			this.ValidateTag(ServiceMessageTag.OpenMessage);
			return (OpenMessage)this.Value!;
		}

		public AckMessage AsAckMessage()
		{
			this.ValidateTag(ServiceMessageTag.AckMessage);
			return (AckMessage)this.Value!;
		}

		public KeepAliveMessage AsKeepAliveMessage()
		{
			this.ValidateTag(ServiceMessageTag.KeepAliveMessage);
			return (KeepAliveMessage)this.Value!;
		}

		private void ValidateTag(ServiceMessageTag tag)
		{
			if (!this.Tag.Equals(tag))
			{
				throw new InvalidOperationException($"Cannot cast '{this.Tag}' to type '{tag}'");
			}
		}
	}

	internal class OpenMessage
	{
		[CandidName("client_key")]
		public ClientKey? ClientKey { get; set; }

		internal void ToCbor(CborWriter writer)
		{
			writer.WriteStartMap(1);

			// Write "client_key"
			writer.WriteTextString("client_key");
			this.ClientKey!.ToCbor(writer);

			writer.WriteEndMap();
		}
	}

	internal class AckMessage
	{
		[CandidName("last_incoming_sequence_num")]
		public ulong LastIncomingSequenceNumber { get; set; }
	}

	internal class KeepAliveMessage
	{
		[CandidName("last_incoming_sequence_num")]
		public ulong LastIncomingSequenceNumber { get; set; }
	}
}
