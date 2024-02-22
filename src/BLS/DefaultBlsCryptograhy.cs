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
		// The BLS parameter x for BLS12-381 is -0xd201000000010000
		private const ulong BLS_X = 0xd201_0000_0001_0000;
		/// <inheritdoc />
		public bool VerifySignature(byte[] publicKey, byte[] messageHash, byte[] signature)
		{
			G2Projective[] hashes = new[]
			{
				G2Projective.FromCompressed(messageHash)
			};
			G1Projective[] publicKeys = new G1Projective[]
			{
				G1Projective.FromCompressed(publicKey)
			};
			G2Affine sig = G2Affine.FromCompressed(signature);
			return this.Verify(sig, hashes, publicKeys);
		}

		private bool Verify(G2Affine signature, G2Projective[] hashes, G1Projective[] publicKeys)
		{
			if (hashes.Length == 0 || publicKeys.Length == 0)
			{
				return false;
			}

			int nHashes = hashes.Length;

			if (nHashes != publicKeys.Length)
			{
				return false;
			}

			// Zero keys should always fail
			if (publicKeys.Any(pk => pk.IsIdentity()))
			{
				return false;
			}

			// Enforce distinct messages to counter BLS's rogue-key attack
			HashSet<byte[]> distinctHashes = hashes
				.Select(h => h.ToCompressed())
				.ToHashSet(new ByteArrayComparer());
			if (distinctHashes.Count() != nHashes)
			{
				return false;
			}

			Fp12 ml = Fp12.One();
			foreach ((G1Projective pk, G2Projective hash) in publicKeys.Zip(hashes, (pk, h) => (pk, h)))
			{
				G1Affine pkAffine = pk.ToAffine();
				G2Affine hAffine = hash.ToAffine();
				ml = ml.Add(Bls12.MultiMillerLoop((pkAffine, hAffine)));
			}

			var g1Neg = G1Affine.NegativeGenerator();
			var r = Bls12.MultiMillerLoop(g1Neg, signature);
			ml = ml.Add(r);

			return ml.FinalExponentiation() == Gt.Identity;
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
			Fp12? t1 = f.Invert();
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
			ulong x = BLS_X;
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
