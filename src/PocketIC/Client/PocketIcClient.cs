using System.Text;
using System.Text.Json;
using EdjCase.ICP.Candid.Models;
using EdjCase.ICP.Candid;
using System.Text.Json.Nodes;

namespace EdjCase.ICP.PocketIC.Client
{
	public class PocketIcClient : IAsyncDisposable
	{
		private readonly HttpClient httpClient;
		private readonly string baseUrl;
		private readonly TimeSpan processingTimeout;
		private readonly int instanceId;
		private readonly CandidConverter candidConverter;
		private readonly Dictionary<string, SubnetTopology> topology;

		private PocketIcClient(
			HttpClient httpClient,
			string url,
			int instanceId,
			TimeSpan processingTimeout,
			Dictionary<string, SubnetTopology> topology,
			CandidConverter? candidConverter = null)
		{
			this.httpClient = httpClient;
			this.baseUrl = url;
			this.instanceId = instanceId;
			this.processingTimeout = processingTimeout;
			this.candidConverter = candidConverter ?? CandidConverter.Default;
			this.topology = topology;
		}



		public async Task TickAsync()
		{
			await this.PostAsync<object>("/update/tick", null);
		}

		public async Task<byte[]> GetPublicKeyAsync(Principal subnetId)
		{
			var request = new
			{
				subnet_id = subnetId.Raw
			};
			return await this.PostAsync<byte[]>("/read/pub_key", request);
		}

		public Dictionary<string, SubnetTopology> GetTopology()
		{
			return this.topology;
		}

		public async Task<ICTimestamp> GetTimeAsync()
		{
			var response = await this.GetAsync<GetTimeResponse>("/read/get_time");
			return ICTimestamp.FromNanoSeconds(response.NanosSinceEpoch);
		}

		public async Task SetTimeAsync(ICTimestamp timestamp)
		{
			if (!timestamp.NanoSeconds.TryToUInt64(out ulong nanosSinceEpoch))
			{
				throw new ArgumentException("Nanoseconds is too large to convert to ulong");
			}
			var request = new
			{
				nanos_since_epoch = nanosSinceEpoch
			};
			await this.PostAsync<object>("/update/set_time", request);
		}

		public async Task<TResponse> QueryCallAsync<TRequest, TResponse>(
			Principal sender,
			Principal canisterId,
			string method,
			TRequest request,
			EffectivePrincipal? effectivePrincipal = null)
			where TRequest : notnull
		{
			return await this.RequestInternalAsync<TRequest, TResponse>(
				"/read/query",
				sender,
				canisterId,
				method,
				request,
				effectivePrincipal
			);
		}

		public async Task<TResponse> UpdateCallAsync<TRequest, TResponse>(
			Principal sender,
			Principal canisterId,
			string method,
			TRequest request,
			EffectivePrincipal? effectivePrincipal = null)
			where TRequest : notnull
		{
			return await this.RequestInternalAsync<TRequest, TResponse>(
				"/update/execute_ingress_message",
				sender,
				canisterId,
				method,
				request,
				effectivePrincipal
			);
		}

		private async Task<TResponse> RequestInternalAsync<TRequest, TResponse>(
			string route,
			Principal sender,
			Principal canisterId,
			string method,
			TRequest request,
			EffectivePrincipal? effectivePrincipal = null)
			where TRequest : notnull
		{
			CandidTypedValue requestValue = this.candidConverter.FromTypedObject(request);
			CandidArg arg = CandidArg.FromCandid(requestValue);
			byte[] payload = arg.Encode();
			var effectivePrincipalJson = effectivePrincipal == null ? "None" : (effectivePrincipal.Type == EffectivePrincipalType.Subnet ? new
			{
				SubnetId = effectivePrincipal.Id.Raw
			} : new
			{
				CanisterId = effectivePrincipal.Id.Raw
			});
			var options = new
			{
				canister_id = canisterId.Raw,
				effective_principal = effectivePrincipalJson,
				method = method,
				payload = payload,
				sender = sender.Raw,
			};
			var response = await this.PostAsync<CanisterCallResponse>(route, options);
			CandidArg candidResponse = CandidArg.FromBytes(response.Body);
			return candidResponse.ToObjects<TResponse>();
		}

		public async Task<Principal?> GetCanisterSubnetIdAsync(Principal canisterId)
		{
			var request = new
			{
				canister_id = canisterId.Raw
			};
			var response = await this.PostAsync<GetSubnetIdResponse>("/read/get_subnet", request);
			return response.SubnetId;
		}

		public async Task<ulong> GetCyclesBalanceAsync(Principal canisterId)
		{
			var request = new
			{
				canister_id = canisterId.Raw
			};
			var response = await this.PostAsync<GetCyclesBalanceResponse>("/read/get_cycles", request);
			return response.Cycles;
		}

		public async Task<ulong> AddCyclesAsync(Principal canisterId, ulong amount)
		{
			var request = new
			{
				canister_id = canisterId.Raw,
				amount = amount
			};
			var response = await this.PostAsync<AddCyclesResponse>("/update/add_cycles", request);
			return response.Cycles;
		}

		public async Task SetStableMemoryAsync(Principal canisterId, byte[] memory)
		{
			var blobId = await this.UploadBlobAsync(memory);
			var request = new
			{
				canister_id = canisterId.Raw,
				blob_id = blobId
			};
			await this.PostAsync<object>("/update/set_stable_memory", request);
		}

