using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EdjCase.ICP.Candid.Crypto;

namespace EdjCase.ICP.Candid.Models
{
	public class RawPublicKey : IHashable, IPublicKey
	{
		public byte[] Value { get; }

		public RawPublicKey(byte[] value)
		{
			this.Value = value ?? throw new ArgumentNullException(nameof(value));
		}

		public byte[] ComputeHash(IHashFunction hashFunction)
		{
			return hashFunction.ComputeHash(this.Value);
		}

		public DerEncodedPublicKey GetDerEncodedBytes()
		{
			// TODO
			//// The Bit String header needs to include the unused bit count byte in its length
			//var bitStringHeaderLength = 2 + encodeLenBytes(this.Value.Length + 1);
			//var len = oid.byteLength + bitStringHeaderLength + this.Value.Length;
			//int offset = 0;
			//var buf = new byte[1 + encodeLenBytes(len) + len];
			//// Sequence
			//buf[offset++] = 0x30;
			//// Sequence Length
			//offset += encodeLen(buf, offset, len);

			//// OID
			//buf.set(oid, offset);
			//offset += oid.byteLength;

			//// Bit String Header
			//buf[offset++] = 0x03;
			//offset += encodeLen(buf, offset, payload.byteLength + 1);
			//// 0 padding
			//buf[offset++] = 0x00;
			//buf.set(new Uint8Array(payload), offset);

			return new DerEncodedPublicKey(new byte[32]);
		}
	}
}
