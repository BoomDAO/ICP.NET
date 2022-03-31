using Dfinity.Common.Models;
using Dfinity.Common.Types;
using System;
using System.Collections.Generic;
using System.Formats.Cbor;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dfinity.Agent.Cbor
{
    public class PrincipalIdCborConverter : ICborConverter<PrincipalId>
    {
        public PrincipalId Read(ref CborReader reader)
        {
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

            throw new CborContentException("Failed to deserialize PrincipalId, invalid bytes");
        }

        public void Write(ref CborWriter writer, PrincipalId value)
        {
            byte[] rawValue = value.Raw;
            LEB128 byteLength = LEB128.FromUInt64((ulong)rawValue.Length);
            byte[] bytes = new byte[] { 1 } // First byte is 1
                .Concat(byteLength.Raw) // Then a LEB128 of the size of the raw value
                .Concat(rawValue) // Then have the raw byte value
                .ToArray();
            writer.WriteByteString(bytes);
        }
    }
}
