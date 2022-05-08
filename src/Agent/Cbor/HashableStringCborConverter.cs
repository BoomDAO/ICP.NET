using Dahomey.Cbor.Serialization;
using Dahomey.Cbor.Serialization.Converters;
using Dahomey.Cbor.Serialization.Converters.Mappings;
using ICP.Candid.Encodings;
using ICP.Candid.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace ICP.Agent.Cbor
{
    public class HashableStringCborConverter : CborConverterBase<HashableString?>
    {

        public override HashableString? Read(ref CborReader reader)
        {
            if (reader.GetCurrentDataItemType() == CborDataItemType.Null)
            {
                return null;
            }
            string? value = reader.ReadString();
            return value == null ? null : new HashableString(value);
        }
        public override void Write(ref CborWriter writer, HashableString? value)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }
            writer.WriteString(value.Value);
        }
    }
}
