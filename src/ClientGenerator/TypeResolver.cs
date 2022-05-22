using System;
using System.Collections.Generic;
using System.Text;

namespace ICP.ClientGenerator
{
    internal class TypeResolver
    {





        //public static List<(string FileName, string Source)> GenerateSourceCode(CandidServiceFile serviceFile, string didFileName)
        //{
        //    var helper = new SourceGenerationHelper();
        //    Dictionary<CandidId, TypeStringResult> unprocessedDeclaredTypes = serviceFile.DeclaredTypes
        //           .ToDictionary(t => t.Key, t => helper.GetTypeString(t.Value));
        //    Dictionary<string, string> aliases = new();
        //    Dictionary<CandidId, TypeStringResult.CreateTypeInfo> typesToCreate = new();

        //    var generatedFiles = new List<(string FileName, string Source)>();
        //    while (unprocessedDeclaredTypes.Any())
        //    {
        //        foreach (CandidId id in unprocessedDeclaredTypes.Select(d => d.Key))
        //        {
        //            bool processed;
        //            TypeStringResult result = unprocessedDeclaredTypes[id];
        //            switch (result.Type)
        //            {
        //                case TypeStringResultType.Null:
        //                    // TODO
        //                    throw new Exception();
        //                case TypeStringResultType.AlreadyCreated:
        //                    {
        //                        TypeStringResult.AlreadyCreatedInfo info = result.AsAlreadyCreated();

        //                        aliases.Add(id.ToString(), info.GenerateName(prefix));
        //                        processed = true;
        //                        break;
        //                    }
        //                case TypeStringResultType.CreateType:
        //                    {
        //                        TypeStringResult.CreateTypeInfo info = result.AsCreateType();
        //                        if (info.Type is CandidServiceType s)
        //                        {
        //                            string typeName = info.GenerateTypeName(id.ToString());
        //                            string clientSource = helper.GenerateFromService(typeName, s);
        //                            generatedFiles.Add(("Type_" + typeName, clientSource));
        //                        }
        //                        else
        //                        {
        //                            string typeName = info.GenerateTypeName(id.ToString());
        //                            string typeSource = helper.GenerateDeclaredType(id, info);
        //                            generatedFiles.Add(("Type_" + typeName, typeSource));
        //                        }
        //                        processed = true;
        //                        break;
        //                    }
        //                default:
        //                    throw new NotImplementedException();
        //            }
        //            if (processed)
        //            {
        //                unprocessedDeclaredTypes.Remove(id);
        //            }
        //        }
        //    }

        //    if (aliases.Any())
        //    {
        //        string aliasSource = helper.GenerateAliasFile(aliases);
        //        generatedFiles.Add(("Aliases", aliasSource));
        //    }
        //    // Only write client once
        //    if (serviceFile.ServiceReferenceId == null)
        //    {
        //        string source = helper.GenerateFromService(didFileName, serviceFile.Service);
        //        generatedFiles.Add(("Client_" + didFileName, source));
        //    }
        //    return generatedFiles;
        //}


        //private string GenerateAliasFile(Dictionary<string, string> aliases)
        //{
        //    IndentedStringBuilder builder = new();
        //    foreach ((string id, string aliasedType) in aliases)
        //    {
        //        // Convert system types to full namespace
        //        string fullTypeName = aliasedType switch
        //        {
        //            "string" => typeof(string).FullName,
        //            "byte" => typeof(byte).FullName,
        //            "ushort" => typeof(ushort).FullName,
        //            "uint" => typeof(uint).FullName,
        //            "ulong" => typeof(ulong).FullName,
        //            "sbyte" => typeof(sbyte).FullName,
        //            "short" => typeof(short).FullName,
        //            "int" => typeof(int).FullName,
        //            "long" => typeof(long).FullName,
        //            "float" => typeof(float).FullName,
        //            "double" => typeof(double).FullName,
        //            "bool" => typeof(bool).FullName,
        //            _ => aliasedType
        //        };
        //        builder.AppendLine($"global using {id} = {fullTypeName};");
        //    }
        //    return builder.ToString();
        //}

        //private string GenerateDeclaredType(CandidId id, TypeStringResult.CreateTypeInfo info)
        //{
        //    var builder = new IndentedStringBuilder();
        //    this.WriteUsings(builder);
        //    this.WriteNamespace(builder, "EdjCase.ICP.Clients.Models", () =>
        //    {
        //        this.WriteType(builder, id.ToString(), info);
        //    });
        //    return builder.ToString();
        //}

