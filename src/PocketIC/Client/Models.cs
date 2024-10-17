using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using EdjCase.ICP.Candid.Models;

namespace EdjCase.ICP.PocketIC.Client;

public class EffectivePrincipal
{
	public required EffectivePrincipalType Type { get; set; }
	public required Principal Id { get; set; }
}

public enum EffectivePrincipalType
{
	Subnet,
	Canister
}


public class SubnetConfig
{
	public bool? EnableDeterministicTimeSlicing { get; set; }
	public bool? EnableBenchmarkingInstructionLimits { get; set; }
	public required SubnetStateConfig State { get; set; }
}

public class SubnetStateConfig
{
	public SubnetStateType Type { get; private set; }
	public string? Path { get; private set; }
	public Principal? SubnetId { get; private set; }

	private SubnetStateConfig(SubnetStateType type, string? path, Principal? subnetId)
	{
		this.Type = type;
		this.Path = path;
		this.SubnetId = subnetId;
	}

	public static SubnetStateConfig New()
	{
		return new SubnetStateConfig(SubnetStateType.New, null, null);
	}

	public static SubnetStateConfig FromPath(string path, Principal subnetId)
	{
		return new SubnetStateConfig(SubnetStateType.FromPath, path, subnetId);
	}
}

public enum SubnetStateType
{
	New,
	FromPath
}