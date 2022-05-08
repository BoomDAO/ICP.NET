using Common.Models;
using ICP.Common.Candid;
using ICP.Common.Candid.Constants;
using ICP.Common.Encodings;
using ICP.Common.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public static CandidArg Read(byte[] value)
        {
            if (value.Length < 5)
            {
                // TODO
                throw new Exception();
            }
            var reader = new CandidReader(value);
            reader.ReadMagicNumber();

            //Read type table (all compound type definitions)
            List<Func<DefinitionResolver, CompoundCandidTypeDefinition>> compoundDefOrRefs = reader.ReadVectorInner(() =>
            {
                DefintionOrReference t = reader.ReadType();
                if (t.Type != DefintionOrReferenceType.Compound)
                {
                    // TODO
                    throw new Exception();
                }
                return t.DefinitionFunc!;
            });
            DefinitionResolver resolver = new DefinitionResolver(compoundDefOrRefs);

            try
            {
                // Get all arg types
                List<CandidTypeDefinition> types = reader.ReadVectorInner(() =>
                {
                    DefintionOrReference t = reader.ReadType();
                    return resolver.Resolve(t);
                });
                Dictionary<string, CompoundCandidTypeDefinition> recursiveTypes = new();

                // Get an arg value for each type
                List<(CandidValue Value, CandidTypeDefinition Type)> args = types
                    .Select(t => (reader.ReadValue(t, recursiveTypes), t))
                    .ToList();

                // Remaining bytes are opaque reference bytes
                byte[] opaqueReferenceBytes = value
                    .AsMemory()
                    .Slice((int)reader.reader.BaseStream.Position)
                    .ToArray();
                return CandidArg.FromCandid(args, opaqueReferenceBytes);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to parse the candid arg. Here is the trace that it parsed so far:\n{resolver.Tracer}", ex);
            }
        }

        private void ReadMagicNumber()
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
                return DefintionOrReference.Reference((uint)typeCode);
            }
            return this.ReadTypeInner((int)typeCode);
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
                        resolver.Tracer.StartCompound("Opt");
                        CandidTypeDefinition t = resolver.Resolve(innerType);
                        resolver.Tracer.EndCompound("Opt");
                        return new OptCandidTypeDefinition(t);
                    });
                case IDLTypeCode.Record:
                    UnboundedUInt size = this.ReadNat();
                    Dictionary<Label, DefintionOrReference> recordFields = this.ReadRecordInner(size);
                    return DefintionOrReference.CompoundDefintion(resolver =>
                    {
                        var map = new Dictionary<Label, CandidTypeDefinition>();
                        resolver.Tracer.StartCompound("Record");
                        foreach ((Label field, DefintionOrReference defOrRef) in recordFields)
                        {
                            resolver.Tracer.Field(field);
                            CandidTypeDefinition type = resolver.Resolve(defOrRef);
                            map.Add(field, type);
                        }
                        resolver.Tracer.EndCompound("Record");
                        return new RecordCandidTypeDefinition(map);
                    });
                case IDLTypeCode.Vector:
                    DefintionOrReference innerVectorType = this.ReadType();
                    return DefintionOrReference.CompoundDefintion(resolver =>
                    {
                        resolver.Tracer.StartCompound("Vector");
                        CandidTypeDefinition t = resolver.Resolve(innerVectorType);
                        resolver.Tracer.EndCompound("Vector");
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
                        var map = new Dictionary<Label, CandidTypeDefinition>();
                        resolver.Tracer.StartCompound("Variant");
                        foreach ((Label field, DefintionOrReference defOrRef) in variantOptions)
                        {
                            resolver.Tracer.Field(field);
                            CandidTypeDefinition type = resolver.Resolve(defOrRef);
                            map.Add(field, type);
                        }
                        resolver.Tracer.StartCompound("Variant");
                        return new VariantCandidTypeDefinition(map);
                    });
                case IDLTypeCode.Func:
                    List<DefintionOrReference> argTypes = this.ReadVectorInner();
                    List<DefintionOrReference> returnTypes = this.ReadVectorInner();
                    List<byte> modes = this.ReadVectorInner(() => this.ReadByte());
                    return DefintionOrReference.CompoundDefintion(resolver =>
                    {
                        resolver.Tracer.StartCompound("Func");
                        List<CandidTypeDefinition> a = argTypes
                            .Select(a => resolver.Resolve(a))
                            .ToList();
                        List<CandidTypeDefinition> r = returnTypes
                            .Select(a => resolver.Resolve(a))
                            .ToList();
                        List<FuncMode> m = modes
                            .Select(m =>
                            {
                                resolver.Tracer.Primitive(CandidPrimitiveType.Int8);
                                return (FuncMode)m;
                            })
                            .ToList();
                        resolver.Tracer.EndCompound("Func");
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
                        resolver.Tracer.StartCompound("Service");

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
                        resolver.Tracer.EndCompound("Service");
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
        private CandidValue ReadValue(CandidTypeDefinition typeDef, Dictionary<string, CompoundCandidTypeDefinition> recursiveTypes)
        {
            return typeDef switch
            {
                PrimitiveCandidTypeDefinition p => this.ReadPrimitive(p.PrimitiveType),
                RecursiveReferenceCandidTypeDefinition r => this.ReadRecursiveValue(r.RecursiveId, recursiveTypes),
                CompoundCandidTypeDefinition c => this.ReadCompoundValue(c, recursiveTypes),
                _ => throw new NotImplementedException(),
            };
        }

        private CandidValue ReadCompoundValue(CompoundCandidTypeDefinition c, Dictionary<string, CompoundCandidTypeDefinition> recursiveTypes)
        {
            if (c.RecursiveId != null)
            {
                recursiveTypes[c.RecursiveId] = c;
            }
            return c switch
            {
                OptCandidTypeDefinition o => this.ReadOptValue(o.Value, recursiveTypes),
                VectorCandidTypeDefinition ve => this.ReadVectorValue(ve.Value, recursiveTypes),
                RecordCandidTypeDefinition r => this.ReadRecordValue(r.Fields.ToDictionary(f => f.Key, f => f.Value), recursiveTypes),
                VariantCandidTypeDefinition va => this.ReadVariantValue(va.Fields.ToDictionary(f => f.Key, f => f.Value), recursiveTypes),
                ServiceCandidTypeDefinition s => this.ReadServiceValue(),
                FuncCandidTypeDefinition f => this.ReadFuncValue(),
                _ => throw new NotImplementedException(),
            };
        }

        private CandidValue ReadRecursiveValue(string recursiveId, Dictionary<string, CompoundCandidTypeDefinition> recursiveTypes)
        {
            CompoundCandidTypeDefinition? typeDef = recursiveTypes.GetValueOrDefault(recursiveId);
            if (typeDef == null)
            {
                // TODO
                throw new Exception();
            }
            return this.ReadValue(typeDef, recursiveTypes);
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
                CandidPrimitiveType.Principal => CandidPrimitive.Pricipal(this.ReadPrincipal()),
                CandidPrimitiveType.Reserved => CandidPrimitive.Reserved(),
                CandidPrimitiveType.Empty => CandidPrimitive.Empty(),
                CandidPrimitiveType.Null => CandidPrimitive.Null(),
                _ => throw new NotImplementedException(),
            };
        }

        private PrincipalId? ReadPrincipal()
        {
            bool isRef = !this.ReadBool();
            if (isRef)
            {
                // Opaque reference
                return null;
            }
            else
            {
                List<byte> bytes = this.ReadVectorInner(() => this.ReadByte());
                return PrincipalId.FromRaw(bytes.ToArray());
            }
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

        private bool ReadBool()
        {
            byte b = this.ReadByte();
            if (b == 0)
            {
                return false;
            }
            if (b == 1)
            {
                return true;
            }
            // TODO 
            throw new Exception();
        }

        private CandidValue ReadFuncValue()
        {
            bool isRef = !this.ReadBool();
            if (isRef)
            {
                return CandidFunc.OpaqueReference();
            }
            else
            {
                CandidService service = this.ReadServiceValue();
                string method = this.ReadText();
                return CandidFunc.TrasparentReference(service, method);
            }
        }

        private CandidService ReadServiceValue()
        {
            bool isRef = !this.ReadBool();
            if (isRef)
            {
                return CandidService.OpaqueReference();
            }
            else
            {
                PrincipalId? principalId = this.ReadPrincipal();
                return CandidService.TraparentReference(principalId);
            }
        }

        private CandidVariant ReadVariantValue(Dictionary<Label, CandidTypeDefinition> options, Dictionary<string, CompoundCandidTypeDefinition> recursiveTypes)
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
            CandidValue value = this.ReadValue(typeDef, recursiveTypes);
            return new CandidVariant(tag, value);
        }

        private CandidRecord ReadRecordValue(Dictionary<Label, CandidTypeDefinition> fields, Dictionary<string, CompoundCandidTypeDefinition> recursiveTypes)
        {
            Dictionary<Label, CandidValue> fieldValues = fields
                // TODO are all the fields always there?
                .OrderBy(f => f.Key.Id)
                .Select(f => (f.Key, this.ReadValue(f.Value, recursiveTypes)))
                .ToDictionary(f => f.Key, f => f.Item2);
            return new CandidRecord(fieldValues);
        }

        private CandidVector ReadVectorValue(CandidTypeDefinition innerTypeDef, Dictionary<string, CompoundCandidTypeDefinition> recursiveTypes)
        {
            List<CandidValue> values = this.ReadVectorInner(() =>
            {
                return this.ReadValue(innerTypeDef, recursiveTypes);
            });
            return new CandidVector(values.ToArray());
        }

        private CandidOptional ReadOptValue(CandidTypeDefinition innerTypeDef, Dictionary<string, CompoundCandidTypeDefinition> recursiveTypes)
        {
            byte isSet = this.ReadByte();
            CandidValue? innerValue = isSet switch
            {
                0 => null,
                1 => this.ReadValue(innerTypeDef, recursiveTypes),
                _ => throw new Exception() // TODO
            };
            return new CandidOptional(innerValue);
        }

        public void Dispose()
        {
            this.reader.Dispose();
        }



        private class DefintionOrReference
        {
            public DefintionOrReferenceType Type { get; }
            public PrimitiveCandidTypeDefinition? Definition { get; }
            public Func<DefinitionResolver, CompoundCandidTypeDefinition>? DefinitionFunc { get; }
            public uint? ReferenceIndex { get; }
            public DefintionOrReference(
                DefintionOrReferenceType type,
                PrimitiveCandidTypeDefinition? definition,
                Func<DefinitionResolver, CompoundCandidTypeDefinition>? definitionFunc,
                uint? referenceIndex)
            {
                this.Type = type;
                this.Definition = definition;
                this.DefinitionFunc = definitionFunc;
                this.ReferenceIndex = referenceIndex;
            }

            public static DefintionOrReference Reference(uint index)
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
            private List<TypeInfo> Types { get; }
            public CandidDebugTracer Tracer { get; }
            private int referenceCount = 0;


            public DefinitionResolver(List<Func<DefinitionResolver, CompoundCandidTypeDefinition>> types)
            {
                this.Types = types.Select(t => new TypeInfo(t)).ToList();
                this.Tracer = new CandidDebugTracer();
            }

            public CandidTypeDefinition Resolve(DefintionOrReference defOrRef)
            {
                this.Tracer.Indent();
                try
                {
                    switch (defOrRef.Type)
                    {
                        case DefintionOrReferenceType.Reference:
                            TypeInfo typeInfo = this.Types[(int)defOrRef.ReferenceIndex!];

                            if (!typeInfo.ResolvingOrResolved)
                            {
                                // If not resolved, try to resolve
                                typeInfo.ResolvingOrResolved = true;
                                typeInfo.ResolvedType = typeInfo.ResolveFunc(this);
                                if (typeInfo.RecursiveId != null)
                                {
                                    // If there was a recursive reference, stamp the resolved (parent) type to have the id
                                    typeInfo.ResolvedType.RecursiveId = typeInfo.RecursiveId;
                                }
                                return typeInfo.ResolvedType;
                            }
                            if (typeInfo.ResolvedType != null)
                            {
                                // If already resolved, use it
                                return typeInfo.ResolvedType;
                            }
                            typeInfo.RecursiveId = $"rec_{++this.referenceCount}";
                            // If neither, then it is 'resolving', meaning it is a recursive type
                            this.Tracer.RecursiveReference(typeInfo.RecursiveId);
                            return new RecursiveReferenceCandidTypeDefinition(typeInfo.RecursiveId);
                        case DefintionOrReferenceType.Compound:
                            return defOrRef.DefinitionFunc!(this);
                        case DefintionOrReferenceType.Primitive:
                            this.Tracer.Primitive(defOrRef.Definition!.PrimitiveType);
                            return defOrRef.Definition!;
                        default:
                            throw new NotImplementedException();
                    }
                }
                finally
                {
                    this.Tracer.Unindent();
                }
            }
            public class TypeInfo
            {
                public Func<DefinitionResolver, CompoundCandidTypeDefinition> ResolveFunc { get; }
                public CompoundCandidTypeDefinition? ResolvedType { get; set; }
                public bool ResolvingOrResolved { get; set; }
                public string? RecursiveId { get; set; }

                public TypeInfo(Func<DefinitionResolver, CompoundCandidTypeDefinition> resolveFunc)
                {
                    this.ResolveFunc = resolveFunc ?? throw new ArgumentNullException(nameof(resolveFunc));
                    this.ResolvedType = null;
                    this.ResolvingOrResolved = false;
                    this.RecursiveId = null;
                }
            }
        }

        private class CandidDebugTracer
        {
            private StringBuilder stringBuilder = new StringBuilder();
            private int indentLevel = 0;

            public void StartCompound(string type)
            {
                this.indentLevel++;
                this.AppendIndent();
                this.stringBuilder.Append(type);
                this.stringBuilder.AppendLine(" Start");
                this.indentLevel++;
            }

            public void EndCompound(string type)
            {
                this.indentLevel--;
                this.AppendIndent();
                this.stringBuilder.Append(type);
                this.stringBuilder.AppendLine(" End");
                this.indentLevel--;
            }

            public void Primitive(CandidPrimitiveType type)
            {
                this.indentLevel++;
                this.AppendIndent();
                this.stringBuilder.AppendLine(type.ToString());
                this.indentLevel--;
            }

            public void RecursiveReference(string recursiveId)
            {
                this.indentLevel++;
                this.AppendIndent();
                this.stringBuilder.AppendLine("rec " + recursiveId);
                this.indentLevel--;
            }

            public void Field(Label field)
            {
                this.indentLevel++;
                this.AppendIndent();
                this.stringBuilder.Append("Field:");
                this.stringBuilder.AppendLine(field.ToString());
                this.indentLevel--;
            }

            public void Indent()
            {
                this.indentLevel++;
            }

            public void Unindent()
            {
                this.indentLevel--;
            }

            private void AppendIndent()
            {
                for (int i = 0; i < this.indentLevel; i++)
                {
                    this.stringBuilder.Append('\t');
                }
            }

            public override string ToString()
            {
                return this.stringBuilder.ToString();
            }
        }
    }
}
