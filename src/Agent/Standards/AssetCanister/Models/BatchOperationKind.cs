using EdjCase.ICP.Candid.Mapping;
using System;

namespace EdjCase.ICP.Agent.Standards.AssetCanister.Models
{
	/// <summary>
	/// Represents a batch operation kind for asset canister operations, supporting different variants.
	/// </summary>
	[Variant]
	public class BatchOperationKind
	{
		/// <summary>
		/// The tag indicating the type of the batch operation.
		/// </summary>
		[VariantTagProperty]
		public BatchOperationKindTag Tag { get; set; }

		/// <summary>
		/// The value of the batch operation.
		/// </summary>
		[VariantValueProperty]
		public object? Value { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="BatchOperationKind"/> class.
		/// </summary>
		/// <param name="tag">The tag indicating the type of the operation.</param>
		/// <param name="value">The value of the operation.</param>
		public BatchOperationKind(BatchOperationKindTag tag, object? value)
		{
			this.Tag = tag;
			this.Value = value;
		}

		/// <summary>
		/// Protected constructor for <see cref="BatchOperationKind"/>.
		/// </summary>
		protected BatchOperationKind()
		{
		}

		/// <summary>
		/// Creates an instance of <see cref="BatchOperationKind"/> for the 'CreateAsset' operation.
		/// </summary>
		/// <param name="info">The arguments for the 'CreateAsset' operation.</param>
		/// <returns>A new instance of <see cref="BatchOperationKind"/>.</returns>
		public static BatchOperationKind CreateAsset(CreateAssetArguments info)
		{
			return new BatchOperationKind(BatchOperationKindTag.CreateAsset, info);
		}

		/// <summary>
		/// Creates an instance of <see cref="BatchOperationKind"/> for the 'SetAssetContent' operation.
		/// </summary>
		/// <param name="info">The arguments for the 'SetAssetContent' operation.</param>
		/// <returns>A new instance of <see cref="BatchOperationKind"/>.</returns>
		public static BatchOperationKind SetAssetContent(SetAssetContentArguments info)
		{
			return new BatchOperationKind(BatchOperationKindTag.SetAssetContent, info);
		}

		/// <summary>
		/// Creates an instance of <see cref="BatchOperationKind"/> for the 'SetAssetProperties' operation.
		/// </summary>
		/// <param name="info">The arguments for the 'SetAssetProperties' operation.</param>
		/// <returns>A new instance of <see cref="BatchOperationKind"/>.</returns>
		public static BatchOperationKind SetAssetProperties(SetAssetPropertiesRequest info)
		{
			return new BatchOperationKind(BatchOperationKindTag.SetAssetProperties, info);
		}

		/// <summary>
		/// Creates an instance of <see cref="BatchOperationKind"/> for the 'UnsetAssetContent' operation.
		/// </summary>
		/// <param name="info">The arguments for the 'UnsetAssetContent' operation.</param>
		/// <returns>A new instance of <see cref="BatchOperationKind"/>.</returns>
		public static BatchOperationKind UnsetAssetContent(UnsetAssetContentArguments info)
		{
			return new BatchOperationKind(BatchOperationKindTag.UnsetAssetContent, info);
		}

		/// <summary>
		/// Creates an instance of <see cref="BatchOperationKind"/> for the 'DeleteAsset' operation.
		/// </summary>
		/// <param name="info">The arguments for the 'DeleteAsset' operation.</param>
		/// <returns>A new instance of <see cref="BatchOperationKind"/>.</returns>
		public static BatchOperationKind DeleteAsset(DeleteAssetArguments info)
		{
			return new BatchOperationKind(BatchOperationKindTag.DeleteAsset, info);
		}

		/// <summary>
		/// Creates an instance of <see cref="BatchOperationKind"/> for the 'Clear' operation.
		/// </summary>
		/// <returns>A new instance of <see cref="BatchOperationKind"/>.</returns>
		public static BatchOperationKind Clear()
		{
			return new BatchOperationKind(BatchOperationKindTag.Clear, null);
		}

		/// <summary>
		/// Retrieves the 'CreateAsset' arguments from the instance.
		/// </summary>
		/// <returns>The 'CreateAsset' arguments.</returns>
		public CreateAssetArguments AsCreateAsset()
		{
			this.ValidateTag(BatchOperationKindTag.CreateAsset);
			return (CreateAssetArguments)this.Value!;
		}

		/// <summary>
		/// Retrieves the 'SetAssetContent' arguments from the instance.
		/// </summary>
		/// <returns>The 'SetAssetContent' arguments.</returns>
		public SetAssetContentArguments AsSetAssetContent()
		{
			this.ValidateTag(BatchOperationKindTag.SetAssetContent);
			return (SetAssetContentArguments)this.Value!;
		}

		/// <summary>
		/// Retrieves the 'SetAssetProperties' arguments from the instance.
		/// </summary>
		/// <returns>The 'SetAssetProperties' arguments.</returns>
		public SetAssetPropertiesRequest AsSetAssetProperties()
		{
			this.ValidateTag(BatchOperationKindTag.SetAssetProperties);
			return (SetAssetPropertiesRequest)this.Value!;
		}

		/// <summary>
		/// Retrieves the 'UnsetAssetContent' arguments from the instance.
		/// </summary>
		/// <returns>The 'UnsetAssetContent' arguments.</returns>
		public UnsetAssetContentArguments AsUnsetAssetContent()
		{
			this.ValidateTag(BatchOperationKindTag.UnsetAssetContent);
			return (UnsetAssetContentArguments)this.Value!;
		}

		/// <summary>
		/// Retrieves the 'DeleteAsset' arguments from the instance.
		/// </summary>
		/// <returns>The 'DeleteAsset' arguments.</returns>
		public DeleteAssetArguments AsDeleteAsset()
		{
			this.ValidateTag(BatchOperationKindTag.DeleteAsset);
			return (DeleteAssetArguments)this.Value!;
		}

		/// <summary>
		/// Validates that the current tag matches the expected tag.
		/// </summary>
		/// <param name="tag">The expected tag.</param>
		/// <exception cref="InvalidOperationException">Thrown if the current tag does not match the expected tag.</exception>
		private void ValidateTag(BatchOperationKindTag tag)
		{
			if (!this.Tag.Equals(tag))
			{
				throw new InvalidOperationException($"Cannot cast '{this.Tag}' to type '{tag}'");
			}
		}
	}

	/// <summary>
	/// Enumerates the tags for different types of batch operations.
	/// </summary>
	public enum BatchOperationKindTag
	{
		/// <summary>
		/// Tag for the 'CreateAsset' operation.
		/// </summary>
		CreateAsset,

		/// <summary>
		/// Tag for the 'SetAssetContent' operation.
		/// </summary>
		SetAssetContent,

		/// <summary>
		/// Tag for the 'SetAssetProperties' operation.
		/// </summary>
		SetAssetProperties,

		/// <summary>
		/// Tag for the 'UnsetAssetContent' operation.
		/// </summary>
		UnsetAssetContent,

		/// <summary>
		/// Tag for the 'DeleteAsset' operation.
		/// </summary>
		DeleteAsset,

		/// <summary>
		/// Tag for the 'Clear' operation.
		/// </summary>
		Clear
	}
}
