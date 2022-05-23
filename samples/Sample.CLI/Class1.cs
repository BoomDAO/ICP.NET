
global using Token = EdjCase.ICP.Candid.Models.Principal;
global using OrderId = System.UInt32;
using EdjCase.ICP.Agent.Agents;
using EdjCase.ICP.Agent.Responses;
using EdjCase.ICP.Candid.Models;
using EdjCase.ICP.Candid.Models.Types;
using EdjCase.ICP.Candid.Models.Values;

namespace EdjCase.ICP.Clients.Models
{
	public class AccountIdentifier
	{
		public System.Collections.Generic.List<byte> hash { get; set; }

	}
}


namespace EdjCase.ICP.Clients.Models
{
	public enum ActionType
	{
		RegisterKnownNeuron,
		ManageNeuron,
		ExecuteNnsFunction,
		RewardNodeProvider,
		SetDefaultFollowees,
		RewardNodeProviders,
		ManageNetworkEconomics,
		ApproveGenesisKyc,
		AddOrRemoveNodeProvider,
		Motion,
	}
	public class Action
	{
		public ActionType Type { get; }
		private readonly object? value;

		public Action(ActionType type, object? value)
		{
			this.Type = type;
			this.value = value;
		}

		public static Action RegisterKnownNeuron(EdjCase.ICP.Clients.Models.KnownNeuron info)
		{
			return new Action(ActionType.RegisterKnownNeuron, info);
		}

		public EdjCase.ICP.Clients.Models.KnownNeuron AsRegisterKnownNeuron()
		{
			this.ValidateType(ActionType.RegisterKnownNeuron);
			return (EdjCase.ICP.Clients.Models.KnownNeuron)this.value!;
		}

		public static Action ManageNeuron(EdjCase.ICP.Clients.Models.ManageNeuron info)
		{
			return new Action(ActionType.ManageNeuron, info);
		}

		public EdjCase.ICP.Clients.Models.ManageNeuron AsManageNeuron()
		{
			this.ValidateType(ActionType.ManageNeuron);
			return (EdjCase.ICP.Clients.Models.ManageNeuron)this.value!;
		}

		public static Action ExecuteNnsFunction(EdjCase.ICP.Clients.Models.ExecuteNnsFunction info)
		{
			return new Action(ActionType.ExecuteNnsFunction, info);
		}

		public EdjCase.ICP.Clients.Models.ExecuteNnsFunction AsExecuteNnsFunction()
		{
			this.ValidateType(ActionType.ExecuteNnsFunction);
			return (EdjCase.ICP.Clients.Models.ExecuteNnsFunction)this.value!;
		}

		public static Action RewardNodeProvider(EdjCase.ICP.Clients.Models.RewardNodeProvider info)
		{
			return new Action(ActionType.RewardNodeProvider, info);
		}

		public EdjCase.ICP.Clients.Models.RewardNodeProvider AsRewardNodeProvider()
		{
			this.ValidateType(ActionType.RewardNodeProvider);
			return (EdjCase.ICP.Clients.Models.RewardNodeProvider)this.value!;
		}

		public static Action SetDefaultFollowees(EdjCase.ICP.Clients.Models.SetDefaultFollowees info)
		{
			return new Action(ActionType.SetDefaultFollowees, info);
		}

		public EdjCase.ICP.Clients.Models.SetDefaultFollowees AsSetDefaultFollowees()
		{
			this.ValidateType(ActionType.SetDefaultFollowees);
			return (EdjCase.ICP.Clients.Models.SetDefaultFollowees)this.value!;
		}

		public static Action RewardNodeProviders(EdjCase.ICP.Clients.Models.RewardNodeProviders info)
		{
			return new Action(ActionType.RewardNodeProviders, info);
		}

		public EdjCase.ICP.Clients.Models.RewardNodeProviders AsRewardNodeProviders()
		{
			this.ValidateType(ActionType.RewardNodeProviders);
			return (EdjCase.ICP.Clients.Models.RewardNodeProviders)this.value!;
		}

		public static Action ManageNetworkEconomics(EdjCase.ICP.Clients.Models.NetworkEconomics info)
		{
			return new Action(ActionType.ManageNetworkEconomics, info);
		}

		public EdjCase.ICP.Clients.Models.NetworkEconomics AsManageNetworkEconomics()
		{
			this.ValidateType(ActionType.ManageNetworkEconomics);
			return (EdjCase.ICP.Clients.Models.NetworkEconomics)this.value!;
		}

		public static Action ApproveGenesisKyc(EdjCase.ICP.Clients.Models.ApproveGenesisKyc info)
		{
			return new Action(ActionType.ApproveGenesisKyc, info);
		}

		public EdjCase.ICP.Clients.Models.ApproveGenesisKyc AsApproveGenesisKyc()
		{
			this.ValidateType(ActionType.ApproveGenesisKyc);
			return (EdjCase.ICP.Clients.Models.ApproveGenesisKyc)this.value!;
		}

		public static Action AddOrRemoveNodeProvider(EdjCase.ICP.Clients.Models.AddOrRemoveNodeProvider info)
		{
			return new Action(ActionType.AddOrRemoveNodeProvider, info);
		}

		public EdjCase.ICP.Clients.Models.AddOrRemoveNodeProvider AsAddOrRemoveNodeProvider()
		{
			this.ValidateType(ActionType.AddOrRemoveNodeProvider);
			return (EdjCase.ICP.Clients.Models.AddOrRemoveNodeProvider)this.value!;
		}

		public static Action Motion(EdjCase.ICP.Clients.Models.Motion info)
		{
			return new Action(ActionType.Motion, info);
		}

		public EdjCase.ICP.Clients.Models.Motion AsMotion()
		{
			this.ValidateType(ActionType.Motion);
			return (EdjCase.ICP.Clients.Models.Motion)this.value!;
		}

		private void ValidateType(ActionType type)
		{
			if (this.Type != type)
			{
				throw new InvalidOperationException($"Cannot cast '{this.Type}' to type '{type}'");
			}
		}
	}
}


namespace EdjCase.ICP.Clients.Models
{
	public class AddHotKey
	{
		public EdjCase.ICP.Candid.Models.Principal? new_hot_key { get; set; }

	}
}


namespace EdjCase.ICP.Clients.Models
{
	public class AddOrRemoveNodeProvider
	{
		public EdjCase.ICP.Clients.Models.Change? change { get; set; }

	}
}


namespace EdjCase.ICP.Clients.Models
{
	public class Amount
	{
		public ulong e8s { get; set; }

	}
}


namespace EdjCase.ICP.Clients.Models
{
	public class ApproveGenesisKyc
	{
		public System.Collections.Generic.List<EdjCase.ICP.Candid.Models.Principal> principals { get; set; }

	}
}


namespace EdjCase.ICP.Clients.Models
{
	public class Ballot
	{
		public int vote { get; set; }

		public ulong voting_power { get; set; }

	}
}


namespace EdjCase.ICP.Clients.Models
{
	public class BallotInfo
	{
		public int vote { get; set; }

		public EdjCase.ICP.Clients.Models.NeuronId? proposal_id { get; set; }

	}
}


namespace EdjCase.ICP.Clients.Models
{
	public enum ByType
	{
		NeuronIdOrSubaccount,
		MemoAndController,
		Memo,
	}
	public class By
	{
		public ByType Type { get; }
		private readonly object? value;

		public By(ByType type, object? value)
		{
			this.Type = type;
			this.value = value;
		}

		public static By NeuronIdOrSubaccount(NeuronIdOrSubaccountInfo info)
		{
			return new By(ByType.NeuronIdOrSubaccount, info);
		}

		public NeuronIdOrSubaccountInfo AsNeuronIdOrSubaccount()
		{
			this.ValidateType(ByType.NeuronIdOrSubaccount);
			return (NeuronIdOrSubaccountInfo)this.value!;
		}

		public static By MemoAndController(EdjCase.ICP.Clients.Models.ClaimOrRefreshNeuronFromAccount info)
		{
			return new By(ByType.MemoAndController, info);
		}

		public EdjCase.ICP.Clients.Models.ClaimOrRefreshNeuronFromAccount AsMemoAndController()
		{
			this.ValidateType(ByType.MemoAndController);
			return (EdjCase.ICP.Clients.Models.ClaimOrRefreshNeuronFromAccount)this.value!;
		}

		public static By Memo(ulong info)
		{
			return new By(ByType.Memo, info);
		}

		public ulong AsMemo()
		{
			this.ValidateType(ByType.Memo);
			return (ulong)this.value!;
		}

		private void ValidateType(ByType type)
		{
			if (this.Type != type)
			{
				throw new InvalidOperationException($"Cannot cast '{this.Type}' to type '{type}'");
			}
		}
		public class NeuronIdOrSubaccountInfo
		{
		}
	}
}


namespace EdjCase.ICP.Clients.Models
{
	public enum ChangeType
	{
		ToRemove,
		ToAdd,
	}
	public class Change
	{
		public ChangeType Type { get; }
		private readonly object? value;

		public Change(ChangeType type, object? value)
		{
			this.Type = type;
			this.value = value;
		}

		public static Change ToRemove(EdjCase.ICP.Clients.Models.NodeProvider info)
		{
			return new Change(ChangeType.ToRemove, info);
		}

		public EdjCase.ICP.Clients.Models.NodeProvider AsToRemove()
		{
			this.ValidateType(ChangeType.ToRemove);
			return (EdjCase.ICP.Clients.Models.NodeProvider)this.value!;
		}

		public static Change ToAdd(EdjCase.ICP.Clients.Models.NodeProvider info)
		{
			return new Change(ChangeType.ToAdd, info);
		}

		public EdjCase.ICP.Clients.Models.NodeProvider AsToAdd()
		{
			this.ValidateType(ChangeType.ToAdd);
			return (EdjCase.ICP.Clients.Models.NodeProvider)this.value!;
		}

		private void ValidateType(ChangeType type)
		{
			if (this.Type != type)
			{
				throw new InvalidOperationException($"Cannot cast '{this.Type}' to type '{type}'");
			}
		}
	}
}


namespace EdjCase.ICP.Clients.Models
{
	public class ClaimOrRefresh
	{
		public EdjCase.ICP.Clients.Models.By? by { get; set; }

	}
}


