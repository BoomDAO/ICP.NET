using Dahomey.Cbor.Attributes;
using EdjCase.ICP.Candid.Models;
using System;
using System.Collections.Generic;

namespace EdjCase.ICP.Agent.Models
{
	public class SignedContent : IRepresentationIndependentHashItem
	{
		[CborProperty(Properties.CONTENT)]
		public Dictionary<string, IHashable> Content { get; }

		[CborIgnoreIfDefault]
		[CborProperty(Properties.SENDER_PUBLIC_KEY)]
		public byte[]? SenderPublicKey { get; }

		[CborIgnoreIfDefault]
		[CborProperty(Properties.SENDER_DELEGATION)]
		public List<SignedDelegation>? SenderDelegations { get; }

		[CborIgnoreIfDefault]
		[CborProperty(Properties.SENDER_SIGNATURE)]
		public byte[]? SenderSignature { get; }

		public SignedContent(
			Dictionary<string, IHashable> content,
			byte[]? senderPublicKey,
			List<SignedDelegation>? delegations,
			byte[]? senderSignature
		)
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
				{Properties.CONTENT, this.Content.ToHashable()}
			};
			if (this.SenderPublicKey != null)
			{
				properties.Add(Properties.SENDER_PUBLIC_KEY, this.SenderPublicKey.ToHashable());
			}
			if (this.SenderSignature != null)
			{
				properties.Add(Properties.SENDER_SIGNATURE, this.SenderSignature.ToHashable());
			}
			if (this.SenderDelegations != null)
			{
				properties.Add(Properties.SENDER_DELEGATION, this.SenderDelegations.ToHashable());
			}
			return properties;
		}

		internal class Properties
		{
			public const string CONTENT = "content";
			public const string SENDER_PUBLIC_KEY = "sender_pubkey";
			public const string SENDER_SIGNATURE = "sender_sig";
			public const string SENDER_DELEGATION = "sender_delegation";
		}
	}
}
