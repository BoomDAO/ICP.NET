using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Models;
using ICP.Common.Candid;
using ICP.Common.Crypto;
using ICP.Common.Encodings;

namespace ICP.Common.Models
{
	public class EncodedArgument : IHashable
	{
		public byte[] Value { get; }

		public EncodedArgument(byte[] value)
		{
			this.Value = value;
		}

		public byte[] ComputeHash(IHashFunction hashFunction)
		{
			return hashFunction.ComputeHash(this.Value);
		}

		public static EncodedArgument FromCandid(List<(CandidValue, CandidTypeDefinition)> args)
		{
			IDLBuilder builder = IDLBuilder.FromArgs(args);
			return new EncodedArgument(builder.Encode());
		}

		public static EncodedArgument FromCandid(params (CandidValue, CandidTypeDefinition)[] args)
		{
			IDLBuilder builder = IDLBuilder.FromArgs(args);
			return new EncodedArgument(builder.Encode());
		}
	}
}