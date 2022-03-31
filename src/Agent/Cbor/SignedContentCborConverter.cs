using ICP.Agent.Auth;
using System;
using System.Collections.Generic;
using System.Formats.Cbor;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICP.Agent.Cbor
{
	public class SignedContentCborConverter : ICborConverter<SignedContent>
	{
		public SignedContent Read(ref CborReader reader)
		{

		}

		public void Write(ref CborWriter writer, SignedContent value)
		{
			writer.WriteStartMap(3);
			writer.WriteTextString("content");
			writer.WriteStartMap(7);
			writer.WriteTextString("request_type");
			writer.WriteTextString(value.Content);
		}
	}
}
