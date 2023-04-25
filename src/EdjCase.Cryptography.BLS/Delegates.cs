using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using static EdjCase.Cryptography.BLS.Interop;

namespace EdjCase.Cryptography.BLS
{
	internal static class Delegates
	{
		public delegate int Init(int curveType, int compiledTimeVar);
		public delegate void SetEthSerialization(int mode);
		public delegate int SetMapToMode(int mode);
		public delegate int SetGeneratorOfPublicKey(in Interop.PublicKey pub);
		public delegate int MclBnG1SetDst([In][MarshalAs(UnmanagedType.LPStr)] string dst, ulong dstSize);
		public delegate ulong PublicKeyDeserialize(ref Interop.PublicKey pub, [In] byte[] buf, ulong bufSize);
		public delegate ulong SignatureDeserialize(ref Signature sig, [In] byte[] buf, ulong bufSize);
		public delegate int Verify(in Signature sig, in PublicKey pub, [In] byte[] buf, ulong size);
		public delegate int PublicKeySetHexStr(ref PublicKey pub, [In][MarshalAs(UnmanagedType.LPStr)] string buf, ulong bufSize);
	}
}
