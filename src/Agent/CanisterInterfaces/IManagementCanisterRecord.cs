using EdjCase.ICP.Candid.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdjCase.ICP.Agent.CanisterInterfaces
{
	public interface IManagementCanisterRecord
	{
		/// <summary>
		/// As a provisional method on development instances, the provisional_create_canister_with_cycles method is provided. It behaves as create_canister, but initializes the canister’s balance with amount fresh cycles (using MAX_CANISTER_BALANCE if amount = null, else capping the balance at MAX_CANISTER_BALANCE).
		/// Cycles added to this call via ic0.call_cycles_add128 are returned to the caller.
		/// This method is only available in local development instances.
		/// </summary>
		/// <param name="settings">Optional. Settings for canister creation</param>
		/// <param name="cyclesToAdd">Optional. Adds cycles to the balance of canister identified by amount (implicitly capping it at MAX_CANISTER_BALANCE)</param>
		/// <returns></returns>
		Task<Principal> ProvisionalCreateCanisterWithCyclesAsync(CanisterSettings? settings = null, ulong cyclesToAdd = 0);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="type"></param>
		/// <param name="canisterId"></param>
		/// <param name="wasmModule"></param>
		/// <param name="argument"></param>
		/// <returns></returns>
		Task InstallCodeAsync(InstallType type, Principal canisterId, byte[] wasmModule, byte[] argument);
	}

	public enum InstallType
	{
		/// <summary>
		/// The canister must be empty before.
		/// This will instantiate the canister module and invoke its canister_init method (if present),
		/// passing the arg to the canister.
		/// </summary>
		Install,
		Reinstall,
		Upgrade
	}

	public class CanisterSettings
	{

		/// <summary>
		/// A list of principals.
		/// Must be between 0 and 10 in size.
		/// This value is assigned to the controllers attribute of the canister.
		/// Default value: A list containing only the caller of the create_canister call.
		/// </summary>
		public List<Principal>? Controllers { get; }
		/// <summary>
		/// Must be a number between 0 and 100, inclusively.
		/// It indicates how much compute power should be guaranteed to this canister, expressed as a percentage 
		/// of the maximum compute power that a single canister can allocate. If the IC cannot provide the requested 
		/// allocation, for example because it is oversubscribed, the call will be rejected.
		/// Default value: 0
		/// </summary>
		public ulong? ComputeAllocation { get; }
		/// <summary>
		/// Must be a number between 0 and 2^48 (i.e 256TB), inclusively.
		/// It indicates how much memory the canister is allowed to use in total.
		/// Any attempt to grow memory usage beyond this allocation will fail.
		/// If the IC cannot provide the requested allocation, for example because it is oversubscribed, the call will be rejected
		/// If set to 0, then memory growth of the canister will be best-effort and subject to the available memory on the IC.
		/// Default value: 0
		/// </summary>
		public ulong? MemoryAllocation { get; }
		/// <summary>
		/// Must be a number between 0 and 2^64-1, inclusively, and indicates a length of time in seconds.
		/// A canister is considered frozen whenever the IC estimates that the canister would be depleted of cycles before 
		/// freezing_threshold seconds pass, given the canister’s current size and the IC’s current cost for storage.
		/// Calls to a frozen canister will be rejected(like for a stopping canister). Additionally, a canister cannot 
		/// perform calls if that would, due the cost of the call and transferred cycles, would push the balance into 
		/// frozen territory; these calls fail with ic0.call_perform returning a non-zero error code.
		/// Default value: 2592000 (approximately 30 days).
		/// </summary>
		public ulong? FreezingThreshold { get; }


		public CanisterSettings(
			List<Principal>? controllers = null,
			ulong? computeAllocation = null,
			ulong? memoryAllocation = null,
			ulong? freezingThreshold = null)
		{
			this.Controllers = controllers;
			this.ComputeAllocation = computeAllocation;
			this.MemoryAllocation = memoryAllocation;
			this.FreezingThreshold = freezingThreshold;
		}
	}
}
