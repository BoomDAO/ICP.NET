﻿using Dahomey.Cbor.Serialization;
using Dahomey.Cbor.Serialization.Converters;
using ICP.Common.Encodings;
using ICP.Common.Models;
using System;
using System.Linq;

namespace ICP.Agent.Cbor
{
    public class PrincipalIdCborConverter : CborConverterBase<PrincipalId?>
    {
        public override PrincipalId? Read(ref CborReader reader)
        {
            if (reader.GetCurrentDataItemType() == CborDataItemType.Null)
            {
                return null;
            }
            ReadOnlySpan<byte> raw = reader.ReadByteString();

            if (raw[0] == 1) // first byte must be 1
            {
                int i = 1;
                byte b;
                do
                {
                    b = raw[i];
                }
                while (b >= 0b1000_0000); // Last byte will not have a left most 1

                byte[] bytes = raw.Slice(1, i).ToArray();
                if (LEB128.TryFromRaw(bytes, out LEB128? length))
                {
                    if (length.TryToUInt64(out ulong lengthLong)
                        && lengthLong <= int.MaxValue
                        && (int)lengthLong <= bytes.Length + i)
                    {
                        byte[] rawPrincipalId = raw
                            .Slice(i, (int)lengthLong)
                            .ToArray();
                        return PrincipalId.FromRaw(rawPrincipalId);
                    }
                }
            }
            
            throw new Dahomey.Cbor.CborException("Failed to deserialize PrincipalId, invalid bytes");
        }

        public override void Write(ref CborWriter writer, PrincipalId? value)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }
            writer.WriteByteString(value.Raw);
        }
    }
}
