using System;

namespace EdjCase.ICP.BLS.Models
{
	internal class G2Projective
	{
		public Fp2 X { get; }
		public Fp2 Y { get; }
		public Fp2 Z { get; }

		public G2Projective(Fp2 x, Fp2 y, Fp2 z)
		{
			this.X = x;
			this.Y = y;
			this.Z = z;
		}


		public G2Affine ToAffine()
		{
			Fp2 zinv = this.Z.Invert() ?? Fp2.Zero();
			if (zinv.IsZero())
			{
				return G2Affine.Identity();
			}
			Fp2 x = this.X.Multiply(zinv);
			Fp2 y = this.Y.Multiply(zinv);
			return new(x, y, false);
		}

		public static G2Projective FromCompressed(byte[] bytes)
		{
			return G2Affine.FromCompressed(bytes).ToProjective();
		}

		internal byte[] ToCompressed()
		{
			throw new NotImplementedException();
		}
	}
}
