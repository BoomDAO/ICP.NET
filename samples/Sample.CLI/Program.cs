using EdjCase.ICP.Agent.Agents;
using EdjCase.ICP.Candid.Models;
using System;
using System.Threading.Tasks;
using EdjCase.ICP.Agent.Identities;
using CommandLine;
using Sample.Shared.Governance;
using EdjCase.ICP.Candid;
using EdjCase.ICP.Agent.Responses;
using System.Collections.Generic;
using EdjCase.ICP.Agent;
using System.IO;
using Sample.Shared.Governance.Models;
using System.Reflection.Metadata.Ecma335;
using EdjCase.ICP.Agent.Standards.AssetCanister;
using System.Text;
using EdjCase.ICP.Agent.Standards.AssetCanister.Models;

public class Program
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	[Verb("upload-asset", HelpText = "Upload a file to an asset canister.")]
	class UploadOptions
	{
		[Option('u', "url", Required = false, HelpText = "The url to the boundy node", Default = "https://ic0.app")]
		public string? Url { get; set; }

		[Option('c', "canister-id", Required = true, HelpText = "The asset canister id to upload to")]
		public string CanisterId { get; set; }

		[Option('f', "file-path", Required = true, HelpText = "The path to the file to upload.")]
		public string FilePath { get; set; }

		[Option('k', "asset-key", Required = true, HelpText = "The name of the asset to upload")]
		public string Key { get; set; }

		[Option('t', "content-type", Required = true, HelpText = "The content type of the asset to upload (e.g text/plain)")]
		public string ContentType { get; set; }

		[Option('e', "encoding", Required = true, HelpText = "The encoding of the asset to upload (e.g UTF-8)")]
		public string Encoding { get; set; }

		[Option('i', "identity-pem-path", Required = false, HelpText = "The path to an identity pem file to auth the download")]
		public string IdentityPEMFilePath { get; set; }

		[Option('p', "identity-pem-password", Required = false, HelpText = "The password for the identity PEM file")]
		public string IdentityPassword { get; set; }
	};

	[Verb("download-asset", HelpText = "Download a file from an asset canister.")]
	class DownloadOptions
	{
		[Option('u', "url", Required = false, HelpText = "The url to the boundy node", Default = "https://ic0.app")]
		public string? Url { get; set; }

		[Option('c', "canister-id", Required = true, HelpText = "The asset canister id to download from")]
		public string CanisterId { get; set; }

		[Option('f', "file-path", Required = true, HelpText = "The path to the file to save to")]
		public string FilePath { get; set; }

		[Option('k', "asset-key", Required = true, HelpText = "The name of the asset to download")]
		public string Key { get; set; }

		[Option('e', "encoding", Required = true, HelpText = "The encoding of the asset to download (e.g UTF-8)")]
		public string Encoding { get; set; }

		[Option('i', "identity-pem-path", Required = false, HelpText = "The path to an identity PEM file to auth the download")]
		public string IdentityPEMFilePath { get; set; }

		[Option('p', "identity-pem-password", Required = false, HelpText = "The password for the identity PEM file")]
		public string IdentityPassword { get; set; }
	};


#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	public static async Task Main(string[] args)
	{
		var result = Parser.Default.ParseArguments<UploadOptions, DownloadOptions>(args)
			.WithNotParsed(errors => { });
		await result
			.WithParsedAsync<UploadOptions>(async options =>
			{
				IIdentity? identity = null;
				if (options.IdentityPEMFilePath != null)
				{
					identity = IdentityUtil.FromPemFile(options.IdentityPEMFilePath, options.IdentityPassword);
				}
				Uri httpBoundryNodeUrl = new Uri(options.Url!);
				var agent = new HttpAgent(identity, httpBoundryNodeUrl);
				Samples s = new(agent);

				Principal canisterId = Principal.FromText(options.CanisterId!);
				await s.UploadChunkedFileAsync(
					canisterId,
					options.Key,
					options.ContentType,
					options.Encoding,
					options.FilePath
				);
			});

		await result
			.WithParsedAsync<DownloadOptions>(async options =>
			{
				IIdentity? identity = null;
				if (options.IdentityPEMFilePath != null)
				{
					identity = IdentityUtil.FromPemFile(options.IdentityPEMFilePath, options.IdentityPassword);
				}
				Uri httpBoundryNodeUrl = new Uri(options.Url!);
				var agent = new HttpAgent(identity, httpBoundryNodeUrl);
				Samples s = new(agent);

				Principal canisterId = Principal.FromText(options.CanisterId!);
				await s.DownloadFileAsync(canisterId, options.Key, options.Encoding, options.FilePath);
			});

	}

	private class Samples
	{
		private IAgent agent;
		public Samples(IAgent agent)
		{
			this.agent = agent;
		}

		//public async Task GetProposalAsync()
		//{
		//	Principal canisterId = Principal.FromText("rrkah-fqaaa-aaaaa-aaaaq-cai");
		//	var client = new GovernanceApiClient(this.agent, canisterId);
		//	ulong proposalId = 110174;
		//	Console.WriteLine($"Getting info for proposal {proposalId}...");
		//	OptionalValue<ProposalInfo> proposalInfo = await client.GetProposalInfo(proposalId);

		//	CandidTypedValue rawCandid = CandidTypedValue.FromObject(proposalInfo);
		//	Console.WriteLine("ProposalInfo:\n" + rawCandid.Value.ToString());
		//}

		//public async Task GetGovernanceCanisterState()
		//{
		//	Principal canisterId = Principal.FromText("rrkah-fqaaa-aaaaa-aaaaq-cai");
		//	Console.WriteLine();
		//	Console.WriteLine($"Getting the state for the governance canister ({canisterId})...");
		//	var paths = new List<StatePath>
		//	{
		//	};
		//	ReadStateResponse readStateResponse = await this.agent.ReadStateAsync(canisterId, paths);

		//	Console.WriteLine("State:\n" + readStateResponse.Certificate.Tree);

		//}

		public async Task DownloadFileAsync(
			Principal canisterId,
			string key,
			string encoding,
			string outputFilePath
		)
		{
			var client = new AssetCanisterApiClient(this.agent, canisterId);

			Console.WriteLine($"Downloading asset '{key}'...");
			GetResult result = await client.GetAsync(key, new List<string> { encoding });
			File.WriteAllBytes(outputFilePath, result.Content);
			Console.WriteLine($"Downloaded asset '{key}' to {outputFilePath}");
		}

		public async Task UploadChunkedFileAsync(
			Principal canisterId,
			string key,
			string contentType,
			string encoding,
			string filePath
		)
		{
			var client = new AssetCanisterApiClient(this.agent, canisterId);

			Console.WriteLine($"Uploading chunks for asset '{key}'...");
			Stream? contentStream = null;
			try
			{
				contentStream = File.OpenRead(filePath);
			
				await client.UploadAssetChunkedAsync(
						key: key,
						contentType: contentType,
						contentEncoding: encoding,
						contentStream: contentStream,
						sha256: null,
						allowRawAccess: true,
						enableAliasing: true,
						headers: new List<(string, string)>
						{
						},
						maxAge: 2_000_000_000_000
					);
			}
			finally
			{
				contentStream?.Dispose();
			}
			Console.WriteLine($"Asset '{key}' is fully uploaded");
		}
	}
}
