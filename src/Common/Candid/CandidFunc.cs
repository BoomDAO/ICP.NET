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
		public override int GetHashCode()
		{
			return HashCode.Combine(this.Name, this.Service.GetHashCode());
		}

		public override bool Equals(CandidValue? other)
		{
			if (other is CandidFunc f)
			{
				return this.Name == f.Name && this.Service == f.Service;
			}
			return false;
		}

        public override string ToString()
        {
			return $"(Method: {this.Name}, Service: {this.Service})";
        }
    }

}
