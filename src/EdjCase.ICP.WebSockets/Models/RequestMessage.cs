using EdjCase.ICP.Agent.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EdjCase.ICP.WebSockets.Models
{
	internal class RequestMessage<T>
	{
		public SignedContent Envelope { get; set; }

		internal byte[] ToCbor()
		{
			throw new NotImplementedException();
		}
	}
}
