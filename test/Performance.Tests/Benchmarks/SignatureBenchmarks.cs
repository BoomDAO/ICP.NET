using BenchmarkDotNet.Attributes;
using EdjCase.ICP.Candid.Crypto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Performance.Tests.Benchmarks
{
	public class SignatureBenchmarks
	{
		private const int N = 10000;
		private readonly byte[] data;
		private readonly SHA256HashFunction sha256;

		public SignatureBenchmarks()
		{
			this.data = new byte[N];
			new Random(42).NextBytes(this.data);
			this.sha256 = SHA256HashFunction.Create();
		}

		[Benchmark]
		public void SHA256()
		{
			this.sha256.ComputeHash(this.data);
		}
	}
}
