using Dahomey.Cbor.Serialization;
using Dahomey.Cbor.Serialization.Converters;
using EdjCase.ICP.Candid.Models;
using System;

namespace Agent.Cbor
{
	internal class HashableCborConverter : CborConverterBase<IHashable>
	{
		private readonly CborConverterRegistry registry;

		public HashableCborConverter(CborConverterRegistry registry)
		{
			this.registry = registry;
		}

		public override IHashable Read(ref CborReader reader)
		{
			// Never reads
			throw new NotImplementedException();
		}

		public override void Write(ref CborWriter writer, IHashable value)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}
			if (value is HashableArray a)
			{
				writer.WriteBeginArray(a.Items.Count);
				foreach (IHashable item in a.Items)
				{
					this.Write(ref writer, item);
				}
				writer.WriteEndArray(a.Items.Count);
			}
			else
			{
				ICborConverter converter = this.registry.Lookup(value.GetType());
				converter.Write(ref writer, value);
			}
		}
	}
}
