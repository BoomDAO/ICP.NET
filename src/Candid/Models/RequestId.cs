using EdjCase.ICP.Candid.Crypto;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace EdjCase.ICP.Candid.Models
{
	/// <summary>
	/// A helper class to wrap around the request id byte array that identifies a request
	/// </summary>
	public class RequestId
	{

		/// <summary>
		/// The raw id value
		/// </summary>
		public byte[] RawValue { get; }

		/// <param name="rawValue">The raw id value</param>
		public RequestId(byte[] rawValue)
		{
			this.RawValue = rawValue;
		}

		/// <summary>
		/// Converts a hashable object into a request id
		/// </summary>
		/// <param name="properties">The properties of the object to hash</param>
		/// <returns>A request id object</returns>
		public static RequestId FromObject(IDictionary<string, IHashable> properties)
		{
			byte[] bytes = properties.ToHashable().ComputeHash(SHA256HashFunction.Create());
			return new RequestId(bytes);
		}
	}
}
