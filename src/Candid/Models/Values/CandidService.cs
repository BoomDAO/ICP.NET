using EdjCase.ICP.Candid.Models;
using EdjCase.ICP.Candid.Models.Types;
using System;
using System.Linq;

namespace EdjCase.ICP.Candid.Models.Values
{
	public class CandidService : CandidValue
	{
		public override CandidValueType Type { get; } = CandidValueType.Service;
		public bool IsOpqaueReference { get; }

		private readonly Principal? principalId;

		public CandidService(Principal? principalId)
		{
			this.principalId = principalId ?? throw new ArgumentNullException(nameof(principalId));
			this.IsOpqaueReference = false;
		}

		private CandidService()
		{
			this.principalId = null;
			this.IsOpqaueReference = true;
		}

		public Principal GetAsPrincipal()
		{
			if (this.IsOpqaueReference)
			{
				throw new InvalidOperationException("Cannot get principal from opaque service reference.");
			}
			return this.principalId!;
		}

		public override byte[] EncodeValue(CandidType type, Func<CandidId, CandidCompoundType> getReferencedType)
		{
			CandidServiceType t;
			if (type is CandidReferenceType r)
			{
				t = (CandidServiceType)getReferencedType(r.Id);
			}
			else
			{
				t = (CandidServiceType)type;
			}
			if (this.IsOpqaueReference)
            {
				return new byte[] { 0 };
            }
			return new byte[] { 1 }
				.Concat(this.principalId!.Raw)
				.ToArray();
		}
		public override int GetHashCode()
		{
			return HashCode.Combine(this.IsOpqaueReference, this.principalId);
		}

		public override bool Equals(CandidValue? other)
		{
			if (other is CandidService s)
			{
				if(this.IsOpqaueReference != s.IsOpqaueReference)
                {
					return false;
                }
                if (this.IsOpqaueReference)
                {
					// TODO can we ever tell if they are the same? do we care?
					return false;
                }
				return this.principalId == s.principalId;
			}
			return false;
		}

        public override string ToString()
        {
			return this.IsOpqaueReference
				? "(Opaque Reference)"
				: this.principalId!.ToString();
		}

		public static CandidService TraparentReference(Principal? principalId)
		{
			return new CandidService(principalId);
		}

		public static CandidService OpaqueReference()
		{
			return new CandidService();
		}
	}
}