        //private void WriteUsings(IndentedStringBuilder builder)
        //{
        //    // TODO needed?
        //    //builder.AppendLine("using EdjCase.ICP.Candid;");
        //    //builder.AppendLine("using EdjCase.ICP.Candid.Models;");
        //    //builder.AppendLine("using EdjCase.ICP.Agent;");
        //}

        //private void WriteType(IndentedStringBuilder builder, string name, TypeStringResult result, bool skipAlreadyCreated = false)
        //{
        //    switch (result.Type)
        //    {
        //        case TypeStringResultType.Null:
        //            break; // Skip
        //        case TypeStringResultType.AlreadyCreated:
        //            if (!skipAlreadyCreated)
        //            {
        //                string fullTypeName = this.ResolveFullTypeName(result.AsAlreadyCreated().Name);
        //                builder.AppendLine(fullTypeName);
        //            }
        //            break;
        //        case TypeStringResultType.CreateType:
        //            {
        //                TypeStringResult.CreateTypeInfo info = result.AsCreateType();
        //                this.WriteType(builder, name, info);
        //                break;
        //            }
        //        default:
        //            break;
        //    }
        //}

        //private void WriteType(IndentedStringBuilder builder, string name, TypeStringResult.CreateTypeInfo info)
        //{
        //    switch (info.Type)
        //    {
        //        case CandidRecordType record:
        //            this.WriteRecord(builder, name, record);
        //            break;
        //        case CandidVariantType v:
        //            this.WriteVariant(builder, name, v);
        //            break;
        //        case CandidServiceType s:
        //            this.WriteService(builder, name, s);
        //            break;
        //        case CandidFuncType f:
        //            this.WriteFunc(builder, name, f);
        //            break;
        //        default:
        //            throw new NotImplementedException();
        //    };
        //}


        //private string GetEnumValue(CandidTag tag)
        //{
        //    return tag.Name ?? "O" + tag.Id;
        //}

        //private string GenerateFromService(string name, CandidServiceType service)
        //{
        //    var builder = new IndentedStringBuilder();
        //    this.WriteUsings(builder);
        //    // TODO needed?
        //    //builder.AppendLine("using EdjCase.ICP.Clients.Models;");
        //    this.WriteNamespace(builder, "EdjCase.ICP.Clients", () =>
        //    {
        //        this.WriteService(builder, name, service);
        //    });
        //    return builder.ToString() ?? "";
        //}



        //private TypeStringResult GetTypeString(CandidType type)
        //{
        //    switch (type)
        //    {
        //        case CandidPrimitiveType prim:
        //            {
        //                string? value = this.GetPrimitiveString(prim.PrimitiveType);
        //                if (value == null)
        //                {
        //                    return TypeStringResult.Null();
        //                }
        //                this.resolvedTypes.Add(value, value); // Add primitive as itself
        //                var info = new TypeStringResult.AlreadyCreatedInfo(value);
        //                return TypeStringResult.AlreadyCreated(info);
        //            }
        //        case CandidReferenceType r:
        //            {
        //                var info = new TypeStringResult.AlreadyCreatedInfo(r.Id.ToString());
        //                // Dont resolve type yet. Wait till all others are resolved
        //                return TypeStringResult.AlreadyCreated(info);
        //            }
        //        case CandidVectorType vec:
        //            {
        //                TypeStringResult childTypeResult = this.GetTypeString(vec.Value);
        //                string template = "System.Collection.Generic.List<{0}>";
        //                return this.CreateTemplate(childTypeResult, template);
        //            }
        //        case CandidOptType opt:
        //            {
        //                TypeStringResult childTypeResult = this.GetTypeString(opt.Value);
        //                string template = "{0}?";
        //                return this.CreateTemplate(childTypeResult, template);
        //            }
        //        case CandidRecordType:
        //        case CandidVariantType:
        //        case CandidServiceType:
        //        case CandidFuncType:
        //            return TypeStringResult.CreateType(new TypeStringResult.CreateTypeInfo(type, null));
        //        default:
        //            throw new NotImplementedException();
        //    }
        //}

        //private TypeStringResult CreateTemplate(TypeStringResult result, string template)
        //{
        //    switch (result.Type)
        //    {
        //        case TypeStringResultType.Null:
        //            //TODO 
        //            throw new Exception();
        //        case TypeStringResultType.AlreadyCreated:
        //            {
        //                string typeName = result.AsAlreadyCreated().Name;
        //                string fullTypeName = this.ResolveFullTypeName(typeName);
        //                this.resolvedTypes.Add(typeName, fullTypeName);

