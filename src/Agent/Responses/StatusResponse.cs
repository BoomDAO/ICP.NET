using Dahomey.Cbor.ObjectModel;

namespace EdjCase.ICP.Agent.Responses
{
	public class StatusResponse
	{
		// ic_api_version
		public string ICApiVersion { get; }

		// impl_source
		public string? ImplementationSource { get; }

		// impl_version
		public string? ImplementationVersion { get; }

		// impl_revision
		public string? ImplementationRevision { get; }

		// Only for development networks
		public byte[]? DevelopmentRootKey { get; }


		public StatusResponse(
			string icApiVersion,
			string? implementationSource,
			string? implementationVersion,
			string? implementationRevision,
			byte[]? developmentRootKey)
		{
			this.ICApiVersion = icApiVersion;
			this.ImplementationSource = implementationSource;
			this.ImplementationVersion = implementationVersion;
			this.ImplementationRevision = implementationRevision;
			this.DevelopmentRootKey = developmentRootKey;
		}

		public static StatusResponse FromCbor(CborObject cbor)
		{
			string icApiVersion = cbor["ic_api_version"].Value<string>();
			string? implementationSource = cbor["impl_source"].Value<string?>();
			string? implementationVersion = cbor["impl_version"].Value<string?>();
			string? implementationRevision = cbor["impl_revision"].Value<string?>();
			byte[]? rawDevelopmentRootKey = cbor["root_key"].Value<byte[]?>();
			return new StatusResponse(
				icApiVersion,
				implementationSource,
				implementationVersion,
				implementationRevision,
				rawDevelopmentRootKey
			);
		}
	}
}