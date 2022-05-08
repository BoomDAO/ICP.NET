using System;
using ICP.Candid.Crypto;
using ICP.Candid.Encodings;
using ICP.Candid.Models.Values;

namespace ICP.Candid.Models
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

		public static ICTimestamp FromEpochNanoSeconds(ulong nanosecondsFromNow)
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
			return FromEpochNanoSeconds(0);
		}

		public byte[] ComputeHash(IHashFunction hashFunction)
		{
			return hashFunction.ComputeHash(LEB128.EncodeUnsigned(this.NanoSeconds));
		}

		public CandidValue ToCandid()
		{
			return CandidPrimitive.Nat(this.NanoSeconds);
		}
	}
}