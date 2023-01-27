using EdjCase.ICP.Candid.Models.Types;
using System;
using System.Linq;
using System.Text;

namespace EdjCase.ICP.Candid.Models.Values
{
	/// <summary>
	/// A model to represent the value of a candid func
	/// </summary>
	public class CandidFunc : CandidValue
	{
		/// <inheritdoc />
		public override CandidValueType Type { get; } = CandidValueType.Func;

		/// <summary>
		/// True if the candid func definition is an opaque (non standard/system specific definition),
		/// otherwise false
		/// </summary>
		public bool IsOpaqueReference { get; }

		private readonly (CandidService Service, string Method)? serviceInfo;

		/// <param name="service">The candid service definition the function lives in</param>
		/// <param name="name">The name of the function</param>
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

		/// <inheritdoc />
		internal override byte[] EncodeValue(CandidType type, Func<CandidId, CandidCompoundType> getReferencedType)
		{
			if (this.IsOpaqueReference)
			{
				return new byte[] { 0 };
			}
			CandidFuncType t;
			if (type is CandidReferenceType r)
			{
				t = (CandidFuncType)getReferencedType(r.Id);
			}
			else
			{
				t = (CandidFuncType)type;
			}
			(CandidService service, string method) = this.serviceInfo!.Value;
			return new byte[] { 1 }
				.Concat(service.EncodeValue(t, getReferencedType))
				.Concat(Encoding.UTF8.GetBytes(method))
				.ToArray();
		}

		/// <inheritdoc />
		public override int GetHashCode()
		{
			return HashCode.Combine(this.IsOpaqueReference, this.serviceInfo);
		}

		/// <inheritdoc />
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

		/// <inheritdoc />
		public override string ToString()
		{
			if (this.IsOpaqueReference)
			{
				return "(Opaque Reference)";
			}
			(CandidService service, string method) = this.serviceInfo!.Value;
			return $"(Method: {method}, Service: {service})";
		}

		/// <summary>
		/// Creates an opaque reference to a function that is defined by the system
		/// vs being defined in candid
		/// </summary>
		/// <returns>A opaque candid func</returns>
		public static CandidFunc OpaqueReference()
		{
			return new CandidFunc();
		}
	}

}
