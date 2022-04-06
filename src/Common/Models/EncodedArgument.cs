using System.Collections.Generic;
using System.Linq;
using System.Text;
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

		public static EncodedArgument FromCandid(List<CandidToken> args)
		{
			TypeTable typeTable = TypeTable.FromArgs(args);

			byte[] encodedPrefix = Encoding.UTF8.GetBytes("DIDL");
			byte[] encodedTable = typeTable.Encode();
			byte[] encodedLength = LEB128.FromUInt64((ulong)args.Count).Raw;
			IEnumerable<byte> encodedTypes = args
				.SelectMany(a => a.EncodeType());
			IEnumerable<byte> encodedValues = args
				.SelectMany(a => a.EncodeValue());

			byte[] encodedBytes = encodedPrefix
				.Concat(encodedTable)
				.Concat(encodedLength)
				.Concat(encodedTypes)
				.Concat(encodedValues)
				.ToArray();

			return new EncodedArgument(encodedBytes);
		}
	}
}