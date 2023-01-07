using EdjCase.ICP.Candid.Crypto;
using EdjCase.ICP.Candid.Utilities;
using System;
using System.Linq;

namespace EdjCase.ICP.Candid.Models
{
	public class Principal : IHashable, IEquatable<Principal>
	{
		private const byte anonymousSuffix = 4;
		private const byte selfAuthenticatingSuffix = 2;

		public byte[] Raw { get; }
		public Principal(byte[] raw)
		{
			this.Raw = raw;
		}

		public string ToText()
		{
			// Add checksum to beginning of byte array
			byte[] checksum = CRC32.ComputeHash(this.Raw);
			byte[] bytesWithChecksum = checksum
				.Concat(this.Raw)
				.ToArray();
			return Base32EncodingUtil.FromBytes(bytesWithChecksum, groupedWithChecksum: true);
		}

		public override string ToString()
		{
			return this.ToText();
		}

		public string ToHex()
		{
			return ByteUtil.ToHexString(this.Raw);
		}

		public bool IsAnonymous()
		{
			return this.Raw.Length == 1 && this.Raw[0] == anonymousSuffix;
		}

		public static Principal ICManagementCanisterId()
		{
			return new Principal(new byte[0]);
		}

		public static Principal FromHex(string hex)
		{
			// TODO validation
			byte[] bytes = ByteUtil.FromHexString(hex);
			return new Principal(bytes);
		}

		public static Principal Anonymous()
		{
			return new Principal(new byte[] { anonymousSuffix });
		}

		public static Principal SelfAuthenticating(byte[] publicKey)
		{
			byte[] digest = new SHA224().GenerateDigest(publicKey);

			// bytes = digest + selfAuthenticatingSuffix
			byte[] bytes = new byte[digest.Length + 1];
			digest.CopyTo(bytes.AsSpan());
			bytes[bytes.Length - 1] = selfAuthenticatingSuffix;
			return new Principal(bytes);
		}

		public static Principal FromText(string text)
		{
			string canisterIdNoDash = text
				.ToLower()
				.Replace("-", "");

			byte[] bytes = Base32EncodingUtil.ToBytes(canisterIdNoDash);

			// Remove first 4 bytes which is the checksum
			bytes = bytes
				.AsSpan()
				.Slice(4)
				.ToArray();

			var principal = new Principal(bytes);
			string parsedText = principal.ToText();
			if (parsedText != text)
			{
				throw new Exception($"Principal '{parsedText}' does not have a valid checksum.");
			}

			return principal;
		}

		public static Principal FromRaw(byte[] raw)
		{
			// TODO any validation?
			return new Principal(raw);
		}

		public byte[] ComputeHash(IHashFunction hashFunction)
		{
			return hashFunction.ComputeHash(this.Raw);
		}

		public bool Equals(Principal? other)
		{
			if (other == null)
			{
				return false;
			}
			return this.Raw.SequenceEqual(other.Raw);
		}

		public override bool Equals(object? obj)
		{
			return this.Equals(obj as Principal);
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(this.Raw);
		}
	}
}