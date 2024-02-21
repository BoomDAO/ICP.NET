using System;
using EdjCase.ICP.BLS.Models;

namespace EdjCase.ICP.BLS.Models
{
	internal class Signature
	{
		private readonly G2Affine g2;
		public Signature(G2Affine g2)
		{
			this.g2 = g2;
		}

		public static Signature FromBytes(byte[] signature)
		{
			G2Affine g2Affine = G2Affine.FromCompressed(signature);
			return new Signature(g2Affine);
		}
	}
}
