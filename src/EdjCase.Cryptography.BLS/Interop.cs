using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace EdjCase.Cryptography.BLS
{
	// Modified from https://github.com/NethermindEth/cortex-cryptography-bls/blob/641f25297465e494dfcf62cd31ec44d6dc86a927/src/Cortex.Cryptography.Bls/Bls384Interop.cs
	internal static class Interop
	{
		public const int MCL_BLS12_381 = 5;

		//#define MCLBN_COMPILED_TIME_VAR ((MCLBN_FR_UNIT_SIZE) * 10 + (MCLBN_FP_UNIT_SIZE))
		public const int MCLBN_COMPILED_TIME_VAR = MCLBN_FR_UNIT_SIZE * 10 + MCLBN_FP_UNIT_SIZE;

		// This will search and load bls384_256.dll on Windows, and libbls384_256.dll on Linux
		private const string DllName = "lib/bls384_256";

		//#define MCLBN_FP_UNIT_SIZE 6
		private const int MCLBN_FP_UNIT_SIZE = 6;

		private const int MCLBN_FR_UNIT_SIZE = 4;






		//initialize this library
		//call this once before using the other functions
		//@param curve [in] enum value defined in mcl/bn.h
		//@param compiledTimeVar [in] specify MCLBN_COMPILED_TIME_VAR,
		//which macro is used to make sure that the values
		//are the same when the library is built and used
		//@return 0 if success
		//@note blsInit() is not thread safe
		// BLS_DLL_API int blsInit(int curve, int compiledTimeVar);
		[DllImport(DllName, EntryPoint = "blsInit")]
		public static extern int Init(int curve, int compiledTimeVar);

		// return 1 if valid
		//BLS_DLL_API int blsVerifyHash(const blsSignature* sig, const blsPublicKey* pub, const void* h, mclSize size);
		[DllImport("bls384_256", EntryPoint = "blsVerifyHash")]
		public static extern unsafe int VerifyHash(ref BlsSignature sig, ref BlsPublicKey pub, byte* h, int size);

		//BLS_DLL_API mclSize blsPublicKeyDeserialize(blsPublicKey* pub, const void* buf, mclSize bufSize);
		[DllImport(DllName, EntryPoint = "blsPublicKeyDeserialize")]
		public static extern unsafe int PublicKeyDeserialize(ref BlsPublicKey pub, [In] byte[] buf, ulong bufSize);

		//BLS_DLL_API mclSize blsSignatureDeserialize(blsSignature* sig, const void* buf, mclSize bufSize);
		[DllImport(DllName, EntryPoint = "blsSignatureDeserialize")]
		public static extern unsafe int SignatureDeserialize([In, Out] ref BlsSignature sig, byte* buf, int bufSize);









		public struct BlsSignature
		{
			public MclBnG2 v;

			public override string ToString()
			{
				return this.v.ToString();
			}
		}
		public struct MclBnG2
		{
			public MclBnFp2 x;
			public MclBnFp2 y;
			public MclBnFp2 z;

			public override string ToString()
			{
				return $"G2(x={this.x},y={this.y},z={this.z})";
			}
		}
		public struct MclBnFp2
		{
			public MclBnFp d_0;
			public MclBnFp d_1;

			public override string ToString()
			{
				return $"FP2({this.d_0},{this.d_1})";
			}
		}
		public struct MclBnFp
		{
			public ulong d_0;
			public ulong d_1;
			public ulong d_2;
			public ulong d_3;
			public ulong d_4;
			public ulong d_5;

			public override string ToString()
			{
				return $"FP(ulong[{this.d_0:x},{this.d_1:x},{this.d_2:x},{this.d_3:x},{this.d_4:x},{this.d_5:x}])";
			}
		}
		[StructLayout(LayoutKind.Sequential)]
		public unsafe struct BlsPublicKey
		{
			private fixed ulong v[MCLBN_FP_UNIT_SIZE * 6];
		}
	}

}
