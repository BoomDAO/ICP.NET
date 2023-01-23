using EdjCase.ICP.Agent;
using EdjCase.ICP.Agent.Identities;
using EdjCase.ICP.Agent.Models;
using EdjCase.ICP.Candid.Models;
using EdjCase.ICP.InternetIdentity;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ICP.Candid.Tests
{
	public class InternetIdentityTests
	{
		[Fact]
		public async Task Authenticator__LoginAsync()
		{
			Ed25519Identity deviceIdentity = Ed25519Identity.Generate();

			List<DeviceInfo> devices = new List<DeviceInfo>
			{
				new DeviceInfo(deviceIdentity.PublicKey, null)
			};
			ulong anchor = 1;
			string clientHostname = "nns.ic0.app";
			Ed25519Identity sessionIdentity = Ed25519Identity.Generate();
			TimeSpan maxTimeToLive = TimeSpan.FromSeconds(60);
			byte[] challenge = new byte[32];
			var canisterId = Principal.FromText("rrkah-fqaaa-aaaaa-aaaaq-cai");

			var delegation = new Delegation(sessionIdentity.PublicKey.Value, ICTimestamp.Future(maxTimeToLive), targets: null, senders: null);
			byte[] deviceSignature = await deviceIdentity.SignAsync(challenge);
			var delegations = new List<SignedDelegation>
			{
				new SignedDelegation(delegation, deviceSignature)
			};
			var chain = new DelegationChain(deviceIdentity.PublicKey, delegations);
			DelegationIdentity delegationIdentity = new DelegationIdentity(sessionIdentity, chain);

			var identityClient = new Mock<IInternetIdentityClient>(MockBehavior.Strict);
			identityClient
				.Setup(i => i.LookupAsync(anchor))
				.ReturnsAsync(devices);
			identityClient
				.Setup(i => i.PrepareAndGetDelegationAsync(anchor, clientHostname, It.IsAny<DelegationIdentity>(), sessionIdentity, maxTimeToLive))
				.ReturnsAsync(delegationIdentity);
			identityClient
				.Setup(i => i.GetCanisterId())
				.Returns(canisterId);

			var fidoClient = new Mock<IFido2Client>(MockBehavior.Strict);
			fidoClient
				.Setup(f => f.SignAsync(It.IsAny<byte[]>(), devices))
				.ReturnsAsync((deviceIdentity.PublicKey, deviceSignature));
			var authentiator = new Authenticator(identityClient.Object, fidoClient.Object);
			LoginResult result = await authentiator.LoginAsync(anchor, clientHostname, sessionIdentity, maxTimeToLive);
			Assert.True(result.IsSuccessful);
			DelegationIdentity actualDelegationIdentity = result.AsSuccessful();
			Assert.NotNull(actualDelegationIdentity);
			Assert.Equal(delegationIdentity, actualDelegationIdentity);
		}

		[Fact]
		public async Task Authenticator__LoginAsync__No_Devices__Error()
		{
			Ed25519Identity deviceIdentity = Ed25519Identity.Generate();

			List<DeviceInfo> devices = new List<DeviceInfo>();
			ulong anchor = 1;
			string clientHostname = "nns.ic0.app";
			Ed25519Identity sessionIdentity = Ed25519Identity.Generate();
			TimeSpan maxTimeToLive = TimeSpan.FromSeconds(60);
			byte[] challenge = new byte[32];

			var identityClient = new Mock<IInternetIdentityClient>(MockBehavior.Strict);
			identityClient
				.Setup(i => i.LookupAsync(anchor))
				.ReturnsAsync(devices);

			var fidoClient = new Mock<IFido2Client>(MockBehavior.Strict);

			var authentiator = new Authenticator(identityClient.Object, fidoClient.Object);
			LoginResult result = await authentiator.LoginAsync(anchor, clientHostname, sessionIdentity, maxTimeToLive);
			Assert.False(result.IsSuccessful);
			ErrorType failure = result.AsFailure();
			Assert.Equal(ErrorType.InvalidAnchorOrNoDevices, failure);
		}

		[Fact]
		public async Task Authenticator__LoginAsync__Auth_Fail__Error()
		{
			Ed25519Identity deviceIdentity = Ed25519Identity.Generate();
			List<DeviceInfo> devices = new List<DeviceInfo>
			{
				new DeviceInfo(deviceIdentity.PublicKey, null)
			};
			ulong anchor = 1;
			string clientHostname = "nns.ic0.app";
			Ed25519Identity sessionIdentity = Ed25519Identity.Generate();
			TimeSpan maxTimeToLive = TimeSpan.FromSeconds(60);
			byte[] challenge = new byte[32];
			var canisterId = Principal.FromText("rrkah-fqaaa-aaaaa-aaaaq-cai");

			var delegation = new Delegation(sessionIdentity.PublicKey.Value, ICTimestamp.Future(maxTimeToLive), targets: null, senders: null);
			byte[] deviceSignature = await deviceIdentity.SignAsync(challenge);
			var delegations = new List<SignedDelegation>
			{
				new SignedDelegation(delegation, deviceSignature)
			};
			var chain = new DelegationChain(deviceIdentity.PublicKey, delegations);
			DelegationIdentity delegationIdentity = new DelegationIdentity(sessionIdentity, chain);

			var identityClient = new Mock<IInternetIdentityClient>(MockBehavior.Strict);
			identityClient
				.Setup(i => i.LookupAsync(anchor))
				.ReturnsAsync(devices);
			identityClient
				.Setup(i => i.PrepareAndGetDelegationAsync(anchor, clientHostname, It.IsAny<DelegationIdentity>(), sessionIdentity, maxTimeToLive))
				.ThrowsAsync(new Fido2Net.FidoException(Fido2Net.FidoStatus.Ok));
			identityClient
				.Setup(i => i.GetCanisterId())
				.Returns(canisterId);

			var fidoClient = new Mock<IFido2Client>(MockBehavior.Strict);
			fidoClient
				.Setup(f => f.SignAsync(It.IsAny<byte[]>(), devices))
				.ReturnsAsync((deviceIdentity.PublicKey, deviceSignature));
			var authentiator = new Authenticator(identityClient.Object, fidoClient.Object);
			LoginResult result = await authentiator.LoginAsync(anchor, clientHostname, sessionIdentity, maxTimeToLive);
			Assert.False(result.IsSuccessful);
			ErrorType failure = result.AsFailure();
			Assert.Equal(ErrorType.CouldNotAuthenticate, failure);
		}

		[Fact]
		public async Task Authenticator__LoginAsync__No_Matching_Device__Error()
		{
			Ed25519Identity deviceIdentity = Ed25519Identity.Generate();
			List<DeviceInfo> devices = new List<DeviceInfo>
			{
				new DeviceInfo(deviceIdentity.PublicKey, null)
			};
			ulong anchor = 1;
			string clientHostname = "nns.ic0.app";
			Ed25519Identity sessionIdentity = Ed25519Identity.Generate();
			TimeSpan maxTimeToLive = TimeSpan.FromSeconds(60);
			byte[] challenge = new byte[32];
			var canisterId = Principal.FromText("rrkah-fqaaa-aaaaa-aaaaq-cai");

			var delegation = new Delegation(sessionIdentity.PublicKey.Value, ICTimestamp.Future(maxTimeToLive), targets: null, senders: null);
			byte[] deviceSignature = await deviceIdentity.SignAsync(challenge);
			var delegations = new List<SignedDelegation>
			{
				new SignedDelegation(delegation, deviceSignature)
			};
			var chain = new DelegationChain(deviceIdentity.PublicKey, delegations);
			DelegationIdentity delegationIdentity = new DelegationIdentity(sessionIdentity, chain);

			var identityClient = new Mock<IInternetIdentityClient>(MockBehavior.Strict);
			identityClient
				.Setup(i => i.LookupAsync(anchor))
				.ReturnsAsync(devices);
			identityClient
				.Setup(i => i.PrepareAndGetDelegationAsync(anchor, clientHostname, It.IsAny<DelegationIdentity>(), sessionIdentity, maxTimeToLive))
				.ThrowsAsync(new Fido2Net.FidoException(Fido2Net.FidoStatus.Ok));
			identityClient
				.Setup(i => i.GetCanisterId())
				.Returns(canisterId);

			var fidoClient = new Mock<IFido2Client>(MockBehavior.Strict);
			fidoClient
				.Setup(f => f.SignAsync(It.IsAny<byte[]>(), devices))
				.ReturnsAsync((ValueTuple<DerEncodedPublicKey, byte[]>?)null);
			var authentiator = new Authenticator(identityClient.Object, fidoClient.Object);
			LoginResult result = await authentiator.LoginAsync(anchor, clientHostname, sessionIdentity, maxTimeToLive);
			Assert.False(result.IsSuccessful);
			ErrorType failure = result.AsFailure();
			Assert.Equal(ErrorType.NoMatchingDevice, failure);
		}
	}
}
