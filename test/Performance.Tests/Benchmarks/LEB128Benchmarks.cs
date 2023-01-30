using BenchmarkDotNet.Attributes;
using EdjCase.ICP.Candid.Crypto;
using EdjCase.ICP.Candid.Encodings;
using EdjCase.ICP.Candid.Models;
using EdjCase.ICP.Candid.Models.Types;
using EdjCase.ICP.Candid.Models.Values;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Performance.Tests.Benchmarks
{
	[MemoryDiagnoser]
	public class LEB128Benchmarks
	{
		private const ulong value_leb128 = 10000000000000000000L;
		private readonly byte[] bytes_leb128 = LEB128.EncodeUnsigned(value_leb128);
		private IBufferWriter<byte> destination = new ArrayBufferWriter<byte>();


		[Benchmark]
		public void LEB128_EncodeSigned()
		{
			LEB128.EncodeSigned(value_leb128, this.destination);
		}

		[Benchmark]
		public void LEB128_EncodeUnsigned()
		{
			LEB128.EncodeUnsigned(value_leb128, this.destination);
		}

		[Benchmark]
		public void LEB128_DecodeSigned()
		{
			using (MemoryStream stream = new MemoryStream(this.bytes_leb128))
			{
				_ = LEB128.DecodeSigned(stream);
			}
		}

		[Benchmark]
		public void LEB128_DecodeUnsigned()
		{
			using (MemoryStream stream = new MemoryStream(this.bytes_leb128))
			{
				_ = LEB128.DecodeUnsigned(stream);
			}
		}
	}
}
