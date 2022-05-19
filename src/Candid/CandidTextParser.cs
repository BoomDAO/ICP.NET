using ICP.Candid.Exceptions;
using ICP.Candid.Models;
using ICP.Candid.Models.Types;
using ICP.Candid.Models.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICP.Candid
{
    public static class CandidTextParser
    {
        public static CandidType Parse(string text)
        {
            return Parse<CandidType>(text);
        }

        public static T Parse<T>(string text)
            where T : CandidType
        {
            CandidTextTokenHelper helper = CandidTextTokenizer.Tokenize(text);
            CandidType def = ParseInternal(helper);
            if (def.GetType() != typeof(T))
            {
                throw new InvalidOperationException($"Cannot convert parsed type '{def.GetType().Name}' to type '{typeof(T).Name}'");
            }
            return (T)def;
        }


        private static CandidType ParseInternal(CandidTextTokenHelper helper)
        {
            switch (helper.CurrentToken.Type)
            {
                case CandidTextTokenType.Text:
                    CandidType typeDef = GetNamedType(helper);
                    return typeDef;
                case CandidTextTokenType.OpenParenthesis:
                    return CandidTextParser.GetFunc(helper);
                default:
                    // TODO
                    throw new NotImplementedException();
            }
        }

        private static CandidFuncType GetFunc(CandidTextTokenHelper helper)
        {
            helper.CurrentToken.ValidateType(CandidTextTokenType.OpenParenthesis);

            helper.MoveNextOrThrow();
            var argTypes = new List<CandidType>();
            while (helper.CurrentToken.Type != CandidTextTokenType.CloseParenthesis)
            {
                CandidType t = ParseInternal(helper);
                argTypes.Add(t);
            }

            helper.MoveNextOrThrow();
            string arrow = helper.CurrentToken.GetTextValueOrThrow();
            if (arrow != "->")
            {
                throw new CandidTextParseException($"Expected token '->', got '{arrow}'");
            }
            helper.MoveNextOrThrow();
            helper.CurrentToken.ValidateType(CandidTextTokenType.OpenParenthesis);

            helper.MoveNextOrThrow();
            var returnTypes = new List<CandidType>();
            while (helper.CurrentToken.Type != CandidTextTokenType.CloseParenthesis)
            {
                CandidType t = ParseInternal(helper);
                returnTypes.Add(t);
            }

            var modes = new List<FuncMode>();
            while (helper.MoveNext())
            {
                string rawMode = helper.CurrentToken.GetTextValueOrThrow();
                FuncMode mode = rawMode switch
                {
                    "query" => FuncMode.Query,
                    "oneway" => FuncMode.Oneway,
                    _ => throw new CandidTextParseException($"Unexpected func mode '{rawMode}'. Valid func modes: {string.Join(", ", Enum.GetValues(typeof(FuncMode)))}")
                };
                modes.Add(mode);
            }
            return new CandidFuncType(modes, argTypes, returnTypes);
        }
        private static CandidOptType GetOpt(CandidTextTokenHelper helper, CandidId? recursiveId)
        {
            CandidType innerValue = ParseInternal(helper);
            return new CandidOptType(innerValue, recursiveId);
        }

        private static CandidRecordType GetRecord(CandidTextTokenHelper helper, CandidId? recursiveId)
        {
            Dictionary<CandidTag, CandidType> fields = new();
            helper.CurrentToken.ValidateType(CandidTextTokenType.OpenCurlyBrace);
            helper.MoveNextOrThrow();
            uint index = 0;
            while (helper.CurrentToken.Type != CandidTextTokenType.CloseCurlyBrace)
            {
                CandidTag label;
                CandidType fieldType;
                if (helper.NextToken?.Type == CandidTextTokenType.Colon)
                {
                    // `label`: `type`
                    string rawLabel = helper.CurrentToken.GetTextValueOrThrow();
                    label = CandidTag.FromName(rawLabel);
                    helper.MoveNextOrThrow();
                    helper.CurrentToken.ValidateType(CandidTextTokenType.Colon);
                    helper.MoveNextOrThrow();
                    fieldType = ParseInternal(helper);
                }
                else
                {
                    // `type` (based on position/index)
                    label = CandidTag.FromId(index++);
                    fieldType = ParseInternal(helper);
                }
                fields.Add(label, fieldType);
            }
            helper.MoveNextOrThrow();
            return new CandidRecordType(fields, recursiveId);
        }

        private static CandidVectorType GetVec(CandidTextTokenHelper helper, CandidId? recursiveId)
        {
            CandidType innerValue = ParseInternal(helper);
            return new CandidVectorType(innerValue, recursiveId);
        }

        private static CandidVariantType GetVariant(CandidTextTokenHelper helper, CandidId? recursiveId)
        {
            Dictionary<CandidTag, CandidType> options = new();
            helper.CurrentToken.ValidateType(CandidTextTokenType.OpenCurlyBrace);
            helper.MoveNextOrThrow();
            while (helper.CurrentToken.Type != CandidTextTokenType.CloseCurlyBrace)
            {
                string label = helper.CurrentToken.GetTextValueOrThrow();
                helper.MoveNextOrThrow();
                CandidType fieldType;
                if (helper.CurrentToken.Type == CandidTextTokenType.SemiColon)
                {
                    fieldType = new CandidPrimitiveType(PrimitiveType.Null);
                }
                else
                {
                    helper.CurrentToken.ValidateType(CandidTextTokenType.Colon);
                    helper.MoveNextOrThrow();
                    fieldType = ParseInternal(helper);
                }
                options.Add(CandidTag.FromName(label), fieldType);
            }
            helper.MoveNextOrThrow();
            return new CandidVariantType(options, recursiveId);
        }

        private static CandidServiceType GetService(CandidTextTokenHelper helper, CandidId? recursiveId)
        {
            // TODO
            throw new NotImplementedException();
        }

        private static CandidType GetNamedType(CandidTextTokenHelper helper)
        {
            CandidId? recursiveId = null;
            string type = helper.CurrentToken.GetTextValueOrThrow();
            // Check to see if text is recursive id like `μrec_1.record` vs just the type name `record`
            if (type.StartsWith("μ"))
            {
                recursiveId = CandidId.Parse(type[1..]);
                helper.MoveNextOrThrow();
                helper.CurrentToken.ValidateType(CandidTextTokenType.Period); // Period seperates recursive id and type
                helper.MoveNextOrThrow();
                type = helper.CurrentToken.GetTextValueOrThrow();
            }
            helper.MoveNextOrThrow();

            CandidType t = CandidTextParser.GetNamedType(type, helper, recursiveId);
            if (helper.CurrentToken.Type == CandidTextTokenType.SemiColon)
            {
                helper.MoveNextOrThrow();
            }
            return t;
        }

        private static CandidType GetNamedType(string type, CandidTextTokenHelper helper, CandidId? recursiveId)
        {
            switch (type)
            {
                case "opt":
                    return CandidTextParser.GetOpt(helper, recursiveId);
                case "record":
                    return CandidTextParser.GetRecord(helper, recursiveId);
                case "vec":
                    return CandidTextParser.GetVec(helper, recursiveId);
                case "variant":
                    return CandidTextParser.GetVariant(helper, recursiveId);
                case "service":
                    return CandidTextParser.GetService(helper, recursiveId);
                case "int8":
                    ThrowIfRecursiveIdSet();
                    return new CandidPrimitiveType(PrimitiveType.Int8);
                case "int16":
                    ThrowIfRecursiveIdSet();
                    return new CandidPrimitiveType(PrimitiveType.Int16);
                case "int32":
                    ThrowIfRecursiveIdSet();
                    return new CandidPrimitiveType(PrimitiveType.Int32);
                case "int64":
                    ThrowIfRecursiveIdSet();
                    return new CandidPrimitiveType(PrimitiveType.Int64);
                case "int":
                    ThrowIfRecursiveIdSet();
                    return new CandidPrimitiveType(PrimitiveType.Int);
                case "nat8":
                    ThrowIfRecursiveIdSet();
                    return new CandidPrimitiveType(PrimitiveType.Nat8);
                case "nat16":
                    ThrowIfRecursiveIdSet();
                    return new CandidPrimitiveType(PrimitiveType.Nat16);
                case "nat32":
                    ThrowIfRecursiveIdSet();
                    return new CandidPrimitiveType(PrimitiveType.Nat32);
                case "nat64":
                    ThrowIfRecursiveIdSet();
                    return new CandidPrimitiveType(PrimitiveType.Nat64);
                case "nat":
                    return new CandidPrimitiveType(PrimitiveType.Nat);
                case "text":
                    ThrowIfRecursiveIdSet();
                    return new CandidPrimitiveType(PrimitiveType.Text);
                case "float32":
                    ThrowIfRecursiveIdSet();
                    return new CandidPrimitiveType(PrimitiveType.Float32);
                case "float64":
                    ThrowIfRecursiveIdSet();
                    return new CandidPrimitiveType(PrimitiveType.Float64);
                case "bool":
                    ThrowIfRecursiveIdSet();
                    return new CandidPrimitiveType(PrimitiveType.Bool);
                case "principal":
                    ThrowIfRecursiveIdSet();
                    return new CandidPrimitiveType(PrimitiveType.Principal);
                case "reserved":
                    ThrowIfRecursiveIdSet();
                    return new CandidPrimitiveType(PrimitiveType.Reserved);
                case "empty":
                    ThrowIfRecursiveIdSet();
                    return new CandidPrimitiveType(PrimitiveType.Empty);
                case "null":
                    ThrowIfRecursiveIdSet();
                    return new CandidPrimitiveType(PrimitiveType.Null);
                default:
                    return new CandidReferenceType(CandidId.Parse(type), () => throw new NotImplementedException()); // TODO
            };

            void ThrowIfRecursiveIdSet()
            {
                if (recursiveId != null)
                {
                    throw new CandidTextParseException($"Recursive reference id '{recursiveId}' is set on an invalid type '{type}'");
                }
            }
        }
    }
}
