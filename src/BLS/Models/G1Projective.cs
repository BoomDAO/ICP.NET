namespace EdjCase.ICP.BLS.Models
{
	internal class G1Projective
	{
		public Fp X { get; }
		public Fp Y { get; }
		public Fp Z { get; }

		public G1Projective(Fp x, Fp y, Fp z)
		{
			this.X = x;
			this.Y = y;
			this.Z = z;
		}


		public G1Affine ToAffine()
		{
			Fp zinv = this.Z.Invert() ?? Fp.Zero();
			if (zinv.IsZero())
			{
				return G1Affine.Identity();
			}
			Fp x = this.X.Multiply(zinv);
			Fp y = this.Y.Multiply(zinv);
			return new(x, y, false);
		}
	}
}
