using System;
using System.Collections.Generic;
using System.Text;

namespace EdjCase.ICP.Agent
{
	/// <summary>
	/// A model to contain OID information for cryptographic algorithms and their curves.
	/// Used in SubjectPublicKeyInfo models
	/// </summary>
	public class AlgorithmIdentifier
	{
		/// <summary>
		/// The OID of the algorithm
		/// </summary>
		public string AlgorithmOid { get; }

		/// <summary>
		/// The OID of the parameters of the algorithm, such as a specific curve OID
		/// </summary>
		public string? ParametersOid { get; }

		/// <summary>
		/// Default constructor
		/// </summary>
		/// <param name="algorithmOid">The OID of the algorithm</param>
		/// <param name="parametersOid">The OID of the parameters of the algorithm, such as a specific curve OID</param>
		public AlgorithmIdentifier(string algorithmOid, string? parametersOid = null)
		{
			this.AlgorithmOid = algorithmOid ?? throw new ArgumentNullException(nameof(algorithmOid));
			this.ParametersOid = parametersOid;
		}

		/// <summary>
		/// Helper method to create an `AlgorithmIdentifier` for Ed25519
		/// </summary>
		/// <returns>AlgorithmIdentifier for Ed25519</returns>
		public static AlgorithmIdentifier Ed25519()
		{
			return new AlgorithmIdentifier("1.3.101.112");
		}

		/// <summary>
		/// Helper method to create an `AlgorithmIdentifier` for Secp256k1
		/// </summary>
		/// <returns>AlgorithmIdentifier for Secp256k1</returns>
		public static AlgorithmIdentifier Secp256k1()
		{
			return Ecdsa("1.3.132.0.10");
		}

		/// <summary>
		/// Helper method to create an `AlgorithmIdentifier` for Ecdsa
		/// </summary>
		/// <param name="curveOid">The OID of the specific curve to use for ECDSA</param>
		/// <returns>AlgorithmIdentifier for Ecdsa</returns>
		public static AlgorithmIdentifier Ecdsa(string curveOid)
		{
			return new AlgorithmIdentifier("1.2.840.10045.2.1", curveOid);
		}

		/// <summary>
		/// Helper method to create an `AlgorithmIdentifier` for Bls
		/// </summary>
		/// <returns>AlgorithmIdentifier for Bls</returns>
		public static AlgorithmIdentifier Bls()
		{
			return new AlgorithmIdentifier("1.3.6.1.4.1.44668.5.3.1.2.1", "1.3.6.1.4.1.44668.5.3.2.1");
		}
	}
}
