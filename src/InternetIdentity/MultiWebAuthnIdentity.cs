using System.Collections.Generic;
using Fido2Net;
using Dahomey.Cbor.Serialization;
using Dahomey.Cbor.Util;
using System.Threading.Tasks;
using System;
using EdjCase.ICP.Agent.Identities;
using EdjCase.ICP.Agent.Keys;
using System.Linq;

namespace EdjCase.ICP.InternetIdentity
{

	internal static class Fido2Ext
	{
		public static DisposeAction OpenAuto(this FidoDevice device, string deviceName)
		{
			device.Open(deviceName);
			return new DisposeAction(() => device.Close());
		}

		public class DisposeAction : IDisposable
		{
			public DisposeAction(System.Action act)
			{
				this._act = act;
			}

			public void Dispose()
			{
				this._act?.Invoke();
			}

			private System.Action _act;
		}
	}

	public class WebAuthnIdentitySignerOptions
	{
		public TimeSpan Timeout { get; } = TimeSpan.FromSeconds(60.0);

		public static readonly WebAuthnIdentitySignerOptions Default = new WebAuthnIdentitySignerOptions();
	}

	public static class WebAuthnIdentitySigner
	{
		private static string ClientDataTemplate = "{{\"type\":\"webauthn.get\",\"challenge\":\"{0}\",\"origin\":\"https://identity.ic0.app\",\"crossOrigin\":false}}";
		private static string RpId = "identity.ic0.app";
		private static byte[] authenticator_data = System.Text.Encoding.ASCII.GetBytes("authenticator_data");
		private static byte[] client_data_json = System.Text.Encoding.ASCII.GetBytes("client_data_json");
		private static byte[] signature = System.Text.Encoding.ASCII.GetBytes("signature");

		private static string GetFidoDeviceNameForSign()
		{
			// TODO: on Windows platforms, we must use windows hello (even if we're using an external, i.e. non-platform authenticator).
			// on other platforms, we actually have to pick a device (so give the user some choice, or assume theres only one device?)
			return "windows://hello";
		}

		private static (string, byte[]) GetClientData(byte[] challenge)
		{
			var str = string.Format(ClientDataTemplate, UrlBase64.Encode(challenge));
			var bytes = System.Text.Encoding.ASCII.GetBytes(str);
			return (str, bytes);
		}


		private static byte[] SerializeAssertion(FidoAssertionStatement assertion, string clientDataStr)
		{
			using (ByteBufferWriter bufferWriter = new ByteBufferWriter())
			{
				var writer = new CborWriter(bufferWriter);
				writer.WriteSemanticTag(55799);

				writer.WriteBeginMap(3);

				// TODO: figure out why the authenticator data has garbage at the start!?
				// this is super fragile...
				writer.WriteString(authenticator_data);
				writer.WriteByteString(assertion.AuthData.Slice(2));

				writer.WriteString(client_data_json);
				writer.WriteString(clientDataStr);

				writer.WriteString(signature);
				writer.WriteByteString(assertion.Signature);

				writer.WriteEndMap(3);
				return bufferWriter.WrittenSpan.ToArray();
			}
		}

		public static byte[] Fido2AssertSync(
			byte[] challenge,
			FidoAssertion assert,
			WebAuthnIdentitySignerOptions signerOptions,
			IEnumerable<byte[]> deviceCredentials
		)
		{
			using var device = new FidoDevice();
			using var _opened = device.OpenAuto(GetFidoDeviceNameForSign());

			var (clientData, clientDataBytes) = GetClientData(challenge);

			// configure the assertion request
			assert.SetClientData(clientDataBytes);
			assert.Rp = RpId;
			foreach (byte[] credential in deviceCredentials)
			{
				assert.AllowCredential(credential);
			}

			assert.SetExtensions(FidoExtensions.None);

			// these aren't required by the II spec, but may be useful? not clear if it really matters
			//assert.SetUserPresenceRequired(false);
			//assert.SetUserVerificationRequired(false);

			// confiugre the device
			//if (options.timeout.TotalSeconds > 0.0f)
			//{
			//	device.SetTimeout(options.timeout);
			//}

			// get the assertion
			device.GetAssert(assert, null); // note: blocks for a long time!

			// convert the assertion response into the form required by II (cbor)
			return SerializeAssertion(assert[0], clientData);
		}

