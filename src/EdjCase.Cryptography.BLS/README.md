# Cryptography.BLS - Minimal BLS library used for ICP BLS signature verification 

- Nuget: [`EdjCase.Cryptography.BLS`](https://www.nuget.org/packages/EdjCase.Cryptography.BLS)

# Overview
This is a simple library to validate BLS signatures (ICP flavor only).

It acts as a wrapper around the https://github.com/herumi/bls native library

# Usage
```cs
bool isValid = EdjCase.Cryptography.BLS.IcpBlsUtil.VerifySignature(publicKey, messageHash, signature);
```
