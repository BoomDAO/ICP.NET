using System;
using System.Formats.Cbor;

namespace EdjCase.ICP.WebSockets.Models
{
	internal class ClientIncomingMessage
	{
		public string Key { get; }
		public byte[] Content { get; }
		public byte[] Cert { get; }
		public byte[] Tree { get; }

		public ClientIncomingMessage(string key, byte[] content, byte[] cert, byte[] tree)
		{
			this.Key = key ?? throw new ArgumentNullException(nameof(key));
			this.Content = content ?? throw new ArgumentNullException(nameof(content));
			this.Cert = cert ?? throw new ArgumentNullException(nameof(cert));
			this.Tree = tree ?? throw new ArgumentNullException(nameof(tree));
		}

		public static ClientIncomingMessage FromCbor(CborReader reader)
		{
			string? key = null;
			byte[]? content = null;
			byte[]? cert = null;
			byte[]? tree = null;

			reader.ReadStartMap();
			while (reader.PeekState() != CborReaderState.EndMap)
			{
				switch (reader.ReadTextString())
				{
					case "key":
						key = reader.ReadTextString();
						break;
					case "content":
						content = reader.ReadByteString();
						break;
					case "cert":
						cert = reader.ReadByteString();
						break;
					case "tree":
						tree = reader.ReadByteString();
						break;
				}
			}
			reader.ReadEndMap();
			if (key == null)
			{
				throw new CborContentException("Missing field from incoming client message: key");
			}
			if (content == null)
			{
				throw new CborContentException("Missing field from incoming client message: content");
			}
			if (cert == null)
			{
				throw new CborContentException("Missing field from incoming client message: cert");
			}
			if (tree == null)
			{
				throw new CborContentException("Missing field from incoming client message: tree");
			}
			return new ClientIncomingMessage(
				key,
				content,
				cert,
				tree
			);
		}

		internal void ToCbor(CborWriter writer)
		{
			writer.WriteStartMap(4); // There are 4 fields to write

			// Write "key"
			writer.WriteTextString("key");
			writer.WriteTextString(this.Key);

			// Write "content"
			writer.WriteTextString("content");
			writer.WriteByteString(this.Content);

			// Write "cert"
			writer.WriteTextString("cert");
			writer.WriteByteString(this.Cert);

			// Write "tree"
			writer.WriteTextString("tree");
			writer.WriteByteString(this.Tree);

			writer.WriteEndMap();
		}
	}
}
