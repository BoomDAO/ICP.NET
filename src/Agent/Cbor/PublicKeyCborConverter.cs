using Dahomey.Cbor.Serialization;
using Dahomey.Cbor.Serialization.Converters;
using EdjCase.ICP.Candid.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agent.Cbor
{
	//internal class PublicKeyCborConverter : CborConverterBase<IPublicKey?>
	//{
	//	public override IPublicKey? Read(ref CborReader reader)
	//	{
	//		throw new NotImplementedException();
	//	}

	//	public override void Write(ref CborWriter writer, IPublicKey? value)
	//	{
	//		if (value == null)
	//		{
	//			writer.WriteNull();
	//			return;
	//		}
	//		writer.WriteByteString(value.GetRawBytes());
	//	}
	//}
}
