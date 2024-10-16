using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Models;
using System.Net;

namespace EdjCase.ICP.PocketIC
{
	public class PocketIcClient : IDisposable
	{
		private readonly HttpClient httpClient;
		private readonly string baseUrl;
		private readonly TimeSpan processingTimeout;
		private readonly int instanceId;
		private bool isInstanceDeleted = false;

		public PocketIcClient(HttpClient httpClient, string url, int instanceId, TimeSpan processingTimeout)
		{
			this.httpClient = httpClient;
			this.baseUrl = url;
			this.instanceId = instanceId;
			this.processingTimeout = processingTimeout;
		}

		public static async Task<PocketIcClient> CreateAsync(
			string url,
			CreateInstanceRequest? request = null,
			TimeSpan? processingTimeout = null)
		{
			var timeout = processingTimeout ?? TimeSpan.FromSeconds(30);

			request ??= new CreateInstanceRequest();
			var httpClient = new HttpClient();
			var response = await PostAsync<CreateInstanceResponse>(httpClient, "/instances", request);

			if (response.Error != null)
			{
				throw new Exception($"Failed to create PocketIC instance: {response.Error.Message}");
			}

			var client = new PocketIcClient(httpClient, url, response.Created!.InstanceId, timeout);
			return client;
		}

		public async Task DeleteInstanceAsync()
		{
			this.AssertInstanceNotDeleted();
			await this.httpClient.DeleteAsync($"{this.baseUrl}/instances/{this.instanceId}");
			this.isInstanceDeleted = true;
		}

		public async Task TickAsync()
		{
			await this.PostAsync<object>("/update/tick", null);
		}

		public async Task<Principal> GetPublicKeyAsync(GetPublicKeyRequest request)
		{
			return await this.PostAsync<byte[]>("/read/pub_key", request)
		}

		public async Task<ulong> GetTimeAsync()
		{
			var response = await this.GetAsync<GetTimeResponse>("/read/get_time");
			return response.NanosSinceEpoch / 1_000_000;
		}

		public async Task SetTimeAsync(ICTimestamp timestamp)
		{
			if(!timestamp.NanoSeconds.TryToUInt64(out ulong nanosSinceEpoch))
			{
				throw new ArgumentException("Nanoseconds is too large to convert to ulong");
			}
			var request = new SetTimeRequest { NanosSinceEpoch = nanosSinceEpoch };
			await this.PostAsync<object>("/update/set_time", request);
		}

		public async Task InstallCodeAsync(InstallCodeOptions options)
		{
			await this.PostAsync<object>("/update/install_code", options);
		}

		public async Task<byte[]> QueryCallAsync(CanisterCallOptions options)
		{
			var response = await this.PostAsync<CanisterCallResponse>("/read/query", options);
			return response.Body;
		}

		public async Task<byte[]> UpdateCallAsync(CanisterCallOptions options)
		{
			var response = await this.PostAsync<CanisterCallResponse>("/update/execute_ingress_message", options);
			return response.Body;
		}

		public async Task<Principal> GetCanisterSubnetIdAsync(Principal canisterId)
		{
			var request = new GetSubnetIdRequest { CanisterId = canisterId };
			var response = await this.PostAsync<GetSubnetIdResponse>("/read/get_subnet", request);
			return response.SubnetId;
		}

		public async Task<ulong> GetCyclesBalanceAsync(Principal canisterId)
		{
			var request = new GetCyclesBalanceRequest { CanisterId = canisterId };
			var response = await this.PostAsync<GetCyclesBalanceResponse>("/read/get_cycles", request);
			return response.Cycles;
		}

		public async Task<ulong> AddCyclesAsync(Principal canisterId, ulong amount)
		{
			var request = new AddCyclesRequest { CanisterId = canisterId, Amount = amount };
			var response = await this.PostAsync<AddCyclesResponse>("/update/add_cycles", request);
			return response.Cycles;
		}

		public async Task SetStableMemoryAsync(Principal canisterId, byte[] memory)
		{
			var blobId = await this.UploadBlobAsync(memory);
			var request = new SetStableMemoryRequest { CanisterId = canisterId, BlobId = blobId };
			await this.PostAsync<object>("/update/set_stable_memory", request);
		}

		public async Task<byte[]> GetStableMemoryAsync(Principal canisterId)
		{
			var request = new GetStableMemoryRequest { CanisterId = canisterId };
			var response = await this.PostAsync<GetStableMemoryResponse>("/read/get_stable_memory", request);
			return response.Blob;
		}

		private async Task<string> UploadBlobAsync(byte[] blob)
		{
			this.AssertInstanceNotDeleted();
			var content = new ByteArrayContent(blob);
			var response = await this.httpClient.PostAsync($"{this.baseUrl}/blobstore", content);
			response.EnsureSuccessStatusCode();
			return await response.Content.ReadAsStringAsync();
		}

