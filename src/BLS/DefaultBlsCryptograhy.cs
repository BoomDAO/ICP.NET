using EdjCase.ICP.BLS.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EdjCase.ICP.BLS
{

	/// <summary>
	/// Represents the default implementation of the IBlsCryptography interface for BLS cryptography.
	/// </summary>
	public class DefaultBlsCryptograhy : IBlsCryptography
	{
		/// <inheritdoc />
		public bool VerifySignature(byte[] publicKey, byte[] messageHash, byte[] signature)
		{
			G2Projective[] hashes = new[]
			{
				G2Projective.FromCompressed(messageHash)
			};
			G1Projective[] publicKeys = new []
			{
				G1Projective.FromCompressed(publicKey)
			};
			G2Affine sig = G2Affine.FromCompressed(signature);
			return this.Verify(sig, hashes, publicKeys);
		}

		private static void HashToCurve(byte[] message, byte[] dst)
		{
			(Fp2 u1, Fp2 u2) = HashToField(message, dst);
			G2Projective p1 = MapToCurve(u1);
			G2Projective p2 = MapToCurve(u2);
			(p1 + p2).ClearH();
		}

		private static G2Projective MapToCurve(Fp2 fp)
		{
			//			let usq = u.square();
			//			let xi_usq = SSWU_XI * usq;
			//			let xisq_u4 = xi_usq.square();
			//			let nd_common = xisq_u4 + xi_usq; // XI^2 * u^4 + XI * u^2
			//			let x_den = SSWU_ELLP_A * Fp2::conditional_select(&(-nd_common), &SSWU_XI, nd_common.is_zero());
			//			let x0_num = SSWU_ELLP_B * (Fp2::one() + nd_common); // B * (1 + (XI^2 * u^4 + XI * u^2))

			//			// compute g(x0(u))
			//			let x_densq = x_den.square();
			//			let gx_den = x_densq * x_den;
			//			// x0_num^3 + A * x0_num * x_den^2 + B * x_den^3
			//			let gx0_num = (x0_num.square() + SSWU_ELLP_A * x_densq) * x0_num + SSWU_ELLP_B * gx_den;

			//			// compute g(x0(u)) ^ ((p^2 - 9) // 16)
			//			let sqrt_candidate = {
			//		let vsq = gx_den.square(); // v^2
			//			let v_3 = vsq * gx_den; // v^3
			//			let v_4 = vsq.square(); // v^4
			//			let uv_7 = gx0_num * v_3 * v_4; // u v^7
			//			let uv_15 = uv_7 * v_4.square(); // u v^15
			//			uv_7* chain_p2m9div16(&uv_15) // u v^7 (u v^15) ^ ((p^2 - 9) // 16)
			//    };

			//		// set y = sqrt_candidate * Fp2::one(), check candidate against other roots of unity
			//		let mut y = sqrt_candidate;
			//    // check Fp2(0, 1)
			//    let tmp = Fp2 {

			//		c0: -sqrt_candidate.c1,
			//        c1: sqrt_candidate.c0,
			//    };
			//	y.conditional_assign(&tmp, (tmp.square()* gx_den).ct_eq(&gx0_num));
			//    // check Fp2(RV1, RV1)
			//    let tmp = sqrt_candidate * SSWU_RV1;
			//	y.conditional_assign(&tmp, (tmp.square()* gx_den).ct_eq(&gx0_num));
			//    // check Fp2(RV1, -RV1)
			//    let tmp = Fp2 {
			//        c0: tmp.c1,
			//        c1: -tmp.c0,
			//    };
			//y.conditional_assign(&tmp, (tmp.square() * gx_den).ct_eq(&gx0_num));

			//// compute g(x1(u)) = g(x0(u)) * XI^3 * u^6
			//let gx1_num = gx0_num * xi_usq * xisq_u4;
			//// compute g(x1(u)) * u^3
			//let sqrt_candidate = sqrt_candidate * usq * u;
			//let mut eta_found = Choice::from(0u8);
			//for eta in &SSWU_ETAS[..] {
			//	let tmp = sqrt_candidate * eta;
			//	let found = (tmp.square() * gx_den).ct_eq(&gx1_num);
			//	y.conditional_assign(&tmp, found);
			//	eta_found |= found;
			//}

			//let x_num = Fp2::conditional_select(&x0_num, &(x0_num * xi_usq), eta_found);
			//// ensure sign of y and sign of u agree
			//y.conditional_negate(u.sgn0() ^ y.sgn0());

			//G2Projective {
			//        x: x_num,
			//		y: y* x_den,
			//		z: x_den,
			//    }
			G2Projective v = ;
			return IsoMap(v);
		}

		private static G2Projective IsoMap(G2Projective v)
		{
			//const COEFFS: [&[Fp2]; 4] = [&ISO3_XNUM, &ISO3_XDEN, &ISO3_YNUM, &ISO3_YDEN];

			//// unpack input point
			//let G2Projective { x, y, z } = *u;

			//// xnum, xden, ynum, yden
			//let mut mapvals = [Fp2::zero(); 4];

			//// compute powers of z
			//let zsq = z.square();
			//let zpows = [z, zsq, zsq * z];

			//// compute map value by Horner's rule
			//for idx in 0..4 {
			//	let coeff = COEFFS[idx];
			//	let clast = coeff.len() - 1;
			//	mapvals[idx] = coeff[clast];
			//	for jdx in 0..clast {
			//		mapvals[idx] = mapvals[idx] * x + zpows[jdx] * coeff[clast - 1 - jdx];
			//	}
			//}

			//// x denominator is order 1 less than x numerator, so we need an extra factor of z
			//mapvals[1] *= z;

			//// multiply result of Y map by the y-coord, y / z
			//mapvals[2] *= y;
			//mapvals[3] *= z;

			//G2Projective {
			//x: mapvals[0] * mapvals[3], // xnum * yden,
			//     y: mapvals[2] * mapvals[1], // ynum * xden,
			//     z: mapvals[1] * mapvals[3], // xden * yden
			// }
			return v;
		}

		private static (Fp2, Fp2) HashToField(byte[] message, byte[] dst)
		{
			//let len_per_elm = Self::InputLength::to_usize();
			//let len_in_bytes = output.len() * len_per_elm;
			//let mut expander = X::init_expand(message, dst, len_in_bytes);

			//let mut buf = GenericArray::< u8, Self::InputLength >::default();
			//output.iter_mut().for_each(| item | {
			//	expander.read_into(&mut buf[..]);
			//	*item = Self::from_okm(&buf);
			//});
		}

		private bool Verify(G2Affine signature, G2Projective[] g2Values, G1Projective[] g1Values)
		{
			if (g2Values.Length == 0 || g1Values.Length == 0)
			{
				return false;
			}

			int nHashes = g2Values.Length;

			if (nHashes != g1Values.Length)
			{
				return false;
			}

			// Zero keys should always fail
			if (g1Values.Any(pk => pk.IsIdentity()))
			{
				return false;
			}

			// Enforce distinct messages to counter BLS's rogue-key attack
			HashSet<byte[]> distinctHashes = g2Values
				.Select(h => h.ToCompressed())
				.ToHashSet(new ByteArrayComparer());
			if (distinctHashes.Count() != nHashes)
			{
				return false;
			}

			Fp12 millerLoopValue = Fp12.One();
			int i = 0;
			foreach ((G1Projective pk, G2Projective hash) in g1Values.Zip(g2Values, (pk, h) => (pk, h)))
			{
				G1Affine pkAffine = pk.ToAffine();
				G2Affine hAffine = hash.ToAffine();
				G2Prepared hPrepared = hAffine.ToPrepared();
				Fp12 result = BlsUtil.MillerLoop(
					Fp12.One(),
					(f) => DoublingStep(f, pkAffine, hPrepared, ref i),
					(f) => AddingStep(f, pkAffine, hPrepared, ref i),
					(f) => f.Square(),
					(f) => f.Conjugate()
				);
				millerLoopValue *= result;
			}

			G1Affine g1Neg = G1Affine.Generator().Neg();
			G2Prepared signaturePrepared = signature.ToPrepared();
			i = 0;
			Fp12 r = BlsUtil.MillerLoop(
					Fp12.One(),
					(f) => DoublingStep(f, g1Neg, signaturePrepared, ref i),
					(f) => AddingStep(f, g1Neg, signaturePrepared, ref i),
					(f) => f.Square(),
					(f) => f.Conjugate()
				);
			millerLoopValue *= r;

			return FinalExponentiation(millerLoopValue).Equals(Fp12.One());
		}


		private static Fp12 DoublingStep(Fp12 f, G1Affine publicKey, G2Prepared hash, ref int index)
		{
			try
			{
				bool eitherIdentity = publicKey.IsIdentity() || hash.IsInfinity;
				if (eitherIdentity)
				{
					return f;
				}
				Fp12 newF = Ell(f, hash.Coefficients[index], publicKey);
				return newF;
			}
			finally
			{
				index++;
			}
		}

		private static Fp12 AddingStep(Fp12 f, G1Affine publicKey, G2Prepared hash, ref int index)
		{
			try
			{
				bool eitherIdentity = publicKey.IsIdentity() || hash.IsInfinity;
				if (eitherIdentity)
				{
					return f;
				}
				Fp12 newF = Ell(f, hash.Coefficients[index], publicKey);
				return newF;
			}
			finally
			{
				index++;
			}
		}

		private static Fp12 Ell(Fp12 f, (Fp2, Fp2, Fp2) value, G1Affine publicKey)
		{
			(Fp2 c0, Fp2 c1, Fp2 c2) = value;

			c0 = new Fp2(c0.C0 * publicKey.Y, c0.C1 * publicKey.Y);
			c1 = new Fp2(c1.C0 * publicKey.X, c1.C1 * publicKey.X);

			return f.MultiplyBy014(c2, c1, c0);
		}

		private static Fp12 FinalExponentiation(Fp12 f)
		{
			Fp12 t0 = f
				.FrobeniusMap()
				.FrobeniusMap()
				.FrobeniusMap()
				.FrobeniusMap()
				.FrobeniusMap()
				.FrobeniusMap();
			Fp12 t1 = f.Invert() ?? throw new Exception("Failed to invert");
			Fp12 t2 = t0.Multiply(t1);
			t1 = t2;
			t2 = t2.FrobeniusMap().FrobeniusMap();
			t2 = t2.Multiply(t1);
			t1 = CyclotonmicSquare(t2).Conjugate();
			Fp12 t3 = CyclotomicExp(t2);
			Fp12 t4 = CyclotonmicSquare(t3);
			Fp12 t5 = t1.Multiply(t3);
			t1 = CyclotomicExp(t5);
			t0 = CyclotomicExp(t1);
			Fp12 t6 = CyclotomicExp(t0);
			t6 = t6.Multiply(t4);
			t4 = CyclotomicExp(t6);
			t5 = t5.Conjugate();
			t4 = t4.Multiply(t5).Multiply(t2);
			t5 = t2.Conjugate();
			t1 = t1.Multiply(t2);
			t1 = t1.FrobeniusMap().FrobeniusMap().FrobeniusMap();
			t6 = t6.Multiply(t5);
			t6 = t6.FrobeniusMap();
			t3 = t3.Multiply(t0);
			t3 = t3.FrobeniusMap().FrobeniusMap();
			t3 = t3.Multiply(t1);
			t3 = t3.Multiply(t6);
			f = t3.Multiply(t4);
			return f;
		}

		private static (Fp2, Fp2) Fp4Square(Fp2 a, Fp2 b)
		{
			Fp2 t0 = a.Square();
			Fp2 t1 = b.Square();
			Fp2 t2 = t1.MultiplyByNonresidue();
			Fp2 c0 = t2.Add(t0);
			t2 = a.Add(b);
			t2 = t2.Square();
			t2 = t2.Subtract(t0);
			Fp2 c1 = t2.Subtract(t1);

			return (c0, c1);
		}

		private static Fp12 CyclotonmicSquare(Fp12 f)
		{
			Fp2 z0 = f.C0.C0;
			Fp2 z4 = f.C0.C1;
			Fp2 z3 = f.C0.C2;
			Fp2 z2 = f.C1.C0;
			Fp2 z1 = f.C1.C1;
			Fp2 z5 = f.C1.C2;

			(Fp2 t0, Fp2 t1) = Fp4Square(z0, z1);

			// For A
			z0 = t0.Subtract(z0);
			z0 = z0.Add(z0).Add(t0);

			z1 = t1.Add(z1);
			z1 = z1.Add(z1).Add(t1);

			(t0, t1) = Fp4Square(z2, z3);
			(Fp2 t2, Fp2 t3) = Fp4Square(z4, z5);

			// For C
			z4 = t0.Subtract(z4);
			z4 = z4.Add(z4).Add(t0);

			z5 = t1.Add(z5);
			z5 = z5.Add(z5).Add(t1);

			// For B
			t0 = t3.MultiplyByNonresidue();
			z2 = t0.Add(z2);
			z2 = z2.Add(z2).Add(t0);

			z3 = t2.Subtract(z3);
			z3 = z3.Add(z3).Add(t2);
			return new Fp12(new Fp6(z0, z4, z3), new Fp6(z2, z1, z5));

		}

		private static Fp12 CyclotomicExp(Fp12 f)
		{
			ulong x = Constants.BLS_X;
			Fp12 tmp = Fp12.One();
			bool foundOne = false;
			for (int i = 63; i >= 0; i--)
			{
				bool b = ((x >> i) & 1) == 1;
				if (foundOne)
				{
					tmp = CyclotonmicSquare(tmp);
				}
				else
				{
					foundOne = b;
				}

				if (b)
				{
					tmp = tmp.Multiply(f);
				}
			}

			return tmp.Conjugate();
		}

		class ByteArrayComparer : IEqualityComparer<byte[]>
		{
			public bool Equals(byte[] x, byte[] y)
			{
				return x.SequenceEqual(y);
			}

			public int GetHashCode(byte[] obj)
			{
				return obj.GetHashCode();
			}
		}
	}

}
