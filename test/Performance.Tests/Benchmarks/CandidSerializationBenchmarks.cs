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
	public class CandidSerializationBenchmarks
	{
		private const ulong value_leb128 = 10000000000000000000L;

		private CandidValue[] vectorValues = Enumerable.Range(0, 10_000).Select(r => CandidValue.Nat(value_leb128)).ToArray();


		[Benchmark]
		public void Encode()
		{
			IBufferWriter<byte> destination = new ArrayBufferWriter<byte>();
			CandidArg.FromCandid(
				new CandidTypedValue(
					new CandidVector(this.vectorValues),
					new CandidVectorType(CandidType.Nat())
				)
			).Encode(destination);
		}

		[Benchmark]
		public void Decode()
		{

		}
	}
}