namespace EdjCase.ICP.Clients.Models
{
	public class ClaimOrRefreshNeuronFromAccount
	{
		public EdjCase.ICP.Candid.Models.Principal? controller { get; set; }

		public ulong memo { get; set; }

	}
}


namespace EdjCase.ICP.Clients.Models
{
	public class ClaimOrRefreshNeuronFromAccountResponse
	{
		public EdjCase.ICP.Clients.Models.Result_1? result { get; set; }

	}
}


namespace EdjCase.ICP.Clients.Models
{
	public class ClaimOrRefreshResponse
	{
		public EdjCase.ICP.Clients.Models.NeuronId? refreshed_neuron_id { get; set; }

	}
}


namespace EdjCase.ICP.Clients.Models
{
	public enum CommandType
	{
		Spawn,
		Split,
		Follow,
		ClaimOrRefresh,
		Configure,
		RegisterVote,
		Merge,
		DisburseToNeuron,
		MakeProposal,
		MergeMaturity,
		Disburse,
	}
	public class Command
	{
		public CommandType Type { get; }
		private readonly object? value;

		public Command(CommandType type, object? value)
		{
			this.Type = type;
			this.value = value;
		}

		public static Command Spawn(EdjCase.ICP.Clients.Models.Spawn info)
		{
			return new Command(CommandType.Spawn, info);
		}

		public EdjCase.ICP.Clients.Models.Spawn AsSpawn()
		{
			this.ValidateType(CommandType.Spawn);
			return (EdjCase.ICP.Clients.Models.Spawn)this.value!;
		}

		public static Command Split(EdjCase.ICP.Clients.Models.Split info)
		{
			return new Command(CommandType.Split, info);
		}

		public EdjCase.ICP.Clients.Models.Split AsSplit()
		{
			this.ValidateType(CommandType.Split);
			return (EdjCase.ICP.Clients.Models.Split)this.value!;
		}

		public static Command Follow(EdjCase.ICP.Clients.Models.Follow info)
		{
			return new Command(CommandType.Follow, info);
		}

		public EdjCase.ICP.Clients.Models.Follow AsFollow()
		{
			this.ValidateType(CommandType.Follow);
			return (EdjCase.ICP.Clients.Models.Follow)this.value!;
		}

		public static Command ClaimOrRefresh(EdjCase.ICP.Clients.Models.ClaimOrRefresh info)
		{
			return new Command(CommandType.ClaimOrRefresh, info);
		}

		public EdjCase.ICP.Clients.Models.ClaimOrRefresh AsClaimOrRefresh()
		{
			this.ValidateType(CommandType.ClaimOrRefresh);
			return (EdjCase.ICP.Clients.Models.ClaimOrRefresh)this.value!;
		}

		public static Command Configure(EdjCase.ICP.Clients.Models.Configure info)
		{
			return new Command(CommandType.Configure, info);
		}

		public EdjCase.ICP.Clients.Models.Configure AsConfigure()
		{
			this.ValidateType(CommandType.Configure);
			return (EdjCase.ICP.Clients.Models.Configure)this.value!;
		}

		public static Command RegisterVote(EdjCase.ICP.Clients.Models.RegisterVote info)
		{
			return new Command(CommandType.RegisterVote, info);
		}

		public EdjCase.ICP.Clients.Models.RegisterVote AsRegisterVote()
		{
			this.ValidateType(CommandType.RegisterVote);
			return (EdjCase.ICP.Clients.Models.RegisterVote)this.value!;
		}

		public static Command Merge(EdjCase.ICP.Clients.Models.Merge info)
		{
			return new Command(CommandType.Merge, info);
		}

		public EdjCase.ICP.Clients.Models.Merge AsMerge()
		{
			this.ValidateType(CommandType.Merge);
			return (EdjCase.ICP.Clients.Models.Merge)this.value!;
		}

		public static Command DisburseToNeuron(EdjCase.ICP.Clients.Models.DisburseToNeuron info)
		{
			return new Command(CommandType.DisburseToNeuron, info);
		}

		public EdjCase.ICP.Clients.Models.DisburseToNeuron AsDisburseToNeuron()
		{
			this.ValidateType(CommandType.DisburseToNeuron);
			return (EdjCase.ICP.Clients.Models.DisburseToNeuron)this.value!;
		}

		public static Command MakeProposal(EdjCase.ICP.Clients.Models.Proposal info)
		{
			return new Command(CommandType.MakeProposal, info);
		}

		public EdjCase.ICP.Clients.Models.Proposal AsMakeProposal()
		{
			this.ValidateType(CommandType.MakeProposal);
			return (EdjCase.ICP.Clients.Models.Proposal)this.value!;
		}

		public static Command MergeMaturity(EdjCase.ICP.Clients.Models.MergeMaturity info)
		{
			return new Command(CommandType.MergeMaturity, info);
		}

		public EdjCase.ICP.Clients.Models.MergeMaturity AsMergeMaturity()
		{
			this.ValidateType(CommandType.MergeMaturity);
			return (EdjCase.ICP.Clients.Models.MergeMaturity)this.value!;
		}

		public static Command Disburse(EdjCase.ICP.Clients.Models.Disburse info)
		{
			return new Command(CommandType.Disburse, info);
		}

		public EdjCase.ICP.Clients.Models.Disburse AsDisburse()
		{
			this.ValidateType(CommandType.Disburse);
			return (EdjCase.ICP.Clients.Models.Disburse)this.value!;
		}

		private void ValidateType(CommandType type)
		{
			if (this.Type != type)
			{
				throw new InvalidOperationException($"Cannot cast '{this.Type}' to type '{type}'");
			}
		}
	}
}


namespace EdjCase.ICP.Clients.Models
{
	public enum Command_1Type
	{
		Error,
		Spawn,
		Split,
		Follow,
		ClaimOrRefresh,
		Configure,
		RegisterVote,
		Merge,
		DisburseToNeuron,
		MakeProposal,
		MergeMaturity,
		Disburse,
	}
	public class Command_1
	{
		public Command_1Type Type { get; }
		private readonly object? value;

		public Command_1(Command_1Type type, object? value)
		{
			this.Type = type;
			this.value = value;
		}

		public static Command_1 Error(EdjCase.ICP.Clients.Models.GovernanceError info)
		{
			return new Command_1(Command_1Type.Error, info);
		}

		public EdjCase.ICP.Clients.Models.GovernanceError AsError()
		{
			this.ValidateType(Command_1Type.Error);
			return (EdjCase.ICP.Clients.Models.GovernanceError)this.value!;
		}

		public static Command_1 Spawn(EdjCase.ICP.Clients.Models.SpawnResponse info)
		{
			return new Command_1(Command_1Type.Spawn, info);
		}

		public EdjCase.ICP.Clients.Models.SpawnResponse AsSpawn()
		{
			this.ValidateType(Command_1Type.Spawn);
			return (EdjCase.ICP.Clients.Models.SpawnResponse)this.value!;
		}

		public static Command_1 Split(EdjCase.ICP.Clients.Models.SpawnResponse info)
		{
			return new Command_1(Command_1Type.Split, info);
		}

		public EdjCase.ICP.Clients.Models.SpawnResponse AsSplit()
		{
			this.ValidateType(Command_1Type.Split);
			return (EdjCase.ICP.Clients.Models.SpawnResponse)this.value!;
		}

		public static Command_1 Follow(FollowInfo info)
		{
			return new Command_1(Command_1Type.Follow, info);
		}

		public FollowInfo AsFollow()
		{
			this.ValidateType(Command_1Type.Follow);
			return (FollowInfo)this.value!;
		}

		public static Command_1 ClaimOrRefresh(EdjCase.ICP.Clients.Models.ClaimOrRefreshResponse info)
		{
			return new Command_1(Command_1Type.ClaimOrRefresh, info);
		}

		public EdjCase.ICP.Clients.Models.ClaimOrRefreshResponse AsClaimOrRefresh()
		{
			this.ValidateType(Command_1Type.ClaimOrRefresh);
			return (EdjCase.ICP.Clients.Models.ClaimOrRefreshResponse)this.value!;
		}

		public static Command_1 Configure(ConfigureInfo info)
		{
			return new Command_1(Command_1Type.Configure, info);
		}

		public ConfigureInfo AsConfigure()
		{
			this.ValidateType(Command_1Type.Configure);
			return (ConfigureInfo)this.value!;
		}

		public static Command_1 RegisterVote(RegisterVoteInfo info)
		{
			return new Command_1(Command_1Type.RegisterVote, info);
		}

		public RegisterVoteInfo AsRegisterVote()
		{
			this.ValidateType(Command_1Type.RegisterVote);
			return (RegisterVoteInfo)this.value!;
		}

		public static Command_1 Merge(MergeInfo info)
		{
			return new Command_1(Command_1Type.Merge, info);
		}

		public MergeInfo AsMerge()
		{
			this.ValidateType(Command_1Type.Merge);
			return (MergeInfo)this.value!;
		}

		public static Command_1 DisburseToNeuron(EdjCase.ICP.Clients.Models.SpawnResponse info)
		{
			return new Command_1(Command_1Type.DisburseToNeuron, info);
		}

		public EdjCase.ICP.Clients.Models.SpawnResponse AsDisburseToNeuron()
		{
			this.ValidateType(Command_1Type.DisburseToNeuron);
			return (EdjCase.ICP.Clients.Models.SpawnResponse)this.value!;
		}

		public static Command_1 MakeProposal(EdjCase.ICP.Clients.Models.MakeProposalResponse info)
		{
			return new Command_1(Command_1Type.MakeProposal, info);
		}

		public EdjCase.ICP.Clients.Models.MakeProposalResponse AsMakeProposal()
		{
			this.ValidateType(Command_1Type.MakeProposal);
			return (EdjCase.ICP.Clients.Models.MakeProposalResponse)this.value!;
		}

		public static Command_1 MergeMaturity(EdjCase.ICP.Clients.Models.MergeMaturityResponse info)
		{
			return new Command_1(Command_1Type.MergeMaturity, info);
		}

		public EdjCase.ICP.Clients.Models.MergeMaturityResponse AsMergeMaturity()
		{
			this.ValidateType(Command_1Type.MergeMaturity);
			return (EdjCase.ICP.Clients.Models.MergeMaturityResponse)this.value!;
		}

