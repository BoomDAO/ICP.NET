using Dahomey.Cbor.Attributes;
using EdjCase.ICP.Candid.Models;
using System;
using System.Collections.Generic;

namespace EdjCase.ICP.Agent.Models
{
	/// <summary>
	/// A model containing content and the signature information of it
	/// </summary>
	public class SignedContent : IRepresentationIndependentHashItem
	{
		/// <summary>
		/// The content that is signed in the form of key value pairs
		/// </summary>
		[CborProperty(Properties.CONTENT)]
		public Dictionary<string, IHashable> Content { get; }

		/// <summary>
		/// Public key used to authenticate this request, unless anonymous, then null
		/// </summary>
		[CborIgnoreIfDefault]
		[CborProperty(Properties.SENDER_PUBLIC_KEY)]
		public byte[]? SenderPublicKey { get; }

		/// <summary>
		/// Optional. A chain of delegations, starting with the one signed by sender_pubkey
		/// and ending with the one delegating to the key relating to sender_sig.
		/// </summary>
		[CborIgnoreIfDefault]
		[CborProperty(Properties.SENDER_DELEGATION)]
		public List<SignedDelegation>? SenderDelegations { get; }

		/// <summary>
		/// Signature to authenticate this request, unless anonymous, then null
		/// </summary>
		[CborIgnoreIfDefault]
		[CborProperty(Properties.SENDER_SIGNATURE)]
		public byte[]? SenderSignature { get; }

		/// <param name="content">The content that is signed in the form of key value pairs</param>
		/// <param name="senderPublicKey">Public key used to authenticate this request, unless anonymous, then null</param>
		/// <param name="delegations">Optional. A chain of delegations, starting with the one signed by sender_pubkey 
		/// and ending with the one delegating to the key relating to sender_sig.</param>
		/// <param name="senderSignature">Signature to authenticate this request, unless anonymous, then null</param>
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

		/// <inheritdoc />
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