		private async Task<T> GetAsync<T>(string endpoint)
		{
			this.AssertInstanceNotDeleted();
			var response = await this.httpClient.GetAsync($"{this.baseUrl}{endpoint}");
			response.EnsureSuccessStatusCode();
			var content = await response.Content.ReadAsStringAsync();
			return JsonSerializer.Deserialize<T>(content)!;
		}

		private async Task<T> PostAsync<T>(string endpoint, object? data)
		{
			this.AssertInstanceNotDeleted();
			string url = $"{this.baseUrl}{endpoint}";
			return await PocketIcClient.PostAsync<T>(this.httpClient, url, data);
		}

		private static async Task<T> PostAsync<T>(HttpClient httpClient, string url, object? data)
		{
			var json = data != null ? JsonSerializer.Serialize(data) : "";
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await httpClient.PostAsync(url, content);
			response.EnsureSuccessStatusCode();
			var responseContent = await response.Content.ReadAsStringAsync();
			return JsonSerializer.Deserialize<T>(responseContent)!;
		}

		private void AssertInstanceNotDeleted()
		{
			if (this.isInstanceDeleted)
			{
				throw new InvalidOperationException("This PocketIC instance has been deleted.");
			}
		}

		public void Dispose()
		{
			this.httpClient.Dispose();
		}

	}

	// Request and Response classes
	public class CreateInstanceRequest
	{
		public CreateInstanceSubnetConfig SubnetConfigSet { get; set; }
		public bool NonmainnetFeatures { get; set; }
	}

	public class CreateInstanceSubnetConfig
	{
		public SubnetConfig? Nns { get; set; }
		public SubnetConfig? Sns { get; set; }
		public SubnetConfig? Ii { get; set; }
		public SubnetConfig? Fiduciary { get; set; }
		public SubnetConfig? Bitcoin { get; set; }
		public List<SubnetConfig> System { get; set; } = new List<SubnetConfig>();
		public List<SubnetConfig> Application { get; set; } = new List<SubnetConfig>();
		public List<SubnetConfig> VerifiedApplication { get; set; } = new List<SubnetConfig>();
	}

	public class SubnetConfig
	{
		public string DtsFlag { get; set; } // "Enabled" or "Disabled"
		public string InstructionConfig { get; set; } // "Production" or "Benchmarking"
		public object StateConfig { get; set; } // "New" or { "FromPath": [string, { "subnet_id": string }] }
	}

	public class CreateInstanceResponse
	{
		public CreatedInstance? Created { get; set; }
		public ErrorResponse? Error { get; set; }
	}

	public class CreatedInstance
	{
		public int InstanceId { get; set; }
		public Dictionary<string, SubnetTopology> Topology { get; set; }
	}

	public class SubnetTopology
	{
		public string SubnetKind { get; set; }
		public int Size { get; set; }
		public List<CanisterRange> CanisterRanges { get; set; }
	}

	public class CanisterRange
	{
		public CanisterId Start { get; set; }
		public CanisterId End { get; set; }
	}

	public class CanisterId
	{
		public string CanisterIdValue { get; set; }
	}

	public class ErrorResponse
	{
		public string Message { get; set; }
	}

	public class GetTimeResponse
	{
		public ulong NanosSinceEpoch { get; set; }
	}

	public class SetTimeRequest
	{
		public ulong NanosSinceEpoch { get; set; }
	}

	public class CreateCanisterResponse
	{
		public Principal CanisterId { get; set; }
	}

	public class InstallCodeOptions
	{
		public Principal CanisterId { get; set; }
		public byte[] Wasm { get; set; }
		public byte[]? Arg { get; set; }
		public Principal? Sender { get; set; }
	}

	public class CanisterCallOptions
	{
		public Principal Sender { get; set; }
		public Principal CanisterId { get; set; }
		public string Method { get; set; }
		public byte[] Payload { get; set; }
	}

	public class CanisterCallResponse
	{
		public byte[] Body { get; set; }
	}

	public class GetSubnetIdRequest
	{
		public Principal CanisterId { get; set; }
	}

	public class GetSubnetIdResponse
	{
		public Principal SubnetId { get; set; }
	}

	public class GetCyclesBalanceRequest
	{
		public Principal CanisterId { get; set; }
	}

	public class GetCyclesBalanceResponse
	{
		public ulong Cycles { get; set; }
	}

	public class AddCyclesRequest
	{
		public Principal CanisterId { get; set; }
		public ulong Amount { get; set; }
	}

	public class AddCyclesResponse
	{
		public ulong Cycles { get; set; }
	}

	public class SetStableMemoryRequest
	{
		public Principal CanisterId { get; set; }
		public string BlobId { get; set; }
	}

	public class GetStableMemoryRequest
	{
		public Principal CanisterId { get; set; }
	}

	public class GetStableMemoryResponse
	{
		public byte[] Blob { get; set; }
	}
}