		public static Command_1 Disburse(EdjCase.ICP.Clients.Models.DisburseResponse info)
		{
			return new Command_1(Command_1Type.Disburse, info);
		}

		public EdjCase.ICP.Clients.Models.DisburseResponse AsDisburse()
		{
			this.ValidateType(Command_1Type.Disburse);
			return (EdjCase.ICP.Clients.Models.DisburseResponse)this.value!;
		}

		private void ValidateType(Command_1Type type)
		{
			if (this.Type != type)
			{
				throw new InvalidOperationException($"Cannot cast '{this.Type}' to type '{type}'");
			}
		}
		public class FollowInfo
		{
		}
		public class ConfigureInfo
		{
		}
		public class RegisterVoteInfo
		{
		}
		public class MergeInfo
		{
		}
	}
}


namespace EdjCase.ICP.Clients.Models
{
	public enum Command_2Type
	{
		Spawn,
		Split,
		Configure,
		Merge,
		DisburseToNeuron,
		ClaimOrRefreshNeuron,
		MergeMaturity,
		Disburse,
	}
	public class Command_2
	{
		public Command_2Type Type { get; }
		private readonly object? value;

		public Command_2(Command_2Type type, object? value)
		{
			this.Type = type;
			this.value = value;
		}

		public static Command_2 Spawn(EdjCase.ICP.Clients.Models.Spawn info)
		{
			return new Command_2(Command_2Type.Spawn, info);
		}

		public EdjCase.ICP.Clients.Models.Spawn AsSpawn()
		{
			this.ValidateType(Command_2Type.Spawn);
			return (EdjCase.ICP.Clients.Models.Spawn)this.value!;
		}

		public static Command_2 Split(EdjCase.ICP.Clients.Models.Split info)
		{
			return new Command_2(Command_2Type.Split, info);
		}

		public EdjCase.ICP.Clients.Models.Split AsSplit()
		{
			this.ValidateType(Command_2Type.Split);
			return (EdjCase.ICP.Clients.Models.Split)this.value!;
		}

		public static Command_2 Configure(EdjCase.ICP.Clients.Models.Configure info)
		{
			return new Command_2(Command_2Type.Configure, info);
		}

		public EdjCase.ICP.Clients.Models.Configure AsConfigure()
		{
			this.ValidateType(Command_2Type.Configure);
			return (EdjCase.ICP.Clients.Models.Configure)this.value!;
		}

		public static Command_2 Merge(EdjCase.ICP.Clients.Models.Merge info)
		{
			return new Command_2(Command_2Type.Merge, info);
		}

		public EdjCase.ICP.Clients.Models.Merge AsMerge()
		{
			this.ValidateType(Command_2Type.Merge);
			return (EdjCase.ICP.Clients.Models.Merge)this.value!;
		}

		public static Command_2 DisburseToNeuron(EdjCase.ICP.Clients.Models.DisburseToNeuron info)
		{
			return new Command_2(Command_2Type.DisburseToNeuron, info);
		}

		public EdjCase.ICP.Clients.Models.DisburseToNeuron AsDisburseToNeuron()
		{
			this.ValidateType(Command_2Type.DisburseToNeuron);
			return (EdjCase.ICP.Clients.Models.DisburseToNeuron)this.value!;
		}

		public static Command_2 ClaimOrRefreshNeuron(EdjCase.ICP.Clients.Models.ClaimOrRefresh info)
		{
			return new Command_2(Command_2Type.ClaimOrRefreshNeuron, info);
		}

		public EdjCase.ICP.Clients.Models.ClaimOrRefresh AsClaimOrRefreshNeuron()
		{
			this.ValidateType(Command_2Type.ClaimOrRefreshNeuron);
			return (EdjCase.ICP.Clients.Models.ClaimOrRefresh)this.value!;
		}

		public static Command_2 MergeMaturity(EdjCase.ICP.Clients.Models.MergeMaturity info)
		{
			return new Command_2(Command_2Type.MergeMaturity, info);
		}

		public EdjCase.ICP.Clients.Models.MergeMaturity AsMergeMaturity()
		{
			this.ValidateType(Command_2Type.MergeMaturity);
			return (EdjCase.ICP.Clients.Models.MergeMaturity)this.value!;
		}

		public static Command_2 Disburse(EdjCase.ICP.Clients.Models.Disburse info)
		{
			return new Command_2(Command_2Type.Disburse, info);
		}

		public EdjCase.ICP.Clients.Models.Disburse AsDisburse()
		{
			this.ValidateType(Command_2Type.Disburse);
			return (EdjCase.ICP.Clients.Models.Disburse)this.value!;
		}

		private void ValidateType(Command_2Type type)
		{
			if (this.Type != type)
			{
				throw new InvalidOperationException($"Cannot cast '{this.Type}' to type '{type}'");
			}
		}
	}
}


namespace EdjCase.ICP.Clients.Models
{
	public class Configure
	{
		public EdjCase.ICP.Clients.Models.Operation? operation { get; set; }

	}
}


namespace EdjCase.ICP.Clients.Models
{
	public class Disburse
	{
		public EdjCase.ICP.Clients.Models.AccountIdentifier? to_account { get; set; }

		public EdjCase.ICP.Clients.Models.Amount? amount { get; set; }

	}
}


namespace EdjCase.ICP.Clients.Models
{
	public class DisburseResponse
	{
		public ulong transfer_block_height { get; set; }

	}
}


namespace EdjCase.ICP.Clients.Models
{
	public class DisburseToNeuron
	{
		public ulong dissolve_delay_seconds { get; set; }

		public bool kyc_verified { get; set; }

		public ulong amount_e8s { get; set; }

		public EdjCase.ICP.Candid.Models.Principal? new_controller { get; set; }

		public ulong nonce { get; set; }

	}
}


namespace EdjCase.ICP.Clients.Models
{
	public enum DissolveStateType
	{
		DissolveDelaySeconds,
		WhenDissolvedTimestampSeconds,
	}
	public class DissolveState
	{
		public DissolveStateType Type { get; }
		private readonly object? value;

		public DissolveState(DissolveStateType type, object? value)
		{
			this.Type = type;
			this.value = value;
		}

		public static DissolveState DissolveDelaySeconds(ulong info)
		{
			return new DissolveState(DissolveStateType.DissolveDelaySeconds, info);
		}

		public ulong AsDissolveDelaySeconds()
		{
			this.ValidateType(DissolveStateType.DissolveDelaySeconds);
			return (ulong)this.value!;
		}

		public static DissolveState WhenDissolvedTimestampSeconds(ulong info)
		{
			return new DissolveState(DissolveStateType.WhenDissolvedTimestampSeconds, info);
		}

		public ulong AsWhenDissolvedTimestampSeconds()
		{
			this.ValidateType(DissolveStateType.WhenDissolvedTimestampSeconds);
			return (ulong)this.value!;
		}

		private void ValidateType(DissolveStateType type)
		{
			if (this.Type != type)
			{
				throw new InvalidOperationException($"Cannot cast '{this.Type}' to type '{type}'");
			}
		}
	}
}


namespace EdjCase.ICP.Clients.Models
{
	public class ExecuteNnsFunction
	{
		public int nns_function { get; set; }

		public System.Collections.Generic.List<byte> payload { get; set; }

	}
}


namespace EdjCase.ICP.Clients.Models
{
	public class Follow
	{
		public int topic { get; set; }

		public System.Collections.Generic.List<EdjCase.ICP.Clients.Models.NeuronId> followees { get; set; }

	}
}


namespace EdjCase.ICP.Clients.Models
{
	public class Followees
	{
		public System.Collections.Generic.List<EdjCase.ICP.Clients.Models.NeuronId> followees { get; set; }

	}
}


namespace EdjCase.ICP.Clients.Models
{
	public class Governance
	{
		public System.Collections.Generic.List<default_followeesInfo> default_followees { get; set; }

		public ulong wait_for_quiet_threshold_seconds { get; set; }

		public EdjCase.ICP.Clients.Models.GovernanceCachedMetrics? metrics { get; set; }

		public System.Collections.Generic.List<EdjCase.ICP.Clients.Models.NodeProvider> node_providers { get; set; }

		public EdjCase.ICP.Clients.Models.NetworkEconomics? economics { get; set; }

		public EdjCase.ICP.Clients.Models.RewardEvent? latest_reward_event { get; set; }

		public System.Collections.Generic.List<EdjCase.ICP.Clients.Models.NeuronStakeTransfer> to_claim_transfers { get; set; }

		public ulong short_voting_period_seconds { get; set; }

		public System.Collections.Generic.List<proposalsInfo> proposals { get; set; }

		public System.Collections.Generic.List<in_flight_commandsInfo> in_flight_commands { get; set; }

		public System.Collections.Generic.List<neuronsInfo> neurons { get; set; }

		public ulong genesis_timestamp_seconds { get; set; }

		public class default_followeesInfo
		{
			public int F0 { get; set; }

			public EdjCase.ICP.Clients.Models.Followees F1 { get; set; }

		}
		public class proposalsInfo
		{
			public ulong F0 { get; set; }

			public EdjCase.ICP.Clients.Models.ProposalData F1 { get; set; }

		}
		public class in_flight_commandsInfo
		{
			public ulong F0 { get; set; }

			public EdjCase.ICP.Clients.Models.NeuronInFlightCommand F1 { get; set; }

		}
		public class neuronsInfo
		{
			public ulong F0 { get; set; }

			public EdjCase.ICP.Clients.Models.Neuron F1 { get; set; }

		}
	}
}


namespace EdjCase.ICP.Clients.Models
{
	public class GovernanceCachedMetrics
	{
		public System.Collections.Generic.List<not_dissolving_neurons_e8s_bucketsInfo> not_dissolving_neurons_e8s_buckets { get; set; }

		public ulong garbage_collectable_neurons_count { get; set; }

		public ulong neurons_with_invalid_stake_count { get; set; }

		public System.Collections.Generic.List<not_dissolving_neurons_count_bucketsInfo> not_dissolving_neurons_count_buckets { get; set; }

		public ulong total_supply_icp { get; set; }

		public ulong neurons_with_less_than_6_months_dissolve_delay_count { get; set; }

		public ulong dissolved_neurons_count { get; set; }

		public ulong total_staked_e8s { get; set; }

