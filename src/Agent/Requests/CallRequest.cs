using EdjCase.ICP.Candid.Models;
using System;
using System.Collections.Generic;

namespace EdjCase.ICP.Agent.Requests
{
	public class CallRequest : IRepresentationIndependentHashItem
	{
		/// <summary>
		/// The principal of the canister to call
		/// </summary>
		[Dahomey.Cbor.Attributes.CborProperty(Properties.CANISTER_ID)]
		public Principal CanisterId { get; }
		/// <summary>
		/// Name of the canister method to call
		/// </summary>
		[Dahomey.Cbor.Attributes.CborProperty(Properties.METHOD_NAME)]
		public string Method { get; }
		/// <summary>
		/// Argument to pass to the canister method
		/// </summary>
		[Dahomey.Cbor.Attributes.CborProperty(Properties.ARG)]
		public CandidArg Arg { get; }
		/// <summary>
		/// The user who issued the request
		/// </summary>
		[Dahomey.Cbor.Attributes.CborProperty(Properties.SENDER)]
		public Principal Sender { get; }
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

		public CallRequest(
			Principal canisterId,
			string method,
			CandidArg encodedArgument,
			Principal sender,
			ICTimestamp ingressExpiry,
			byte[]? nonce = null
		)
		{
			this.CanisterId = canisterId ?? throw new ArgumentNullException(nameof(canisterId));
			this.Method = method ?? throw new ArgumentNullException(nameof(method));
			this.Arg = encodedArgument ?? throw new ArgumentNullException(nameof(encodedArgument));
			this.Sender = sender ?? throw new ArgumentNullException(nameof(sender));
			this.IngressExpiry = ingressExpiry ?? throw new ArgumentNullException(nameof(ingressExpiry));
			this.Nonce = nonce;
		}

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

	public class CallRejectedException : Exception
	{
		public UnboundedUInt RejectCode { get; }
		public string RejectMessage { get; }
		public string? ErrorCode { get; }
		public CallRejectedException(UnboundedUInt rejectCode, string rejectMessage, string? errorCode)
		{
			this.RejectCode = rejectCode;
			this.RejectMessage = rejectMessage;
			this.ErrorCode = errorCode;
		}
	}
}