using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Models;

namespace EdjCase.ICP.PocketIC
{
	public class PocketIcClient : IDisposable
	{
		private readonly HttpClient httpClient;
		private readonly string baseUrl;
		private readonly TimeSpan processingTimeout;
		private bool isInstanceDeleted = false;

		public PocketIcClient(string url, TimeSpan processingTimeout)
		{
			this.httpClient = new HttpClient();
			this.baseUrl = url;
			this.processingTimeout = processingTimeout;
		}

		public static async Task<PocketIcClient> CreateAsync(string url, TimeSpan? processingTimeout = null)
		{
			var timeout = processingTimeout ?? TimeSpan.FromSeconds(30);
			var client = new PocketIcClient(url, timeout);

			// Here you might want to add any initialization logic, 
			// such as creating an instance on the server

			return client;
		}

		public async Task DeleteInstanceAsync()
		{
			this.AssertInstanceNotDeleted();
			await this.httpClient.DeleteAsync($"{this.baseUrl}/instances");
			this.isInstanceDeleted = true;
		}

		public async Task TickAsync()
		{
			await this.PostAsync<object>("/update/tick", null);
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

		public async Task<Principal> CreateCanisterAsync(CreateCanisterOptions? options = null)
		{
			options ??= new CreateCanisterOptions();
			var response = await this.PostAsync<CreateCanisterResponse>("/update/create_canister", options);
			return response.CanisterId;
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
			var json = data != null ? JsonSerializer.Serialize(data) : "";
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			Console.WriteLine($"{this.baseUrl}{endpoint}");
			var response = await this.httpClient.PostAsync($"{this.baseUrl}{endpoint}", content);
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
	public class GetTimeResponse
	{
		public ulong NanosSinceEpoch { get; set; }
	}

	public class SetTimeRequest
	{
		public ulong NanosSinceEpoch { get; set; }
	}

	public class CreateCanisterOptions
	{
		public Principal? Sender { get; set; }
		public ulong? Cycles { get; set; }
		public List<Principal>? Controllers { get; set; }
		public ulong? ComputeAllocation { get; set; }
		public ulong? MemoryAllocation { get; set; }
		public ulong? FreezingThreshold { get; set; }
		public ulong? ReservedCyclesLimit { get; set; }
		public Principal? TargetSubnetId { get; set; }
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
