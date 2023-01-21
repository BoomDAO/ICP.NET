using EdjCase.ICP.Agent.Identities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EdjCase.ICP.InternetIdentity
{

	public class LoginResult
	{
		private ErrorType? Error { get; }
		private DelegationIdentity? identity { get; }

		private LoginResult(ErrorType? error, DelegationIdentity? identity)
		{
			this.Error = error;
			this.identity = identity;
		}

		public bool IsSuccessful => this.identity != null;

		public ErrorType AsFailure()
		{
			if (this.IsSuccessful)
			{
				throw new InvalidOperationException("Cannot get error from a successful login");
			}
			return this.Error!.Value;
		}

		public DelegationIdentity AsSuccessful()
		{
			if (!this.IsSuccessful)
			{
				throw new InvalidOperationException("Cannot get an identity from a successful login");
			}
			return this.identity!;
		}


		public static LoginResult FromError(ErrorType type)
		{
			return new LoginResult(type, null);
		}

		public static LoginResult FromSuccess(DelegationIdentity identity)
		{
			return new LoginResult(null, identity);
		}
	}

	public enum ErrorType
	{
		CouldNotAuthenticate,
		NoMatchingDevice,
		InvalidAnchorOrNoDevices
	}
}
