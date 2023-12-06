using EdjCase.ICP.Candid.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EdjCase.ICP.WebSockets.Models
{
	internal class ClientKey
	{
		public Principal Id { get; set; }
		public ulong Nonce { get; set; }
	}
}
