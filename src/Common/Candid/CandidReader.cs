using Common.Models;
using ICP.Common.Candid;
using ICP.Common.Candid.Constants;
using ICP.Common.Encodings;
using ICP.Common.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Common.Candid
{
    public class CandidReader : IDisposable
    {
        private readonly BinaryReader reader;
        public CandidReader(byte[] value)
        {
            this.reader = new BinaryReader(new MemoryStream(value));
        }

        public void ReadMagicNumber()
        {
            byte[] magicNumber = this.reader.ReadBytes(4);
            if (!magicNumber.SequenceEqual(new byte[] { 68, 73, 68, 76 }))
            {
                // TODO
                throw new Exception();
            }
        }

        private DefintionOrReference ReadType()
        {
            UnboundedInt typeCode = this.ReadInt();
            if (typeCode >= 0)
            {
                return DefintionOrReference.Reference((UnboundedUInt)typeCode);
            }
            if (typeCode.TryToInt64(out long typeCodeInt))
            {
                return this.ReadTypeInner(typeCodeInt);
            }
            // TODO
            throw new Exception();
        }

        private DefintionOrReference ReadTypeInner(long typeCodeInt)
        {
            IDLTypeCode type = (IDLTypeCode)typeCodeInt;
            switch (type)
            {
                case IDLTypeCode.Int:
                    return DefintionOrReference.Primitive(new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Int));
                case IDLTypeCode.Int64:
                    return DefintionOrReference.Primitive(new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Int64));
                case IDLTypeCode.Int32:
                    return DefintionOrReference.Primitive(new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Int32));
                case IDLTypeCode.Int16:
                    return DefintionOrReference.Primitive(new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Int16));
                case IDLTypeCode.Int8:
                    return DefintionOrReference.Primitive(new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Int8));
                case IDLTypeCode.Nat:
                    return DefintionOrReference.Primitive(new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Nat));
                case IDLTypeCode.Nat64:
                    return DefintionOrReference.Primitive(new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Nat64));
                case IDLTypeCode.Nat32:
                    return DefintionOrReference.Primitive(new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Nat32));
                case IDLTypeCode.Nat16:
                    return DefintionOrReference.Primitive(new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Nat16));
                case IDLTypeCode.Nat8:
                    return DefintionOrReference.Primitive(new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Nat8));
                case IDLTypeCode.Float32:
                    return DefintionOrReference.Primitive(new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Float32));
                case IDLTypeCode.Float64:
                    return DefintionOrReference.Primitive(new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Float64));
                case IDLTypeCode.Empty:
                    return DefintionOrReference.Primitive(new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Empty));
                case IDLTypeCode.Null:
                    return DefintionOrReference.Primitive(new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Null));
                case IDLTypeCode.Reserved:
                    return DefintionOrReference.Primitive(new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Reserved));
                case IDLTypeCode.Text:
                    return DefintionOrReference.Primitive(new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Text));
                case IDLTypeCode.Principal:
                    return DefintionOrReference.Primitive(new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Principal));
                case IDLTypeCode.Bool:
                    return DefintionOrReference.Primitive(new PrimitiveCandidTypeDefinition(CandidPrimitiveType.Bool));
                case IDLTypeCode.Opt:
                    DefintionOrReference innerType = this.ReadType();
                    return DefintionOrReference.CompoundDefintion(resolver =>
                    {
                        CandidTypeDefinition t = resolver.Resolve(innerType);
                        return new OptCandidTypeDefinition(t);
                    });
                case IDLTypeCode.Record:
                    UnboundedUInt size = this.ReadNat();
                    Dictionary<Label, DefintionOrReference> recordFields = this.ReadRecordInner(size);
                    return DefintionOrReference.CompoundDefintion(resolver =>
                    {
                        Dictionary<Label, CandidTypeDefinition> t = recordFields
                            .ToDictionary(f => f.Key, f => resolver.Resolve(f.Value));
                        return new RecordCandidTypeDefinition(t);
                    });
                case IDLTypeCode.Vector:
                    DefintionOrReference innerVectorType = this.ReadType();
                    return DefintionOrReference.CompoundDefintion(resolver =>
                    {
                        CandidTypeDefinition t = resolver.Resolve(innerVectorType);
                        return new VectorCandidTypeDefinition(t);
                    });
                case IDLTypeCode.Variant:
                    UnboundedUInt length = this.ReadNat();
                    var variantOptions = new Dictionary<Label, DefintionOrReference>();
                    while (length > 0)
                    {
                        length--;
                        UnboundedUInt tag = this.ReadNat();
                        DefintionOrReference option = this.ReadType();
                        variantOptions.Add(tag, option);
                    }
                    return DefintionOrReference.CompoundDefintion(resolver =>
                    {
                        Dictionary<Label, CandidTypeDefinition> map = variantOptions
                            .ToDictionary(o => o.Key, o => resolver.Resolve(o.Value));
                        return new VariantCandidTypeDefinition(map);
                    });
                case IDLTypeCode.Func:
                    List<DefintionOrReference> argTypes = this.ReadVectorInner();
                    List<DefintionOrReference> returnTypes = this.ReadVectorInner();
                    List<byte> modes = this.ReadVectorInner(() => this.ReadByte());
                    return DefintionOrReference.CompoundDefintion(resolver =>
                    {
                        List<CandidTypeDefinition> a = argTypes
                            .Select(a => resolver.Resolve(a))
                            .ToList();
                        List<CandidTypeDefinition> r = returnTypes
                            .Select(a => resolver.Resolve(a))
                            .ToList();
                        List<FuncMode> m = modes
                            .Select(m => (FuncMode)m)
                            .ToList();
                        return new FuncCandidTypeDefinition(m, a, r);
                    });
                case IDLTypeCode.Service:
                    List<(string Name, DefintionOrReference Type)> methods = this.ReadVectorInner(() =>
                    {
                        string name = this.ReadText();
                        DefintionOrReference type = this.ReadType();
                        return (name, type);
                    });
                    return DefintionOrReference.CompoundDefintion(resolver =>
                    {
                        IReadOnlyDictionary<string, FuncCandidTypeDefinition> m = methods
                            .ToDictionary(m => m.Name, m =>
                            {
                                var type = resolver.Resolve(m.Type);
                                if (type is FuncCandidTypeDefinition f)
                                {
                                    return f;
                                }
                                // TODO 
                                throw new Exception();
                            });
                        return new ServiceCandidTypeDefinition(m);
                    });
                default:
                    throw new NotImplementedException($"Invalid type code '{typeCodeInt}'");
            };
        }

        private string ReadText()
        {
            UnboundedUInt length = this.ReadNat();
            return this.ReadTextInner(length);
        }

        private string ReadTextInner(UnboundedUInt nameLenth)
        {
            var bytes = new List<byte>();
            while (nameLenth > 0)
            {
                bytes.Add(this.ReadByte());
                nameLenth--;
            }
            return Encoding.UTF8.GetString(bytes.ToArray());
        }

        private byte ReadByte()
        {
            return this.reader.ReadByte();
        }

        private Dictionary<Label, DefintionOrReference> ReadRecordInner(UnboundedUInt size)
        {
            var map = new Dictionary<Label, DefintionOrReference>();
            while (size > 0)
            {
                UnboundedUInt label = this.ReadNat();
                DefintionOrReference type = this.ReadType();
                map.Add(Label.FromId(label), type);
                size--;
            }
            return map;
        }

        private List<DefintionOrReference> ReadVectorInner()
        {
            return this.ReadVectorInner(() => this.ReadType());
        }

        private List<T> ReadVectorInner<T>(Func<T> func)
        {
            UnboundedUInt length = this.ReadNat();
            var items = new List<T>();
            while (length > 0)
            {
                length--;
                T type = func();
                items.Add(type);
            }
            return items;
        }

        private UnboundedUInt ReadNat()
        {
            return LEB128.DecodeUnsigned(this.reader.BaseStream);
        }
        private UnboundedInt ReadInt()
        {
            return LEB128.DecodeSigned(this.reader.BaseStream);
        }
        private CandidValue ReadValue(CandidTypeDefinition typeDef)
        {
            return typeDef switch
            {
                PrimitiveCandidTypeDefinition p => this.ReadPrimitive(p.PrimitiveType),
                OptCandidTypeDefinition o => this.ReadOptValue(o.Value),
                VectorCandidTypeDefinition ve => this.ReadVectorValue(ve.Value),
                RecordCandidTypeDefinition r => this.ReadRecordValue(r.Fields.ToDictionary(f => f.Key, f => f.Value)),
                VariantCandidTypeDefinition va => this.ReadVariantValue(va.Fields.ToDictionary(f => f.Key, f => f.Value)),
                ServiceCandidTypeDefinition s => this.ReadServiceValue(s.Methods.ToDictionary(f => f.Key, f => f.Value)),
                FuncCandidTypeDefinition f => this.ReadFuncValue(f.ArgTypes, f.ReturnTypes, f.Modes),
                _ => throw new NotImplementedException(),
            };
        }

        private CandidPrimitive ReadPrimitive(CandidPrimitiveType type)
        {
            return type switch
            {
                CandidPrimitiveType.Text => CandidPrimitive.Text(this.ReadText()),
                CandidPrimitiveType.Nat => CandidPrimitive.Nat(this.ReadNat()),
                CandidPrimitiveType.Nat8 => CandidPrimitive.Nat8(this.ReadByte()),
                CandidPrimitiveType.Nat16 => CandidPrimitive.Nat16(this.reader.ReadUInt16()),
                CandidPrimitiveType.Nat32 => CandidPrimitive.Nat32(this.reader.ReadUInt32()),
                CandidPrimitiveType.Nat64 => CandidPrimitive.Nat64(this.reader.ReadUInt64()),
                CandidPrimitiveType.Int => CandidPrimitive.Int(this.ReadInt()),
                CandidPrimitiveType.Int8 => CandidPrimitive.Int8(this.ReadInt8()),
                CandidPrimitiveType.Int16 => CandidPrimitive.Int16(this.ReadInt16()),
                CandidPrimitiveType.Int32 => CandidPrimitive.Int32(this.ReadInt32()),
                CandidPrimitiveType.Int64 => CandidPrimitive.Int64(this.ReadInt64()),
                CandidPrimitiveType.Float32 => CandidPrimitive.Float32(BitConverter.ToSingle(this.reader.ReadBytes(4))),
                CandidPrimitiveType.Float64 => CandidPrimitive.Float64(BitConverter.ToDouble(this.reader.ReadBytes(8))),
                CandidPrimitiveType.Bool => CandidPrimitive.Bool(this.reader.ReadByte() > 0),
                CandidPrimitiveType.Principal => throw new NotImplementedException(),//CandidPrimitive.Pricipal(PrincipalId.FromRaw(this.Rea)),
                CandidPrimitiveType.Reserved => CandidPrimitive.Reserved(),
                CandidPrimitiveType.Empty => CandidPrimitive.Empty(),
                CandidPrimitiveType.Null => CandidPrimitive.Null(),
                _ => throw new NotImplementedException(),
            };
        }

        private sbyte ReadInt8()
        {
            return this.reader.ReadSByte();
        }

        private short ReadInt16()
        {
            return this.reader.ReadInt16();
        }

        private int ReadInt32()
        {
            return this.reader.ReadInt32();
        }

        private long ReadInt64()
        {
            return this.reader.ReadInt64();
        }

        private CandidFunc ReadFuncValue(IEnumerable<CandidTypeDefinition> argTypes, IEnumerable<CandidTypeDefinition> returnTypes, IEnumerable<FuncMode> modes)
        {
            // TODO
            throw new NotImplementedException();
        }

        private CandidService ReadServiceValue(Dictionary<string, FuncCandidTypeDefinition> methods)
        {
            // TODO
            throw new NotImplementedException();
        }

        private CandidVariant ReadVariantValue(Dictionary<Label, CandidTypeDefinition> options)
        {
            UnboundedUInt index = this.ReadNat();
            if (!index.TryToUInt64(out ulong i) || i > int.MaxValue)
            {
                //TODO
                throw new Exception();
            }
            (Label tag, CandidTypeDefinition typeDef) = options
                .OrderBy(f => f.Key)
                .Select(f => (f.Key, f.Value))
                .ElementAt((int)i);
            CandidValue value = this.ReadValue(typeDef);
            return new CandidVariant(tag, value);
        }

        private CandidRecord ReadRecordValue(Dictionary<Label, CandidTypeDefinition> fields)
        {
            Dictionary<Label, CandidValue> fieldValues = fields
                // TODO are all the fields always there?
                .OrderBy(f => f.Key.Id)
                .Select(f => (f.Key, this.ReadValue(f.Value)))
                .ToDictionary(f => f.Key, f => f.Item2);
            return new CandidRecord(fieldValues);
        }

        private CandidVector ReadVectorValue(CandidTypeDefinition innerTypeDef)
        {
            List<CandidValue> values = this.ReadVectorInner(() =>
            {
                return this.ReadValue(innerTypeDef);
            });
            return new CandidVector(values.ToArray());
        }

        private CandidOptional ReadOptValue(CandidTypeDefinition innerTypeDef)
        {
            byte isSet = this.ReadByte();
            CandidValue? innerValue = isSet switch
            {
                0 => null,
                1 => this.ReadValue(innerTypeDef),
                _ => throw new Exception() // TODO
            };
            return new CandidOptional(innerValue);
        }

        public void Dispose()
        {
            this.reader.Dispose();
        }


        public static List<(CandidValue, CandidTypeDefinition)> Read(byte[] value)
        {
            if (value.Length < 5)
            {
                // TODO
                throw new Exception();
            }
            var reader = new CandidReader(value);
            reader.ReadMagicNumber();
            List<DefintionOrReference> compoundDefOrRefs = reader.ReadVectorInner(() =>
            {
                var t = reader.ReadType();
                if (t.Type != DefintionOrReferenceType.Compound)
                {
                    // TODO
                    throw new Exception();
                }
                return t;
            });
            DefinitionResolver resolver = DefinitionResolver.Build(compoundDefOrRefs);


            List<CandidTypeDefinition> types = reader.ReadVectorInner(() =>
            {
                DefintionOrReference t = reader.ReadType();
                return resolver.Resolve(t);
            });
            return types
                .Select(t => (reader.ReadValue(t), t))
                .ToList();
        }

        private class DefintionOrReference
        {
            public DefintionOrReferenceType Type { get; }
            public PrimitiveCandidTypeDefinition? Definition { get; }
            public Func<DefinitionResolver, CompoundCandidTypeDefinition>? DefinitionFunc { get; }
            public UnboundedUInt? ReferenceIndex { get; }
            public DefintionOrReference(
                DefintionOrReferenceType type,
                PrimitiveCandidTypeDefinition? definition,
                Func<DefinitionResolver, CompoundCandidTypeDefinition>? definitionFunc,
                UnboundedUInt? referenceIndex)
            {
                this.Type = type;
                this.Definition = definition;
                this.DefinitionFunc = definitionFunc;
                this.ReferenceIndex = referenceIndex;
            }

            public static DefintionOrReference Reference(UnboundedUInt index)
            {
                return new DefintionOrReference(DefintionOrReferenceType.Reference, null, null, index);
            }

            public static DefintionOrReference CompoundDefintion(Func<DefinitionResolver, CompoundCandidTypeDefinition> definitionFunc)
            {
                return new DefintionOrReference(DefintionOrReferenceType.Compound, null, definitionFunc, null);
            }

            public static DefintionOrReference Primitive(PrimitiveCandidTypeDefinition definition)
            {
                return new DefintionOrReference(DefintionOrReferenceType.Primitive, definition, null, null);
            }

            public override string ToString()
            {
                return this.Type switch
                {
                    DefintionOrReferenceType.Reference => $"Ref({this.ReferenceIndex!})",
                    DefintionOrReferenceType.Compound => "Compound",
                    DefintionOrReferenceType.Primitive => $"Primitive({this.Definition!.Type})",
                    _ => throw new NotImplementedException(),
                };
            }
        }

        private enum DefintionOrReferenceType
        {
            Reference,
            Compound,
            Primitive
        }

        private class DefinitionResolver
        {
            public List<DefintionOrReference> Types { get; }
            public DefinitionResolver(List<DefintionOrReference> types)
            {
                this.Types = types;
            }
            public CandidTypeDefinition Resolve(DefintionOrReference defOrRef)
            {
                return defOrRef.Type switch
                {
                    DefintionOrReferenceType.Reference => this.ResolveByIndex(defOrRef.ReferenceIndex!),
                    DefintionOrReferenceType.Compound => defOrRef.DefinitionFunc!(this),
                    DefintionOrReferenceType.Primitive => defOrRef.Definition!,
                    _ => throw new NotImplementedException(),
                };
            }

            public CandidTypeDefinition ResolveByIndex(UnboundedUInt referenceIndex)
            {
                int index = (int)referenceIndex;
                if (this.Types.Count <= index)
                {
                    // TODO 
                    throw new Exception();
                }
                return this.Resolve(this.Types[index]);
            }

            public static DefinitionResolver Build(List<DefintionOrReference> defOrRefList)
            {
                return new DefinitionResolver(defOrRefList);
            }

            public static List<CandidTypeDefinition> Resolve(List<DefintionOrReference> defOrRefList)
            {
                var resolver = new DefinitionResolver(defOrRefList);
                return defOrRefList
                    .Select(d => resolver.Resolve(d))
                    .ToList();
            }
        }
    }
}
