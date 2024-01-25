using EdjCase.ICP.Candid.Mapping;
using Key = System.String;

namespace EdjCase.ICP.Agent.Standards.AssetCanister.Models
{
	/// <summary>
	/// Represents the arguments for deleting an asset in the asset canister.
	/// </summary>
	public class DeleteAssetArguments
	{
		/// <summary>
		/// The key of the asset to delete.
		/// </summary>
		[CandidName("key")]
		public Key Key { get; set; }

		/// <summary>
		/// Initializes a new instance of the DeleteAssetArguments class with a specified asset key.
		/// </summary>
		/// <param name="key">The key of the asset to delete.</param>
		public DeleteAssetArguments(Key key)
		{
			this.Key = key;
		}

		/// <summary>
		/// Initializes a new instance of the DeleteAssetArguments class.
		/// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public DeleteAssetArguments()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		{
		}
	}
}
