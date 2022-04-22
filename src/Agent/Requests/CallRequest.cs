using ICP.Common.Candid;
using ICP.Common.Models;
using System;
using System.Collections.Generic;

namespace ICP.Agent.Requests
{
	public class CallRequest : IRepresentationIndependentHashItem
	{
		/// <summary>
		/// The principal of the canister to call
		/// </summary>
		[Dahomey.Cbor.Attributes.CborProperty(Properties.CANISTER_ID)]
		public PrincipalId CanisterId { get; }
		/// <summary>
		/// Name of the canister method to call
		/// </summary>
		[Dahomey.Cbor.Attributes.CborProperty(Properties.METHOD_NAME)]
		public string Method { get; }
		/// <summary>
		/// Argument to pass to the canister method
		/// </summary>
		[Dahomey.Cbor.Attributes.CborProperty(Properties.ARG)]
		public CandidArg EncodedArgument { get; }
		/// <summary>
		/// The user who issued the request
		/// </summary>
		[Dahomey.Cbor.Attributes.CborProperty(Properties.SENDER)]
		public PrincipalId Sender { get; }
		/// <summary>
		/// An upper limit on the validity of the request, expressed in nanoseconds since 1970-01-01
		/// </summary>
		[Dahomey.Cbor.Attributes.CborProperty(Properties.INGRESS_EXPIRY)]
		public ICTimestamp IngressExpiry { get; }
		/// <summary>
		/// Optional. Arbitrary user-provided data, typically randomly generated. This can be used to create distinct requests with otherwise identical fields.
		/// </summary>
		[Dahomey.Cbor.Attributes.CborProperty(Properties.NONCE)]
		public byte[]? Nonce { get; }

		public CallRequest(PrincipalId canisterId, string method, CandidArg encodedArgument, PrincipalId sender, ICTimestamp ingressExpiry)
		{
			this.CanisterId = canisterId ?? throw new ArgumentNullException(nameof(canisterId));
			this.Method = method ?? throw new ArgumentNullException(nameof(method));
			this.EncodedArgument = encodedArgument ?? throw new ArgumentNullException(nameof(encodedArgument));
			this.Sender = sender ?? throw new ArgumentNullException(nameof(sender));
			this.IngressExpiry = ingressExpiry ?? throw new ArgumentNullException(nameof(ingressExpiry));
		}

		public Dictionary<string, IHashable> BuildHashableItem()
		{
			var properties = new Dictionary<string, IHashable>
			{
				{Properties.REQUEST_TYPE, "call".ToHashable()},
				{Properties.CANISTER_ID, this.CanisterId},
				{Properties.METHOD_NAME, this.Method.ToHashable()},
				{Properties.ARG, this.EncodedArgument},
				{Properties.SENDER, this.Sender},
				{Properties.INGRESS_EXPIRY, this.IngressExpiry},
			};
			if (this.Nonce != null)
			{
				properties.Add(Properties.NONCE, this.Nonce.ToHashable());
			}
			return properties;
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