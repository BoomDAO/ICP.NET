using EdjCase.ICP.Candid.Encodings;
using EdjCase.ICP.Candid.Models;
using System;
using System.Collections.Generic;
using System.Formats.Cbor;
using System.Linq;
using System.Text;

namespace EdjCase.ICP.Agent.Responses
{
	internal class CallRejectedResponse
	{
		/// <summary>
		/// The type of query reject
		/// </summary>
		public RejectCode Code { get; }

		/// <summary>
		/// Optional. A human readable message about the rejection
		/// </summary>
		public string? Message { get; }

		/// <summary>
		/// Optional. A specific error id for the reject
		/// </summary>
		public string? ErrorCode { get; }

		internal CallRejectedResponse(RejectCode code, string? message, string? errorCode)
		{
			this.Code = code;
			this.Message = message;
			this.ErrorCode = errorCode;
		}

		public static CallRejectedResponse FromCbor(CborReader reader)
		{

			reader.ReadStartMap();

			UnboundedUInt? code = null;
			string? message = null;
			string? errorCode = null;

			if (reader.ReadTag() != CborTag.SelfDescribeCbor)
			{
				throw new CborContentException("Expected self describe tag");
			}
			reader.ReadStartMap();
			while (reader.PeekState() != CborReaderState.EndMap)
			{
				string name = reader.ReadTextString();
				switch (name)
				{
					case "reject_code":
						CborReaderState state = reader.PeekState();
						switch (state)
						{
							case CborReaderState.UnsignedInteger:
								code = reader.ReadUInt64();
								break;
							default:
								byte[] codeBytes = reader.ReadByteString().ToArray();
								code = LEB128.DecodeUnsigned(codeBytes);
								break;
						}
						break;
					case "reject_message":
						message = reader.ReadTextString();
						break;
					case "error_code":
						errorCode = reader.ReadTextString();
						break;
					default:
						throw new NotImplementedException($"Cannot deserialize query response. Unknown field '{name}'");
				}
			}
			reader.ReadEndMap();
			if (code == null)
			{
				throw new CborContentException("Missing field: reject_code");
			}
			RejectCode rejectCode = (RejectCode)(ulong)code!;
			return new CallRejectedResponse(rejectCode, message, errorCode);
		}
	}
}
