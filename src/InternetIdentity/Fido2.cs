using Dahomey.Cbor.Serialization;
using Dahomey.Cbor.Util;
using EdjCase.ICP.Agent;
using Fido2Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdjCase.ICP.InternetIdentity
{

	internal static class Fido2
	{
		private const string clientDataTemplate = "{{\"type\":\"webauthn.get\",\"challenge\":\"{0}\",\"origin\":\"https://identity.ic0.app\",\"crossOrigin\":false}}";
		private const string RpId = "identity.ic0.app";
		private static byte[] authenticator_data = Encoding.ASCII.GetBytes("authenticator_data");
		private static byte[] client_data_json = Encoding.ASCII.GetBytes("client_data_json");
		private static byte[] signature = Encoding.ASCII.GetBytes("signature");


		public static Task<(DerEncodedPublicKey PublicKey, byte[] Signature)> SignAsync(
			byte[] challenge,
			FidoAssertion assert,
			IList<DeviceInfo> devices)
		{
			return Task.Factory.StartNew(
				function: () => Sign(challenge, assert, devices),
				creationOptions: TaskCreationOptions.DenyChildAttach | TaskCreationOptions.LongRunning
			);
		}

		public static (DerEncodedPublicKey PublicKey, byte[] Signature) Sign(
			byte[] challenge,
			FidoAssertion assert,
			IList<DeviceInfo> devices
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
					foreach (DeviceInfo d in devices)
					{
						if (d.CredentialId != null)
						{
							assert.AllowCredential(d.CredentialId);
						}
					}

					assert.SetExtensions(FidoExtensions.None);

					// get the assertion
					device.GetAssert(assert, null); // note: blocks for a long time!

					// convert the assertion response into the form required by II (cbor)
					byte[] assertionBytes = SerializeAssertion(assert[0], clientDataJson);
					ReadOnlySpan<byte> a = assert[0].Id;
					DeviceInfo chosenDevice = devices
						.First(d => assert[0].Id.SequenceEqual(d.CredentialId));

					return (chosenDevice.PublicKey, assertionBytes);
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
