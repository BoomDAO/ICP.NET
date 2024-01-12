using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using Wasmtime;

namespace EdjCase.ICP.BLS
{
	/// <summary>
	/// Class with functions around BLS signatures (ICP flavor only)
	/// </summary>
	public class WasmBlsCryptography : IBlsCryptography
	{
		private const int PublicKeyLength = 96;
		private const int SignatureLength = 48;

		private static object intializeLock = new object();
		private static bool isInitialized = false;
		private static WasmBlsInstance? instance;

		/// <inheritdoc />
		public bool VerifySignature(
			byte[] publicKey,
			byte[] messageHash,
			byte[] signature
		)
		{
			if (signature.Length != SignatureLength)
			{
				throw new ArgumentOutOfRangeException(nameof(signature), signature.Length, $"Signature must be {SignatureLength} bytes long.");
			}
			if (publicKey.Length != PublicKeyLength)
			{
				throw new ArgumentOutOfRangeException(nameof(publicKey), publicKey.Length, $"Public Key must be {PublicKeyLength} bytes long.");
			}

			return VerifySignatureInternal(
				publicKey,
				messageHash,
				signature
			);
		}

		private static bool VerifySignatureInternal(
			byte[] publicKey,
			byte[] messageHash,
			byte[] signature
		)
		{
			try
			{

				EnsureInitialized();
			}
			catch (DllNotFoundException ex)
			{
				throw new Exception("Unable to load the wasmtime bls library", ex);
			}
			byte[] blsPublicKey = instance!.PublicKeyDeserialize(publicKey);

			byte[] blsSignature = instance.SignatureDeserialize(signature);

			return instance.Verify(blsSignature, blsPublicKey, messageHash);
		}


		private static void EnsureInitialized()
		{
			lock (WasmBlsCryptography.intializeLock)
			{
				if (!WasmBlsCryptography.isInitialized)
				{
					if (!Environment.Is64BitProcess)
					{
						throw new PlatformNotSupportedException("not 64-bit system");
					}
					if (instance == null)
					{
						instance = WasmBlsInstance.Create();
					}
					instance.Init(Constants.BLS12_381, Constants.COMPILED_TIME_VAR);

					instance.SetEthSerialization(true);
					instance.SetMapToMode(WasmBlsInstance.MapToMode.HashToCurve);
					string s = "1 0x24aa2b2f08f0a91260805272dc51051c6e47ad4fa403b02b4510b647ae3d1770bac0326a805bbefd48056c8c121bdb8 0x13e02b6052719f607dacd3a088274f65596bd0d09920b61ab5da61bbdc7f5049334cf11213945d57e5ac7d055d042b7e 0x0ce5d527727d6e118cc9cdc6da2e351aadfd9baa8cbdd3a76d429a695160d12c923ac9cc3baca289e193548608b82801 0x0606c4a02ea734cc32acd2b02bc28b99cb3e287e85a763af267492ab572e99ab3f370d275cec1da1aaa9075ff05f79be";
					byte[] gen = instance.PublicKeySetHexStr(s);
					instance.SetGeneratorOfPublicKey(gen);
					string dst = "BLS_SIG_BLS12381G1_XMD:SHA-256_SSWU_RO_NUL_";
					instance.MclBnG1SetDst(dst);
					WasmBlsCryptography.isInitialized = true;
				}
			}
		}
	}
	internal class WasmBlsInstance
	{
		private readonly Instance instance;

		public WasmBlsInstance(Instance instance)
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

		public static WasmBlsInstance Create()
		{
			var engine = new Engine(new Config().WithReferenceTypes(true));
			using Stream stream = typeof(WasmBlsInstance)
				.Assembly
				.GetManifestResourceStream("EdjCase.ICP.BLS.bls.wasm");
			var module = Module.FromStream(engine, "hello", stream);

			var linker = new Linker(engine);
			var store = new Store(engine);



			Instance instance = linker.Instantiate(store, module);



			// TODO dispose?

			return new WasmBlsInstance(instance);
		}
		internal enum MapToMode
		{
			Original = 0, // for backward compatibility
			HashToCurve = 5, // irtf-cfrg-hash-to-curve
		}

		private class MallocScope : IDisposable
		{
			private WasmBlsInstance blsLib;
			public int Address { get; }
			public int Size { get; }

			public MallocScope(WasmBlsInstance blsLib, int address, int size)
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
	}

}
