using Dahomey.Cbor.Attributes;
using Dahomey.Cbor.ObjectModel;

namespace EdjCase.ICP.Agent.Responses
{
	public class StatusResponse
	{
		[CborProperty(Properties.IC_API_VERSION)]
		public string ICApiVersion { get; }

		[CborProperty(Properties.IMPLEMENTATION_SOURCE)]
		public string? ImplementationSource { get; }

		[CborIgnoreIfDefault]
		[CborProperty(Properties.IMPLEMENTATION_VERSION)]
		public string? ImplementationVersion { get; }

		[CborIgnoreIfDefault]
		[CborProperty(Properties.IMPLEMENTATION_REVISION)]
		public string? ImplementationRevision { get; }

		// Only for development networks
		[CborIgnoreIfDefault]
		[CborProperty(Properties.ROOT_KEY)]
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

		private class Properties
		{
			public const string IC_API_VERSION = "ic_api_version";
			public const string IMPLEMENTATION_SOURCE = "impl_source";
			public const string IMPLEMENTATION_VERSION = "impl_version";
			public const string IMPLEMENTATION_REVISION = "impl_revision";
			public const string ROOT_KEY = "root_key";
		}
	}
}