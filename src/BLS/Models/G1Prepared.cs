//using System.Collections.Generic;

// TODO this was a guess
//namespace EdjCase.ICP.BLS.Models
//{
//	internal class G1Prepared
//	{
//		public bool IsInfinity { get; }
//		public List<(Fp, Fp, Fp)> Coefficients { get; }

//		public G1Prepared(bool isInfinity, List<(Fp, Fp, Fp)> coefficients)
//		{
//			this.IsInfinity = isInfinity;
//			this.Coefficients = coefficients;
//		}


//		internal static G1Projective DoublingStep(G1Projective value, out (Fp, Fp, Fp) values)
//		{
//			Fp tmp0 = value.X.Square();
//			Fp tmp1 = value.Y.Square();
//			Fp tmp2 = tmp1.Square();
//			Fp tmp3 = (tmp1 + value.X).Square() - tmp0 - tmp2;
//			tmp3 += tmp3;
//			Fp tmp4 = tmp0 + tmp0 + tmp0;
//			Fp tmp6 = value.X + tmp4;
//			Fp tmp5 = tmp4.Square();
//			Fp zsquared = value.Z.Square();
//			Fp newX = tmp5 - tmp3 - tmp3;
//			Fp newZ = (value.Z + value.Y).Square() - tmp1 - zsquared;
//			Fp newY = (tmp3 - newX) * tmp4;
//			tmp2 += tmp2;
//			tmp2 += tmp2;
//			tmp2 += tmp2;
//			newY -= tmp2;
//			tmp3 = tmp4 * zsquared;
//			tmp3 += tmp3;
//			tmp3 = tmp3.Neg();
//			tmp6 = tmp6.Square() - tmp0 - tmp5;
//			tmp1 += tmp1;
//			tmp1 += tmp1;
//			tmp6 -= tmp1;
//			tmp0 = newZ * zsquared;
//			tmp0 += tmp0;

//			values = (tmp0, tmp3, tmp6);
//			return new G1Projective(newX, newY, newZ);
//		}

//		internal static G1Projective AdditionStep(G1Projective cur, G1Affine q, out (Fp, Fp, Fp) values)
//		{
//			Fp zsquared = cur.Z.Square();
//			Fp ysquared = q.Y.Square();
//			Fp t0 = zsquared * q.X;
//			Fp t1 = ((q.Y + cur.Z).Square() - ysquared - zsquared) * zsquared;
//			Fp t2 = t0 - cur.X;
//			Fp t3 = t2.Square();
//			Fp t4 = t3 + t3;
//			t4 += t4;
//			Fp t5 = t4 * t2;
//			Fp t6 = t1 - cur.Y - cur.Y;
//			Fp t9 = t6 * q.X;
//			Fp t7 = t4 * cur.X;
//			Fp newX = t6.Square() - t5 - t7 - t7;
//			Fp newZ = (cur.Z + t2).Square() - zsquared - t3;
//			Fp t10 = q.Y + newZ;
//			Fp t8 = (t7 - newX) * t6;
//			t0 = cur.Y * t5;
//			t0 += t0;
//			Fp newY = t8 - t0;
//			t10 = t10.Square() - ysquared;
//			var ztsquared = newZ.Square();
//			t10 -= ztsquared;
//			t9 = t9 + t9 - t10;
//			t10 = newZ + newZ;
//			t6 = t6.Neg();
//			t1 = t6 + t6;

//			values = (t10, t1, t9);

//			return new G1Projective(newX, newY, newZ);
//		}
//	}
//}
