using Dahomey.Cbor.Serialization;
using Dahomey.Cbor.Util;
using Fido2Net;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EdjCase.ICP.InternetIdentity
{

	internal static class WebAuthnIdentitySigner
	{
		private const string clientDataTemplate = "{{\"type\":\"webauthn.get\",\"challenge\":\"{0}\",\"origin\":\"https://identity.ic0.app\",\"crossOrigin\":false}}";
		private const string RpId = "identity.ic0.app";
		private static byte[] authenticator_data = Encoding.ASCII.GetBytes("authenticator_data");
		private static byte[] client_data_json = Encoding.ASCII.GetBytes("client_data_json");
		private static byte[] signature = Encoding.ASCII.GetBytes("signature");


		public static Task<byte[]> Fido2Assert(
			byte[] challenge,
			FidoAssertion assert,
			WebAuthnOptions signerOptions,
			IEnumerable<DeviceInfo> devices)
		{
			return Task.Factory.StartNew(
				function: () => Fido2AssertSync(challenge, assert, signerOptions, devices),
				creationOptions: TaskCreationOptions.DenyChildAttach | TaskCreationOptions.LongRunning
			);
		}

		private static byte[] Fido2AssertSync(
			byte[] challenge,
			FidoAssertion assert,
			WebAuthnOptions signerOptions,
			IEnumerable<DeviceInfo> devices
		)
		{
			using (var device = new FidoDevice())
			{
				try
				{
					string deviceName = GetFidoDeviceNameForSign();
					device.Open(deviceName);

					string clientDataJson = string.Format(clientDataTemplate, UrlBase64.Encode(challenge));
					byte[] clientDataBytes = Encoding.ASCII.GetBytes(clientDataJson);

					// configure the assertion request
					assert.SetClientData(clientDataBytes);
					assert.Rp = RpId;
					foreach (DeviceInfo deviceInfo in devices)
					{
						if (deviceInfo.CredentialId != null)
						{
							assert.AllowCredential(deviceInfo.CredentialId);
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

					// get the assertion
					device.GetAssert(assert, null); // note: blocks for a long time!

					// convert the assertion response into the form required by II (cbor)
					byte[] assertionBytes = SerializeAssertion(assert[0], clientDataJson);
					return assertionBytes;
				}
				finally
				{
					device.Close();
				}
			}
		}


		private static string GetFidoDeviceNameForSign()
		{
			// TODO: on Windows platforms, we must use windows hello (even if we're using an external, i.e. non-platform authenticator).
			// on other platforms, we actually have to pick a device (so give the user some choice, or assume theres only one device?)
			return "windows://hello";
		}



		private static byte[] SerializeAssertion(FidoAssertionStatement assertion, string clientDataJson)
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
				writer.WriteString(clientDataJson);

				writer.WriteString(signature);
				writer.WriteByteString(assertion.Signature);

				writer.WriteEndMap(3);
				return bufferWriter.WrittenSpan.ToArray();
			}
		}
	}
}
