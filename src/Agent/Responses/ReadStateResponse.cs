using System;
using System.Formats.Cbor;
using EdjCase.ICP.Agent.Models;
using EdjCase.ICP.Candid.Models;

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


		internal static ReadStateResponse ReadCbor(CborReader reader)
		{
			if(reader.ReadTag() != CborTag.SelfDescribeCbor)
			{
				throw new CborContentException("Expected self describe tag");
			}
			Certificate? certificate = null;
			reader.ReadStartMap();
			while (reader.PeekState() != CborReaderState.EndMap)
			{
				string field = reader.ReadTextString();
				switch (field)
				{
					case "certificate":
						var certReader = new CborReader(reader.ReadByteString());
						certificate = Certificate.FromCbor(certReader);
						break;
					default:
						throw new NotImplementedException($"Cannot deserialize read_state response. Unknown field '{field}'");
				}
			}
			reader.ReadEndMap();

			if(certificate == null)
			{
				throw new CborContentException("Missing field: certificate");
			}

			return new ReadStateResponse(certificate);
		}
	}
}