using BenchmarkDotNet.Attributes;
using EdjCase.ICP.Candid.Crypto;
using System.Security.Cryptography;

[MemoryDiagnoser]
public class HashingBenchmarks
{
	private const int N = 10000;
	private readonly byte[] data;

	public HashingBenchmarks()
	{
		this.data = new byte[N];
		new Random(42).NextBytes(this.data);
	}

	[Benchmark]
	public void Crc32()
	{
		CRC32.ComputeHash(this.data);
	}
}