		public ulong not_dissolving_neurons_count { get; set; }

		public ulong dissolved_neurons_e8s { get; set; }

		public ulong neurons_with_less_than_6_months_dissolve_delay_e8s { get; set; }

		public System.Collections.Generic.List<dissolving_neurons_count_bucketsInfo> dissolving_neurons_count_buckets { get; set; }

		public ulong dissolving_neurons_count { get; set; }

		public System.Collections.Generic.List<dissolving_neurons_e8s_bucketsInfo> dissolving_neurons_e8s_buckets { get; set; }

		public ulong community_fund_total_staked_e8s { get; set; }

		public ulong timestamp_seconds { get; set; }

		public class not_dissolving_neurons_e8s_bucketsInfo
		{
			public ulong F0 { get; set; }

			public double F1 { get; set; }

		}
		public class not_dissolving_neurons_count_bucketsInfo
		{
			public ulong F0 { get; set; }

			public ulong F1 { get; set; }

		}
		public class dissolving_neurons_count_bucketsInfo
		{
			public ulong F0 { get; set; }

			public ulong F1 { get; set; }

		}
		public class dissolving_neurons_e8s_bucketsInfo
		{
			public ulong F0 { get; set; }

			public double F1 { get; set; }

		}
	}
}


namespace EdjCase.ICP.Clients.Models
{
	public class GovernanceError
	{
		public string error_message { get; set; }

		public int error_type { get; set; }

	}
}


namespace EdjCase.ICP.Clients.Models
{
	public class IncreaseDissolveDelay
	{
		public uint additional_dissolve_delay_seconds { get; set; }

	}
}


namespace EdjCase.ICP.Clients.Models
{
	public class KnownNeuron
	{
		public EdjCase.ICP.Clients.Models.NeuronId? id { get; set; }

		public EdjCase.ICP.Clients.Models.KnownNeuronData? known_neuron_data { get; set; }

	}
}


namespace EdjCase.ICP.Clients.Models
{
	public class KnownNeuronData
	{
		public string name { get; set; }

		public string? description { get; set; }

	}
}


namespace EdjCase.ICP.Clients.Models
{
	public class ListKnownNeuronsResponse
	{
		public System.Collections.Generic.List<EdjCase.ICP.Clients.Models.KnownNeuron> known_neurons { get; set; }

	}
}


namespace EdjCase.ICP.Clients.Models
{
	public class ListNeurons
	{
		public System.Collections.Generic.List<ulong> neuron_ids { get; set; }

		public bool include_neurons_readable_by_caller { get; set; }

	}
}


namespace EdjCase.ICP.Clients.Models
{
	public class ListNeuronsResponse
	{
		public System.Collections.Generic.List<neuron_infosInfo> neuron_infos { get; set; }

		public System.Collections.Generic.List<EdjCase.ICP.Clients.Models.Neuron> full_neurons { get; set; }

		public class neuron_infosInfo
		{
			public ulong F0 { get; set; }

			public EdjCase.ICP.Clients.Models.NeuronInfo F1 { get; set; }

		}
	}
}


namespace EdjCase.ICP.Clients.Models
{
	public class ListNodeProvidersResponse
	{
		public System.Collections.Generic.List<EdjCase.ICP.Clients.Models.NodeProvider> node_providers { get; set; }

	}
}


namespace EdjCase.ICP.Clients.Models
{
	public class ListProposalInfo
	{
		public System.Collections.Generic.List<int> include_reward_status { get; set; }

		public EdjCase.ICP.Clients.Models.NeuronId? before_proposal { get; set; }

		public uint limit { get; set; }

		public System.Collections.Generic.List<int> exclude_topic { get; set; }

		public System.Collections.Generic.List<int> include_status { get; set; }

	}
}


namespace EdjCase.ICP.Clients.Models
{
	public class ListProposalInfoResponse
	{
		public System.Collections.Generic.List<EdjCase.ICP.Clients.Models.ProposalInfo> proposal_info { get; set; }

	}
}


namespace EdjCase.ICP.Clients.Models
{
	public class MakeProposalResponse
	{
		public EdjCase.ICP.Clients.Models.NeuronId? proposal_id { get; set; }

	}
}


namespace EdjCase.ICP.Clients.Models
{
	public class ManageNeuron
	{
		public EdjCase.ICP.Clients.Models.NeuronId? id { get; set; }

		public EdjCase.ICP.Clients.Models.Command? command { get; set; }

		public EdjCase.ICP.Clients.Models.NeuronIdOrSubaccount? neuron_id_or_subaccount { get; set; }

	}
}


namespace EdjCase.ICP.Clients.Models
{
	public class ManageNeuronResponse
	{
		public EdjCase.ICP.Clients.Models.Command_1? command { get; set; }

	}
}


namespace EdjCase.ICP.Clients.Models
{
	public class Merge
	{
		public EdjCase.ICP.Clients.Models.NeuronId? source_neuron_id { get; set; }

	}
}


namespace EdjCase.ICP.Clients.Models
{
	public class MergeMaturity
	{
		public uint percentage_to_merge { get; set; }

	}
}


namespace EdjCase.ICP.Clients.Models
{
	public class MergeMaturityResponse
	{
		public ulong merged_maturity_e8s { get; set; }

		public ulong new_stake_e8s { get; set; }

	}
}


namespace EdjCase.ICP.Clients.Models
{
	public class Motion
	{
		public string motion_text { get; set; }

	}
}


namespace EdjCase.ICP.Clients.Models
{
	public class NetworkEconomics
	{
		public ulong neuron_minimum_stake_e8s { get; set; }

		public uint max_proposals_to_keep_per_topic { get; set; }

		public ulong neuron_management_fee_per_proposal_e8s { get; set; }

		public ulong reject_cost_e8s { get; set; }

		public ulong transaction_fee_e8s { get; set; }

		public ulong neuron_spawn_dissolve_delay_seconds { get; set; }

		public ulong minimum_icp_xdr_rate { get; set; }

		public ulong maximum_node_provider_rewards_e8s { get; set; }

	}
}


namespace EdjCase.ICP.Clients.Models
{
	public class Neuron
	{
		public EdjCase.ICP.Clients.Models.NeuronId? id { get; set; }

		public EdjCase.ICP.Candid.Models.Principal? controller { get; set; }

		public System.Collections.Generic.List<EdjCase.ICP.Clients.Models.BallotInfo> recent_ballots { get; set; }

		public bool kyc_verified { get; set; }

		public bool not_for_profit { get; set; }

		public ulong maturity_e8s_equivalent { get; set; }

		public ulong cached_neuron_stake_e8s { get; set; }

		public ulong created_timestamp_seconds { get; set; }

		public ulong aging_since_timestamp_seconds { get; set; }

		public System.Collections.Generic.List<EdjCase.ICP.Candid.Models.Principal> hot_keys { get; set; }

		public System.Collections.Generic.List<byte> account { get; set; }

		public ulong? joined_community_fund_timestamp_seconds { get; set; }

		public EdjCase.ICP.Clients.Models.DissolveState? dissolve_state { get; set; }

		public System.Collections.Generic.List<followeesInfo> followees { get; set; }

		public ulong neuron_fees_e8s { get; set; }

		public EdjCase.ICP.Clients.Models.NeuronStakeTransfer? transfer { get; set; }

		public EdjCase.ICP.Clients.Models.KnownNeuronData? known_neuron_data { get; set; }

		public class followeesInfo
		{
			public int F0 { get; set; }

			public EdjCase.ICP.Clients.Models.Followees F1 { get; set; }

		}
	}
}


namespace EdjCase.ICP.Clients.Models
{
	public class NeuronId
	{
		public ulong id { get; set; }

	}
}


namespace EdjCase.ICP.Clients.Models
{
	public enum NeuronIdOrSubaccountType
	{
		Subaccount,
		NeuronId,
	}
	public class NeuronIdOrSubaccount
	{
		public NeuronIdOrSubaccountType Type { get; }
		private readonly object? value;

		public NeuronIdOrSubaccount(NeuronIdOrSubaccountType type, object? value)
		{
			this.Type = type;
			this.value = value;
		}

		public static NeuronIdOrSubaccount Subaccount(System.Collections.Generic.List<byte> info)
		{
			return new NeuronIdOrSubaccount(NeuronIdOrSubaccountType.Subaccount, info);
		}

		public System.Collections.Generic.List<byte> AsSubaccount()
		{
			this.ValidateType(NeuronIdOrSubaccountType.Subaccount);
			return (System.Collections.Generic.List<byte>)this.value!;
		}

		public static NeuronIdOrSubaccount NeuronId(EdjCase.ICP.Clients.Models.NeuronId info)
		{
			return new NeuronIdOrSubaccount(NeuronIdOrSubaccountType.NeuronId, info);
		}

		public EdjCase.ICP.Clients.Models.NeuronId AsNeuronId()
		{
			this.ValidateType(NeuronIdOrSubaccountType.NeuronId);
			return (EdjCase.ICP.Clients.Models.NeuronId)this.value!;
		}

		private void ValidateType(NeuronIdOrSubaccountType type)
		{
			if (this.Type != type)
			{
				throw new InvalidOperationException($"Cannot cast '{this.Type}' to type '{type}'");
			}
		}
	}
}


namespace EdjCase.ICP.Clients.Models
{
	public class NeuronInFlightCommand
	{
		public EdjCase.ICP.Clients.Models.Command_2? command { get; set; }

		public ulong timestamp { get; set; }

	}
}


namespace EdjCase.ICP.Clients.Models
{
	public class NeuronInfo
	{
		public ulong dissolve_delay_seconds { get; set; }

		public System.Collections.Generic.List<EdjCase.ICP.Clients.Models.BallotInfo> recent_ballots { get; set; }

		public ulong created_timestamp_seconds { get; set; }

		public int state { get; set; }

		public ulong stake_e8s { get; set; }

		public ulong? joined_community_fund_timestamp_seconds { get; set; }

		public ulong retrieved_at_timestamp_seconds { get; set; }

		public EdjCase.ICP.Clients.Models.KnownNeuronData? known_neuron_data { get; set; }

		public ulong voting_power { get; set; }

		public ulong age_seconds { get; set; }

	}
}


namespace EdjCase.ICP.Clients.Models
{
	public class NeuronStakeTransfer
	{
		public System.Collections.Generic.List<byte> to_subaccount { get; set; }

