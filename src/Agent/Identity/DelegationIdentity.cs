using EdjCase.ICP.Agent;
using EdjCase.ICP.Agent.Auth;
using EdjCase.ICP.Candid.Crypto;
using EdjCase.ICP.Candid.Models;
using EdjCase.ICP.Candid.Models.Keys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdjCase.ICP.Agent.Identity
{
	public class DelegationIdentity : SigningIdentityBase
	{
		public SigningIdentityBase Identity { get; }
		public DelegationChain Chain { get; }

		public DelegationIdentity(SigningIdentityBase identity, DelegationChain chain)
		{
			this.Identity = identity;
			this.Chain = chain;
		}

		public override IPublicKey GetPublicKey()
		{
			return this.Chain.PublicKey;
		}

		public override Signature Sign(byte[] blob)
		{
			return this.Identity.Sign(blob);
		}

		public override List<SignedDelegation>? GetSenderDelegations()
		{
			return this.Chain.Delegations;
		}
	}

	public class DelegationChain
	{
		public IPublicKey PublicKey { get; }
		public List<SignedDelegation> Delegations { get; }
		public DelegationChain(IPublicKey publicKey, List<SignedDelegation> delegations)
		{
			this.PublicKey = publicKey;
			this.Delegations = delegations;
		}

		public static DelegationChain Create(
			SigningIdentityBase identity,
			IPublicKey publicKey,
			ICTimestamp expiration,
			DelegationChain? previousChain = null,
			List<Principal>? principalIds = null)
		{
			SignedDelegation signedDelegation = SignedDelegation.Create(identity, publicKey, expiration, principalIds);
			List<SignedDelegation> delegations = previousChain?.Delegations ?? new List<SignedDelegation>();
			delegations.Add(signedDelegation);
			return new DelegationChain(publicKey, delegations);
		}
	}

	public class SignedDelegation : IHashable
	{
		[Dahomey.Cbor.Attributes.CborProperty("delegation")]
		public Delegation Delegation { get; }
		[Dahomey.Cbor.Attributes.CborProperty("signature")]
		public Signature Signature { get; }

		public SignedDelegation(Delegation delegation, Signature signature)
		{
			this.Delegation = delegation ?? throw new ArgumentNullException(nameof(delegation));
			this.Signature = signature ?? throw new ArgumentNullException(nameof(signature));
		}

		public static SignedDelegation Create(
			SigningIdentityBase fromIdentity,
			IPublicKey publicKey,
			ICTimestamp expiration,
			List<Principal>? targets = null)
		{
			var delegation = new Delegation(publicKey.GetRawBytes(), expiration, targets);
			Dictionary<string, IHashable> hashable = delegation.BuildHashableItem();
			// The signature is calculated by signing the concatenation of the domain separator
			// and the message.
			var hashFunction = SHA256HashFunction.Create();

			byte[] delegationHashDigest = new HashableObject(hashable).ComputeHash(hashFunction);
			byte[] challenge = Encoding.UTF8.GetBytes("\x1Aic-request-auth-delegation") // Prefix with domain seperator
				.Concat(delegationHashDigest)
				.ToArray();

			Signature signature = fromIdentity.Sign(challenge); // Sign the domain sep + delegation hash digest
			return new SignedDelegation(delegation, signature);
		}

		public byte[] ComputeHash(IHashFunction hashFunction)
		{
			return new Dictionary<string, IHashable>
			{
				{ "delegation", this.Delegation },
				{ "signature", this.Signature }
			}
				.ToHashable()
				.ComputeHash(hashFunction);
		}
	}

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

		public class Properties
		{
			public const string PUBLIC_KEY = "pubkey";
			public const string EXPIRATION = "expiration";
			public const string TARGETS = "targets";
			public const string SENDERS = "senders";
		}
	}
}
