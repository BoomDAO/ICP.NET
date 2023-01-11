using EdjCase.ICP.Agent;
using EdjCase.ICP.Agent.Auth;
using EdjCase.ICP.Candid.Models;
using System.Collections.Generic;
using IIClient = EdjCase.ICP.InternetIdentity.Models;
using Fido2Net;
using Dahomey.Cbor.Serialization;
using Dahomey.Cbor.Util;
using System.Threading.Tasks;

namespace EdjCase.ICP.InternetIdentity
{
	public class WebAuthnIdentitySignerOptions
	{
		public readonly System.TimeSpan timeout = System.TimeSpan.FromSeconds(60.0);

		public static readonly WebAuthnIdentitySignerOptions Default = new WebAuthnIdentitySignerOptions();
	}

	public static class WebAuthnIdentitySigner
	{
		private static string ClientDataTemplate = "{{\"type\": \"webauthn.get\", \"challenge\": \"{0}\", \"origin\": \"https://identity.ic0.app\", \"crossOrigin\": false}}";
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

		public static async Task<byte[]> Fido2Assert(byte[] challenge, FidoAssertion assert, WebAuthnIdentitySignerOptions options, IEnumerable<IIClient.DeviceData> devices)
		{
			using var device = new FidoDevice();
			device.Open(GetFidoDeviceNameForSign());

			var (clientData, clientDataBytes) = GetClientData(challenge);

			// configure the assertion request
			assert.SetClientData(clientDataBytes);
			assert.Rp = RpId;
			foreach (var d in devices)
			{
				if (d.CredentialId.TryGetValue(out var cred))
				{
					// TODO: avoid copying here! CredentialId doesn't need to be a List
					assert.AllowCredential(cred.ToArray());
				}
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

			// get the assertion, and close the device
			device.GetAssert(assert, null); // note: blocks for a long time!
			device.Close();

			// convert the assertion response into the form required by II (cbor)
			return SerializeAssertion(assert[0], clientData);
		}

	}

	public class WebAuthnIdentity : SigningIdentityBase
	{
		public readonly IIClient.DeviceData device;
		public readonly WebAuthnIdentitySignerOptions signerOptions;
		public IPublicKey publicKey { get; }

		public WebAuthnIdentity(IIClient.DeviceData device, WebAuthnIdentitySignerOptions? signerOptions = null)
		{
			this.device = device ?? throw new System.InvalidOperationException("Devices must be non-null");
			this.signerOptions = signerOptions ?? WebAuthnIdentitySignerOptions.Default;
			this.publicKey = DerCosePublicKey.FromDer(device.Pubkey.ToArray());
		}

		public override IPublicKey GetPublicKey() => this.publicKey;

		private IEnumerable<IIClient.DeviceData> GetDevices()
		{
			yield return this.device;
		}

		public override async Task<Signature> Sign(byte[] sign)
		{
			using var assert = new FidoAssertion();
			return new Signature(await WebAuthnIdentitySigner.Fido2Assert(sign, assert, this.signerOptions, this.GetDevices()));
		}
	}

	public class MultiWebAuthnIdentity : SigningIdentityBase
	{
		public readonly IEnumerable<IIClient.DeviceData> devices;
		public readonly WebAuthnIdentitySignerOptions signerOptions;
		public SigningIdentityBase? selectedIdentity { get => this._selectedIdentity; }

		public MultiWebAuthnIdentity(IEnumerable<IIClient.DeviceData> devices, WebAuthnIdentitySignerOptions? signerOptions = null)
		{
			this.devices = devices ?? throw new System.InvalidOperationException("Devices must be non-null");
			this.signerOptions = signerOptions ?? WebAuthnIdentitySignerOptions.Default;
		}

		public static MultiWebAuthnIdentity FromDevices(IEnumerable<IIClient.DeviceData> devices)
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
				throw new System.InvalidOperationException("Cannot use MultiWebAuthnIdentity.GetPublicKey before the first call to Sign");
			}
		}

		private static bool SpanEquals(System.ReadOnlySpan<byte> a, IReadOnlyList<byte> b)
		{
			if (a.Length != b.Count) return false;

			for (int i = 0; i < a.Length; ++i)
			{
				if (a[i] != b[i]) return false;
			}
			return true;
		}

		public override async Task<Signature> Sign(byte[] sign)
		{
			if (this._selectedIdentity != null)
			{
				return await this._selectedIdentity.Sign(sign);
			}

			using var assert = new FidoAssertion();
			var signature = await WebAuthnIdentitySigner.Fido2Assert(sign, assert, this.signerOptions, this.devices);
			return this.ConvSignature(assert, signature);
		}

		private Signature ConvSignature(FidoAssertion assert, byte[] signature)
		{
			var id = assert[0].Id;

			// find the device which the user actually signed with, and use that device exclusively in the future.
			// its unclear why this really matters (we never sign with this identity but once), but II does this, so replicate it here.
			foreach (var d in this.devices)
			{
				if (d.CredentialId.TryGetValue(out var cred) && SpanEquals(id, cred))
				{
					this._selectedIdentity = new WebAuthnIdentity(d, this.signerOptions);
					break;
				}
			}

			return new Signature(signature);
		}

		private SigningIdentityBase? _selectedIdentity;
	}
}