		public ulong neuron_stake_e8s { get; set; }

		public EdjCase.ICP.Candid.Models.Principal? from { get; set; }

		public ulong memo { get; set; }

		public System.Collections.Generic.List<byte> from_subaccount { get; set; }

		public ulong transfer_timestamp { get; set; }

		public ulong block_height { get; set; }

	}
}


namespace EdjCase.ICP.Clients.Models
{
	public class NodeProvider
	{
		public EdjCase.ICP.Candid.Models.Principal? id { get; set; }

		public EdjCase.ICP.Clients.Models.AccountIdentifier? reward_account { get; set; }

	}
}


namespace EdjCase.ICP.Clients.Models
{
	public enum OperationType
	{
		RemoveHotKey,
		AddHotKey,
		StopDissolving,
		StartDissolving,
		IncreaseDissolveDelay,
		JoinCommunityFund,
		SetDissolveTimestamp,
	}
	public class Operation
	{
		public OperationType Type { get; }
		private readonly object? value;

		public Operation(OperationType type, object? value)
		{
			this.Type = type;
			this.value = value;
		}

		public static Operation RemoveHotKey(EdjCase.ICP.Clients.Models.RemoveHotKey info)
		{
			return new Operation(OperationType.RemoveHotKey, info);
		}

		public EdjCase.ICP.Clients.Models.RemoveHotKey AsRemoveHotKey()
		{
			this.ValidateType(OperationType.RemoveHotKey);
			return (EdjCase.ICP.Clients.Models.RemoveHotKey)this.value!;
		}

		public static Operation AddHotKey(EdjCase.ICP.Clients.Models.AddHotKey info)
		{
			return new Operation(OperationType.AddHotKey, info);
		}

		public EdjCase.ICP.Clients.Models.AddHotKey AsAddHotKey()
		{
			this.ValidateType(OperationType.AddHotKey);
			return (EdjCase.ICP.Clients.Models.AddHotKey)this.value!;
		}

		public static Operation StopDissolving(StopDissolvingInfo info)
		{
			return new Operation(OperationType.StopDissolving, info);
		}

		public StopDissolvingInfo AsStopDissolving()
		{
			this.ValidateType(OperationType.StopDissolving);
			return (StopDissolvingInfo)this.value!;
		}

		public static Operation StartDissolving(StartDissolvingInfo info)
		{
			return new Operation(OperationType.StartDissolving, info);
		}

		public StartDissolvingInfo AsStartDissolving()
		{
			this.ValidateType(OperationType.StartDissolving);
			return (StartDissolvingInfo)this.value!;
		}

		public static Operation IncreaseDissolveDelay(EdjCase.ICP.Clients.Models.IncreaseDissolveDelay info)
		{
			return new Operation(OperationType.IncreaseDissolveDelay, info);
		}

		public EdjCase.ICP.Clients.Models.IncreaseDissolveDelay AsIncreaseDissolveDelay()
		{
			this.ValidateType(OperationType.IncreaseDissolveDelay);
			return (EdjCase.ICP.Clients.Models.IncreaseDissolveDelay)this.value!;
		}

		public static Operation JoinCommunityFund(JoinCommunityFundInfo info)
		{
			return new Operation(OperationType.JoinCommunityFund, info);
		}

		public JoinCommunityFundInfo AsJoinCommunityFund()
		{
			this.ValidateType(OperationType.JoinCommunityFund);
			return (JoinCommunityFundInfo)this.value!;
		}

		public static Operation SetDissolveTimestamp(EdjCase.ICP.Clients.Models.SetDissolveTimestamp info)
		{
			return new Operation(OperationType.SetDissolveTimestamp, info);
		}

		public EdjCase.ICP.Clients.Models.SetDissolveTimestamp AsSetDissolveTimestamp()
		{
			this.ValidateType(OperationType.SetDissolveTimestamp);
			return (EdjCase.ICP.Clients.Models.SetDissolveTimestamp)this.value!;
		}

		private void ValidateType(OperationType type)
		{
			if (this.Type != type)
			{
				throw new InvalidOperationException($"Cannot cast '{this.Type}' to type '{type}'");
			}
		}
		public class StopDissolvingInfo
		{
		}
		public class StartDissolvingInfo
		{
		}
		public class JoinCommunityFundInfo
		{
		}
	}
}


namespace EdjCase.ICP.Clients.Models
{
	public class Proposal
	{
		public string url { get; set; }

		public string? title { get; set; }

		public EdjCase.ICP.Clients.Models.Action? action { get; set; }

		public string summary { get; set; }

	}
}


namespace EdjCase.ICP.Clients.Models
{
	public class ProposalData
	{
		public EdjCase.ICP.Clients.Models.NeuronId? id { get; set; }

		public EdjCase.ICP.Clients.Models.GovernanceError? failure_reason { get; set; }

		public System.Collections.Generic.List<ballotsInfo> ballots { get; set; }

		public ulong proposal_timestamp_seconds { get; set; }

		public ulong reward_event_round { get; set; }

		public ulong failed_timestamp_seconds { get; set; }

		public ulong reject_cost_e8s { get; set; }

		public EdjCase.ICP.Clients.Models.Tally? latest_tally { get; set; }

		public ulong decided_timestamp_seconds { get; set; }

		public EdjCase.ICP.Clients.Models.Proposal? proposal { get; set; }

		public EdjCase.ICP.Clients.Models.NeuronId? proposer { get; set; }

		public EdjCase.ICP.Clients.Models.WaitForQuietState? wait_for_quiet_state { get; set; }

		public ulong executed_timestamp_seconds { get; set; }

		public class ballotsInfo
		{
			public ulong F0 { get; set; }

			public EdjCase.ICP.Clients.Models.Ballot F1 { get; set; }

		}
	}
}


namespace EdjCase.ICP.Clients.Models
{
	public class ProposalInfo
	{
		public EdjCase.ICP.Clients.Models.NeuronId? id { get; set; }

		public int status { get; set; }

		public int topic { get; set; }

		public EdjCase.ICP.Clients.Models.GovernanceError? failure_reason { get; set; }

		public System.Collections.Generic.List<ballotsInfo> ballots { get; set; }

		public ulong proposal_timestamp_seconds { get; set; }

		public ulong reward_event_round { get; set; }

		public ulong? deadline_timestamp_seconds { get; set; }

		public ulong failed_timestamp_seconds { get; set; }

		public ulong reject_cost_e8s { get; set; }

		public EdjCase.ICP.Clients.Models.Tally? latest_tally { get; set; }

		public int reward_status { get; set; }

		public ulong decided_timestamp_seconds { get; set; }

		public EdjCase.ICP.Clients.Models.Proposal? proposal { get; set; }

		public EdjCase.ICP.Clients.Models.NeuronId? proposer { get; set; }

		public ulong executed_timestamp_seconds { get; set; }

		public class ballotsInfo
		{
			public ulong F0 { get; set; }

			public EdjCase.ICP.Clients.Models.Ballot F1 { get; set; }

		}
	}
}


namespace EdjCase.ICP.Clients.Models
{
	public class RegisterVote
	{
		public int vote { get; set; }

		public EdjCase.ICP.Clients.Models.NeuronId? proposal { get; set; }

	}
}


namespace EdjCase.ICP.Clients.Models
{
	public class RemoveHotKey
	{
		public EdjCase.ICP.Candid.Models.Principal? hot_key_to_remove { get; set; }

	}
}


namespace EdjCase.ICP.Clients.Models
{
	public enum ResultType
	{
		Ok,
		Err,
	}
	public class Result
	{
		public ResultType Type { get; }
		private readonly object? value;

		public Result(ResultType type, object? value)
		{
			this.Type = type;
			this.value = value;
		}

		public static Result Ok()
		{
			return new Result(ResultType.Ok, null);
		}

		public static Result Err(EdjCase.ICP.Clients.Models.GovernanceError info)
		{
			return new Result(ResultType.Err, info);
		}

		public EdjCase.ICP.Clients.Models.GovernanceError AsErr()
		{
			this.ValidateType(ResultType.Err);
			return (EdjCase.ICP.Clients.Models.GovernanceError)this.value!;
		}

		private void ValidateType(ResultType type)
		{
			if (this.Type != type)
			{
				throw new InvalidOperationException($"Cannot cast '{this.Type}' to type '{type}'");
			}
		}
	}
}


namespace EdjCase.ICP.Clients.Models
{
	public enum Result_1Type
	{
		Error,
		NeuronId,
	}
	public class Result_1
	{
		public Result_1Type Type { get; }
		private readonly object? value;

		public Result_1(Result_1Type type, object? value)
		{
			this.Type = type;
			this.value = value;
		}

		public static Result_1 Error(EdjCase.ICP.Clients.Models.GovernanceError info)
		{
			return new Result_1(Result_1Type.Error, info);
		}

		public EdjCase.ICP.Clients.Models.GovernanceError AsError()
		{
			this.ValidateType(Result_1Type.Error);
			return (EdjCase.ICP.Clients.Models.GovernanceError)this.value!;
		}

		public static Result_1 NeuronId(EdjCase.ICP.Clients.Models.NeuronId info)
		{
			return new Result_1(Result_1Type.NeuronId, info);
		}

		public EdjCase.ICP.Clients.Models.NeuronId AsNeuronId()
		{
			this.ValidateType(Result_1Type.NeuronId);
			return (EdjCase.ICP.Clients.Models.NeuronId)this.value!;
		}

		private void ValidateType(Result_1Type type)
		{
			if (this.Type != type)
			{
				throw new InvalidOperationException($"Cannot cast '{this.Type}' to type '{type}'");
			}
		}
	}
}


namespace EdjCase.ICP.Clients.Models
{
	public enum Result_2Type
	{
		Ok,
		Err,
	}
	public class Result_2
	{
		public Result_2Type Type { get; }
		private readonly object? value;

		public Result_2(Result_2Type type, object? value)
		{
			this.Type = type;
			this.value = value;
		}

		public static Result_2 Ok(EdjCase.ICP.Clients.Models.Neuron info)
		{
			return new Result_2(Result_2Type.Ok, info);
		}

		public EdjCase.ICP.Clients.Models.Neuron AsOk()
		{
			this.ValidateType(Result_2Type.Ok);
			return (EdjCase.ICP.Clients.Models.Neuron)this.value!;
		}