		public async Task<byte[]> GetStableMemoryAsync(Principal canisterId)
		{
			var request = new
			{
				canister_id = canisterId
			};
			var response = await this.PostAsync<GetStableMemoryResponse>("/read/get_stable_memory", request);
			return response.Blob;
		}

		private async Task<UploadBlobResponse> UploadBlobAsync(byte[] blob)
		{
			var content = new ByteArrayContent(blob);
			var response = await this.httpClient.PostAsync($"{this.baseUrl}/blobstore", content);
			response.EnsureSuccessStatusCode();
			var responseContent = await response.Content.ReadAsStringAsync();
			return JsonSerializer.Deserialize<UploadBlobResponse>(responseContent)!;
		}

		private async Task<T> GetAsync<T>(string endpoint)
		{
			var response = await this.httpClient.GetAsync($"{this.baseUrl}/instances/{this.instanceId}{endpoint}");
			response.EnsureSuccessStatusCode();
			var content = await response.Content.ReadAsStringAsync();
			return JsonSerializer.Deserialize<T>(content)!;
		}

		private async Task<T> PostAsync<T>(string endpoint, object? data)
		{
			string url = $"{this.baseUrl}/instances/{this.instanceId}{endpoint}";
			return await PocketIcClient.PostAsync<T>(this.httpClient, url, data);
		}

		private static async Task<T> PostAsync<T>(HttpClient httpClient, string url, object? data)
		{
			var json = data != null ? JsonSerializer.Serialize(data) : "";
			Console.WriteLine("POST " + url);
			Console.WriteLine(json);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await httpClient.PostAsync(url, content);
			response.EnsureSuccessStatusCode();
			var responseContent = await response.Content.ReadAsStringAsync();
			return JsonSerializer.Deserialize<T>(responseContent)!;
		}

		public async ValueTask DisposeAsync()
		{
			try
			{
				HttpResponseMessage message = await this.httpClient.DeleteAsync($"{this.baseUrl}/instances/{this.instanceId}");
				message.EnsureSuccessStatusCode();
			}
			catch (Exception e)
			{
				throw new Exception("Failed to delete PocketIC instance", e);
			}
		}


		public static async Task<PocketIcClient> CreateAsync(
			string url,
			List<SubnetConfig>? applicationSubnets = null,
			SubnetConfig? bitcoinSubnet = null,
			SubnetConfig? fiduciarySubnet = null,
			SubnetConfig? iiSubnet = null,
			SubnetConfig? nnsSubnet = null,
			SubnetConfig? snsSubnet = null,
			List<SubnetConfig>? systemSubnets = null,
			List<SubnetConfig>? verifiedApplicationSubnets = null,
			bool nonmainnetFeatures = false,
			TimeSpan? processingTimeout = null,
			CandidConverter? candidConverter = null
		)
		{
			var timeout = processingTimeout ?? TimeSpan.FromSeconds(30);

			// Default to a single application subnet
			applicationSubnets ??= new List<SubnetConfig>
			{
				new SubnetConfig
				{
					State = SubnetStateConfig.New()
				}
			};

			object? MapSubnet(SubnetConfig? subnetConfig)
			{
				if (subnetConfig == null)
				{
					return null;
				}
				object stateConfig;
				switch (subnetConfig.State.Type)
				{
					case SubnetStateType.New:
						stateConfig = "New";
						break;
					case SubnetStateType.FromPath:
						stateConfig = new
						{
							FromPath = new List<object> {
								subnetConfig.State.Path!,
								new {
									subnet_id = Convert.ToBase64String(subnetConfig.State.SubnetId!.Raw)
								}
							}
						};
						break;
					default:
						throw new NotImplementedException();
				}
				return new
				{
					dts_flag = subnetConfig.EnableDeterministicTimeSlicing == false ? "Disabled" : "Enabled",
					instruction_config = subnetConfig.EnableBenchmarkingInstructionLimits == true ? "Benchmarking" : "Production",
					state_config = stateConfig
				};
			}

			List<object> MapSubnets(List<SubnetConfig>? subnets)
			{
				if (subnets == null)
				{
					return [];
				}
				return subnets.Select(s => MapSubnet(s)!).ToList();
			}


			var request = new
			{
				subnet_config_set = new
				{
					application = MapSubnets(applicationSubnets),
					bitcoin = MapSubnet(bitcoinSubnet),
					fiduciary = MapSubnet(fiduciarySubnet),
					ii = MapSubnet(iiSubnet),
					nns = MapSubnet(nnsSubnet),
					sns = MapSubnet(snsSubnet),
					system = MapSubnets(systemSubnets),
					verified_application = MapSubnets(verifiedApplicationSubnets)
				},
				nonmainnet_features = nonmainnetFeatures
			};

			var httpClient = new HttpClient();
			var response = await PostAsync<CreateInstanceResponse>(httpClient, $"{url}/instances", request);

			if (response.Error != null)
			{
				throw new Exception($"Failed to create PocketIC instance: {response.Error.Message}");
			}

			var client = new PocketIcClient(
				httpClient,
				url,
				response.Created!.InstanceId,
				timeout,
				response.Created!.Topology,
				candidConverter
			);
			return client;
		}
	}


	public class SubnetTopology
	{
		public required Principal Id { get; set; }
		public required SubnetType Type { get; set; }
		public required int Size { get; set; }
		public required List<CanisterRange> CanisterRanges { get; set; }
	}

	public class CanisterRange
	{
		public required Principal Start { get; set; }
		public required Principal End { get; set; }
	}

	public enum SubnetType
	{
		Application,
		Bitcoin,
		Fiduciary,
		InternetIdentity,
		NNS,
		SNS,
		System
	}
}