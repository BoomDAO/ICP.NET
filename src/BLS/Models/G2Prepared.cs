using System.Collections.Generic;

namespace EdjCase.ICP.BLS.Models
{
	internal class G2Prepared
	{
		public bool IsInfinity { get; }
		public (Fp2, Fp2, Fp2)[] Coefficients { get; }

		public G2Prepared(bool isInfinity, (Fp2, Fp2, Fp2)[] coefficients)
		{
			this.IsInfinity = isInfinity;
			this.Coefficients = coefficients;
		}


		internal static G2Projective DoublingStep(G2Projective value, out (Fp2, Fp2, Fp2) values)
		{
			Fp2 tmp0 = value.X.Square();
			Fp2 tmp1 = value.Y.Square();
			Fp2 tmp2 = tmp1.Square();
			Fp2 tmp3 = (tmp1 + value.X).Square() - tmp0 - tmp2;
			tmp3 += tmp3;
			Fp2 tmp4 = tmp0 + tmp0 + tmp0;
			Fp2 tmp6 = value.X + tmp4;
			Fp2 tmp5 = tmp4.Square();
			Fp2 zsquared = value.Z.Square();
			Fp2 newX = tmp5 - tmp3 - tmp3;
			Fp2 newZ = (value.Z + value.Y).Square() - tmp1 - zsquared;
			Fp2 newY = (tmp3 - newX) * tmp4;
			tmp2 += tmp2;
			tmp2 += tmp2;
			tmp2 += tmp2;
			newY -= tmp2;
			tmp3 = tmp4 * zsquared;
			tmp3 += tmp3;
			tmp3 = tmp3.Neg();
			tmp6 = tmp6.Square() - tmp0 - tmp5;
			tmp1 += tmp1;
			tmp1 += tmp1;
			tmp6 -= tmp1;
			tmp0 = newZ * zsquared;
			tmp0 += tmp0;

			values = (tmp0, tmp3, tmp6);
			return new G2Projective(newX, newY, newZ);
		}

		internal static G2Projective AdditionStep(G2Projective cur, G2Affine q, out (Fp2, Fp2, Fp2) values)
		{
			Fp2 zsquared = cur.Z.Square();
			Fp2 ysquared = q.Y.Square();
			Fp2 t0 = zsquared * q.X;
			Fp2 t1 = ((q.Y + cur.Z).Square() - ysquared - zsquared) * zsquared;
			Fp2 t2 = t0 - cur.X;
			Fp2 t3 = t2.Square();
			Fp2 t4 = t3 + t3;
			t4 += t4;
			Fp2 t5 = t4 * t2;
			Fp2 t6 = t1 - cur.Y - cur.Y;
			Fp2 t9 = t6 * q.X;
			Fp2 t7 = t4 * cur.X;
			Fp2 newX = t6.Square() - t5 - t7 - t7;
			Fp2 newZ = (cur.Z + t2).Square() - zsquared - t3;
			Fp2 t10 = q.Y + newZ;
			Fp2 t8 = (t7 - newX) * t6;
			t0 = cur.Y * t5;
			t0 += t0;
			Fp2 newY = t8 - t0;
			t10 = t10.Square() - ysquared;
			var ztsquared = newZ.Square();
			t10 -= ztsquared;
			t9 = t9 + t9 - t10;
			t10 = newZ + newZ;
			t6 = t6.Neg();
			t1 = t6 + t6;

			values = (t10, t1, t9);

			return new G2Projective(newX, newY, newZ);
		}
	}
}
