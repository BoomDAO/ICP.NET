using System;

namespace EdjCase.ICP.Agent.Keys
{
	public interface IPublicKey
	{
		byte[] GetOid();
		byte[] GetDerEncodedBytes();
		byte[] GetRawBytes();
	}
}