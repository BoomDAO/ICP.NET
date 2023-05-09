using System;
using System.IO;
using System.Text;
using Wasmtime;

namespace EdjCase.ICP.BLS
{
	internal class BlsLib
	{
		private readonly Instance instance;

		public BlsLib(
			Instance instance)
		{
			this.instance = instance;
		}

		public void Init(int curve, int compiledVar)
		{
			int error = this.instance
				.GetFunction<int, int, int>("blsInit")!
				.Invoke(curve, compiledVar);
			if (error != 0)
			{
				throw new Exception("BLS failed to initialize with error code: " + error);
			}
		}

		public void SetEthSerialization(bool isEth)
		{
			this.instance
				.GetAction<int>("blsSetETHserialization")!
				.Invoke(isEth ? 1 : 0);
		}

		public void SetMapToMode(MapToMode mode)
		{
			int error = this.instance
				.GetFunction<int, int>("blsSetMapToMode")!
				.Invoke((int)mode);

			if (error != 0)
			{
				throw new Exception("Failed while invoking: SetMapToMode");
			}
		}

		public void SetGeneratorOfPublicKey(byte[] publicKey)
		{
			using MallocScope scope = this.DisposableMalloc(publicKey.Length);
			scope.SetValue(publicKey);

			int error = this.instance
				.GetFunction<int, int>("blsSetGeneratorOfPublicKey")!
				.Invoke(scope.Address);
			if (error != 0)
			{
				throw new Exception("Failed while invoking: SetGeneratorOfPublicKey");
			}
		}

		private MallocScope DisposableMalloc(int size)
		{
			int address = this.Malloc(size);
			return new MallocScope(this, address, size);
		}


		public int Malloc(int size)
		{
			return this.instance
				.GetFunction<int, int>("blsMalloc")!
				.Invoke(size);
		}

		public void Free(int address)
		{
			this.instance
				.GetAction<int>("blsFree")!
				.Invoke(address);
		}

		public void MclBnG1SetDst(string dst)
		{
			using MallocScope dstScope = this.DisposableMalloc(dst.Length);
			dstScope.SetValue(Encoding.ASCII.GetBytes(dst));
			int error = this.instance
				.GetFunction<int, int, int>("mclBnG1_setDst")!
				.Invoke(dstScope.Address, dst.Length);
			if (error != 0)
			{
				throw new Exception("Failed while invoking: SetDstG1:" + dst);
			}
		}

		public byte[] PublicKeyDeserialize(byte[] publicKeyBytes)
		{
			using MallocScope keyScope = this.DisposableMalloc(Constants.PUBLICKEY_SIZE);
			using MallocScope bytesScope = this.DisposableMalloc(publicKeyBytes.Length);
			bytesScope.SetValue(publicKeyBytes);

			int publicKeyBytesRead = this.instance
				.GetFunction<int, int, int, int>("blsPublicKeyDeserialize")!
				.Invoke(keyScope.Address, bytesScope.Address, publicKeyBytes.Length);
			if (publicKeyBytesRead != publicKeyBytes.Length)
			{
				throw new Exception($"Error deserializing BLS public key");
			}
			return keyScope.GetValue();
		}

		public byte[] SignatureDeserialize(byte[] signatureBytes)
		{
			using MallocScope sigScope = this.DisposableMalloc(Constants.SIGNATURE_SIZE);
			using MallocScope bytesScope = this.DisposableMalloc(signatureBytes.Length);
			bytesScope.SetValue(signatureBytes);
			

			int signatureBytesRead = this.instance
				.GetFunction<int, int, int, int>("blsSignatureDeserialize")!
				.Invoke(sigScope.Address, bytesScope.Address, signatureBytes.Length);
			if (signatureBytesRead != signatureBytes.LongLength)
			{
				throw new Exception($"Error deserializing BLS signature, length: {signatureBytesRead}");
			}
			return sigScope.GetValue();
		}

		public bool Verify(byte[] signature, byte[] publicKey, byte[] message)
		{
			using MallocScope sigScope = this.DisposableMalloc(signature.Length);
			sigScope.SetValue(signature);

			using MallocScope keyScope = this.DisposableMalloc(publicKey.Length);
			keyScope.SetValue(publicKey);

			using MallocScope msgScope = this.DisposableMalloc(message.Length);
			msgScope.SetValue(message);

			int verifyResult = this.instance
				.GetFunction<int, int, int, int, int>("blsVerify")!
				.Invoke(sigScope.Address, keyScope.Address, msgScope.Address, message.Length);
			return verifyResult == 1;
		}

		public byte[] PublicKeySetHexStr(string hex)
		{
			byte[] hexBytes = Encoding.ASCII.GetBytes(hex);
			using MallocScope hexScope = this.DisposableMalloc(hexBytes.Length);
			hexScope.SetValue(hexBytes);

			using MallocScope keyScope = this.DisposableMalloc(Constants.PUBLICKEY_SIZE);
			int error = this.instance
				.GetFunction<int, int, int, int>("blsPublicKeySetHexStr")!
				.Invoke(keyScope.Address, hexScope.Address, hex.Length);

			if (error != 0)
			{
				throw new ArgumentException("blsPublicKeySetStr:" + hex);
			}
			return keyScope.GetValue();
		}

		public static BlsLib Create()
		{
			var engine = new Engine(new Config().WithReferenceTypes(true));
			using Stream stream = typeof(BlsUtil)
				.Assembly
				.GetManifestResourceStream("EdjCase.ICP.BLS.bls.wasm");
			var module = Module.FromStream(engine, "hello", stream);

			var linker = new Linker(engine);
			var store = new Store(engine);



			Instance instance = linker.Instantiate(store, module);



			// TODO dispose?

			return new BlsLib(instance);
		}
		internal enum MapToMode
		{
			Original = 0, // for backward compatibility
			HashToCurve = 5, // irtf-cfrg-hash-to-curve
		}

		private class MallocScope : IDisposable
		{
			private BlsLib blsLib;
			public int Address { get; }
			public int Size { get; }

			public MallocScope(BlsLib blsLib, int address, int size)
			{
				this.blsLib = blsLib;
				this.Address = address;
				this.Size = size;
			}

			public void Dispose()
			{
				this.blsLib.Free(this.Address);
			}

			public void SetValue(Span<byte> value)
			{
				Span<byte> dst = this.blsLib.instance.GetMemory("memory")!
					.GetSpan(this.Address, this.Size);
				value.CopyTo(dst);
			}

			public void SetValue<T>(T value)
				where T : unmanaged
			{
				this.blsLib.instance.GetMemory("memory")!
					.Write(this.Address, value);
			}

			public byte[] GetValue()
			{
				return this.blsLib.instance.GetMemory("memory")!
					.GetSpan(this.Address, this.Size)
					.ToArray();
			}

			public T GetValue<T>()
				where T : unmanaged
			{
				return this.blsLib.instance.GetMemory("memory")!
					.Read<T>(this.Address);
			}
		}


		//[StructLayout(LayoutKind.Sequential)]
		//internal unsafe struct PublicKey
		//{
		//	public fixed uint v[Constants.PUBLICKEY_UNIT_SIZE];
		//}

		//[StructLayout(LayoutKind.Sequential)]
		//internal unsafe struct Signature
		//{
		//	public fixed ulong v[Constants.SIGNATURE_UNIT_SIZE];
		//}
	}
}
