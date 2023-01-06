using System;

namespace EdjCase.ICP.Agent.Keys
{
	public interface IPublicKey
	{
		byte[] GetDerEncodedBytes();
		byte[] GetRawBytes();
	}
}