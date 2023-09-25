using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid.Models;
using System;

namespace EdjCase.ICP.Agent.Standards.ICRC1.Models
{

	/// <summary>
	/// A model representing metadata from an icrc1 token
	/// </summary>
	public class MetaData
	{
		/// <summary>
		/// The key or name of the metadata value
		/// </summary>
		[CandidName("0")]
		public string Key { get; set; }

		/// <summary>
		/// The associated value for the metadata key
		/// </summary>
		[CandidName("1")]
		public MetaDataValue Value { get; set; }

		/// <summary>
		/// Primary constructor
		/// </summary>
		/// <param name="key">The key or name of the metadata value</param>
		/// <param name="value">The associated value for the metadata key</param>
		public MetaData(string key, MetaDataValue value)
		{
			this.Key = key;
			this.Value = value;
		}
	}

	/// <summary>
	/// A model representing the metadata value from an icrc1 token
	/// </summary>
	[Variant]
	public class MetaDataValue
	{
		/// <summary>
		/// The metadata variant option tag/type
		/// </summary>
		[VariantTagProperty]
		public MetaDataValueTag Tag { get; set; }

		/// <summary>
		/// The metadata variant option raw value
		/// </summary>
		[VariantValueProperty]
		public object? Value { get; set; }

		private MetaDataValue(MetaDataValueTag tag, object? value = null)
		{
			this.Tag = tag;
			this.Value = value;
		}

		/// <summary>
		/// Constructor for reflection
		/// </summary>
		protected MetaDataValue()
		{
		}

		/// <summary>
		/// Constructs a metadata value with a Nat
		/// </summary>
		/// <param name="value">The Nat value to use</param>
		/// <returns>A metadata value with a Nat</returns>
		public static MetaDataValue Nat(UnboundedUInt value)
		{
			return new MetaDataValue(MetaDataValueTag.Nat, value);
		}

		/// <summary>
		/// Gets the Nat value from the metadata. If the variant is not a Nat, will throw an error
		/// </summary>
		/// <returns>The Nat value of the metadata</returns>
		public UnboundedUInt AsNat()
		{
			this.ValidateTag(MetaDataValueTag.Nat);
			return (UnboundedUInt)this.Value!;
		}


		/// <summary>
		/// Constructs a metadata value with a Int
		/// </summary>
		/// <param name="value">The Int value to use</param>
		/// <returns>A metadata value with a Int</returns>
		public static MetaDataValue Int(UnboundedInt value)
		{
			return new MetaDataValue(MetaDataValueTag.Int, value);
		}

		/// <summary>
		/// Gets the Int value from the metadata. If the variant is not a Int, will throw an error
		/// </summary>
		/// <returns>The Int value of the metadata</returns>
		public UnboundedInt AsInt()
		{
			this.ValidateTag(MetaDataValueTag.Int);
			return (UnboundedInt)this.Value!;
		}

		/// <summary>
		/// Constructs a metadata value with a Text
		/// </summary>
		/// <param name="value">The Text value to use</param>
		/// <returns>A metadata value with a Text</returns>
		public static MetaDataValue Text(string value)
		{
			return new MetaDataValue(MetaDataValueTag.Text, value);
		}

		/// <summary>
		/// Gets the Text value from the metadata. If the variant is not a Text, will throw an error
		/// </summary>
		/// <returns>The Text value of the metadata</returns>
		public string AsText()
		{
			this.ValidateTag(MetaDataValueTag.Text);
			return (string)this.Value!;
		}

		/// <summary>
		/// Constructs a metadata value with a Blob
		/// </summary>
		/// <param name="value">The Blob value to use</param>
		/// <returns>A metadata value with a Blob</returns>
		public static MetaDataValue Blob(byte[] value)
		{
			return new MetaDataValue(MetaDataValueTag.Blob, value);
		}

		/// <summary>
		/// Gets the Blob value from the metadata. If the variant is not a Blob, will throw an error
		/// </summary>
		/// <returns>The Blob value of the metadata</returns>
		public byte[] AsBlob()
		{
			this.ValidateTag(MetaDataValueTag.Blob);
			return (byte[])this.Value!;
		}

		private void ValidateTag(MetaDataValueTag tag)
		{
			if (!this.Tag.Equals(tag))
			{
				throw new InvalidOperationException($"Cannot cast '{this.Tag}' to type '{tag}'");
			}
		}
	}

	/// <summary>
	/// An enum representing the meta data value types
	/// </summary>
	public enum MetaDataValueTag
	{
		/// <summary>
		/// Nat value
		/// </summary>
		Nat,
		/// <summary>
		/// Int value
		/// </summary>
		Int,
		/// <summary>
		/// Text value
		/// </summary>
		Text,
		/// <summary>
		/// Blob value
		/// </summary>
		Blob,
	}
}