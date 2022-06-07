using EdjCase.ICP.Agent.Identity;
using EdjCase.ICP.Candid.Models;
using System;
using System.Collections.Generic;

namespace EdjCase.ICP.Agent.Auth
{
	public class SignedContent : IRepresentationIndependentHashItem
	{
		[Dahomey.Cbor.Attributes.CborProperty("content")]
		public Dictionary<string, IHashable> Content { get; }

		[Dahomey.Cbor.Attributes.CborIgnoreIfDefault]
		[Dahomey.Cbor.Attributes.CborProperty("sender_pubkey")]
		public byte[]? SenderPublicKey { get; }

		[Dahomey.Cbor.Attributes.CborIgnoreIfDefault]
		[Dahomey.Cbor.Attributes.CborProperty("sender_delegation")]
		public List<SignedDelegation>? SenderDelegations { get; }

		[Dahomey.Cbor.Attributes.CborIgnoreIfDefault]
		[Dahomey.Cbor.Attributes.CborProperty("sender_sig")]
		public Signature? SenderSignature { get; }

		public SignedContent(Dictionary<string, IHashable> content, byte[]? senderPublicKey, List<SignedDelegation>? delegations, Signature? senderSignature)
		{
			this.Content = content ?? throw new ArgumentNullException(nameof(content));
			this.SenderPublicKey = senderPublicKey;
			this.SenderDelegations = delegations;
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
				properties.Add("sender_pubkey", this.SenderPublicKey.ToHashable());
			}
			if (this.SenderSignature != null)
			{
				properties.Add("sender_sig", this.SenderSignature);
			}
			if (this.SenderDelegations != null)
			{
				properties.Add("sender_delegation", this.SenderDelegations.ToHashable());
			}
			return properties;
		}
	}
}
