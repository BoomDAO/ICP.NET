using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Sample.Shared.Governance.Models
{
	public enum DissolveStateType
	{
		DissolveDelaySeconds,
		WhenDissolvedTimestampSeconds,
	}
	public class DissolveState : EdjCase.ICP.Candid.CandidVariantValueBase<DissolveStateType>
	{
		public DissolveState(DissolveStateType type, object? value)  : base(type, value)
		{
		}
		
		protected DissolveState()
		{
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
		
	}
}

