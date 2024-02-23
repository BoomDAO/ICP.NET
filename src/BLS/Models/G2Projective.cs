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
			return this.ToAffine().ToCompressed();
		}

		public G2Projective Add(G2Projective rhs)
		{
			Fp2 t0 = this.X 
			//let t0 = self.x * rhs.x;
   //     let t1 = self.y * rhs.y;
   //     let t2 = self.z * rhs.z;
   //     let t3 = self.x + self.y;
   //     let t4 = rhs.x + rhs.y;
   //     let t3 = t3 * t4;
   //     let t4 = t0 + t1;
   //     let t3 = t3 - t4;
   //     let t4 = self.y + self.z;
   //     let x3 = rhs.y + rhs.z;
   //     let t4 = t4 * x3;
   //     let x3 = t1 + t2;
   //     let t4 = t4 - x3;
   //     let x3 = self.x + self.z;
   //     let y3 = rhs.x + rhs.z;
   //     let x3 = x3 * y3;
   //     let y3 = t0 + t2;
   //     let y3 = x3 - y3;
   //     let x3 = t0 + t0;
   //     let t0 = x3 + t0;
   //     let t2 = mul_by_3b(t2);
   //     let z3 = t1 + t2;
   //     let t1 = t1 - t2;
   //     let y3 = mul_by_3b(y3);
   //     let x3 = t4 * y3;
   //     let t2 = t3 * t1;
   //     let x3 = t2 - x3;
   //     let y3 = y3 * t0;
   //     let t1 = t1 * z3;
   //     let y3 = t1 + y3;
   //     let t0 = t0 * t3;
   //     let z3 = z3 * t4;
   //     let z3 = z3 + t0;

   //     G2Projective {
   //         x: x3,
   //         y: y3,
   //         z: z3,
   //     }
		}

		public static G2Projective operator +(G2Projective lhs, G2Projective rhs)
		{
			return lhs.Add(rhs);
		}


		internal bool IsIdentity()
		{
			return this.Z.IsZero();
		}
	}
}
