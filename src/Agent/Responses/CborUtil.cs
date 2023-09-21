using EdjCase.ICP.Candid.Encodings;
using EdjCase.ICP.Candid.Models;
using System;
using System.Collections.Generic;
using System.Formats.Cbor;
using System.Linq;
using System.Text;

namespace EdjCase.ICP.Agent.Responses
{
	internal static class CborUtil
	{
		public static UnboundedUInt ReadNat(CborReader reader)
		{
			CborReaderState state = reader.PeekState();
			switch (state)
			{
				case CborReaderState.UnsignedInteger:
					return reader.ReadUInt64();
				default:
					byte[] codeBytes = reader.ReadByteString().ToArray();
					return LEB128.DecodeUnsigned(codeBytes);
			}
		}

		public static Principal ReadPrincipal(CborReader reader)
		{
			return Principal.FromBytes(reader.ReadByteString());
		}
	}
}
