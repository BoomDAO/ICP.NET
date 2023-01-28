using System;
using EdjCase.ICP.Agent.Models;

namespace EdjCase.ICP.Agent.Responses
{
	/// <summary>
	/// Model for a reponse to a read state request
	/// </summary>
	public class ReadStateResponse
	{
		/// <summary>
		/// The certificate data of the current canister state
		/// </summary>
		public Certificate Certificate { get; }

		/// <param name="certificate">The certificate data of the current canister state</param>
		/// <exception cref="ArgumentNullException"></exception>
		public ReadStateResponse(Certificate certificate)
		{
			this.Certificate = certificate ?? throw new ArgumentNullException(nameof(certificate));
		}
	}
}