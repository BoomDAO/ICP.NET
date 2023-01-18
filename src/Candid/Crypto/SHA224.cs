namespace EdjCase.ICP.Candid.Crypto
{
	internal class SHA224
	{
		public int HashSize { get; }
		public int BlockSize { get; }
		private readonly HashState state;

		public SHA224()
		{
			this.HashSize = 28;
			this.state = HashState.Create();
		}

		public byte[] GenerateDigest(byte[] data)
		{
			this.state.Reset();
			this.state.TransformBytes(data, 0, data.Length);
			this.state.Finish();
			return CryptoUtil.ConvertUIntsToBytesSwapOrder(this.state.State, 0, 7);
		}

	}

	internal class HashState
	{
		public HashBuffer Buffer { get; }
		public ulong ProcessedBytes;
		public uint[] State { get; }


		private static readonly uint[] k =
		{
			0x428a2f98, 0x71374491, 0xb5c0fbcf, 0xe9b5dba5, 0x3956c25b, 0x59f111f1, 0x923f82a4, 0xab1c5ed5,
			0xd807aa98, 0x12835b01, 0x243185be, 0x550c7dc3, 0x72be5d74, 0x80deb1fe, 0x9bdc06a7, 0xc19bf174,
			0xe49b69c1, 0xefbe4786, 0x0fc19dc6, 0x240ca1cc, 0x2de92c6f, 0x4a7484aa, 0x5cb0a9dc, 0x76f988da,
			0x983e5152, 0xa831c66d, 0xb00327c8, 0xbf597fc7, 0xc6e00bf3, 0xd5a79147, 0x06ca6351, 0x14292967,
			0x27b70a85, 0x2e1b2138, 0x4d2c6dfc, 0x53380d13, 0x650a7354, 0x766a0abb, 0x81c2c92e, 0x92722c85,
			0xa2bfe8a1, 0xa81a664b, 0xc24b8b70, 0xc76c51a3, 0xd192e819, 0xd6990624, 0xf40e3585, 0x106aa070,
			0x19a4c116, 0x1e376c08, 0x2748774c, 0x34b0bcb5, 0x391c0cb3, 0x4ed8aa4a, 0x5b9cca4f, 0x682e6ff3,
			0x748f82ee, 0x78a5636f, 0x84c87814, 0x8cc70208, 0x90befffa, 0xa4506ceb, 0xbef9a3f7, 0xc67178f2
		};

		public HashState(HashBuffer buffer, ulong processedBytes, uint[] state)
		{
			this.Buffer = buffer;
			this.ProcessedBytes = processedBytes;
			this.State = state;
		}

		public void TransformBytes(byte[] data, int index, int length)
		{
			if (!this.Buffer.IsEmpty)
			{
				if (this.Buffer.Feed(data, ref index, ref length, ref this.ProcessedBytes))
					this.TransformBuffer();
			}

			while (length >= this.Buffer.Length)
			{
				this.ProcessedBytes += (ulong)this.Buffer.Length;
				this.TransformBlock(data, index);
				index += this.Buffer.Length;
				length -= this.Buffer.Length;
			}

			if (length > 0)
				this.Buffer.Feed(data, ref index, ref length, ref this.ProcessedBytes);
		}

		public void Finish()
		{
			ulong bits = this.ProcessedBytes * 8;
			int padindex = (this.Buffer.Pos < 56) ? (56 - this.Buffer.Pos) : (120 - this.Buffer.Pos);

			byte[] pad = new byte[padindex + 8];
			pad[0] = 0x80;

			CryptoUtil.ConvertULongToBytesSwapOrder(bits, pad, padindex);
			padindex += 8;

			this.TransformBytes(pad, 0, padindex);
		}

		private void TransformBlock(byte[] data, int index)
		{
			uint[] uintData = new uint[64];
			CryptoUtil.ConvertBytesToUIntsSwapOrder(data, index, this.Buffer.Length, uintData, 0);

			uint A = this.State[0];
			uint B = this.State[1];
			uint C = this.State[2];
			uint D = this.State[3];
			uint E = this.State[4];
			uint F = this.State[5];
			uint G = this.State[6];
			uint H = this.State[7];

			for (int r = 16; r < 64; r++)
			{
				uint T = uintData[r - 2];
				uint T2 = uintData[r - 15];
				uintData[r] = (((T >> 17) | (T << 15)) ^ ((T >> 19) | (T << 13)) ^ (T >> 10)) + uintData[r - 7] +
					(((T2 >> 7) | (T2 << 25)) ^ ((T2 >> 18) | (T2 << 14)) ^ (T2 >> 3)) + uintData[r - 16];
			}

			for (int r = 0; r < 64; r++)
			{
				uint T = k[r] + uintData[r] + H + (((E >> 6) | (E << 26)) ^ ((E >> 11) | (E << 21)) ^ ((E >> 25) |
						 (E << 7))) + ((E & F) ^ (~E & G));
				uint T2 = (((A >> 2) | (A << 30)) ^ ((A >> 13) | (A << 19)) ^
						  ((A >> 22) | (A << 10))) + ((A & B) ^ (A & C) ^ (B & C));
				H = G;
				G = F;
				F = E;
				E = D + T;
				D = C;
				C = B;
				B = A;
				A = T + T2;
			}

			this.State[0] += A;
			this.State[1] += B;
			this.State[2] += C;
			this.State[3] += D;
			this.State[4] += E;
			this.State[5] += F;
			this.State[6] += G;
			this.State[7] += H;
		}


		protected void TransformBuffer()
		{
			this.TransformBlock(this.Buffer.GetBytes(), 0);
		}

		public static HashState Create()
		{
			var state = new uint[8];
			var buffer = new HashBuffer(64);
			return new HashState(buffer, 0, state);
		}

		public void Reset()
		{
			this.State[0] = 0xc1059ed8;
			this.State[1] = 0x367cd507;
			this.State[2] = 0x3070dd17;
			this.State[3] = 0xf70e5939;
			this.State[4] = 0xffc00b31;
			this.State[5] = 0x68581511;
			this.State[6] = 0x64f98fa7;
			this.State[7] = 0xbefa4fa4;
			this.Buffer.Reset();
			this.ProcessedBytes = 0;
		}
	}
}
