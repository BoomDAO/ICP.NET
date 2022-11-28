using EdjCase.ICP.Candid.Crypto;
using EdjCase.ICP.Candid.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EdjCase.ICP.Candid.Models
{
	public class RequestId : IHashable
	{
		public byte[] RawValue { get; }

		public RequestId(byte[] rawValue)
		{
			this.RawValue = rawValue;
		}

		public static RequestId FromObject(IDictionary<string, IHashable> properties, IHashFunction hashFunction)
		{
			var orderedProperties = properties
				.Where(o => o.Value != null) // Remove empty/null ones
				.Select(o =>
				{
					byte[] keyDigest = o.Key.ToHashable().ComputeHash(hashFunction);
					byte[] valueDigest = o.Value!.ComputeHash(hashFunction);

					return (KeyHash: keyDigest, ValueHash: valueDigest);
				}) // Hash key and value bytes
				.OrderBy(o => o.KeyHash, new HashComparer()); // Keys in order
			byte[] bytes = orderedProperties
				.SelectMany(o => o.KeyHash.Concat(o.ValueHash))
				.ToArray(); // Create single byte[] by concatinating them all together
			return new RequestId(hashFunction.ComputeHash(bytes));
		}

		public byte[] ComputeHash(IHashFunction hashFunction)
		{
			return hashFunction.ComputeHash(this.RawValue);
		}
	}
}
