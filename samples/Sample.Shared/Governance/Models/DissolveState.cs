using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid;

namespace Sample.Shared.Governance.Models
{
	public class DissolveState : EdjCase.ICP.Candid.Models.CandidVariantValueBase<DissolveStateType>
	{
		public DissolveState(DissolveStateType type, System.Object? value)  : base(type, value)
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
	public enum DissolveStateType
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("DissolveDelaySeconds")]
		[EdjCase.ICP.Candid.Models.VariantOptionTypeAttribute(typeof(ulong))]
		DissolveDelaySeconds,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("WhenDissolvedTimestampSeconds")]
		[EdjCase.ICP.Candid.Models.VariantOptionTypeAttribute(typeof(ulong))]
		WhenDissolvedTimestampSeconds,
	}
}