		public static Task<byte[]> Fido2Assert(byte[] challenge, FidoAssertion assert, WebAuthnIdentitySignerOptions signerOptions, IEnumerable<byte[]> deviceCredentials)
		{
			return Task.Factory.StartNew(
				function: () => Fido2AssertSync(challenge, assert, signerOptions, deviceCredentials),
				creationOptions: TaskCreationOptions.DenyChildAttach | TaskCreationOptions.LongRunning
			);
		}
	}

	public class WebAuthnIdentity : SigningIdentityBase
	{
		public byte[] DeviceCredential { get; }
		public WebAuthnIdentitySignerOptions SignerOptions { get; }
		public IPublicKey PublicKey { get; }

		public WebAuthnIdentity(byte[] deviceCredential, byte[] devicePublicKey, WebAuthnIdentitySignerOptions? signerOptions = null)
		{
			this.DeviceCredential = deviceCredential ?? throw new ArgumentNullException(nameof(deviceCredential));
			this.SignerOptions = signerOptions ?? WebAuthnIdentitySignerOptions.Default;
			this.PublicKey = DerCosePublicKey.FromDer(devicePublicKey);
		}

		public override IPublicKey GetPublicKey() => this.PublicKey;


		public override async Task<byte[]> SignAsync(byte[] sign)
		{
			using var assert = new FidoAssertion();
			IEnumerable<byte[]> deviceCredentials = Enumerable.Repeat(this.DeviceCredential, 1);
			return await WebAuthnIdentitySigner.Fido2Assert(sign, assert, this.SignerOptions, deviceCredentials);
		}
	}

	public class MultiWebAuthnIdentity : SigningIdentityBase
	{
		public IEnumerable<byte[]> DeviceCredentials { get; }
		public WebAuthnIdentitySignerOptions SignerOptions { get; }
		public SigningIdentityBase? selectedIdentity { get => this._selectedIdentity; }

		public MultiWebAuthnIdentity(IEnumerable<byte[]> deviceCredentials, WebAuthnIdentitySignerOptions? signerOptions = null)
		{
			this.DeviceCredentials = deviceCredentials ?? throw new ArgumentNullException(nameof(deviceCredentials));
			this.SignerOptions = signerOptions ?? WebAuthnIdentitySignerOptions.Default;
		}

		public static MultiWebAuthnIdentity FromDevices(IEnumerable<byte[]> devices)
		{
			return new MultiWebAuthnIdentity(devices);
		}

		public override IPublicKey GetPublicKey()
		{
			if (this._selectedIdentity != null)
			{
				return this._selectedIdentity.GetPublicKey();
			}
			else
			{
				throw new InvalidOperationException("Cannot use MultiWebAuthnIdentity.GetPublicKey before the first call to Sign");
			}
		}

		private static bool SpanEquals(ReadOnlySpan<byte> a, IReadOnlyList<byte> b)
		{
			if (a.Length != b.Count) return false;

			for (int i = 0; i < a.Length; ++i)
			{
				if (a[i] != b[i]) return false;
			}
			return true;
		}

		public override async Task<byte[]> SignAsync(byte[] sign)
		{
			if (this._selectedIdentity != null)
			{
				return await this._selectedIdentity.SignAsync(sign);
			}

			using var assert = new FidoAssertion();
			var signature = await WebAuthnIdentitySigner.Fido2Assert(sign, assert, this.SignerOptions, this.DeviceCredentials);
			return this.ConvSignature(assert, signature);
		}

		private byte[] ConvSignature(FidoAssertion assert, byte[] signature)
		{
			var id = assert[0].Id;

			// find the device which the user actually signed with, and use that device exclusively in the future.
			// its unclear why this really matters (we never sign with this identity but once), but II does this, so replicate it here.
			foreach (byte[] credential in this.DeviceCredentials)
			{
				if (SpanEquals(id, credential))
				{
					this._selectedIdentity = new WebAuthnIdentity(credential, devicePublicKey, this.SignerOptions);
					break;
				}
			}

			return signature;
		}

		private SigningIdentityBase? _selectedIdentity;
	}
}
