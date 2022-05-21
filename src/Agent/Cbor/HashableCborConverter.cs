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