		public static Result_2 Err(EdjCase.ICP.Clients.Models.GovernanceError info)
		{
			return new Result_2(Result_2Type.Err, info);
		}

		public EdjCase.ICP.Clients.Models.GovernanceError AsErr()
		{
			this.ValidateType(Result_2Type.Err);
			return (EdjCase.ICP.Clients.Models.GovernanceError)this.value!;
		}

		private void ValidateType(Result_2Type type)
		{
			if (this.Type != type)
			{
				throw new InvalidOperationException($"Cannot cast '{this.Type}' to type '{type}'");
			}
		}
	}
}


namespace EdjCase.ICP.Clients.Models
{
	public enum Result_3Type
	{
		Ok,
		Err,
	}
	public class Result_3
	{
		public Result_3Type Type { get; }
		private readonly object? value;

		public Result_3(Result_3Type type, object? value)
		{
			this.Type = type;
			this.value = value;
		}

		public static Result_3 Ok(EdjCase.ICP.Clients.Models.RewardNodeProviders info)
		{
			return new Result_3(Result_3Type.Ok, info);
		}

		public EdjCase.ICP.Clients.Models.RewardNodeProviders AsOk()
		{
			this.ValidateType(Result_3Type.Ok);
			return (EdjCase.ICP.Clients.Models.RewardNodeProviders)this.value!;
		}

		public static Result_3 Err(EdjCase.ICP.Clients.Models.GovernanceError info)
		{
			return new Result_3(Result_3Type.Err, info);
		}

		public EdjCase.ICP.Clients.Models.GovernanceError AsErr()
		{
			this.ValidateType(Result_3Type.Err);
			return (EdjCase.ICP.Clients.Models.GovernanceError)this.value!;
		}

		private void ValidateType(Result_3Type type)
		{
			if (this.Type != type)
			{
				throw new InvalidOperationException($"Cannot cast '{this.Type}' to type '{type}'");
			}
		}
	}
}


namespace EdjCase.ICP.Clients.Models
{
	public enum Result_4Type
	{
		Ok,
		Err,
	}
	public class Result_4
	{
		public Result_4Type Type { get; }
		private readonly object? value;

		public Result_4(Result_4Type type, object? value)
		{
			this.Type = type;
			this.value = value;
		}

		public static Result_4 Ok(EdjCase.ICP.Clients.Models.NeuronInfo info)
		{
			return new Result_4(Result_4Type.Ok, info);
		}

		public EdjCase.ICP.Clients.Models.NeuronInfo AsOk()
		{
			this.ValidateType(Result_4Type.Ok);
			return (EdjCase.ICP.Clients.Models.NeuronInfo)this.value!;
		}

		public static Result_4 Err(EdjCase.ICP.Clients.Models.GovernanceError info)
		{
			return new Result_4(Result_4Type.Err, info);
		}

		public EdjCase.ICP.Clients.Models.GovernanceError AsErr()
		{
			this.ValidateType(Result_4Type.Err);
			return (EdjCase.ICP.Clients.Models.GovernanceError)this.value!;
		}

		private void ValidateType(Result_4Type type)
		{
			if (this.Type != type)
			{
				throw new InvalidOperationException($"Cannot cast '{this.Type}' to type '{type}'");
			}
		}
	}
}


namespace EdjCase.ICP.Clients.Models
{
	public enum Result_5Type
	{
		Ok,
		Err,
	}
	public class Result_5
	{
		public Result_5Type Type { get; }
		private readonly object? value;

		public Result_5(Result_5Type type, object? value)
		{
			this.Type = type;
			this.value = value;
		}

		public static Result_5 Ok(EdjCase.ICP.Clients.Models.NodeProvider info)
		{
			return new Result_5(Result_5Type.Ok, info);
		}

		public EdjCase.ICP.Clients.Models.NodeProvider AsOk()
		{
			this.ValidateType(Result_5Type.Ok);
			return (EdjCase.ICP.Clients.Models.NodeProvider)this.value!;
		}

		public static Result_5 Err(EdjCase.ICP.Clients.Models.GovernanceError info)
		{
			return new Result_5(Result_5Type.Err, info);
		}

		public EdjCase.ICP.Clients.Models.GovernanceError AsErr()
		{
			this.ValidateType(Result_5Type.Err);
			return (EdjCase.ICP.Clients.Models.GovernanceError)this.value!;
		}

		private void ValidateType(Result_5Type type)
		{
			if (this.Type != type)
			{
				throw new InvalidOperationException($"Cannot cast '{this.Type}' to type '{type}'");
			}
		}
	}
}


namespace EdjCase.ICP.Clients.Models
{
	public class RewardEvent
	{
		public ulong day_after_genesis { get; set; }

		public ulong actual_timestamp_seconds { get; set; }

		public ulong distributed_e8s_equivalent { get; set; }

		public System.Collections.Generic.List<EdjCase.ICP.Clients.Models.NeuronId> settled_proposals { get; set; }

	}
}


namespace EdjCase.ICP.Clients.Models
{
	public enum RewardModeType
	{
		RewardToNeuron,
		RewardToAccount,
	}
	public class RewardMode
	{
		public RewardModeType Type { get; }
		private readonly object? value;

		public RewardMode(RewardModeType type, object? value)
		{
			this.Type = type;
			this.value = value;
		}

		public static RewardMode RewardToNeuron(EdjCase.ICP.Clients.Models.RewardToNeuron info)
		{
			return new RewardMode(RewardModeType.RewardToNeuron, info);
		}

		public EdjCase.ICP.Clients.Models.RewardToNeuron AsRewardToNeuron()
		{
			this.ValidateType(RewardModeType.RewardToNeuron);
			return (EdjCase.ICP.Clients.Models.RewardToNeuron)this.value!;
		}

		public static RewardMode RewardToAccount(EdjCase.ICP.Clients.Models.RewardToAccount info)
		{
			return new RewardMode(RewardModeType.RewardToAccount, info);
		}

		public EdjCase.ICP.Clients.Models.RewardToAccount AsRewardToAccount()
		{
			this.ValidateType(RewardModeType.RewardToAccount);
			return (EdjCase.ICP.Clients.Models.RewardToAccount)this.value!;
		}

		private void ValidateType(RewardModeType type)
		{
			if (this.Type != type)
			{
				throw new InvalidOperationException($"Cannot cast '{this.Type}' to type '{type}'");
			}
		}
	}
}


namespace EdjCase.ICP.Clients.Models
{
	public class RewardNodeProvider
	{
		public EdjCase.ICP.Clients.Models.NodeProvider? node_provider { get; set; }

		public EdjCase.ICP.Clients.Models.RewardMode? reward_mode { get; set; }

		public ulong amount_e8s { get; set; }

	}
}


namespace EdjCase.ICP.Clients.Models
{
	public class RewardNodeProviders
	{
		public bool? use_registry_derived_rewards { get; set; }

		public System.Collections.Generic.List<EdjCase.ICP.Clients.Models.RewardNodeProvider> rewards { get; set; }

	}
}


namespace EdjCase.ICP.Clients.Models
{
	public class RewardToAccount
	{
		public EdjCase.ICP.Clients.Models.AccountIdentifier? to_account { get; set; }

	}
}


namespace EdjCase.ICP.Clients.Models
{
	public class RewardToNeuron
	{
		public ulong dissolve_delay_seconds { get; set; }

	}
}


namespace EdjCase.ICP.Clients.Models
{
	public class SetDefaultFollowees
	{
		public System.Collections.Generic.List<default_followeesInfo> default_followees { get; set; }

		public class default_followeesInfo
		{
			public int F0 { get; set; }

			public EdjCase.ICP.Clients.Models.Followees F1 { get; set; }

		}
	}
}


namespace EdjCase.ICP.Clients.Models
{
	public class SetDissolveTimestamp
	{
		public ulong dissolve_timestamp_seconds { get; set; }

	}
}


namespace EdjCase.ICP.Clients.Models
{
	public class Spawn
	{
		public uint? percentage_to_spawn { get; set; }

		public EdjCase.ICP.Candid.Models.Principal? new_controller { get; set; }

		public ulong? nonce { get; set; }

	}
}


namespace EdjCase.ICP.Clients.Models
{
	public class SpawnResponse
	{
		public EdjCase.ICP.Clients.Models.NeuronId? created_neuron_id { get; set; }

	}
}


namespace EdjCase.ICP.Clients.Models
{
	public class Split
	{
		public ulong amount_e8s { get; set; }

	}
}


namespace EdjCase.ICP.Clients.Models
{
	public class Tally
	{
		public ulong no { get; set; }

		public ulong yes { get; set; }

		public ulong total { get; set; }

		public ulong timestamp_seconds { get; set; }

	}
}


namespace EdjCase.ICP.Clients.Models
{
	public class UpdateNodeProvider
	{
		public EdjCase.ICP.Clients.Models.AccountIdentifier? reward_account { get; set; }

	}
}


namespace EdjCase.ICP.Clients.Models
{
	public class WaitForQuietState
	{
		public ulong current_deadline_timestamp_seconds { get; set; }

	}
}
,
  
  
namespace EdjCase.ICP.Clients.Models
{
	public enum WithdrawReceiptType
	{
		Err,
		Ok,
	}
	public class WithdrawReceipt
	{
		public WithdrawReceiptType Type { get; }
		private readonly object? value;

		public WithdrawReceipt(WithdrawReceiptType type, object? value)
		{
			this.Type = type;
			this.value = value;
		}

		public static WithdrawReceipt Err(EdjCase.ICP.Clients.Models.WithdrawErr info)
		{
			return new WithdrawReceipt(WithdrawReceiptType.Err, info);
		}

		public EdjCase.ICP.Clients.Models.WithdrawErr AsErr()
		{
			this.ValidateType(WithdrawReceiptType.Err);
			return (EdjCase.ICP.Clients.Models.WithdrawErr)this.value!;
		}

		public static WithdrawReceipt Ok(EdjCase.ICP.Candid.UnboundedUInt info)
		{
			return new WithdrawReceipt(WithdrawReceiptType.Ok, info);
		}

		public EdjCase.ICP.Candid.UnboundedUInt AsOk()
		{
			this.ValidateType(WithdrawReceiptType.Ok);
			return (EdjCase.ICP.Candid.UnboundedUInt)this.value!;
		}

