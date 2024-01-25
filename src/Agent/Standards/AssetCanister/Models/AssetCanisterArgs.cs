using EdjCase.ICP.Candid.Mapping;
using System;

namespace EdjCase.ICP.Agent.Standards.AssetCanister.Models
{
	/// <summary>
	/// Represents arguments for asset canister operations, supporting different variants.
	/// </summary>
	[Variant]
	public class AssetCanisterArgs
	{
		/// <summary>
		/// The tag indicating the type of the asset canister argument.
		/// </summary>
		[VariantTagProperty]
		public AssetCanisterArgsTag Tag { get; set; }

		/// <summary>
		/// The value of the asset canister argument.
		/// </summary>
		[VariantValueProperty]
		public object? Value { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="AssetCanisterArgs"/> class.
		/// </summary>
		/// <param name="tag">The tag indicating the type of the argument.</param>
		/// <param name="value">The value of the argument.</param>
		public AssetCanisterArgs(AssetCanisterArgsTag tag, object? value)
		{
			this.Tag = tag;
			this.Value = value;
		}

		/// <summary>
		/// Protected constructor for <see cref="AssetCanisterArgs"/>.
		/// </summary>
		protected AssetCanisterArgs()
		{
		}

		/// <summary>
		/// Creates an instance of <see cref="AssetCanisterArgs"/> for the 'Init' operation.
		/// </summary>
		/// <returns>A new instance of <see cref="AssetCanisterArgs"/>.</returns>
		public static AssetCanisterArgs Init()
		{
			return new AssetCanisterArgs(AssetCanisterArgsTag.Init, null);
		}

		/// <summary>
		/// Creates an instance of <see cref="AssetCanisterArgs"/> for the 'Upgrade' operation.
		/// </summary>
		/// <param name="info">The upgrade arguments.</param>
		/// <returns>A new instance of <see cref="AssetCanisterArgs"/>.</returns>
		public static AssetCanisterArgs Upgrade(UpgradeArgs info)
		{
			return new AssetCanisterArgs(AssetCanisterArgsTag.Upgrade, info);
		}

		/// <summary>
		/// Retrieves the 'Upgrade' arguments from the instance.
		/// </summary>
		/// <returns>The 'Upgrade' arguments.</returns>
		public UpgradeArgs AsUpgrade()
		{
			this.ValidateTag(AssetCanisterArgsTag.Upgrade);
			return (UpgradeArgs)this.Value!;
		}

		/// <summary>
		/// Validates that the current tag matches the expected tag.
		/// </summary>
		/// <param name="tag">The expected tag.</param>
		/// <exception cref="InvalidOperationException">Thrown if the current tag does not match the expected tag.</exception>
		private void ValidateTag(AssetCanisterArgsTag tag)
		{
			if (!this.Tag.Equals(tag))
			{
				throw new InvalidOperationException($"Cannot cast '{this.Tag}' to type '{tag}'");
			}
		}
	}

	/// <summary>
	/// Enumerates the tags for different types of asset canister arguments.
	/// </summary>
	public enum AssetCanisterArgsTag
	{
		/// <summary>
		/// Tag for initialization arguments.
		/// </summary>
		Init,

		/// <summary>
		/// Tag for upgrade arguments.
		/// </summary>
		Upgrade
	}
}
