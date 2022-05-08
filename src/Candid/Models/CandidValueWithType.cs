using ICP.Candid.Models.Types;
using ICP.Candid.Models.Values;
using System;

namespace ICP.Candid.Models
{
    public class CandidValueWithType
    {
        public CandidValue Value { get; }
        public CandidTypeDefinition Type { get; }

        private CandidValueWithType(CandidValue value, CandidTypeDefinition type)
        {
            this.Value = value ?? throw new ArgumentNullException(nameof(value));
            this.Type = type ?? throw new ArgumentNullException(nameof(type));
        }

        public static CandidValueWithType FromValueAndType(CandidValue value, CandidTypeDefinition type)
        {
            // TODO validate
            return new CandidValueWithType(value, type);
        }
    }
}
