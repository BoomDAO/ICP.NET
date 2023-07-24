<a name='assembly'></a>
# EdjCase.ICP.BLS

## Contents

- [IBlsCryptography](#T-EdjCase-ICP-BLS-IBlsCryptography 'EdjCase.ICP.BLS.IBlsCryptography')
  - [VerifySignature(publicKey,messageHash,signature)](#M-EdjCase-ICP-BLS-IBlsCryptography-VerifySignature-System-Byte[],System-Byte[],System-Byte[]- 'EdjCase.ICP.BLS.IBlsCryptography.VerifySignature(System.Byte[],System.Byte[],System.Byte[])')
- [WasmBlsCryptography](#T-EdjCase-ICP-BLS-WasmBlsCryptography 'EdjCase.ICP.BLS.WasmBlsCryptography')
  - [VerifySignature()](#M-EdjCase-ICP-BLS-WasmBlsCryptography-VerifySignature-System-Byte[],System-Byte[],System-Byte[]- 'EdjCase.ICP.BLS.WasmBlsCryptography.VerifySignature(System.Byte[],System.Byte[],System.Byte[])')

<a name='T-EdjCase-ICP-BLS-IBlsCryptography'></a>
## IBlsCryptography `type`

##### Namespace

EdjCase.ICP.BLS

##### Summary

An interface for all BLS crytography operations

<a name='M-EdjCase-ICP-BLS-IBlsCryptography-VerifySignature-System-Byte[],System-Byte[],System-Byte[]-'></a>
### VerifySignature(publicKey,messageHash,signature) `method`

##### Summary

Verifies a BLS signature (ICP flavor only)

##### Returns

True if the signature is valid, otherwise false

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| publicKey | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The signer public key |
| messageHash | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The SHA256 hash of the message |
| signature | [System.Byte[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Byte[] 'System.Byte[]') | The signature of the message |

<a name='T-EdjCase-ICP-BLS-WasmBlsCryptography'></a>
## WasmBlsCryptography `type`

##### Namespace

EdjCase.ICP.BLS

##### Summary

Class with functions around BLS signatures (ICP flavor only)

<a name='M-EdjCase-ICP-BLS-WasmBlsCryptography-VerifySignature-System-Byte[],System-Byte[],System-Byte[]-'></a>
### VerifySignature() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.
