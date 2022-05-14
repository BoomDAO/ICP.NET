using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICP.Candid.Exceptions
{
    public abstract class CandidSerializationException : Exception
    {

    }

    public class CandidSerializationParseException : CandidSerializationException
    {
        public int ByteEndIndex { get; }
        public string ErrorMessage { get; }
        public CandidSerializationParseException(int byteEndIndex, string message)
        {
            this.ByteEndIndex = byteEndIndex;
            this.ErrorMessage = message;
        }

        public override string Message
        {
            get
            {
                return $"Candid failed with deserilizing at byte index {this.ByteEndIndex} with message: {this.ErrorMessage}";
            }
        }

        internal static CandidSerializationParseException FromReader(BinaryReader reader, string message)
        {
            return new CandidSerializationParseException((int)reader.BaseStream.Position, message);
        }
    }

    public class CandidTypeResolutionException : CandidSerializationException
    {
        public string ErrorMessage { get; }
        public CandidTypeResolutionException(string message)
        {
            this.ErrorMessage = message;
        }

        public override string Message
        {
            get
            {
                return $"Candid failed with while resolving a type reference";
            }
        }

        internal static CandidSerializationParseException FromReader(BinaryReader reader, string message)
        {
            return new CandidSerializationParseException((int)reader.BaseStream.Position, message);
        }
    }
}
