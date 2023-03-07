using EdjCase.ICP.Candid.Mapping;
using Sample.Shared.Governance.Models;
using System.Collections.Generic;

namespace Sample.Shared.Governance.Models
{
	public class CfParticipant
	{
		[CandidName("hotkey_principal")]
		public string HotkeyPrincipal { get; set; }

		[CandidName("cf_neurons")]
		public List<CfNeuron> CfNeurons { get; set; }

		public CfParticipant(string hotkeyPrincipal, List<CfNeuron> cfNeurons)
		{
			this.HotkeyPrincipal = hotkeyPrincipal;
			this.CfNeurons = cfNeurons;
		}

		public CfParticipant()
		{
		}
	}
}