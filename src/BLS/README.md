# Cryptography.BLS - Minimal BLS library used for ICP BLS signature verification 

- Nuget: [`EdjCase.ICP.BLS`](https://www.nuget.org/packages/EdjCase.ICP.BLS)

# Overview
This is a simple library to validate BLS signatures (ICP flavor only).

It acts as a wrapper around the https://github.com/herumi/bls WASM library

# Usage
```cs
bool isValid = EdjCase.ICP.BLS.BlsUtil.VerifySignature(publicKey, messageHash, signature);
```
