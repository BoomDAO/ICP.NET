using System;

namespace EdjCase.ICP.Candid.Models.Keys
{
	public interface IPublicKey
	{
		byte[] GetOid();
		byte[] GetDerEncodedBytes();
		byte[] GetRawBytes();
	}
}