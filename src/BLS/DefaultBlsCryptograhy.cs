using EdjCase.ICP.BLS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EdjCase.ICP.BLS
{
	public class DefaultBlsCryptograhy : IBlsCryptography
	{
		public bool VerifySignature(byte[] publicKey, byte[] messageHash, byte[] signature)
		{
			G2Protective[] hashes = new[]{
				G2Protective.FromBytes(messageHash)
			};
			PublicKey[] publicKeys = new PublicKey[]
			{
				PublicKey.FromBytes(publicKey)
			};
			Signature sig = Signature.FromBytes(signature);
			return Verify(sig, hashes, publicKeys);
		}

		private bool Verify(Signature signature, G2Projective[] hashes, PublicKey[] publicKeys)
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
			if (publicKeys.Any(pk => pk.IsIdentity))
			{
				return false;
			}

			// Enforce distinct messages to counter BLS's rogue-key attack
			var distinctHashes = hashes.Select(h => h.ToCompressed()).ToHashSet();
			if (distinctHashes.Count != nHashes)
			{
				return false;
			}

			MillerLoopResult ml = new MillerLoopResult();
			foreach (var pair in publicKeys.Zip(hashes, (pk, h) => new { PublicKey = pk, Hash = h }))
			{
				var pkAffine = pair.PublicKey.AsAffine();
				var hAffine = pair.Hash.ToAffine();
				ml = ml.Add(Bls12.MultiMillerLoop(new Tuple<AffinePublicKey, AffineHash>(pkAffine, hAffine)));
			}

			var g1Neg = G1Affine.NegativeGenerator();

			ml = ml.Add(Bls12.MultiMillerLoop(new Tuple<AffinePublicKey, Signature>(g1Neg, signature)));

			return ml.FinalExponentiation() == Gt.Identity;
		}
	}
