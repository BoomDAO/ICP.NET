<a name='assembly'></a>
# EdjCase.ICP.InternetIdentity

## Contents

- [Authenticator](#T-EdjCase-ICP-InternetIdentity-Authenticator 'EdjCase.ICP.InternetIdentity.Authenticator')
  - [BuildDeviceIdentityAsync()](#M-EdjCase-ICP-InternetIdentity-Authenticator-BuildDeviceIdentityAsync-System-Collections-Generic-IList{EdjCase-ICP-InternetIdentity-DeviceInfo},EdjCase-ICP-Agent-Identities-IIdentity- 'EdjCase.ICP.InternetIdentity.Authenticator.BuildDeviceIdentityAsync(System.Collections.Generic.IList{EdjCase.ICP.InternetIdentity.DeviceInfo},EdjCase.ICP.Agent.Identities.IIdentity)')
  - [LoginAsync(anchor,clientHostname,sessionIdentity,maxTimeToLive)](#M-EdjCase-ICP-InternetIdentity-Authenticator-LoginAsync-System-UInt64,System-String,EdjCase-ICP-Agent-Identities-IIdentity,System-Nullable{System-TimeSpan}- 'EdjCase.ICP.InternetIdentity.Authenticator.LoginAsync(System.UInt64,System.String,EdjCase.ICP.Agent.Identities.IIdentity,System.Nullable{System.TimeSpan})')
  - [WithHttpAgent(httpBoundryNodeUrl,identityCanisterOverride)](#M-EdjCase-ICP-InternetIdentity-Authenticator-WithHttpAgent-System-Uri,EdjCase-ICP-Candid-Models-Principal- 'EdjCase.ICP.InternetIdentity.Authenticator.WithHttpAgent(System.Uri,EdjCase.ICP.Candid.Models.Principal)')
- [DeviceInfo](#T-EdjCase-ICP-InternetIdentity-DeviceInfo 'EdjCase.ICP.InternetIdentity.DeviceInfo')
  - [CredentialId](#P-EdjCase-ICP-InternetIdentity-DeviceInfo-CredentialId 'EdjCase.ICP.InternetIdentity.DeviceInfo.CredentialId')
  - [PublicKey](#P-EdjCase-ICP-InternetIdentity-DeviceInfo-PublicKey 'EdjCase.ICP.InternetIdentity.DeviceInfo.PublicKey')
- [ErrorType](#T-EdjCase-ICP-InternetIdentity-ErrorType 'EdjCase.ICP.InternetIdentity.ErrorType')
  - [CouldNotAuthenticate](#F-EdjCase-ICP-InternetIdentity-ErrorType-CouldNotAuthenticate 'EdjCase.ICP.InternetIdentity.ErrorType.CouldNotAuthenticate')
  - [InvalidAnchorOrNoDevices](#F-EdjCase-ICP-InternetIdentity-ErrorType-InvalidAnchorOrNoDevices 'EdjCase.ICP.InternetIdentity.ErrorType.InvalidAnchorOrNoDevices')
  - [NoMatchingDevice](#F-EdjCase-ICP-InternetIdentity-ErrorType-NoMatchingDevice 'EdjCase.ICP.InternetIdentity.ErrorType.NoMatchingDevice')
- [Fido2Client](#T-EdjCase-ICP-InternetIdentity-Fido2Client 'EdjCase.ICP.InternetIdentity.Fido2Client')
  - [CreateSignatureFromAssertion()](#M-EdjCase-ICP-InternetIdentity-Fido2Client-CreateSignatureFromAssertion-Fido2Net-FidoAssertionStatement,System-String- 'EdjCase.ICP.InternetIdentity.Fido2Client.CreateSignatureFromAssertion(Fido2Net.FidoAssertionStatement,System.String)')
- [LoginResult](#T-EdjCase-ICP-InternetIdentity-LoginResult 'EdjCase.ICP.InternetIdentity.LoginResult')
  - [IsSuccessful](#P-EdjCase-ICP-InternetIdentity-LoginResult-IsSuccessful 'EdjCase.ICP.InternetIdentity.LoginResult.IsSuccessful')
  - [AsFailure()](#M-EdjCase-ICP-InternetIdentity-LoginResult-AsFailure 'EdjCase.ICP.InternetIdentity.LoginResult.AsFailure')
  - [AsSuccessful()](#M-EdjCase-ICP-InternetIdentity-LoginResult-AsSuccessful 'EdjCase.ICP.InternetIdentity.LoginResult.AsSuccessful')
  - [GetIdentityOrThrow()](#M-EdjCase-ICP-InternetIdentity-LoginResult-GetIdentityOrThrow 'EdjCase.ICP.InternetIdentity.LoginResult.GetIdentityOrThrow')

<a name='T-EdjCase-ICP-InternetIdentity-Authenticator'></a>
## Authenticator `type`

##### Namespace

EdjCase.ICP.InternetIdentity

##### Summary

Authenticator for Internet Identity. Facilitates the login flow for using
the fido device and connecting to the identity canister backend

<a name='M-EdjCase-ICP-InternetIdentity-Authenticator-BuildDeviceIdentityAsync-System-Collections-Generic-IList{EdjCase-ICP-InternetIdentity-DeviceInfo},EdjCase-ICP-Agent-Identities-IIdentity-'></a>
### BuildDeviceIdentityAsync() `method`

##### Summary

This is responsible for converting a "direct" user identity, which identifies the user,
into a delegation identity, which is anonymized, has an expiry, etc.

Corresponds to \`requestFEDelegation' from @dfinity/internet-identity

##### Parameters

This method has no parameters.

<a name='M-EdjCase-ICP-InternetIdentity-Authenticator-LoginAsync-System-UInt64,System-String,EdjCase-ICP-Agent-Identities-IIdentity,System-Nullable{System-TimeSpan}-'></a>
### LoginAsync(anchor,clientHostname,sessionIdentity,maxTimeToLive) `method`

##### Summary

Attempts to create a delegation identity from the Internet Identity flow

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| anchor | [System.UInt64](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.UInt64 'System.UInt64') | Anchor id (user id for internet identity) |
| clientHostname | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | Hostname of the client application to authorize |
| sessionIdentity | [EdjCase.ICP.Agent.Identities.IIdentity](#T-EdjCase-ICP-Agent-Identities-IIdentity 'EdjCase.ICP.Agent.Identities.IIdentity') | Optional. Specifies the identity to delegate to. If not specified, will generate a new identity |
| maxTimeToLive | [System.Nullable{System.TimeSpan}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Nullable 'System.Nullable{System.TimeSpan}') | Max time for the login session/identity to last |

<a name='M-EdjCase-ICP-InternetIdentity-Authenticator-WithHttpAgent-System-Uri,EdjCase-ICP-Candid-Models-Principal-'></a>
### WithHttpAgent(httpBoundryNodeUrl,identityCanisterOverride) `method`

##### Summary

Creates a new instance using an http client

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| httpBoundryNodeUrl | [System.Uri](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Uri 'System.Uri') | Optional. Speicifes the url of the boundry node url. If not specified, will use default |
| identityCanisterOverride | [EdjCase.ICP.Candid.Models.Principal](#T-EdjCase-ICP-Candid-Models-Principal 'EdjCase.ICP.Candid.Models.Principal') | Optional. Specifies the Internet Identity backend canister, if not specified, will use default |

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

<a name='T-EdjCase-ICP-InternetIdentity-ErrorType'></a>
## ErrorType `type`

##### Namespace

EdjCase.ICP.InternetIdentity

##### Summary

The different errors for a login failure

<a name='F-EdjCase-ICP-InternetIdentity-ErrorType-CouldNotAuthenticate'></a>
### CouldNotAuthenticate `constants`

##### Summary

If the fido2 device fails to be unlocked and cannot sign

<a name='F-EdjCase-ICP-InternetIdentity-ErrorType-InvalidAnchorOrNoDevices'></a>
### InvalidAnchorOrNoDevices `constants`

##### Summary

Either the anchor id does not exist or the anchor has no devices associated with it

<a name='F-EdjCase-ICP-InternetIdentity-ErrorType-NoMatchingDevice'></a>
### NoMatchingDevice `constants`

##### Summary

The fido2 device does not match any devices in Internet Identity

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

<a name='T-EdjCase-ICP-InternetIdentity-LoginResult'></a>
## LoginResult `type`

##### Namespace

EdjCase.ICP.InternetIdentity

##### Summary

A result variant that either contains the delegation identity or and error 
if failed

<a name='P-EdjCase-ICP-InternetIdentity-LoginResult-IsSuccessful'></a>
### IsSuccessful `property`

##### Summary

Indicator if the login attempt was successful. If not the

<a name='M-EdjCase-ICP-InternetIdentity-LoginResult-AsFailure'></a>
### AsFailure() `method`

##### Summary

Method to extract the error type if \`IsSuccessful\` is false, otherwise
will throw an exception

##### Returns

The error type of the failure

##### Parameters

This method has no parameters.

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.InvalidOperationException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.InvalidOperationException 'System.InvalidOperationException') | Throws if \`IsSuccessful\` is true |

<a name='M-EdjCase-ICP-InternetIdentity-LoginResult-AsSuccessful'></a>
### AsSuccessful() `method`

##### Summary

Method to extract the identity if \`IsSuccessful\` is true, otherwise will
throw an exception

##### Returns

The session identity

##### Parameters

This method has no parameters.

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.InvalidOperationException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.InvalidOperationException 'System.InvalidOperationException') | Throws if \`IsSuccessful\` is false |

<a name='M-EdjCase-ICP-InternetIdentity-LoginResult-GetIdentityOrThrow'></a>
### GetIdentityOrThrow() `method`

##### Summary

Helper function to get the identity or throw an exception if there 
is a failure

##### Returns

Result identity

##### Parameters

This method has no parameters.

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [EdjCase.ICP.InternetIdentity.InternetIdentityLoginException](#T-EdjCase-ICP-InternetIdentity-InternetIdentityLoginException 'EdjCase.ICP.InternetIdentity.InternetIdentityLoginException') | Throws if \`IsSuccessful\` is false |
