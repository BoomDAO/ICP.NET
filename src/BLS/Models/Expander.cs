using System;
using System.Diagnostics;
using System.Linq;

namespace EdjCase.ICP.BLS.Models
{
	public class Expander
	{
		public byte[] Dst { get; }
		public byte[] B0 { get; }
		public byte[] Bi { get; }
		public int I { get; }
		public int BOffs { get; }
		public int Remain { get; }

		public Expander(byte[] dst, byte[] b0, byte[] bi, int i, int bOffs, int remain)
		{
			this.Dst = dst;
			this.B0 = b0;
			this.Bi = bi;
			this.I = i;
			this.BOffs = bOffs;
			this.Remain = remain;
		}

		public static Expander Create(byte[] message, byte[] dst, int byteLength)
		{
			const int hashSize = 48;
			const int ell = (byteLength + hashSize - 1) / hashSize;
			if (ell > 255)
			{
				throw new ArgumentException("Invalid ExpandMsgXmd usage: ell > 255");
			}
			var dst = ExpandMsgDst.ProcessXmd(dst);

			byte[] b0 = SHA256.Create().ComputeHash(
				message
				.Concat(byteLength.ToBytes())
				.Concat(new byte[] { 0 })
				.Concat(dst)
				.Concat(dst.Length.toBytes())
			);
			byte[] bi = SHA256.Create().ComputeHash(
				b0
				.Concat(new byte[] { 1 })
				.Concat(dst)
				.Concat(dst.Length.toBytes())
			);

			return new Expander(dst, b0, bi, 2, 0, byteLength);
		}

		public byte[] ReadInto()
		{
			int outputLength = 64;
			int readLen = Math.Min(this.Remain, outputLength);
			int offs = 0;
			int hashSize = 32;
			while (offs < readLen)
			{
				int bOffs = this.BOffs;
				int copyLen = hashSize - bOffs;
				if (copyLen > 0)
				{
					copyLen = Math.Min(readLen - offs, copyLen);
					Array.Copy(bi, bOffs, b0, offs, copyLen);
					offs += copyLen;
					bOffs += copyLen;
				}
				else
				{
					byte[] bPrevXor = new byte[b0.Length];
					Array.Copy(b0, bPrevXor, b0.Length);
					for (int j = 0; j < hashSize; j++)
					{
						bPrevXor[j] ^= bi[j];
					}
					this.BI = SHA256.Create().ComputeHash(
						bPrevXor
						.Concat(new byte[] { i })
						.Concat(dst)
						.Concat(dst.Length.toBytes())
					);
					this.BOffs = 0;
					remain -= readLen;
				}
			}
			// 	let read_len = self.remain.min(output.len());
			// let mut offs = 0;
			// let hash_size = H::OutputSize::to_usize();
			// while offs < read_len {
			//     let b_offs = self.b_offs;
			//     let mut copy_len = hash_size - b_offs;
			//     if copy_len > 0 {
			//         copy_len = copy_len.min(read_len - offs);
			//         output[offs..(offs + copy_len)]
			//             .copy_from_slice(&self.b_i[b_offs..(b_offs + copy_len)]);
			//         offs += copy_len;
			//         self.b_offs = b_offs + copy_len;
			//     } else {
			//         let mut b_prev_xor = self.b_0.clone();
			//         for j in 0..hash_size {
			//             b_prev_xor[j] ^= self.b_i[j];
			//         }
			//         self.b_i = H::new()
			//             .chain(b_prev_xor)
			//             .chain([self.i as u8])
			//             .chain(self.dst.data())
			//             .chain([self.dst.len() as u8])
			//             .finalize();
			//         self.b_offs = 0;
			//         self.i += 1;
			//     }
			// }
			// self.remain -= read_len;
			// read_len
		}
	}
}
