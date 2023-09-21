using EdjCase.ICP.Agent.Models;
using EdjCase.ICP.Candid.Models;
using Org.BouncyCastle.Crypto.Agreement;
using System;
using System.Collections.Generic;
using System.Formats.Cbor;
using System.Linq;
using System.Text;

namespace EdjCase.ICP.Agent.Responses
{
	/// <summary>
	/// Signature data from a replica node
	/// </summary>
	public class NodeSignature
	{
		/// <summary>
		/// Timestamp when the signature was created
		/// </summary>
		public ICTimestamp Timestamp { get; }
		/// <summary>
		/// The signature bytes
		/// </summary>
		public byte[] Signature { get; }
		/// <summary>
		/// The identity of the signer
		/// </summary>
		public Principal Identity { get; }

		/// <param name="timestamp">Timestamp when the signature was created</param>
		/// <param name="signature">The signature bytes</param>
		/// <param name="identity">The identity of the signer</param>
		public NodeSignature(ICTimestamp timestamp, byte[] signature, Principal identity)
		{
			this.Timestamp = timestamp ?? throw new ArgumentNullException(nameof(timestamp));
			this.Signature = signature ?? throw new ArgumentNullException(nameof(signature));
			this.Identity = identity ?? throw new ArgumentNullException(nameof(identity));
		}

		internal static NodeSignature ReadCbor(CborReader reader)
		{
			ICTimestamp? timestamp = null;
			byte[]? signature = null;
			Principal? identity = null;
			_ = reader.ReadStartMap();
			while (reader.PeekState() != CborReaderState.EndMap)
			{
				string name = reader.ReadTextString();
				switch (name)
				{
					case "timestamp":
						timestamp = ICTimestamp.FromNanoSeconds(CborUtil.ReadNat(reader));
						break;
					case "signature":
						signature = reader.ReadByteString();
						break;
					case "identity":
						identity = CborUtil.ReadPrincipal(reader);
						break;
					default:
						// Skip
						reader.SkipValue();
						break;
				}
			}
			reader.ReadEndMap();

			if (timestamp == null)
			{
				throw new Exception("Node signature is missing the timestamp field");
			}
			if (signature == null)
			{
				throw new Exception("Node signature is missing the signature field");
			}
			if (identity == null)
			{
				throw new Exception("Node signature is missing the identity field");
			}


			return new NodeSignature(
				timestamp,
				signature,
				identity
			);
		}
	}
}
