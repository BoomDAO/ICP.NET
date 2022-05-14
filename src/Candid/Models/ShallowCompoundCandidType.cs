using ICP.Candid.Models.Types;
using System.Collections.Generic;

namespace ICP.Candid.Models
{
    public abstract record ShallowCandidType
    {

    }

    public abstract record ShallowPrimitiveOrReferenceCandidType : ShallowCandidType
    {
    }
    public abstract record ShallowCompoundCandidType : ShallowCandidType
    {
    }

    public record ShallowVectorCandidType(List<ShallowCandidType> Items) : ShallowCompoundCandidType
    {

    }

    public record ShallowPrimitiveCandidType(PrimitiveCandidTypeDefinition Type) : ShallowPrimitiveOrReferenceCandidType
    {

    }

    public record ShallowVariantCandidType(List<(string, ReferenceCandidTypeDefinition)> Options) : ShallowCompoundCandidType
    {

    }

    public record ShallowRecordCandidType(List<(CandidTag, ReferenceCandidTypeDefinition)> Fields) : ShallowCompoundCandidType
    {

    }
}