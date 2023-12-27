using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid.Models;
using System;
using System.Collections.Generic;
using System.Formats.Cbor;
using System.Text;

namespace EdjCase.ICP.WebSockets.Models
{
	internal class ClientKey
	{
		[CandidName("client_principal")]
		public Principal Id { get; set;  }
		[CandidName("client_nonce")]
		public ulong Nonce { get; set; }



		internal static ClientKey FromCbor(CborReader reader)
		{
			Principal? id = null;
			ulong? nonce = null;

			reader.ReadStartMap();
			while (reader.PeekState() != CborReaderState.EndMap)
			{
				switch (reader.ReadTextString())
				{
					case "client_principal":
						id = Principal.FromBytes(reader.ReadByteString());
						break;
					case "client_nonce":
						nonce = reader.ReadUInt64();
						break;
				}
			}
			reader.ReadEndMap();
			if (id == null)
			{
				throw new CborContentException("Missing field from incoming client message: client_principal");
			}
			if (nonce == null)
			{
				throw new CborContentException("Missing field from incoming client message: client_nonce");
			}
			return new ClientKey
			{
				Id = id,
				Nonce = nonce.Value
			};
		}
	}
}
