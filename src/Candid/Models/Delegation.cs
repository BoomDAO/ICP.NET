using EdjCase.ICP.Candid.Crypto;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdjCase.ICP.Candid.Models
{
	public class Delegation : IRepresentationIndependentHashItem, IHashable
	{
		public DerEncodedPublicKey PublicKey { get; }
		public ICTimestamp Expiration { get; }
		public List<Principal>? Targets { get; }

		public Delegation(DerEncodedPublicKey publicKey, ICTimestamp expiration, List<Principal>? targets)
		{
			this.PublicKey = publicKey ?? throw new ArgumentNullException(nameof(publicKey));
			this.Expiration = expiration;
			this.Targets = targets;
		}

		public Dictionary<string, IHashable> BuildHashableItem()
		{
			var obj = new Dictionary<string, IHashable>
			{
				{Properties.PUBLIC_KEY, this.PublicKey},
				{Properties.EXPIRATION, this.Expiration},
			};

			if (this.Targets?.Any() == true)
			{
				obj.Add(Properties.TARGETS, this.Targets.ToHashable());
			}


			return obj;
		}

		public byte[] ComputeHash(IHashFunction hashFunction)
		{
			return this.BuildHashableItem()
				.ToHashable()
				.ComputeHash(hashFunction);
		}

		public class Properties
		{
			public const string PUBLIC_KEY = "pubkey";
			public const string EXPIRATION = "expiration";
			public const string TARGETS = "targets";
		}
	}
}
