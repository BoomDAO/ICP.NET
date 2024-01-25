using EdjCase.ICP.Candid.Mapping;

namespace EdjCase.ICP.Agent.Standards.AssetCanister.Models
{
	/// <summary>
	/// Represents the result of a certified tree query to the asset canister.
	/// </summary>
	public class CertifiedTreeResult
	{
		/// <summary>
		/// The certificate associated with the certified tree.
		/// </summary>
		[CandidName("certificate")]
		public byte[] Certificate { get; set; }

		/// <summary>
		/// The binary representation of the certified tree.
		/// </summary>
		[CandidName("tree")]
		public byte[] Tree { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="CertifiedTreeResult"/> class with specified certificate and tree.
		/// </summary>
		/// <param name="certificate">The certificate associated with the certified tree.</param>
		/// <param name="tree">The binary representation of the certified tree.</param>
		public CertifiedTreeResult(byte[] certificate, byte[] tree)
		{
			this.Certificate = certificate;
			this.Tree = tree;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CertifiedTreeResult"/> class.
		/// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public CertifiedTreeResult()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		{
		}
	}
}
