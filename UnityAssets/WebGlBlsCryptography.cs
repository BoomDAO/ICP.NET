using EdjCase.ICP.Agent.Agents.Http;
using EdjCase.ICP.BLS;
using System;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

#if UNITY_WEBGL
public class WebGlBlsCryptography : IBlsCryptography
{
    public bool VerifySignature(byte[] publicKey, byte[] messageHash, byte[] signature)
    {
        return BrowserBlsLib.VerifySignature(publicKey, messageHash, signature);
    }
}

internal static class BrowserBlsLib
{
    [DllImport("__Internal")]
    public static extern bool VerifySignature(byte[] publicKey, byte[] messageHash, byte[] signature);
}
#endif

