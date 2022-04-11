using Common.Models;
using System;
using System.Linq;
using System.Text;

namespace ICP.Common.Candid
{
	public class CandidFunc : CandidValue
	{
		public override CandidValueType Type { get; } = CandidValueType.Func;

		public CandidService Service { get; }
		public string Name { get; }

		public CandidFunc(CandidService service, string name)
		{
			this.Service = service;
			this.Name = name;
		}

		public override byte[] EncodeValue()
		{
			return new byte[] { 1 }
				.Concat(this.Service.EncodeValue())
				.Concat(Encoding.UTF8.GetBytes(this.Name))
				.ToArray();
		}
	}

}
