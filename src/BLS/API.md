<a name='assembly'></a>
# EdjCase.ICP.BLS

## Contents

- [BlsUtil](#T-EdjCase-ICP-BLS-BlsUtil 'EdjCase.ICP.BLS.BlsUtil')
  - [VerifySignature(publicKey,messageHash,signature)](#M-EdjCase-ICP-BLS-BlsUtil-VerifySignature-System-Byte[],System-Byte[],System-Byte[]- 'EdjCase.ICP.BLS.BlsUtil.VerifySignature(System.Byte[],System.Byte[],System.Byte[])')

<a name='T-EdjCase-ICP-BLS-BlsUtil'></a>
## BlsUtil `type`

##### Namespace

EdjCase.ICP.BLS

##### Summary

Class with functions around BLS signatures (ICP flavor only)

<a name='M-EdjCase-ICP-BLS-BlsUtil-VerifySignature-System-Byte[],System-Byte[],System-Byte[]-'></a>
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
