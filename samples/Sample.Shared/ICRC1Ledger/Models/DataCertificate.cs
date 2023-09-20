using BlockIndex = EdjCase.ICP.Candid.Models.UnboundedUInt;
using Subaccount = System.Collections.Generic.List<System.Byte>;
using Timestamp = System.UInt64;
using Duration = System.UInt64;
using Tokens = EdjCase.ICP.Candid.Models.UnboundedUInt;
using TxIndex = EdjCase.ICP.Candid.Models.UnboundedUInt;
using QueryArchiveFn = EdjCase.ICP.Candid.Models.Values.CandidFunc;
using Map = System.Collections.Generic.List<(System.String, Sample.Shared.ICRC1Ledger.Models.Value)>;
using Block = Sample.Shared.ICRC1Ledger.Models.Value;
using QueryBlockArchiveFn = EdjCase.ICP.Candid.Models.Values.CandidFunc;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid.Models;
using System.Collections.Generic;

namespace Sample.Shared.ICRC1Ledger.Models
{
	public class DataCertificate
	{
		[CandidName("certificate")]
		public OptionalValue<List<byte>> Certificate { get; set; }

		[CandidName("hash_tree")]
		public List<byte> HashTree { get; set; }

		public DataCertificate(OptionalValue<List<byte>> certificate, List<byte> hashTree)
		{
			this.Certificate = certificate;
			this.HashTree = hashTree;
		}

		public DataCertificate()
		{
		}
	}
}