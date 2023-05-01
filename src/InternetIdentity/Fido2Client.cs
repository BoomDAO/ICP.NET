
using EdjCase.ICP.Agent;
using Fido2Net;
using System;
using System.Collections.Generic;
using System.Formats.Cbor;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdjCase.ICP.InternetIdentity
{

	internal interface IFido2Client
	{
		Task<(SubjectPublicKeyInfo PublicKey, byte[] Signature)?> SignAsync(byte[] challenge, IList<DeviceInfo> devices);
	}

	internal class Fido2Client : IFido2Client
	{
		private const string clientDataTemplate = "{{\"type\":\"webauthn.get\",\"challenge\":\"{0}\",\"origin\":\"https://identity.ic0.app\",\"crossOrigin\":false}}";
		private const string RpId = "identity.ic0.app";
		private static byte[] authenticator_data = Encoding.ASCII.GetBytes("authenticator_data");
		private static byte[] client_data_json = Encoding.ASCII.GetBytes("client_data_json");
		private static byte[] signature = Encoding.ASCII.GetBytes("signature");


		public Task<(SubjectPublicKeyInfo PublicKey, byte[] Signature)?> SignAsync(byte[] challenge, IList<DeviceInfo> devices)
		{
			return Task.Factory.StartNew(
				function: () => Sign(challenge, devices),
				creationOptions: TaskCreationOptions.DenyChildAttach | TaskCreationOptions.LongRunning
			);
		}

		public static (SubjectPublicKeyInfo PublicKey, byte[] Signature)? Sign(byte[] challenge, IList<DeviceInfo> devices)
		{
			using (var assert = new FidoAssertion())
			{
				using (var device = new FidoDevice())
				{
					try
					{
						string? deviceName;
						using (var devlist = new FidoDeviceInfoList(64))
						{
							// todo what about multiple devices
							deviceName = devlist.Select(d => d.Path).FirstOrDefault();
						}
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

						DeviceInfo? chosenDevice = devices
							.FirstOrDefault(d => assert[0].Id.SequenceEqual(d.CredentialId));
						if (chosenDevice == null)
						{
							return null;
						}

						byte[] signature = CreateSignatureFromAssertion(assert[0], clientDataJson);

						return (chosenDevice.PublicKey, signature);
					}
					finally
					{
						device.Close();
					}
				}
			}
		}



		/// <summary>
		/// The signature is a CBOR value consisting of a data item with major type 6 ("Semantic tag")
		/// and tag value 55799, followed by a map with three mandatory fields:
		/// authenticator_data, client_data_json and signature
		/// </summary>
		private static byte[] CreateSignatureFromAssertion(FidoAssertionStatement assertion, string clientDataJson)
		{
				var writer = new CborWriter();
				writer.WriteTag(CborTag.SelfDescribeCbor);

				writer.WriteStartMap(3);

				// TODO: figure out why the authenticator data has garbage at the start!?
				// this is super fragile...
				writer.WriteByteString(authenticator_data);
				writer.WriteByteString(assertion.AuthData.Slice(2));

				writer.WriteByteString(client_data_json);
				writer.WriteTextString(clientDataJson);

				writer.WriteByteString(signature);
				writer.WriteByteString(assertion.Signature);

				writer.WriteEndMap();
				return writer.Encode();
		}

	}
}
