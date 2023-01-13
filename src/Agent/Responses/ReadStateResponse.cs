using System;
using EdjCase.ICP.Agent.Models;

namespace EdjCase.ICP.Agent.Responses
{
	public class ReadStateResponse
	{
		public Certificate Certificate { get; }

		public ReadStateResponse(Certificate certificate)
		{
			this.Certificate = certificate ?? throw new ArgumentNullException(nameof(certificate));
		}
	}
}