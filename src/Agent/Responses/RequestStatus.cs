using EdjCase.ICP.VariantSourceGenerator;
using System;
using System.Collections.Generic;
using System.Text;

namespace EdjCase.ICP.Agent.Responses
{
	[Variant]
	[VariantOption("O1", null)]
	[VariantOption("O2", typeof(string))]
	public class RequestStatus
	{
	}
}
