using Candid;
using ICP.Common.Crypto;
using ICP.Common.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Formats.Cbor;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICP.Agent.Auth
{
	public class SignedContent : IRepresentationIndependentHashItem
	{
		public Dictionary<string, IHashable> Content { get; }
		public DerEncodedPublicKey? SenderPublicKey { get; }
		public Signature? SenderSignature { get; }
		// TODO `? sender_delegation: [*4 signed-delegation]`

		public SignedContent(Dictionary<string, IHashable> content, DerEncodedPublicKey? senderPublicKey, Signature? senderSignature)
		{
			this.Content = content ?? throw new ArgumentNullException(nameof(content));
			this.SenderPublicKey = senderPublicKey;
			this.SenderSignature = senderSignature;
		}

		public Dictionary<string, IHashable> BuildHashableItem()
		{
			var properties = new Dictionary<string, IHashable>
			{
				{"content", this.Content.ToHashable()}
			};
			if (this.SenderPublicKey != null)
			{
				properties.Add("sender_pubkey", this.SenderPublicKey);
			}
			if (this.SenderSignature != null)
			{
				properties.Add("sender_sig", this.SenderSignature);
			}
			return properties;
		}
	}
}
