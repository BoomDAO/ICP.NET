using EdjCase.ICP.Candid.Crypto;
using EdjCase.ICP.Candid.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EdjCase.ICP.Agent.Models
{
	public class Delegation : IRepresentationIndependentHashItem, IHashable
	{
		[Dahomey.Cbor.Attributes.CborProperty(Properties.PUBLIC_KEY)]
		public byte[] PublicKey { get; }

		[Dahomey.Cbor.Attributes.CborProperty(Properties.EXPIRATION)]
		public ICTimestamp Expiration { get; }

		[Dahomey.Cbor.Attributes.CborIgnoreIfDefault]
		[Dahomey.Cbor.Attributes.CborProperty(Properties.TARGETS)]
		public List<Principal>? Targets { get; }

		[Dahomey.Cbor.Attributes.CborIgnoreIfDefault]
		[Dahomey.Cbor.Attributes.CborProperty(Properties.SENDERS)]
		public List<Principal>? Senders { get; }

		public Delegation(byte[] publicKey, ICTimestamp expiration, List<Principal>? targets = null, List<Principal>? senders = null)
		{
			this.PublicKey = publicKey ?? throw new ArgumentNullException(nameof(publicKey));
			this.Expiration = expiration;
			this.Targets = targets;
			this.Senders = senders;
		}

		public Dictionary<string, IHashable> BuildHashableItem()
		{
			var obj = new Dictionary<string, IHashable>
			{
				{Properties.PUBLIC_KEY, this.PublicKey.ToHashable()},
				{Properties.EXPIRATION, this.Expiration},
			};

			if (this.Targets?.Any() == true)
			{
				obj.Add(Properties.TARGETS, this.Targets.ToHashable());
			}
			if (this.Senders?.Any() == true)
			{
				obj.Add(Properties.SENDERS, this.Senders.ToHashable());
			}


			return obj;
		}

		public byte[] ComputeHash(IHashFunction hashFunction)
		{
			return this.BuildHashableItem()
				.ToHashable()
				.ComputeHash(hashFunction);
		}

		/// <summary>
		/// Checks to see if the expiration is on or after the specified time
		/// </summary>
		/// <param name="timestamp">The timestamp to compare the expiration against</param>
		/// <returns>True if the delegation is NOT expired, otherwise false</returns>
		public bool IsExpirationValid(ICTimestamp timestamp)
		{
			return this.Expiration >= timestamp;
		}

		public class Properties
		{
			public const string PUBLIC_KEY = "pubkey";
			public const string EXPIRATION = "expiration";
			public const string TARGETS = "targets";
			public const string SENDERS = "senders";
		}
	}
}
