using System;
using System.Collections.Generic;
using System.Text;

namespace EdjCase.Cryptography.BLS
{
	internal class Constants
	{
		public const int BLS12_381 = 5;

		public const int FR_UNIT_SIZE = 4;
		public const int FP_UNIT_SIZE = 6;
		public const int BLS_COMPILER_TIME_VAR_ADJ = 0;
		public const int COMPILED_TIME_VAR = FR_UNIT_SIZE * 10 + FP_UNIT_SIZE + BLS_COMPILER_TIME_VAR_ADJ;

		public const int PUBLICKEY_UNIT_SIZE = FP_UNIT_SIZE * 6;
		public const int SIGNATURE_UNIT_SIZE = FP_UNIT_SIZE * 3;

		public const int PUBLICKEY_SERIALIZE_SIZE = PUBLICKEY_UNIT_SIZE * 8;
		public const int SIGNATURE_SERIALIZE_SIZE = SIGNATURE_UNIT_SIZE * 8;
	}
}
