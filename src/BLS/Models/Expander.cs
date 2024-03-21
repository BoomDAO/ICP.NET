using System;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;

namespace EdjCase.ICP.BLS.Models
{
	internal class Expander
	{
		public byte[] Dst { get; }
		public byte[] B0 { get; }
		public byte[] Bi { get; private set; }
		public byte I { get; private set; }
		public int BOffs { get; private set; }
		public int Remain { get; private set; }

		private static readonly SHA256 sHA256 = SHA256.Create();

		public Expander(byte[] dst, byte[] b0, byte[] bi, byte i, int bOffs, int remain)
		{
			this.Dst = dst;
			this.B0 = b0;
			this.Bi = bi;
			this.I = i;
			this.BOffs = bOffs;
			this.Remain = remain;
		}

		public static Expander Create(byte[] message, byte[] dst, int byteLength, int hashSize)
		{
			int ell = (byteLength + hashSize - 1) / hashSize;
			if (ell > 255)
			{
				throw new ArgumentException("Invalid ExpandMsgXmd usage: ell > 255");
			}
			if (dst.Length > 255)
			{
				throw new NotImplementedException();
			}

			byte[] b0 = sHA256.ComputeHash(
				new byte[64]
				.Concat(message)
				.Concat(ToU16Bytes(byteLength))
				.Concat(new byte[] { 0 })
				.Concat(dst)
				.Concat(ToU8Bytes(dst.Length))
				.ToArray()
			);
			byte[] bi = sHA256.ComputeHash(
				b0
				.Concat(new byte[] { 1 })
				.Concat(dst)
				.Concat(ToU8Bytes(dst.Length))
				.ToArray()
			);

			return new Expander(dst, b0, bi, 2, 0, byteLength);
		}

		private static byte[] ToU16Bytes(int value)
		{
			if (value > ushort.MaxValue)
			{
				throw new ArgumentException("Max value is " + ushort.MaxValue);
			}
			byte[] bytes = BitConverter.GetBytes((ushort)value);

			if (BitConverter.IsLittleEndian)
			{
				Array.Reverse(bytes); // Convert to big-endian if system is little-endian
			}
			return bytes;
		}
		private static byte[] ToU8Bytes(int value)
		{
			if (value > byte.MaxValue)
			{
				throw new ArgumentException("Max value is " + byte.MaxValue);
			}
			return new byte[] { (byte)value };
		}

		public byte[] ReadInto(int outputLength, int hashSize)
		{
			byte[] output = new byte[outputLength];
			int readLen = Math.Min(this.Remain, outputLength);
			int offs = 0;
			while (offs < readLen)
			{
				int bOffs = this.BOffs;
				int copyLen = hashSize - bOffs;
				if (copyLen > 0)
				{
					copyLen = Math.Min(readLen - offs, copyLen);
					Array.Copy(this.Bi, bOffs, output, offs, copyLen);
					offs += copyLen;
					this.BOffs = bOffs + copyLen;
				}
				else
				{
					byte[] bPrevXor = new byte[this.B0.Length];
					Array.Copy(this.B0, bPrevXor, this.B0.Length);
					for (int j = 0; j < hashSize; j++)
					{
						bPrevXor[j] ^= this.Bi[j];
					}
					this.Bi = sHA256.ComputeHash(
						bPrevXor
						.Concat(new[] { this.I })
						.Concat(this.Dst)
						.Concat(ToU8Bytes(this.Dst.Length))
						.ToArray()
					);
					this.BOffs = 0;
					this.I += 1;
				}
			}
			this.Remain -= readLen;
			return output;
		}
	}
}
