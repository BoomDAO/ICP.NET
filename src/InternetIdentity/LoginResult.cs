using EdjCase.ICP.Agent.Identities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EdjCase.ICP.InternetIdentity
{
	/// <summary>
	/// A result variant that either contains the delegation identity or and error 
	/// if failed
	/// </summary>
	public class LoginResult
	{
		private ErrorType? Error { get; }
		private DelegationIdentity? identity { get; }

		private LoginResult(ErrorType? error, DelegationIdentity? identity)
		{
			this.Error = error;
			this.identity = identity;
		}

		/// <summary>
		/// Indicator if the login attempt was successful. If not the 
		/// </summary>
		public bool IsSuccessful => this.identity != null;

		/// <summary>
		/// Method to extract the error type if `IsSuccessful` is false, otherwise
		/// will throw an exception
		/// </summary>
		/// <returns>The error type of the failure</returns>
		/// <exception cref="InvalidOperationException">Throws if `IsSuccessful` is true</exception>
		public ErrorType AsFailure()
		{
			if (this.IsSuccessful)
			{
				throw new InvalidOperationException("Cannot get error from a successful login");
			}
			return this.Error!.Value;
		}

		/// <summary>
		/// Method to extract the identity if `IsSuccessful` is true, otherwise will
		/// throw an exception
		/// </summary>
		/// <returns>The session identity</returns>
		/// <exception cref="InvalidOperationException">Throws if `IsSuccessful` is false</exception>
		public DelegationIdentity AsSuccessful()
		{
			if (!this.IsSuccessful)
			{
				throw new InvalidOperationException("Cannot get an identity from a successful login");
			}
			return this.identity!;
		}

		/// <summary>
		/// Helper function to get the identity or throw an exception if there 
		/// is a failure
		/// </summary>
		/// <returns>Result identity</returns>
		/// <exception cref="InternetIdentityLoginException">Throws if `IsSuccessful` is false</exception>
		public DelegationIdentity GetIdentityOrThrow()
		{
			if (!this.IsSuccessful)
			{
				throw new InternetIdentityLoginException(this.AsFailure());
			}
			return this.AsSuccessful();
		}

		internal static LoginResult FromError(ErrorType type)
		{
			return new LoginResult(type, null);
		}

		internal static LoginResult FromSuccess(DelegationIdentity identity)
		{
			return new LoginResult(null, identity);
		}
	}

	/// <summary>
	/// The different errors for a login failure
	/// </summary>
	public enum ErrorType
	{
		/// <summary>
		/// If the fido2 device fails to be unlocked and cannot sign
		/// </summary>
		CouldNotAuthenticate,
		/// <summary>
		/// The fido2 device does not match any devices in Internet Identity
		/// </summary>
		NoMatchingDevice,
		/// <summary>
		/// Either the anchor id does not exist or the anchor has no devices associated with it
		/// </summary>
		InvalidAnchorOrNoDevices
	}
}
