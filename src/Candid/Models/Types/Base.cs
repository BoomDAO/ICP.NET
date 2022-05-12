using ICP.Candid.Encodings;
using ICP.Candid.Exceptions;
using ICP.Candid.Models;
using ICP.Candid.Models.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICP.Candid.Models.Types
{
    public abstract class CandidTypeDefinition : IEquatable<CandidTypeDefinition>
    {
        public abstract CandidTypeCode Type { get; }

        public abstract override bool Equals(object? obj);

        public abstract override int GetHashCode();
        public abstract override string ToString();

        public abstract byte[] Encode(CompoundTypeTable compoundTypeTable);

        public bool Equals(CandidTypeDefinition? other)
        {
            return this.Equals(other as object);
        }

        public static bool operator ==(CandidTypeDefinition? def1, CandidTypeDefinition? def2)
        {
            if (object.ReferenceEquals(def1, null))
            {
                return object.ReferenceEquals(def2, null);
            }
            return def1.Equals(def2);
        }

        public static bool operator !=(CandidTypeDefinition? def1, CandidTypeDefinition? def2)
        {
            if (object.ReferenceEquals(def1, null))
            {
                return !object.ReferenceEquals(def2, null);
            }
            return !def1.Equals(def2);
        }

        public static (List<CandidTypeDefinition> Types, FuncMode RequestType) ParseArgs(string value)
        {
            CandidTextTokenHelper helper = CandidTextTokenizer.Tokenize(value);

            helper.CurrentToken.ValidateType(CandidTextTokenType.OpenParenthesis);

            helper.MoveNextOrThrow();

            var types = new List<CandidTypeDefinition>();
            while (helper.CurrentToken.Type != CandidTextTokenType.CloseParenthesis)
            {
                CandidTypeDefinition type = GetType(helper);
                types.Add(type);
            }
            helper.CurrentToken.ValidateType(CandidTextTokenType.CloseParenthesis);
            helper.MoveNext();
            string rawMode = helper.CurrentToken.GetTextValueOrThrow();
            FuncMode mode = rawMode switch
            {
                "query" => FuncMode.Query,
                "oneway"=> FuncMode.Oneway,
                _ => throw new CandidTextParseException()
            };
            return (types, mode);
        }

        private static CandidTypeDefinition GetType(CandidTextTokenHelper helper)
        {
            string? recursiveId = null;
            string type = helper.CurrentToken.GetTextValueOrThrow();

            // Check to see if text is recursive id like `μrec_1.record` vs just the type name `record`
            if (type.StartsWith("μ"))
            {
                recursiveId = type[1..];
                helper.MoveNextOrThrow();
                helper.CurrentToken.ValidateType(CandidTextTokenType.Period); // Period seperates recursive id and type
                helper.MoveNextOrThrow();
                type = helper.CurrentToken.GetTextValueOrThrow();
            }
            helper.MoveNextOrThrow();
            CandidTypeDefinition typeDef = GetType(type, recursiveId, helper);
            if(helper.CurrentToken.Type == CandidTextTokenType.SemiColon)
            {
                helper.MoveNextOrThrow();
            }
            return typeDef;
        }

        private static CandidTypeDefinition GetType(string type, string? recursiveId, CandidTextTokenHelper helper)
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
                        Dictionary<CandidLabel, CandidTypeDefinition> fields = new();
                        helper.CurrentToken.ValidateType(CandidTextTokenType.OpenCurlyBrace);
                        helper.MoveNextOrThrow();
                        uint index = 0;
                        while (helper.CurrentToken.Type != CandidTextTokenType.CloseCurlyBrace)
                        {
                            CandidLabel label;
                            CandidTypeDefinition fieldType;
                            if (helper.NextToken?.Type == CandidTextTokenType.Colon)
                            {
                                // `label`: `type`
                                string rawLabel = helper.CurrentToken.GetTextValueOrThrow();
                                label = CandidLabel.FromName(rawLabel);
                                helper.MoveNextOrThrow();
                                helper.CurrentToken.ValidateType(CandidTextTokenType.Colon);
                                helper.MoveNextOrThrow();
                                fieldType = GetType(helper);
                            }
                            else
                            {
                                // `type` (based on position/index)
                                label = CandidLabel.FromId(index++);
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
                        Dictionary<CandidLabel, CandidTypeDefinition> options = new();
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
                            options.Add(CandidLabel.FromName(label), fieldType);
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
                    return new RecursiveReferenceCandidTypeDefinition(type, () => throw new NotImplementedException()); // TODO
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

    public class CandidLabel : IComparable<CandidLabel>, IComparable, IEquatable<CandidLabel>
    {
        public string? Name { get; }
        public UnboundedUInt Id { get; }

        private CandidLabel(UnboundedUInt id, string? name)
        {
            this.Id = id;
            this.Name = name;
        }

        public CandidLabel(UnboundedUInt id) : this(id, null)
        {

        }


        public bool Equals(CandidLabel? other)
        {
            return this.CompareTo(other) == 0;
        }

        public override bool Equals(object? obj)
        {
            return this.Equals(obj as CandidLabel);
        }

        public int CompareTo(object? obj)
        {
            return this.CompareTo(obj as CandidLabel);
        }

        public int CompareTo(CandidLabel? other)
        {
            if (object.ReferenceEquals(other, null))
            {
                return 1;
            }
            return this.Id.CompareTo(other.Id);
        }
        public static bool operator ==(CandidLabel? l1, CandidLabel? l2)
        {
            if (object.ReferenceEquals(l1, null))
            {
                return object.ReferenceEquals(l2, null);
            }
            return l1.Equals(l2);
        }

        public static bool operator !=(CandidLabel? l1, CandidLabel? l2)
        {
            if (object.ReferenceEquals(l1, null))
            {
                return !object.ReferenceEquals(l2, null);
            }
            return !l1.Equals(l2);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.Id);
        }


        /// <summary>
        /// Hashes the name to get the proper id 
        /// hash(name) = ( Sum_(i=0..k) utf8(name)[i] * 223^(k-i) ) mod 2^32 where k = |utf8(name)|-1
        /// </summary>
        /// <param name="name">Name to hash</param>
        /// <returns>Unsigned 32 byte integer hash</returns>
        public static uint HashName(string name)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(name);
            uint digest = 0;
            foreach (byte b in bytes)
            {
                digest = (digest * 223) + b;
            }
            return digest;
        }

        public static CandidLabel FromName(string name)
        {
            uint id = CandidLabel.HashName(name);

            return new CandidLabel(id, name);
        }

        public static CandidLabel FromId(UnboundedUInt id)
        {
            return new CandidLabel(id, null);
        }

        public static implicit operator CandidLabel(string value)
        {
            return CandidLabel.FromName(value);
        }

        public static implicit operator CandidLabel(UnboundedUInt value)
        {
            return CandidLabel.FromId(value);
        }

        public static implicit operator UnboundedUInt(CandidLabel value)
        {
            return value.Id;
        }

        public override string ToString()
        {
            string value = this.Id.ToString();
            if (this.Name != null)
            {
                value += $"({this.Name})";
            }
            return value;
        }
    }
}