		private void ValidateType(WithdrawReceiptType type)
		{
			if (this.Type != type)
			{
				throw new InvalidOperationException($"Cannot cast '{this.Type}' to type '{type}'");
			}
		}
	}
}


namespace EdjCase.ICP.Clients.Models
{
	public enum WithdrawErrType
	{
		BalanceLow,
		TransferFailure,
	}
	public class WithdrawErr
	{
		public WithdrawErrType Type { get; }
		private readonly object? value;

		public WithdrawErr(WithdrawErrType type, object? value)
		{
			this.Type = type;
			this.value = value;
		}

		public static WithdrawErr BalanceLow()
		{
			return new WithdrawErr(WithdrawErrType.BalanceLow, null);
		}

		public static WithdrawErr TransferFailure()
		{
			return new WithdrawErr(WithdrawErrType.TransferFailure, null);
		}

		private void ValidateType(WithdrawErrType type)
		{
			if (this.Type != type)
			{
				throw new InvalidOperationException($"Cannot cast '{this.Type}' to type '{type}'");
			}
		}
	}
}


namespace EdjCase.ICP.Clients.Models
{
	public enum OrderPlacementReceiptType
	{
		Err,
		Ok,
	}
	public class OrderPlacementReceipt
	{
		public OrderPlacementReceiptType Type { get; }
		private readonly object? value;

		public OrderPlacementReceipt(OrderPlacementReceiptType type, object? value)
		{
			this.Type = type;
			this.value = value;
		}

		public static OrderPlacementReceipt Err(EdjCase.ICP.Clients.Models.OrderPlacementErr info)
		{
			return new OrderPlacementReceipt(OrderPlacementReceiptType.Err, info);
		}

		public EdjCase.ICP.Clients.Models.OrderPlacementErr AsErr()
		{
			this.ValidateType(OrderPlacementReceiptType.Err);
			return (EdjCase.ICP.Clients.Models.OrderPlacementErr)this.value!;
		}

		public static OrderPlacementReceipt Ok(EdjCase.ICP.Clients.Models.Order? info)
		{
			return new OrderPlacementReceipt(OrderPlacementReceiptType.Ok, info);
		}

		public EdjCase.ICP.Clients.Models.Order? AsOk()
		{
			this.ValidateType(OrderPlacementReceiptType.Ok);
			return (EdjCase.ICP.Clients.Models.Order?)this.value!;
		}

		private void ValidateType(OrderPlacementReceiptType type)
		{
			if (this.Type != type)
			{
				throw new InvalidOperationException($"Cannot cast '{this.Type}' to type '{type}'");
			}
		}
	}
}


namespace EdjCase.ICP.Clients.Models
{
	public enum OrderPlacementErrType
	{
		InvalidOrder,
		OrderBookFull,
	}
	public class OrderPlacementErr
	{
		public OrderPlacementErrType Type { get; }
		private readonly object? value;

		public OrderPlacementErr(OrderPlacementErrType type, object? value)
		{
			this.Type = type;
			this.value = value;
		}

		public static OrderPlacementErr InvalidOrder()
		{
			return new OrderPlacementErr(OrderPlacementErrType.InvalidOrder, null);
		}

		public static OrderPlacementErr OrderBookFull()
		{
			return new OrderPlacementErr(OrderPlacementErrType.OrderBookFull, null);
		}

		private void ValidateType(OrderPlacementErrType type)
		{
			if (this.Type != type)
			{
				throw new InvalidOperationException($"Cannot cast '{this.Type}' to type '{type}'");
			}
		}
	}
}


namespace EdjCase.ICP.Clients.Models
{
	public class Order
	{
		public Token from { get; set; }

		public EdjCase.ICP.Candid.UnboundedUInt fromAmount { get; set; }

		public OrderId id { get; set; }

		public EdjCase.ICP.Candid.Models.Principal owner { get; set; }

		public Token to { get; set; }

		public EdjCase.ICP.Candid.UnboundedUInt toAmount { get; set; }

	}
}


