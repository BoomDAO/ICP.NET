
#if UNITY_WEBGL
public class WebGlBlsCryptography : IBlsCryptography
{
    public bool VerifySignature(byte[] publicKey, byte[] messageHash, byte[] signature)
    {
        return true;
    }
}
#endif