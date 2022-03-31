using ICP.Common.Models;
using System;
using System.Collections.Generic;

namespace ICP.Agent.Auth
{
	public class SignedContent : IRepresentationIndependentHashItem
	{
		[Dahomey.Cbor.Attributes.CborProperty("content")]
		public Dictionary<string, IHashable> Content { get; }
		[Dahomey.Cbor.Attributes.CborProperty("sender_pubkey")]
		public DerEncodedPublicKey? SenderPublicKey { get; }
		[Dahomey.Cbor.Attributes.CborProperty("sender_sig")]
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