namespace EdjCase.ICP.Clients
{
	public class DexApiClient
	{
		public IAgent Agent { get; }
		public Principal CanisterId { get; }
		public DexApiClient(IAgent agent, Principal canisterId)
		{
			this.CanisterId = canisterId ?? throw new ArgumentNullException(nameof(canisterId));
			this.Agent = agent ?? throw new ArgumentNullException(nameof(agent));
		}
		public async Task<EdjCase.ICP.Clients.Models.CancelOrderReceipt> cancelOrder(OrderId arg0)
		{
			string method = "cancelOrder";
			CandidValue value0 = null;
			CandidType type0 = new CandidReferenceType(CandidId.Parse("OrderId"));
			var p0 = CandidValueWithType.FromValueAndType(value0, type0);
			var candidArgs = new List<CandidValueWithType>
			{
				p0,
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, method, arg, identityOverride: null);
			QueryReply reply = response.ThrowOrGetReply();
			EdjCase.ICP.Clients.Models.CancelOrderReceipt r0 = MapR0(reply.Arg.Values[0]);
			return (r0);
		}
		private static EdjCase.ICP.Clients.Models.CancelOrderReceipt MapR0(CandidValueWithType model)
		{
		}
		public clear()
		{
			string method = "clear";
			var candidArgs = new List<CandidValueWithType>
			{
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			QueryResponse response = await this.Agent.QueryAsync(canisterId, method, arg, identityOverride: null);
			QueryReply reply = response.ThrowOrGetReply();
			return ();
		}
		public credit(EdjCase.ICP.Candid.Models.Principal arg0, Token arg1, EdjCase.ICP.Candid.UnboundedUInt arg2)
		{
			string method = "credit";
			CandidValue value0 = null;
			CandidType type0 = new CandidPrimitiveType(PrimitiveType.Principal);
			var p0 = CandidValueWithType.FromValueAndType(value0, type0);
			CandidValue value1 = null;
			CandidType type1 = new CandidReferenceType(CandidId.Parse("Token"));
			var p1 = CandidValueWithType.FromValueAndType(value1, type1);
			CandidValue value2 = null;
			CandidType type2 = new CandidPrimitiveType(PrimitiveType.Nat);
			var p2 = CandidValueWithType.FromValueAndType(value2, type2);
			var candidArgs = new List<CandidValueWithType>
			{
				p0,
				p1,
				p2,
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			QueryResponse response = await this.Agent.QueryAsync(canisterId, method, arg, identityOverride: null);
			QueryReply reply = response.ThrowOrGetReply();
			return ();
		}
		public async Task<EdjCase.ICP.Clients.Models.DepositReceipt> deposit(Token arg0)
		{
			string method = "deposit";
			CandidValue value0 = null;
			CandidType type0 = new CandidReferenceType(CandidId.Parse("Token"));
			var p0 = CandidValueWithType.FromValueAndType(value0, type0);
			var candidArgs = new List<CandidValueWithType>
			{
				p0,
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			QueryResponse response = await this.Agent.QueryAsync(canisterId, method, arg, identityOverride: null);
			QueryReply reply = response.ThrowOrGetReply();
			EdjCase.ICP.Clients.Models.DepositReceipt r0 = MapR0(reply.Arg.Values[0]);
			return (r0);
		}
		private static EdjCase.ICP.Clients.Models.DepositReceipt MapR0(CandidTypeWithValue model)
		{
		}
		public async Task<System.Collections.Generic.List<EdjCase.ICP.Clients.Models.Balance>> getAllBalances()
		{
			string method = "getAllBalances";
			var candidArgs = new List<CandidValueWithType>
			{
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			QueryResponse response = await this.Agent.QueryAsync(canisterId, method, arg, identityOverride: null);
			QueryReply reply = response.ThrowOrGetReply();
			System.Collections.Generic.List<EdjCase.ICP.Clients.Models.Balance> r0 = MapR0(reply.Arg.Values[0]);
			return (r0);
		}
		private static System.Collections.Generic.List<EdjCase.ICP.Clients.Models.Balance> MapR0(CandidTypeWithValue model)
		{
		}
		public async Task<EdjCase.ICP.Candid.UnboundedUInt> getBalance(Token arg0)
		{
			string method = "getBalance";
			CandidValue value0 = null;
			CandidType type0 = new CandidReferenceType(CandidId.Parse("Token"));
			var p0 = CandidValueWithType.FromValueAndType(value0, type0);
			var candidArgs = new List<CandidValueWithType>
			{
				p0,
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			QueryResponse response = await this.Agent.QueryAsync(canisterId, method, arg, identityOverride: null);
			QueryReply reply = response.ThrowOrGetReply();
			EdjCase.ICP.Candid.UnboundedUInt r0 = MapR0(reply.Arg.Values[0]);
			return (r0);
		}
		private static EdjCase.ICP.Candid.UnboundedUInt MapR0(CandidTypeWithValue model)
		{
		}
		public async Task<System.Collections.Generic.List<EdjCase.ICP.Clients.Models.Balance>> getBalances()
		{
			string method = "getBalances";
			var candidArgs = new List<CandidValueWithType>
			{
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			QueryResponse response = await this.Agent.QueryAsync(canisterId, method, arg, identityOverride: null);
			QueryReply reply = response.ThrowOrGetReply();
			System.Collections.Generic.List<EdjCase.ICP.Clients.Models.Balance> r0 = MapR0(reply.Arg.Values[0]);
			return (r0);
		}
		private static System.Collections.Generic.List<EdjCase.ICP.Clients.Models.Balance> MapR0(CandidTypeWithValue model)
		{
		}
		public async Task<System.Collections.Generic.List<byte>> getDepositAddress()
		{
			string method = "getDepositAddress";
			var candidArgs = new List<CandidValueWithType>
			{
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			QueryResponse response = await this.Agent.QueryAsync(canisterId, method, arg, identityOverride: null);
			QueryReply reply = response.ThrowOrGetReply();
			System.Collections.Generic.List<byte> r0 = MapR0(reply.Arg.Values[0]);
			return (r0);
		}
		private static System.Collections.Generic.List<byte> MapR0(CandidTypeWithValue model)
		{
		}
		public async Task<EdjCase.ICP.Clients.Models.Order?> getOrder(OrderId arg0)
		{
			string method = "getOrder";
			CandidValue value0 = null;
			CandidType type0 = new CandidReferenceType(CandidId.Parse("OrderId"));
			var p0 = CandidValueWithType.FromValueAndType(value0, type0);
			var candidArgs = new List<CandidValueWithType>
			{
				p0,
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			QueryResponse response = await this.Agent.QueryAsync(canisterId, method, arg, identityOverride: null);
			QueryReply reply = response.ThrowOrGetReply();
			EdjCase.ICP.Clients.Models.Order? r0 = MapR0(reply.Arg.Values[0]);
			return (r0);
		}
		private static EdjCase.ICP.Clients.Models.Order? MapR0(CandidTypeWithValue model)
		{
		}
		public async Task<System.Collections.Generic.List<EdjCase.ICP.Clients.Models.Order>> getOrders()
		{
			string method = "getOrders";
			var candidArgs = new List<CandidValueWithType>
			{
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			QueryResponse response = await this.Agent.QueryAsync(canisterId, method, arg, identityOverride: null);
			QueryReply reply = response.ThrowOrGetReply();
			System.Collections.Generic.List<EdjCase.ICP.Clients.Models.Order> r0 = MapR0(reply.Arg.Values[0]);
			return (r0);
		}
		private static System.Collections.Generic.List<EdjCase.ICP.Clients.Models.Order> MapR0(CandidTypeWithValue model)
		{
		}
		public async Task<string> getSymbol(Token arg0)
		{
			string method = "getSymbol";
			CandidValue value0 = null;
			CandidType type0 = new CandidReferenceType(CandidId.Parse("Token"));
			var p0 = CandidValueWithType.FromValueAndType(value0, type0);
			var candidArgs = new List<CandidValueWithType>
			{
				p0,
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			QueryResponse response = await this.Agent.QueryAsync(canisterId, method, arg, identityOverride: null);
			QueryReply reply = response.ThrowOrGetReply();
			string r0 = MapR0(reply.Arg.Values[0]);
			return (r0);
		}
		private static string MapR0(CandidTypeWithValue model)
		{
		}
		public async Task<EdjCase.ICP.Clients.Models.OrderPlacementReceipt> placeOrder(Token arg0, EdjCase.ICP.Candid.UnboundedUInt arg1, Token arg2, EdjCase.ICP.Candid.UnboundedUInt arg3)
		{
			string method = "placeOrder";
			CandidValue value0 = null;
			CandidType type0 = new CandidReferenceType(CandidId.Parse("Token"));
			var p0 = CandidValueWithType.FromValueAndType(value0, type0);
			CandidValue value1 = null;
			CandidType type1 = new CandidPrimitiveType(PrimitiveType.Nat);
			var p1 = CandidValueWithType.FromValueAndType(value1, type1);
			CandidValue value2 = null;
			CandidType type2 = new CandidReferenceType(CandidId.Parse("Token"));
			var p2 = CandidValueWithType.FromValueAndType(value2, type2);
			CandidValue value3 = null;
			CandidType type3 = new CandidPrimitiveType(PrimitiveType.Nat);
			var p3 = CandidValueWithType.FromValueAndType(value3, type3);
			var candidArgs = new List<CandidValueWithType>
			{
				p0,
				p1,
				p2,
				p3,
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			QueryResponse response = await this.Agent.QueryAsync(canisterId, method, arg, identityOverride: null);
			QueryReply reply = response.ThrowOrGetReply();
			EdjCase.ICP.Clients.Models.OrderPlacementReceipt r0 = MapR0(reply.Arg.Values[0]);
			return (r0);
		}
		private static EdjCase.ICP.Clients.Models.OrderPlacementReceipt MapR0(CandidTypeWithValue model)
		{
		}
		public async Task<EdjCase.ICP.Candid.Models.Principal> whoami()
		{
			string method = "whoami";
			var candidArgs = new List<CandidValueWithType>
			{
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			QueryResponse response = await this.Agent.QueryAsync(canisterId, method, arg, identityOverride: null);
			QueryReply reply = response.ThrowOrGetReply();
			EdjCase.ICP.Candid.Models.Principal r0 = MapR0(reply.Arg.Values[0]);
			return (r0);
		}
		private static EdjCase.ICP.Candid.Models.Principal MapR0(CandidTypeWithValue model)
		{
		}
		public async Task<EdjCase.ICP.Clients.Models.WithdrawReceipt> withdraw(Token arg0, EdjCase.ICP.Candid.UnboundedUInt arg1, EdjCase.ICP.Candid.Models.Principal arg2)
		{
			string method = "withdraw";
			CandidValue value0 = null;
			CandidType type0 = new CandidReferenceType(CandidId.Parse("Token"));
			var p0 = CandidValueWithType.FromValueAndType(value0, type0);
			CandidValue value1 = null;
			CandidType type1 = new CandidPrimitiveType(PrimitiveType.Nat);
			var p1 = CandidValueWithType.FromValueAndType(value1, type1);
			CandidValue value2 = null;
			CandidType type2 = new CandidPrimitiveType(PrimitiveType.Principal);
			var p2 = CandidValueWithType.FromValueAndType(value2, type2);
			var candidArgs = new List<CandidValueWithType>
			{
				p0,
				p1,
				p2,
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			QueryResponse response = await this.Agent.QueryAsync(canisterId, method, arg, identityOverride: null);
			QueryReply reply = response.ThrowOrGetReply();
			EdjCase.ICP.Clients.Models.WithdrawReceipt r0 = MapR0(reply.Arg.Values[0]);
			return (r0);
		}
		private static EdjCase.ICP.Clients.Models.WithdrawReceipt MapR0(CandidTypeWithValue model)
		{
		}
	}
}


namespace EdjCase.ICP.Clients.Models
{
	public enum DepositReceiptType
	{
		Err,
		Ok,
	}
	public class DepositReceipt
	{
		public DepositReceiptType Type { get; }
		private readonly object? value;

		public DepositReceipt(DepositReceiptType type, object? value)
		{
			this.Type = type;
			this.value = value;
		}

		public static DepositReceipt Err(EdjCase.ICP.Clients.Models.DepositErr info)
		{
			return new DepositReceipt(DepositReceiptType.Err, info);
		}

		public EdjCase.ICP.Clients.Models.DepositErr AsErr()
		{
			this.ValidateType(DepositReceiptType.Err);
			return (EdjCase.ICP.Clients.Models.DepositErr)this.value!;
		}

		public static DepositReceipt Ok(EdjCase.ICP.Candid.UnboundedUInt info)
		{
			return new DepositReceipt(DepositReceiptType.Ok, info);
		}

		public EdjCase.ICP.Candid.UnboundedUInt AsOk()
		{
			this.ValidateType(DepositReceiptType.Ok);
			return (EdjCase.ICP.Candid.UnboundedUInt)this.value!;
		}

		private void ValidateType(DepositReceiptType type)
		{
			if (this.Type != type)
			{
				throw new InvalidOperationException($"Cannot cast '{this.Type}' to type '{type}'");
			}
		}
	}
}


namespace EdjCase.ICP.Clients.Models
{
	public enum DepositErrType
	{
		BalanceLow,
		TransferFailure,
	}
	public class DepositErr
	{
		public DepositErrType Type { get; }
		private readonly object? value;

		public DepositErr(DepositErrType type, object? value)
		{
			this.Type = type;
			this.value = value;
		}

		public static DepositErr BalanceLow()
		{
			return new DepositErr(DepositErrType.BalanceLow, null);
		}

		public static DepositErr TransferFailure()
		{
			return new DepositErr(DepositErrType.TransferFailure, null);
		}

		private void ValidateType(DepositErrType type)
		{
			if (this.Type != type)
			{
				throw new InvalidOperationException($"Cannot cast '{this.Type}' to type '{type}'");
			}
		}
	}
}


namespace EdjCase.ICP.Clients.Models
{
	public enum CancelOrderReceiptType
	{
		Err,
		Ok,
	}
	public class CancelOrderReceipt
	{
		public CancelOrderReceiptType Type { get; }
		private readonly object? value;

		public CancelOrderReceipt(CancelOrderReceiptType type, object? value)
		{
			this.Type = type;
			this.value = value;
		}

		public static CancelOrderReceipt Err(EdjCase.ICP.Clients.Models.CancelOrderErr info)
		{
			return new CancelOrderReceipt(CancelOrderReceiptType.Err, info);
		}

		public EdjCase.ICP.Clients.Models.CancelOrderErr AsErr()
		{
			this.ValidateType(CancelOrderReceiptType.Err);
			return (EdjCase.ICP.Clients.Models.CancelOrderErr)this.value!;
		}

		public static CancelOrderReceipt Ok(OrderId info)
		{
			return new CancelOrderReceipt(CancelOrderReceiptType.Ok, info);
		}

		public OrderId AsOk()
		{
			this.ValidateType(CancelOrderReceiptType.Ok);
			return (OrderId)this.value!;
		}

		private void ValidateType(CancelOrderReceiptType type)
		{
			if (this.Type != type)
			{
				throw new InvalidOperationException($"Cannot cast '{this.Type}' to type '{type}'");
			}
		}
	}
}


namespace EdjCase.ICP.Clients.Models
{
	public enum CancelOrderErrType
	{
		NotAllowed,
		NotExistingOrder,
	}
	public class CancelOrderErr
	{
		public CancelOrderErrType Type { get; }
		private readonly object? value;

		public CancelOrderErr(CancelOrderErrType type, object? value)
		{
			this.Type = type;
			this.value = value;
		}

		public static CancelOrderErr NotAllowed()
		{
			return new CancelOrderErr(CancelOrderErrType.NotAllowed, null);
		}

		public static CancelOrderErr NotExistingOrder()
		{
			return new CancelOrderErr(CancelOrderErrType.NotExistingOrder, null);
		}

		private void ValidateType(CancelOrderErrType type)
		{
			if (this.Type != type)
			{
				throw new InvalidOperationException($"Cannot cast '{this.Type}' to type '{type}'");
			}
		}
	}
}


namespace EdjCase.ICP.Clients.Models
{
	public class Balance
	{
		public EdjCase.ICP.Candid.UnboundedUInt amount { get; set; }

		public EdjCase.ICP.Candid.Models.Principal owner { get; set; }

		public Token token { get; set; }

	}
}

  