using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid.Models;
using System.Collections.Generic;

namespace Sample.Shared.ICRC1Ledger.Models
{
	public class DataCertificate
	{
		[CandidName("certificate")]
		public OptionalValue<byte[]> Certificate { get; set; }

		[CandidName("hash_tree")]
		public byte[] HashTree { get; set; }

		public DataCertificate(OptionalValue<byte[]> certificate, byte[] hashTree)
		{
			this.Certificate = certificate;
			this.HashTree = hashTree;
		}

		public DataCertificate()
		{
		}
	}
}