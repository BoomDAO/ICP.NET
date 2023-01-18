using EdjCase.ICP.Candid.Models.Types;
using System;
using System.Linq;
using System.Text;

namespace EdjCase.ICP.Candid.Models.Values
{
	public class CandidFunc : CandidValue
	{
		public override CandidValueType Type { get; } = CandidValueType.Func;
		public bool IsOpaqueReference { get; }
		private readonly (CandidService Service, string Method)? serviceInfo;

		public CandidFunc(CandidService service, string name)
		{
			this.IsOpaqueReference = false;
			this.serviceInfo = (service, name);
		}


		private CandidFunc()
		{
			this.IsOpaqueReference = true;
			this.serviceInfo = null;
		}

		public override byte[] EncodeValue(CandidType type, Func<CandidId, CandidCompoundType> getReferencedType)
		{
			CandidFuncType t;
			if (type is CandidReferenceType r)
			{
				t = (CandidFuncType)getReferencedType(r.Id);
			}
			else
			{
				t = (CandidFuncType)type;
			}
			if (this.IsOpaqueReference)
			{
				return new byte[] { 0 };
			}
			(CandidService service, string method) = this.serviceInfo!.Value;
			return new byte[] { 1 }
				.Concat(service.EncodeValue(t, getReferencedType))
				.Concat(Encoding.UTF8.GetBytes(method))
				.ToArray();
		}
		public override int GetHashCode()
		{
			return HashCode.Combine(this.IsOpaqueReference, this.serviceInfo);
		}

		public override bool Equals(CandidValue? other)
		{
			if (other is CandidFunc f)
			{
				if (this.IsOpaqueReference != f.IsOpaqueReference)
				{
					return false;
				}
				if (this.IsOpaqueReference)
				{
					// TODO can we tell if they are equal?
					return false;
				}
				return this.serviceInfo == f.serviceInfo;
			}
			return false;
		}

		public override string ToString()
		{
			if (this.IsOpaqueReference)
			{
				return "(Opaque Reference)";
			}
			(CandidService service, string method) = this.serviceInfo!.Value;
			return $"(Method: {method}, Service: {service})";
		}

		public static CandidFunc TrasparentReference(CandidService service, string method)
		{
			return new CandidFunc(service, method);
		}

		public static CandidFunc OpaqueReference()
		{
			return new CandidFunc();
		}
	}

}
