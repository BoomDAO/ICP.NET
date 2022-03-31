using System;
using System.Collections.Generic;
using System.Formats.Cbor;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dfinity.Agent.Cbor
{
    public interface ICborConverter<T>
    {
        T Read(ref CborReader reader);
        void Write(ref CborWriter writer, T value);
    }
}
