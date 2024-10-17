using System;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Models;

namespace EdjCase.ICP.PocketIC.Client
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
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

	public class CanisterCallResponse
	{
		public byte[] Body { get; set; }
	}

	public class GetSubnetIdResponse
	{
		public Principal? SubnetId { get; set; }
	}

	public class GetCyclesBalanceResponse
	{
		public ulong Cycles { get; set; }
	}

	public class AddCyclesResponse
	{
		public ulong Cycles { get; set; }
	}

	public class GetStableMemoryResponse
	{
		public byte[] Blob { get; set; }
	}

	public class UploadBlobResponse
	{
		public string BlobId { get; set; }
	}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
}