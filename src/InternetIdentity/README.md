# Internet Identity (Experimental)

- Library of Internet Identity integration. Allowing computers to sign FIDO2/WebAuthn flows to login to Internet Identity
- EXPERIMENTAL: There are security implications. Not recommended to use in production
- Nuget: [`EdjCase.ICP.InternetIdentity`](https://www.nuget.org/packages/EdjCase.ICP.InternetIdentity)

## Usage

```cs
ulong anchor = 1; // Internet Identity anchor
string hostname = "nns.ic0.app"; // Hostname to login to
LoginResult result = await Authenticator
    .WithHttpAgent() // Use http agent to communicate to the Internet Identity canister
    .LoginAsync(anchor, hostname);

DelegationIdentity identity = result.GetIdentityOrThrow(); // Gets the generated identity or throws if login failed

var agent = new HttpAgent(identity); // Use in agent to make authenticated requests
```
