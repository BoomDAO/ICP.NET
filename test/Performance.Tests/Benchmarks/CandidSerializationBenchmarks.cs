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
	[SimpleJob(launchCount: 1, warmupCount: 1, iterationCount: 1)]
	public class CandidSerializationBenchmarks
	{
		private const ulong value_leb128 = 10000000000000000000L;

		private static CandidArg largeArg = CandidArg.FromCandid(
			new CandidTypedValue(
				new CandidVector(Enumerable.Range(0, 10_000).Select(r => CandidValue.Nat(value_leb128)).ToArray()),
				new CandidVectorType(CandidType.Nat())
			)
		);
		private static byte[] encodedLargeArg = largeArg.Encode();

		private static CandidArg smallArg = CandidArg.FromCandid(
			new CandidTypedValue(
				CandidValue.Nat(10000000),
				CandidType.Nat()
			)
		);
		private static byte[] encodedSmallArg = smallArg.Encode();



		[Benchmark]
		public void Encode_Large()
		{
			IBufferWriter<byte> destination = new ArrayBufferWriter<byte>();
			largeArg.Encode(destination);
		}

		[Benchmark]
		public void Decode_Large()
		{
			_ = CandidArg.FromBytes(encodedLargeArg);
		}

		[Benchmark]
		public void Encode_Small()
		{
			IBufferWriter<byte> destination = new ArrayBufferWriter<byte>();
			smallArg.Encode(destination);
		}

		[Benchmark]
		public void Decode_Small()
		{
			_ = CandidArg.FromBytes(encodedSmallArg);
		}
	}
}
