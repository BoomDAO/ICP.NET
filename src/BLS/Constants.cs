using System;
using System.Collections.Generic;
using System.Text;
using Wasmtime;

namespace EdjCase.ICP.BLS
{
	internal class Constants
	{
		public const int BLS12_381 = 5;

		public const int FR_UNIT_SIZE = 4;
		public const int FP_UNIT_SIZE = 6;
		public const int FP_SIZE = FP_UNIT_SIZE * 8;

		public const int COMPILED_TIME_VAR = FR_UNIT_SIZE * 10 + FP_UNIT_SIZE;


		public const int PUBLICKEY_SIZE = 288;
		public const int SIGNATURE_SIZE = 144;

		public const ulong BLS_X = 0xd201_0000_0001_0000;

	}
}
