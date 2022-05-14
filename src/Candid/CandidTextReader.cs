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
    public static class CandidTextReader
    {
        public static FuncCandidTypeDefinition ReadFunc(string value)
        {
            CandidTextTokenHelper helper = CandidTextTokenizer.Tokenize(value);

            helper.CurrentToken.ValidateType(CandidTextTokenType.OpenParenthesis);

            helper.MoveNextOrThrow();
            var argTypes = new List<CandidTypeDefinition>();
            while (helper.CurrentToken.Type != CandidTextTokenType.CloseParenthesis)
            {
                CandidTypeDefinition type = GetType(helper);
                argTypes.Add(type);
            }

            helper.MoveNextOrThrow();
            string arrow = helper.CurrentToken.GetTextValueOrThrow();
            if(arrow != "->")
            {
                // TODO
                throw new CandidTextParseException();
            }
            helper.MoveNextOrThrow();
            helper.CurrentToken.ValidateType(CandidTextTokenType.OpenParenthesis);

            helper.MoveNextOrThrow();
            var returnTypes = new List<CandidTypeDefinition>();
            while (helper.CurrentToken.Type != CandidTextTokenType.CloseParenthesis)
            {
                CandidTypeDefinition type = GetType(helper);
                returnTypes.Add(type);
            }

            var modes = new List<FuncMode>();
            while (helper.MoveNext())
            {
                string rawMode = helper.CurrentToken.GetTextValueOrThrow();
                FuncMode mode = rawMode switch
                {
                    "query" => FuncMode.Query,
                    "oneway" => FuncMode.Oneway,
                    _ => throw new CandidTextParseException()
                };
                modes.Add(mode);
            }
            return new FuncCandidTypeDefinition(modes, argTypes, returnTypes);
        }

        internal static OptCandidTypeDefinition ReadOpt(string text)
        {
            throw new NotImplementedException();
        }

        private static CandidTypeDefinition GetType(CandidTextTokenHelper helper)
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
            CandidTypeDefinition typeDef = GetType(type, recursiveId, helper);
            if (helper.CurrentToken.Type == CandidTextTokenType.SemiColon)
            {
                helper.MoveNextOrThrow();
            }
            return typeDef;
        }

        private static CandidTypeDefinition GetType(string type, CandidId? recursiveId, CandidTextTokenHelper helper)
        {
            switch (type)
            {
                case "opt":
                    {
                        CandidTypeDefinition innerValue = GetType(helper);
                        return new OptCandidTypeDefinition(innerValue, recursiveId);
                    }
                case "record":
                    {
                        Dictionary<CandidTag, CandidTypeDefinition> fields = new();
                        helper.CurrentToken.ValidateType(CandidTextTokenType.OpenCurlyBrace);
                        helper.MoveNextOrThrow();
                        uint index = 0;
                        while (helper.CurrentToken.Type != CandidTextTokenType.CloseCurlyBrace)
                        {
                            CandidTag label;
                            CandidTypeDefinition fieldType;
                            if (helper.NextToken?.Type == CandidTextTokenType.Colon)
                            {
                                // `label`: `type`
                                string rawLabel = helper.CurrentToken.GetTextValueOrThrow();
                                label = CandidTag.FromName(rawLabel);
                                helper.MoveNextOrThrow();
                                helper.CurrentToken.ValidateType(CandidTextTokenType.Colon);
                                helper.MoveNextOrThrow();
                                fieldType = GetType(helper);
                            }
                            else
                            {
                                // `type` (based on position/index)
                                label = CandidTag.FromId(index++);
                                fieldType = GetType(helper);
                            }
                            fields.Add(label, fieldType);
                        }
                        helper.MoveNextOrThrow();
                        return new RecordCandidTypeDefinition(fields, recursiveId);
                    }
                case "vec":
                    {
                        CandidTypeDefinition innerValue = GetType(helper);
                        return new VectorCandidTypeDefinition(innerValue, recursiveId);
                    }
                case "variant":
                    {
                        Dictionary<CandidTag, CandidTypeDefinition> options = new();
                        helper.CurrentToken.ValidateType(CandidTextTokenType.OpenCurlyBrace);
                        helper.MoveNextOrThrow();
                        while (helper.CurrentToken.Type != CandidTextTokenType.CloseCurlyBrace)
                        {
                            string label = helper.CurrentToken.GetTextValueOrThrow();
                            helper.MoveNextOrThrow();
                            CandidTypeDefinition fieldType;
                            if (helper.CurrentToken.Type == CandidTextTokenType.SemiColon)
                            {
                                fieldType = new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Null);
                            }
                            else
                            {
                                helper.CurrentToken.ValidateType(CandidTextTokenType.Colon);
                                helper.MoveNextOrThrow();
                                fieldType = GetType(helper);
                            }
                            options.Add(CandidTag.FromName(label), fieldType);
                        }
                        helper.MoveNextOrThrow();
                        return new VariantCandidTypeDefinition(options, recursiveId);
                    }
                case "func":
                    {
                        // TODO
                        throw new NotImplementedException();
                    }
                case "service":
                    {
                        // TODO
                        throw new NotImplementedException();
                    }
                case "int8":
                    ThrowIfRecursiveIdSet();
                    return new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Int8);
                case "int16":
                    ThrowIfRecursiveIdSet();
                    return new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Int16);
                case "int32":
                    ThrowIfRecursiveIdSet();
                    return new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Int32);
                case "int64":
                    ThrowIfRecursiveIdSet();
                    return new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Int64);
                case "int":
                    ThrowIfRecursiveIdSet();
                    return new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Int);
                case "nat8":
                    ThrowIfRecursiveIdSet();
                    return new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Nat8);
                case "nat16":
                    ThrowIfRecursiveIdSet();
                    return new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Nat16);
                case "nat32":
                    ThrowIfRecursiveIdSet();
                    return new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Nat32);
                case "nat64":
                    ThrowIfRecursiveIdSet();
                    return new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Nat64);
                case "nat":
                    return new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Nat);
                case "text":
                    ThrowIfRecursiveIdSet();
                    return new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Text);
                case "float32":
                    ThrowIfRecursiveIdSet();
                    return new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Float32);
                case "float64":
                    ThrowIfRecursiveIdSet();
                    return new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Float64);
                case "bool":
                    ThrowIfRecursiveIdSet();
                    return new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Bool);
                case "principal":
                    ThrowIfRecursiveIdSet();
                    return new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Principal);
                case "reserved":
                    ThrowIfRecursiveIdSet();
                    return new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Reserved);
                case "empty":
                    ThrowIfRecursiveIdSet();
                    return new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Empty);
                case "null":
                    ThrowIfRecursiveIdSet();
                    return new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Null);
                default:
                    ThrowIfRecursiveIdSet();
                    return new ReferenceCandidTypeDefinition(CandidId.Parse(type), () => throw new NotImplementedException()); // TODO
            }

            void ThrowIfRecursiveIdSet()
            {
                if (recursiveId != null)
                {
                    // TODO
                    throw new CandidTextParseException();
                }
            }
        }
    }
}
