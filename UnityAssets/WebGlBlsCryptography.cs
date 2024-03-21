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

#if UNITY_WEBGL && !UNITY_EDITOR
public class WebGlBlsCryptography : IBlsCryptography
{
    public bool VerifySignature(byte[] publicKey, byte[] messageHash, byte[] signature)
    {
        string publicKeyHex = ToHexString(publicKey);
        string messageHashHex = ToHexString(messageHash);
        string signatureHex = ToHexString(signature);
        return BrowserBlsLib.VerifySignature(publicKeyHex, messageHashHex, signatureHex);
    }

    public static string ToHexString(byte[] bytes)
    {
        char[] stringValue = new char[bytes.Length * 2];
        int i = 0;
        foreach (byte b in bytes)
        {
            int charIndex = i++ * 2;
            int quotient = Math.DivRem(b, 16, out int remainder);
            stringValue[charIndex] = GetChar(quotient);
            stringValue[charIndex + 1] = GetChar(remainder);
        }

        return new string(stringValue); // returns: "48656C6C6F20776F726C64" for "Hello world"

    }
    private static char GetChar(int value)
    {
        if (value < 10)
        {
            return (char)(value + 48); // 0->9
        }
        return (char)(value + 65 - 10); // A->F ASCII
    }
}

internal static class BrowserBlsLib
{
    [DllImport("__Internal")]
    public static extern bool VerifySignature(string publicKeyHex, string messageHashHex, string signatureHex);
}
#endif

