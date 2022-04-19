using Dahomey.Cbor.Serialization;
using Dahomey.Cbor.Serialization.Converters;
using ICP.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agent.Cbor
{
    internal class HashbleCborConverter : CborConverterBase<IHashable>
    {
        private readonly CborConverterRegistry registry;

        public HashbleCborConverter(CborConverterRegistry registry)
        {
            this.registry = registry;
        }

        public override IHashable Read(ref CborReader reader)
        {
            throw new NotImplementedException();
        }

        public override void Write(ref CborWriter writer, IHashable value)
        {
            if(value == null)
            {
                writer.WriteNull();
                return;
            }
            ICborConverter converter = this.registry.Lookup(value.GetType());
            converter.Write(ref writer, value);
        }
    }
}
