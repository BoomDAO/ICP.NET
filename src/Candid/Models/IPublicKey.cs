using System;
using EdjCase.ICP.Candid.Models;

namespace EdjCase.ICP.Candid.Models
{
	public interface IPublicKey
	{
		byte[] GetDerEncodedBytes();
	}
}