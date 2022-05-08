using Common.Candid;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICP.Common.Exceptions
{
    public abstract class CandidDeserializationException : Exception
    {

    }

    public class CandidParseException : CandidDeserializationException
    {
        public int ByteEndIndex { get; }
        public string ErrorMessage { get; }
        public CandidParseException(int byteEndIndex, string message)
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

        internal static CandidParseException FromReader(BinaryReader reader, string message)
        {
            return new CandidParseException((int)reader.BaseStream.Position, message);
        }
    }

    public class CandidTypeResolutionException : CandidDeserializationException
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

        internal static CandidParseException FromReader(BinaryReader reader, string message)
        {
            return new CandidParseException((int)reader.BaseStream.Position, message);
        }
    }
}
