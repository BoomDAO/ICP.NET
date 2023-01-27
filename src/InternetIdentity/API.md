<a name='assembly'></a>
# EdjCase.ICP.InternetIdentity

## Contents

- [Authenticator](#T-EdjCase-ICP-InternetIdentity-Authenticator 'EdjCase.ICP.InternetIdentity.Authenticator')
  - [BuildDeviceIdentityAsync()](#M-EdjCase-ICP-InternetIdentity-Authenticator-BuildDeviceIdentityAsync-System-Collections-Generic-IList{EdjCase-ICP-InternetIdentity-DeviceInfo},EdjCase-ICP-Agent-Identities-IIdentity- 'EdjCase.ICP.InternetIdentity.Authenticator.BuildDeviceIdentityAsync(System.Collections.Generic.IList{EdjCase.ICP.InternetIdentity.DeviceInfo},EdjCase.ICP.Agent.Identities.IIdentity)')
- [DeviceInfo](#T-EdjCase-ICP-InternetIdentity-DeviceInfo 'EdjCase.ICP.InternetIdentity.DeviceInfo')
  - [CredentialId](#P-EdjCase-ICP-InternetIdentity-DeviceInfo-CredentialId 'EdjCase.ICP.InternetIdentity.DeviceInfo.CredentialId')
  - [PublicKey](#P-EdjCase-ICP-InternetIdentity-DeviceInfo-PublicKey 'EdjCase.ICP.InternetIdentity.DeviceInfo.PublicKey')
- [Fido2Client](#T-EdjCase-ICP-InternetIdentity-Fido2Client 'EdjCase.ICP.InternetIdentity.Fido2Client')
  - [CreateSignatureFromAssertion()](#M-EdjCase-ICP-InternetIdentity-Fido2Client-CreateSignatureFromAssertion-Fido2Net-FidoAssertionStatement,System-String- 'EdjCase.ICP.InternetIdentity.Fido2Client.CreateSignatureFromAssertion(Fido2Net.FidoAssertionStatement,System.String)')

<a name='T-EdjCase-ICP-InternetIdentity-Authenticator'></a>
## Authenticator `type`

##### Namespace

EdjCase.ICP.InternetIdentity

<a name='M-EdjCase-ICP-InternetIdentity-Authenticator-BuildDeviceIdentityAsync-System-Collections-Generic-IList{EdjCase-ICP-InternetIdentity-DeviceInfo},EdjCase-ICP-Agent-Identities-IIdentity-'></a>
### BuildDeviceIdentityAsync() `method`

##### Summary

This is responsible for converting a "direct" user identity, which identifies the user,
into a delegation identity, which is anonymized, has an expiry, etc.

Corresponds to \`requestFEDelegation' from @dfinity/internet-identity

##### Parameters

This method has no parameters.

<a name='T-EdjCase-ICP-InternetIdentity-DeviceInfo'></a>
## DeviceInfo `type`

##### Namespace

EdjCase.ICP.InternetIdentity

<a name='P-EdjCase-ICP-InternetIdentity-DeviceInfo-CredentialId'></a>
### CredentialId `property`

##### Summary

Optional. The credential id bytes for the device

<a name='P-EdjCase-ICP-InternetIdentity-DeviceInfo-PublicKey'></a>
### PublicKey `property`

##### Summary

DER encoded public key for the device

<a name='T-EdjCase-ICP-InternetIdentity-Fido2Client'></a>
## Fido2Client `type`

##### Namespace

EdjCase.ICP.InternetIdentity

<a name='M-EdjCase-ICP-InternetIdentity-Fido2Client-CreateSignatureFromAssertion-Fido2Net-FidoAssertionStatement,System-String-'></a>
### CreateSignatureFromAssertion() `method`

##### Summary

The signature is a CBOR value consisting of a data item with major type 6 ("Semantic tag")
and tag value 55799, followed by a map with three mandatory fields:
authenticator_data, client_data_json and signature

##### Parameters

This method has no parameters.
