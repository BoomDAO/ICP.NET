using EdjCase.ICP.Candid.Models;
using System;
using System.Collections.Generic;
using System.Formats.Cbor;
using System.Text;

namespace EdjCase.ICP.WebSockets.Models
{
	internal class HandshakeMessage
	{
		public Principal GatewayPrincipal { get; set; }

		public static HandshakeMessage FromCbor(CborReader reader)
		{
			reader.ReadStartMap();
			reader.ReadTextString();
			byte[] gatewayPrincipalBytes = reader.ReadByteString();
			return new HandshakeMessage
			{
				GatewayPrincipal = Principal.FromBytes(gatewayPrincipalBytes)
			};
		}
	}
}
