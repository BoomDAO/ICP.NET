using EdjCase.ICP.Candid.Models;
using System;
using System.Collections.Generic;
using System.Formats.Cbor;

namespace EdjCase.ICP.Agent.Requests
{
	/// <summary>
	/// A model for making call requests to a canister
	/// </summary>
	public class CallRequest : IRepresentationIndependentHashItem
	{
		/// <summary>
		/// The principal of the canister to call
		/// </summary>
		public Principal CanisterId { get; }
		/// <summary>
		/// Name of the canister method to call
		/// </summary>
		public string Method { get; }
		/// <summary>
		/// Argument to pass to the canister method
		/// </summary>
		public CandidArg Arg { get; }
		/// <summary>
		/// The user who issued the request
		/// </summary>
		public Principal Sender { get; }
		/// <summary>
		/// An upper limit on the validity of the request, expressed in nanoseconds since 1970-01-01
		/// </summary>
		public ICTimestamp IngressExpiry { get; }
		/// <summary>
		/// Optional. Arbitrary user-provided data, typically randomly generated. This can be used to create distinct requests with otherwise identical fields.
		/// </summary>
		public byte[]? Nonce { get; }

		/// <param name="canisterId">The principal of the canister to call</param>
		/// <param name="method">Name of the canister method to call</param>
		/// <param name="arg">Argument to pass to the canister method</param>
		/// <param name="sender">The user who issued the request</param>
		/// <param name="ingressExpiry">An upper limit on the validity of the request, expressed in nanoseconds since 1970-01-01</param>
		/// <param name="nonce">Optional. Arbitrary user-provided data, typically randomly generated. This can be used to create distinct requests with otherwise identical fields.</param>
		public CallRequest(
			Principal canisterId,
			string method,
			CandidArg arg,
			Principal sender,
			ICTimestamp ingressExpiry,
			byte[]? nonce = null
		)
		{
			this.CanisterId = canisterId ?? throw new ArgumentNullException(nameof(canisterId));
			this.Method = method ?? throw new ArgumentNullException(nameof(method));
			this.Arg = arg ?? throw new ArgumentNullException(nameof(arg));
			this.Sender = sender ?? throw new ArgumentNullException(nameof(sender));
			this.IngressExpiry = ingressExpiry ?? throw new ArgumentNullException(nameof(ingressExpiry));
			this.Nonce = nonce;
		}

		/// <inheritdoc />
		public Dictionary<string, IHashable> BuildHashableItem()
		{
			var properties = new Dictionary<string, IHashable>
			{
				{Properties.REQUEST_TYPE, "call".ToHashable()},
				{Properties.CANISTER_ID, this.CanisterId},
				{Properties.METHOD_NAME, this.Method.ToHashable()},
				{Properties.ARG, this.Arg},
				{Properties.SENDER, this.Sender},
				{Properties.INGRESS_EXPIRY, this.IngressExpiry},
			};
			if (this.Nonce != null)
			{
				properties.Add(Properties.NONCE, this.Nonce.ToHashable());
			}
			return properties;
		}

		internal void WriteCbor(CborWriter writer)
		{
			writer.WriteStartMap(null);

			writer.WriteTextString(Properties.CANISTER_ID);
			writer.WritePrincipal(this.CanisterId);

			writer.WriteTextString(Properties.METHOD_NAME);
			writer.WriteTextString(this.Method);

			writer.WriteTextString(Properties.ARG);
			writer.WriteByteString(this.Arg.Encode());

			writer.WriteTextString(Properties.SENDER);
			writer.WritePrincipal(this.Sender);

			writer.WriteTextString(Properties.INGRESS_EXPIRY);
			writer.WriteTimestamp(this.IngressExpiry);

			if (this.Nonce != null)
			{
				writer.WriteTextString(Properties.NONCE);
				writer.WriteByteString(this.Nonce);
			}

			writer.WriteEndMap();
		}

		private static class Properties
		{
			public const string REQUEST_TYPE = "request_type";
			public const string CANISTER_ID = "canister_id";
			public const string METHOD_NAME = "method_name";
			public const string ARG = "arg";
			public const string SENDER = "sender";
			public const string INGRESS_EXPIRY = "ingress_expiry";
			public const string NONCE = "nonce";
		}
	}
}