        //                string name = string.Format(template, fullTypeName);
        //                var info = new TypeStringResult.AlreadyCreatedInfo(name);
        //                return TypeStringResult.AlreadyCreated(info);
        //            }
        //        case TypeStringResultType.CreateType:
        //            {
        //                var info = new TypeStringResult.CreateTypeInfo(result.AsCreateType().Type, template);
        //                return TypeStringResult.CreateType(info);
        //            }
        //        default:
        //            throw new NotImplementedException();
        //    }
        //}

        //private string ResolveFullTypeName(TypeStringResult result)
        //{

        //}
        //private string ResolveFullTypeName(string name)
        //{
        //    if(this.resolvedTypes.TryGetValue(name, out string fullName))
        //    {
        //        return fullName;
        //    }
        //    // TODO
        //    throw new Exception();
        //}

        //private string? GetPrimitiveString(PrimitiveType type)
        //{
        //    return type switch
        //    {
        //        PrimitiveType.Text => "string",
        //        PrimitiveType.Nat => typeof(UnboundedUInt).FullName,
        //        PrimitiveType.Nat8 => "byte",
        //        PrimitiveType.Nat16 => "ushort",
        //        PrimitiveType.Nat32 => "uint",
        //        PrimitiveType.Nat64 => "ulong",
        //        PrimitiveType.Int => typeof(UnboundedInt).FullName,
        //        PrimitiveType.Int8 => "sbyte",
        //        PrimitiveType.Int16 => "short",
        //        PrimitiveType.Int32 => "int",
        //        PrimitiveType.Int64 => "long",
        //        PrimitiveType.Float32 => "float",
        //        PrimitiveType.Float64 => "double",
        //        PrimitiveType.Bool => "bool",
        //        PrimitiveType.Principal => typeof(Principal).FullName,
        //        PrimitiveType.Reserved => throw new NotImplementedException(),
        //        PrimitiveType.Empty => throw new NotImplementedException(),
        //        PrimitiveType.Null => null,
        //        _ => throw new NotImplementedException(),
        //    };
        //}
        //private class TypeStringResult
        //{
        //    public TypeStringResultType Type { get; }
        //    private readonly object? value;

        //    private TypeStringResult(TypeStringResultType type, object? value)
        //    {
        //        this.Type = type;
        //        this.value = value;
        //    }

        //    public static TypeStringResult Null()
        //    {
        //        return new TypeStringResult(TypeStringResultType.Null, null);
        //    }

        //    public static TypeStringResult AlreadyCreated(AlreadyCreatedInfo value)
        //    {
        //        return new TypeStringResult(TypeStringResultType.AlreadyCreated, value);
        //    }

        //    public AlreadyCreatedInfo AsAlreadyCreated()
        //    {
        //        this.ValidateType(TypeStringResultType.AlreadyCreated);
        //        return (AlreadyCreatedInfo)this.value!;
        //    }
        //    public class AlreadyCreatedInfo
        //    {
        //        public string Name { get; }
        //        public AlreadyCreatedInfo(string name)
        //        {
        //            this.Name = name ?? throw new ArgumentNullException(nameof(name));
        //        }
        //    }


        //    public static TypeStringResult CreateType(CreateTypeInfo info)
        //    {
        //        return new TypeStringResult(TypeStringResultType.CreateType, info);
        //    }

        //    public CreateTypeInfo AsCreateType()
        //    {
        //        this.ValidateType(TypeStringResultType.CreateType);
        //        return (CreateTypeInfo)this.value!;
        //    }

        //    public class CreateTypeInfo
        //    {
        //        public CandidType Type { get; }
        //        public string? ParentTemplateName { get; }

        //        public CreateTypeInfo(CandidType type, string? parentTemplateName)
        //        {
        //            this.Type = type ?? throw new ArgumentNullException(nameof(type));
        //            this.ParentTemplateName = parentTemplateName;
        //        }

        //    }

        //    private void ValidateType(TypeStringResultType type)
        //    {
        //        if (this.Type != type)
        //        {
        //            throw new InvalidOperationException($"Cannot cast '{this.Type}' to type '{type}'");
        //        }
        //    }


        //}

        //public enum TypeStringResultType
        //{
        //    Null,
        //    AlreadyCreated,
        //    CreateType
        //}
    }
}
