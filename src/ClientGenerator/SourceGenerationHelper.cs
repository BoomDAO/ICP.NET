using EdjCase.ICP.Candid;
using EdjCase.ICP.Candid.Models;
using EdjCase.ICP.Candid.Models.Types;
using EdjCase.ICP.Candid.Models.Values;
using EdjCase.ICP.ClientGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICP.ClientGenerator
{
    public abstract class Typez
    {
        public string Name { get; }
        public string? NameSpace { get; }
    }
    public class VariantTypez : Typez
    {
        public List<(string VariantName, string? FullTypeName, RecordTypez? Type)> Options { get; }
    }
    public class RecordTypez : Typez
    {
        public List<(string PropertyName, string FullTypeName, Typez? Type)> Fields { get; }
    }
    internal class SourceGenerationHelper
    {
        private readonly Dictionary<string, string> resolvedTypes = new ();

        public static (string FileName, string Source) GenerateSourceCode(Typez type)
        {
            type switch
            {
                VariantTypez v => 
                _ => throw new NotImplementedException(),
            }
        }

        public static List<(string FileName, string Source)> GenerateSourceCode(CandidServiceFile serviceFile, string didFileName)
        {
            var helper = new SourceGenerationHelper();
            Dictionary<CandidId, TypeStringResult> unprocessedDeclaredTypes = serviceFile.DeclaredTypes
                   .ToDictionary(t => t.Key, t => helper.GetTypeString(t.Value));
            Dictionary<string, string> aliases = new();
            Dictionary<CandidId, TypeStringResult.CreateTypeInfo> typesToCreate = new();

            var generatedFiles = new List<(string FileName, string Source)>();
            while (unprocessedDeclaredTypes.Any())
            {
                foreach (CandidId id in unprocessedDeclaredTypes.Select(d => d.Key))
                {
                    bool processed;
                    TypeStringResult result = unprocessedDeclaredTypes[id];
                    switch (result.Type)
                    {
                        case TypeStringResultType.Null:
                            // TODO
                            throw new Exception();
                        case TypeStringResultType.AlreadyCreated:
                            {
                                TypeStringResult.AlreadyCreatedInfo info = result.AsAlreadyCreated();

                                aliases.Add(id.ToString(), info.GenerateName(prefix));
                                processed = true;
                                break;
                            }
                        case TypeStringResultType.CreateType:
                            {
                                TypeStringResult.CreateTypeInfo info = result.AsCreateType();
                                if (info.Type is CandidServiceType s)
                                {
                                    string typeName = info.GenerateTypeName(id.ToString());
                                    string clientSource = helper.GenerateFromService(typeName, s);
                                    generatedFiles.Add(("Type_" + typeName, clientSource));
                                }
                                else
                                {
                                    string typeName = info.GenerateTypeName(id.ToString());
                                    string typeSource = helper.GenerateDeclaredType(id, info);
                                    generatedFiles.Add(("Type_" + typeName, typeSource));
                                }
                                processed = true;
                                break;
                            }
                        default:
                            throw new NotImplementedException();
                    }
                    if (processed)
                    {
                        unprocessedDeclaredTypes.Remove(id);
                    }
                }
            }

            if (aliases.Any())
            {
                string aliasSource = helper.GenerateAliasFile(aliases);
                generatedFiles.Add(("Aliases", aliasSource));
            }
            // Only write client once
            if (serviceFile.ServiceReferenceId == null)
            {
                string source = helper.GenerateFromService(didFileName, serviceFile.Service);
                generatedFiles.Add(("Client_" + didFileName, source));
            }
            return generatedFiles;
        }


        private string GenerateAliasFile(Dictionary<string, string> aliases)
        {
            IndentedStringBuilder builder = new();
            foreach ((string id, string aliasedType) in aliases)
            {
                // Convert system types to full namespace
                string fullTypeName = aliasedType switch
                {
                    "string" => typeof(string).FullName,
                    "byte" => typeof(byte).FullName,
                    "ushort" => typeof(ushort).FullName,
                    "uint" => typeof(uint).FullName,
                    "ulong" => typeof(ulong).FullName,
                    "sbyte" => typeof(sbyte).FullName,
                    "short" => typeof(short).FullName,
                    "int" => typeof(int).FullName,
                    "long" => typeof(long).FullName,
                    "float" => typeof(float).FullName,
                    "double" => typeof(double).FullName,
                    "bool" => typeof(bool).FullName,
                    _ => aliasedType
                };
                builder.AppendLine($"global using {id} = {fullTypeName};");
            }
            return builder.ToString();
        }

        private string GenerateDeclaredType(CandidId id, TypeStringResult.CreateTypeInfo info)
        {
            var builder = new IndentedStringBuilder();
            this.WriteUsings(builder);
            this.WriteNamespace(builder, "EdjCase.ICP.Clients.Models", () =>
            {
                this.WriteType(builder, id.ToString(), info);
            });
            return builder.ToString();
        }

        private void WriteUsings(IndentedStringBuilder builder)
        {
            // TODO needed?
            //builder.AppendLine("using EdjCase.ICP.Candid;");
            //builder.AppendLine("using EdjCase.ICP.Candid.Models;");
            //builder.AppendLine("using EdjCase.ICP.Agent;");
        }

        private void WriteNamespace(IndentedStringBuilder builder, string name, Action inner)
        {
            builder.AppendLine($"namespace {name}");
            builder.AppendLine("{");
            using (builder.Indent())
            {
                inner();
            }
            builder.AppendLine("}");
        }

        private void WriteType(IndentedStringBuilder builder, string name, TypeStringResult result, bool skipAlreadyCreated = false)
        {
            switch (result.Type)
            {
                case TypeStringResultType.Null:
                    break; // Skip
                case TypeStringResultType.AlreadyCreated:
                    if (!skipAlreadyCreated)
                    {
                        string fullTypeName = this.ResolveFullTypeName(result.AsAlreadyCreated().Name);
                        builder.AppendLine(fullTypeName);
                    }
                    break;
                case TypeStringResultType.CreateType:
                    {
                        TypeStringResult.CreateTypeInfo info = result.AsCreateType();
                        this.WriteType(builder, name, info);
                        break;
                    }
                default:
                    break;
            }
        }

        private void WriteType(IndentedStringBuilder builder, string name, TypeStringResult.CreateTypeInfo info)
        {
            switch (info.Type)
            {
                case CandidRecordType record:
                    this.WriteRecord(builder, name, record);
                    break;
                case CandidVariantType v:
                    this.WriteVariant(builder, name, v);
                    break;
                case CandidServiceType s:
                    this.WriteService(builder, name, s);
                    break;
                case CandidFuncType f:
                    this.WriteFunc(builder, name, f);
                    break;
                default:
                    throw new NotImplementedException();
            };
        }

        private void WriteService(IndentedStringBuilder builder, string name, CandidServiceType service)
        {
            this.WriteClass(builder, $"{name}ApiClient", () =>
            {
                foreach ((CandidId methodName, CandidFuncType func) in service.Methods)
                {
                    string methodNameString = methodName.ToString(); // TODO change case
                    this.WriteFunc(builder, methodNameString, func);
                }
            });
        }

        private void WriteVariant(IndentedStringBuilder builder, string variantName, CandidVariantType variant)
        {
            string enumName = $"{variantName}Type";
            List<string> enumValues = variant.Fields
                .Select(f => this.GetEnumValue(f.Key))
                .ToList();
            this.WriteEnum(builder, enumName, enumValues);
            this.WriteClass(builder, variantName, () =>
            {
                builder.AppendLine($"public {enumName} Type {{ get; }}");
                builder.AppendLine("private readonly object? value;");
                builder.AppendLine("");
                this.WriteMethod(
                    builder,
                    inner: () =>
                    {
                        builder.AppendLine("this.Type = type;");
                        builder.AppendLine("this.value = value;");
                    },
                    access: "public",
                    isStatic: false,
                    returnType: null,
                    name: variantName.ToString(),
                    (enumName, "type"),
                    ("object?", "value")
                );
                builder.AppendLine("");

                foreach ((CandidTag optionLabel, CandidType optionType) in variant.Fields)
                {
                    string enumValue = this.GetEnumValue(optionLabel);
                    TypeStringResult result = this.GetTypeString(optionType);
                    switch (result.Type)
                    {
                        case TypeStringResultType.Null:
                            {
                                this.WriteMethod(
                                    builder,
                                    inner: () =>
                                    {
                                        builder.AppendLine($"return new {variantName}({enumName}.{enumValue}, null);");
                                    },
                                    access: "public",
                                    isStatic: true,
                                    returnType: variantName,
                                    name: enumValue
                                );
                                break;
                            }
                        default:
                            {
                                string? infoModelName = result.GenerateTypeName(enumValue + "Info");
                                if (infoModelName == null)
                                {
                                    // TODO
                                    throw new Exception();
                                }

                                this.WriteMethod(
                                    builder,
                                    inner: () =>
                                    {
                                        builder.AppendLine($"return new {variantName}({enumName}.{enumValue}, info);");
                                    },
                                    access: "public",
                                    isStatic: true,
                                    returnType: variantName,
                                    name: enumValue,
                                    (infoModelName, "info")
                                );
                                builder.AppendLine("");

                                this.WriteMethod(
                                    builder,
                                    inner: () =>
                                    {
                                        builder.AppendLine($"this.ValidateType({enumName}.{enumValue});");
                                        builder.AppendLine($"return ({infoModelName})this.value!;");
                                    },
                                    access: "public",
                                    isStatic: false,
                                    returnType: infoModelName,
                                    name: "As" + enumValue
                                );

                                this.WriteType(builder, infoModelName, result, skipAlreadyCreated: true);
                                break;
                            }
                    }
                    builder.AppendLine("");

                }


                this.WriteMethod(
                    builder,
                    inner: () =>
                    {
                        builder.AppendLine("if (this.Type != type)");
                        builder.AppendLine("{");
                        using (builder.Indent())
                        {
                            builder.AppendLine("throw new InvalidOperationException($\"Cannot cast '{this.Type}' to type '{type}'\");");
                        }
                        builder.AppendLine("}");
                    },
                    access: "private",
                    isStatic: false,
                    returnType: "void",
                    name: "ValidateType",
                    (enumName, "type")
                );

            });
        }

        private string GetEnumValue(CandidTag tag)
        {
            return tag.Name ?? "O" + tag.Id;
        }

        private void WriteMethod(IndentedStringBuilder builder, Action inner, string access, bool isStatic, string? returnType, string name, params (string Type, string Param)[] parameters)
        {
            string staticValue = isStatic ? " static" : "";
            string returnValue = returnType != null ? returnType + " " : "";
            string parametersString = string.Join(", ", parameters.Select(p => $"{p.Item1} {p.Param}"));
            builder.AppendLine($"{access}{staticValue} {returnValue}{name}({parametersString})");
            builder.AppendLine("{");
            using (builder.Indent())
            {
                inner();
            }
            builder.AppendLine("}");

        }

        private void WriteRecord(IndentedStringBuilder builder, string name, CandidRecordType type)
        {
            this.WriteClass(builder, name, () =>
            {
                foreach ((CandidTag typeLabel, CandidType type) in type.Fields)
                {
                    string propertyName = typeLabel.Name ?? "F" + typeLabel.Id.ToString(); // TODO change case
                    TypeStringResult result = this.GetTypeString(type);
                    string recordTypeName = propertyName + "Record";
                    string? typeName = result.GenerateTypeName(recordTypeName);

                    builder.AppendLine($"public {typeName} {propertyName} {{ get; set; }}");
                    builder.AppendLine("");
                    this.WriteType(builder, recordTypeName, result, skipAlreadyCreated: true);
                }

            });
        }


        private void WriteEnum(IndentedStringBuilder builder, string name, List<string> values)
        {
            builder.AppendLine($"public enum {name}");
            builder.AppendLine("{");
            using (builder.Indent())
            {
                foreach (string v in values)
                {
                    builder.AppendLine(v + ",");
                }
            }
            builder.AppendLine("}");
        }

        private void WriteClass(IndentedStringBuilder builder, string name, Action inner)
        {
            builder.AppendLine($"public class {name}");
            builder.AppendLine("{");
            using (builder.Indent())
            {
                inner();
            }
            builder.AppendLine("}");
        }

        private string GenerateFromService(string name, CandidServiceType service)
        {
            var builder = new IndentedStringBuilder();
            this.WriteUsings(builder);
            // TODO needed?
            //builder.AppendLine("using EdjCase.ICP.Clients.Models;");
            this.WriteNamespace(builder, "EdjCase.ICP.Clients", () =>
            {
                this.WriteService(builder, name, service);
            });
            return builder.ToString() ?? "";
        }

        private void WriteFunc(IndentedStringBuilder builder, string methodName, CandidFuncType func)
        {
            List<TypeStringResult> argTypeResults = func.ArgTypes
                    .Select(a => this.GetTypeString(a))
                    .Where(a => a.Type != TypeStringResultType.Null) // Skip null args
                    .ToList();
            List<(string TypeName, TypeStringResult Result)> argsWithName = argTypeResults
                .Select(a => (this.ResolveFullTypeName(a), a))
                .ToList();
            (string, string)[] args = argTypeResults
                .Select((a, i) => (a.TypeName, $"arg{i}"))
                .ToArray();
            this.WriteMethod(
                builder,
                () =>
                {
                    //  TODO

                },
                access: "public",
                isStatic: false,
                returnType: "void",
                name: methodName.ToString(),
                args
            );

            foreach ((string typeName, TypeStringResult result) in argsWithName)
            {
                this.WriteType(builder, typeName, result, skipAlreadyCreated: true);
            }
        }


        private TypeStringResult GetTypeString(CandidType type)
        {
            switch (type)
            {
                case CandidPrimitiveType prim:
                    {
                        string? value = this.GetPrimitiveString(prim.PrimitiveType);
                        if (value == null)
                        {
                            return TypeStringResult.Null();
                        }
                        this.resolvedTypes.Add(value, value); // Add primitive as itself
                        var info = new TypeStringResult.AlreadyCreatedInfo(value);
                        return TypeStringResult.AlreadyCreated(info);
                    }
                case CandidReferenceType r:
                    {
                        var info = new TypeStringResult.AlreadyCreatedInfo(r.Id.ToString());
                        // Dont resolve type yet. Wait till all others are resolved
                        return TypeStringResult.AlreadyCreated(info);
                    }
                case CandidVectorType vec:
                    {
                        TypeStringResult childTypeResult = this.GetTypeString(vec.Value);
                        string template = "System.Collection.Generic.List<{0}>";
                        return this.CreateTemplate(childTypeResult, template);
                    }
                case CandidOptType opt:
                    {
                        TypeStringResult childTypeResult = this.GetTypeString(opt.Value);
                        string template = "{0}?";
                        return this.CreateTemplate(childTypeResult, template);
                    }
                case CandidRecordType:
                case CandidVariantType:
                case CandidServiceType:
                case CandidFuncType:
                    return TypeStringResult.CreateType(new TypeStringResult.CreateTypeInfo(type, null));
                default:
                    throw new NotImplementedException();
            }
        }

        private TypeStringResult CreateTemplate(TypeStringResult result, string template)
        {
            switch (result.Type)
            {
                case TypeStringResultType.Null:
                    //TODO 
                    throw new Exception();
                case TypeStringResultType.AlreadyCreated:
                    {
                        string typeName = result.AsAlreadyCreated().Name;
                        string fullTypeName = this.ResolveFullTypeName(typeName);
                        this.resolvedTypes.Add(typeName, fullTypeName);

                        string name = string.Format(template, fullTypeName);
                        var info = new TypeStringResult.AlreadyCreatedInfo(name);
                        return TypeStringResult.AlreadyCreated(info);
                    }
                case TypeStringResultType.CreateType:
                    {
                        var info = new TypeStringResult.CreateTypeInfo(result.AsCreateType().Type, template);
                        return TypeStringResult.CreateType(info);
                    }
                default:
                    throw new NotImplementedException();
            }
        }

        private string ResolveFullTypeName(TypeStringResult result)
        {

        }
        private string ResolveFullTypeName(string name)
        {
            if(this.resolvedTypes.TryGetValue(name, out string fullName))
            {
                return fullName;
            }
            // TODO
            throw new Exception();
        }

        private string? GetPrimitiveString(PrimitiveType type)
        {
            return type switch
            {
                PrimitiveType.Text => "string",
                PrimitiveType.Nat => typeof(UnboundedUInt).FullName,
                PrimitiveType.Nat8 => "byte",
                PrimitiveType.Nat16 => "ushort",
                PrimitiveType.Nat32 => "uint",
                PrimitiveType.Nat64 => "ulong",
                PrimitiveType.Int => typeof(UnboundedInt).FullName,
                PrimitiveType.Int8 => "sbyte",
                PrimitiveType.Int16 => "short",
                PrimitiveType.Int32 => "int",
                PrimitiveType.Int64 => "long",
                PrimitiveType.Float32 => "float",
                PrimitiveType.Float64 => "double",
                PrimitiveType.Bool => "bool",
                PrimitiveType.Principal => typeof(Principal).FullName,
                PrimitiveType.Reserved => throw new NotImplementedException(),
                PrimitiveType.Empty => throw new NotImplementedException(),
                PrimitiveType.Null => null,
                _ => throw new NotImplementedException(),
            };
        }
        private class TypeStringResult
        {
            public TypeStringResultType Type { get; }
            private readonly object? value;

            private TypeStringResult(TypeStringResultType type, object? value)
            {
                this.Type = type;
                this.value = value;
            }

            public static TypeStringResult Null()
            {
                return new TypeStringResult(TypeStringResultType.Null, null);
            }

            public static TypeStringResult AlreadyCreated(AlreadyCreatedInfo value)
            {
                return new TypeStringResult(TypeStringResultType.AlreadyCreated, value);
            }

            public AlreadyCreatedInfo AsAlreadyCreated()
            {
                this.ValidateType(TypeStringResultType.AlreadyCreated);
                return (AlreadyCreatedInfo)this.value!;
            }
            public class AlreadyCreatedInfo
            {
                public string Name { get; }
                public AlreadyCreatedInfo(string name)
                {
                    this.Name = name ?? throw new ArgumentNullException(nameof(name));
                }
            }


            public static TypeStringResult CreateType(CreateTypeInfo info)
            {
                return new TypeStringResult(TypeStringResultType.CreateType, info);
            }

            public CreateTypeInfo AsCreateType()
            {
                this.ValidateType(TypeStringResultType.CreateType);
                return (CreateTypeInfo)this.value!;
            }

            public class CreateTypeInfo
            {
                public CandidType Type { get; }
                public string? ParentTemplateName { get; }

                public CreateTypeInfo(CandidType type, string? parentTemplateName)
                {
                    this.Type = type ?? throw new ArgumentNullException(nameof(type));
                    this.ParentTemplateName = parentTemplateName;
                }

            }

            private void ValidateType(TypeStringResultType type)
            {
                if (this.Type != type)
                {
                    throw new InvalidOperationException($"Cannot cast '{this.Type}' to type '{type}'");
                }
            }


        }

        public enum TypeStringResultType
        {
            Null,
            AlreadyCreated,
            CreateType
        }
    }
}
