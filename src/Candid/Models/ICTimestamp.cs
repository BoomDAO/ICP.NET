using System;
using EdjCase.ICP.Candid.Crypto;
using EdjCase.ICP.Candid.Encodings;
using EdjCase.ICP.Candid.Models.Values;

namespace EdjCase.ICP.Candid.Models
{
	public class ICTimestamp : IHashable
	{
		private const int REPLICA_PERMITTED_DRIFT_MILLISECONDS = 60_000;

		private static readonly DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
		public UnboundedUInt NanoSeconds { get; }
		public ICTimestamp(UnboundedUInt nanoSeconds)
		{
			this.NanoSeconds = nanoSeconds ?? throw new ArgumentNullException(nameof(nanoSeconds));
		}

		public static ICTimestamp FromNanoSeconds(ulong nanosecondsSinceEpoch)
		{
			return new ICTimestamp(nanosecondsSinceEpoch.ToUnbounded());
		}

		public static ICTimestamp FromNanoSecondsInFuture(ulong nanosecondsFromNow)
		{
			ulong futureDate = EpochNowInNanoseconds() + nanosecondsFromNow;
			return new ICTimestamp(futureDate.ToUnbounded());
		}

		private static ulong EpochNowInNanoseconds()
		{
			return (ulong)(((DateTime.UtcNow - epoch).TotalMilliseconds + REPLICA_PERMITTED_DRIFT_MILLISECONDS) * 1_000_000);
		}

		public static ICTimestamp Now()
		{
			return FromNanoSecondsInFuture(0);
		}

		public byte[] ComputeHash(IHashFunction hashFunction)
		{
			return hashFunction.ComputeHash(LEB128.EncodeUnsigned(this.NanoSeconds));
		}

		public CandidValue ToCandid()
		{
			return CandidPrimitive.Nat(this.NanoSeconds);
		}

		public override string ToString()
		{
			TimeSpan offset = TimeSpan.FromMilliseconds((double)this.NanoSeconds.ToBigInteger() / 1000000);
			return epoch
				.Add(offset)
				.ToString("s");
		}
	}
}