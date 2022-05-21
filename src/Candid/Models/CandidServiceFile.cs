using ICP.Candid.Models;
using ICP.Candid.Models.Types;
using ICP.Candid.Parsers;
using System;
using System.Collections.Generic;
using System.Text;

namespace ICP.Candid.Models
{
    public class CandidServiceFile
    {
        public CandidServiceType Service { get; }
        public Dictionary<CandidId, CandidType> DeclaredTypes { get; }

        public CandidServiceFile(
            CandidServiceType service,
            Dictionary<CandidId, CandidType> declaredTypes)
        {
            this.Service = service ?? throw new ArgumentNullException(nameof(service));
            this.DeclaredTypes = declaredTypes ?? throw new ArgumentNullException(nameof(declaredTypes));
        }

        public static CandidServiceFile Parse(string text)
        {
            return CandidServiceFileParser.Parse(text);
        }
    }
}
