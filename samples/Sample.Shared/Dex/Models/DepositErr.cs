using Token = EdjCase.ICP.Candid.Models.Principal;
using OrderId = System.UInt32;

namespace Sample.Shared.Dex.Models
{
	public enum DepositErr
	{
		BalanceLow,
		TransferFailure
	}
}