using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid;

namespace Sample.Shared.Governance.Models
{
	public class Result2 : EdjCase.ICP.Candid.Models.CandidVariantValueBase<Result2Type>
	{
		public Result2(Result2Type type, System.Object? value)  : base(type, value)
		{
		}
		
		protected Result2()
		{
		}
		
		public static Result2 Ok(Neuron info)
		{
			return new Result2(Result2Type.Ok, info);
		}
		
		public Neuron AsOk()
		{
			this.ValidateType(Result2Type.Ok);
			return (Neuron)this.value!;
		}
		
		public static Result2 Err(GovernanceError info)
		{
			return new Result2(Result2Type.Err, info);
		}
		
		public GovernanceError AsErr()
		{
			this.ValidateType(Result2Type.Err);
			return (GovernanceError)this.value!;
		}
		
	}
	public enum Result2Type
	{
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("Ok")]
		[EdjCase.ICP.Candid.Models.VariantOptionTypeAttribute(typeof(Neuron))]
		Ok,
		[EdjCase.ICP.Candid.Mapping.CandidNameAttribute("Err")]
		[EdjCase.ICP.Candid.Models.VariantOptionTypeAttribute(typeof(GovernanceError))]
		Err,
	}
}

