using EdjCase.ICP.Candid.Crypto;
using EdjCase.ICP.Candid.Models;
using EdjCase.ICP.Agent.Keys;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EdjCase.ICP.Agent.Models;

namespace EdjCase.ICP.Agent.Identities
{
	/// <summary>
	/// An identity type that has signing capabilities
	/// </summary>
	public abstract class SigningIdentityBase : IIdentity
	{
		/// <summary>
		/// Returns the public key of the identity
		/// </summary>
		/// <returns>Public key of the identity</returns>
		public abstract IPublicKey GetPublicKey();

		/// <summary>
		/// Gets the signed delegations for the identity.
		/// Delegations will exist if the identity is a delegated identity
		/// instead of having the raw keys. This is used in Internet Identity
		/// </summary>
		/// <returns>The signed delegations, otherwise an empty list</returns>
		public virtual List<SignedDelegation> GetSenderDelegations()
		{
			return new List<SignedDelegation>();
		}

		/// <summary>
		/// Signs the specified bytes with the identity key
		/// </summary>
		/// <param name="data">The byte data to sign</param>
		/// <returns>The signature bytes of the specified data bytes</returns>
		public abstract byte[] Sign(byte[] data);

		/// <inheritdoc/>
		public Principal GetPrincipal()
		{
			IPublicKey publicKey = this.GetPublicKey();
			return Principal.SelfAuthenticating(publicKey.GetRawBytes());
		}

		/// <inheritdoc/>
		public SignedContent SignContent(Dictionary<string, IHashable> content)
		{
			IPublicKey senderPublicKey = this.GetPublicKey();
			var sha256 = SHA256HashFunction.Create();
			byte[] contentHash = content.ToHashable().ComputeHash(sha256);
			byte[] domainSeparator = Encoding.UTF8.GetBytes("\x0Aic-request");
			byte[] senderSignature = this.Sign(domainSeparator.Concat(contentHash).ToArray());
			List<SignedDelegation>? senderDelegations = this.GetSenderDelegations();
			return new SignedContent(content, senderPublicKey.GetRawBytes(), senderDelegations, senderSignature);
		}
	}
}
