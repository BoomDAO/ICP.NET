# Cryptography.BLS - Minimal BLS library used for ICP BLS signature verification

- Nuget: [`EdjCase.ICP.BLS`](https://www.nuget.org/packages/EdjCase.ICP.BLS)

# Overview

This is a simple library to validate BLS signatures (ICP flavor only).

Implementation is a conversion from the following projects
https://github.com/filecoin-project/bls-signatures
https://github.com/zkcrypto/bls12_381

# Usage

```cs
bool isValid = EdjCase.ICP.BLS.BlsUtil.VerifySignature(publicKey, messageHash, signature);
```