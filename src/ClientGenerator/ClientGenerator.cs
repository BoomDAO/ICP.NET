using EdjCase.ICP.Candid;
using EdjCase.ICP.Candid.Models;
using EdjCase.ICP.Candid.Models.Types;
using EdjCase.ICP.Candid.Models.Values;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace EdjCase.ICP.ClientGenerator
{
    [Generator]
    public class ClientGenerator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
            IEnumerable<AdditionalText> didFiles = this.GetDidFiles(context);
            foreach (AdditionalText didFile in didFiles)
            {
                this.GenerateClient(context, didFile);
            }
        }

        public void Initialize(GeneratorInitializationContext context)
        {
        }

        private void GenerateClient(GeneratorExecutionContext context, AdditionalText didFile)
        {
            SourceText? sourceText = didFile.GetText();
            if (sourceText == null)
            {
                return;
            }
            string value = sourceText.ToString();
            string name = Path.GetFileNameWithoutExtension(didFile.Path);
            CandidServiceFile serviceFile;
            try
            {
                serviceFile = CandidServiceFile.Parse(value);
            }
            catch (Exception ex)
            {
                // TODO handle this better
                DiagnosticDescriptor descriptor = new DiagnosticDescriptor("1", "Failed to parse DID file", ex.ToString(), "ParseError", DiagnosticSeverity.Error, true);
                var location = Location.Create(didFile.Path, TextSpan.FromBounds(0, 0), new LinePositionSpan());
                context.ReportDiagnostic(Diagnostic.Create(descriptor, location));
                return;
            }
            foreach ((CandidId id, CandidType type) in serviceFile.DeclaredTypes)
            {
                string typeSource = this.GenerateDeclaredType(id, type);
                context.AddSource("Type_" + id.ToString(), typeSource);
            }
            string source = this.GenerateFromService(name, serviceFile.Service);
            context.AddSource(name, source);
        }

        private string GenerateDeclaredType(CandidId id, CandidType type)
        {
            var builder = new IndentedStringBuilder();
            this.WriteUsings(builder);
            this.WriteNamespace(builder, "EdjCase.ICP.Clients.Models", () =>
            {
                this.WriteType(builder, id.ToString(), type);
            });
            return builder.ToString();
        }

        private void WriteUsings(IndentedStringBuilder builder)
        {
            builder.AppendLine("using EdjCase.ICP.Candid;");
            builder.AppendLine("using EdjCase.ICP.Candid.Models;");
            builder.AppendLine("using EdjCase.ICP.Agent;");
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

        private void WriteType(IndentedStringBuilder builder, string name, CandidType type)
        {
            (string Label, CandidCompoundType? TypeToCreate)? info = this.GetTypeString(type, typeCreationIndex: 0);
            if(info == null)
            {
                return;
            }
            if (info.Value.TypeToCreate != null)
            {
                if (type != info.Value.TypeToCreate)
                {
                    if (info.Value.TypeToCreate == null)
                    {
                        builder.AppendLine(name);
                    }
                    else
                    {
                        this.WriteType(builder, info.Value.Label, info.Value.TypeToCreate);
                    }
                }
                else
                {
                    switch (info.Value.TypeToCreate)
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
            }
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
            this.WriteClass(builder, variantName, () =>
            {
                string enumName = $"{variantName}Type";
                builder.AppendLine($"public {enumName} Type {{ get; }}");
                builder.AppendLine("private readonly object value;");
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
                    (variantName.ToString(), "type"),
                    ("object", "value")
                );
                builder.AppendLine("");

                foreach ((CandidTag optionLabel, CandidType optionType) in variant.Fields)
                {
                    string optionName = optionLabel.Name ?? "O" + optionLabel.Id.ToString(); // TODO change case
                    (string Label, CandidCompoundType? TypeToCreate)? option = this.GetTypeString(optionType, typeCreationIndex: 0);
                    if (option == null)
                    {
                        this.WriteMethod(
                            builder,
                            inner: () =>
                            {
                                builder.AppendLine($"return new {variantName}({enumName}.{optionName}, null);");
                            },
                            access: "public",
                            isStatic: true,
                            returnType: variantName,
                            name: optionName
                        );
                    }
                    else
                    {
                        this.WriteMethod(
                            builder,
                            inner: () =>
                            {
                                builder.AppendLine($"return new {variantName}({enumName}.{optionName}, info);");
                            },
                            access: "public",
                            isStatic: true,
                            returnType: variantName,
                            name: optionName,
                            (option.Value.Label, "info")
                        );
                        builder.AppendLine("");

                        this.WriteMethod(
                            builder,
                            inner: () =>
                            {
                                builder.AppendLine($"this.ValidateType({enumName}.{optionName});");
                                builder.AppendLine($"return ({option.Value.Label})this.value;");
                            },
                            access: "public",
                            isStatic: false,
                            returnType: option.Value.Label,
                            name: optionName
                        );

                        if (option.Value.TypeToCreate != null)
                        {
                            this.WriteType(builder, option.Value.Label, option.Value.TypeToCreate);
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
                int typeCreationIndex = 0;
                foreach ((CandidTag typeLabel, CandidType type) in type.Fields)
                {
                    string typeLabelName = typeLabel.Name ?? "F" + typeLabel.Id.ToString(); // TODO change case
                    (string Label, CandidCompoundType? TypeToCreate)? field = this.GetTypeString(type, typeCreationIndex);
                    if(field == null)
                    {
                        // TODO 
                        throw new Exception();
                    }
                    builder.AppendLine($"public {field.Value.Label} {typeLabelName} {{ get; set; }}");
                    builder.AppendLine("");
                    if (field.Value.TypeToCreate != null)
                    {
                        this.WriteType(builder, field.Value.Label, field.Value.TypeToCreate);
                        typeCreationIndex++;
                    }
                }

            });
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
            this.WriteNamespace(builder, "EdjCase.ICP.Clients", () =>
            {
                this.WriteService(builder, name, service);
            });
            return builder.ToString() ?? "";
        }

        private void WriteFunc(IndentedStringBuilder builder, string methodName, CandidFuncType func)
        {
            List<(string Type, string Name, CandidCompoundType? TypeToCreate)> args = GenerateArgs(func.ArgTypes);
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
                args.Select(a => (a.Type, a.Name)).ToArray()
            );

            foreach (var arg in args)
            {
                if (arg.TypeToCreate != null)
                {
                    this.WriteType(builder, arg.Name, arg.TypeToCreate);
                }
            }

            List<(string Type, string Name, CandidCompoundType? TypeToCreate)> GenerateArgs(IReadOnlyList<CandidType> argTypes)
            {
                int i = 0;
                var types = new List<(string, string, CandidCompoundType?)>();
                int typeCreationIndex = 0;
                foreach (CandidType argType in argTypes)
                {
                    (string Label, CandidCompoundType? TypeToCreate)? argInfo = this.GetTypeString(argType, typeCreationIndex);
                    if(argInfo == null)
                    {
                        continue; // skip null params
                    }
                    // TODO better naming?
                    types.Add((argInfo.Value.Label, $"arg{i}", argInfo.Value.TypeToCreate));
                    i++;
                    if (argInfo.Value.TypeToCreate != null)
                    {
                        typeCreationIndex++;
                    }
                }
                return types;
            }
        }

        private (string Label, CandidCompoundType? TypeToCreate)? GetTypeString(CandidType type, int typeCreationIndex)
        {
            switch (type)
            {
                case CandidPrimitiveType prim:
                    string? value = this.GetPrimitiveString(prim.PrimitiveType);
                    if(value == null)
                    {
                        return null;
                    }
                    return (value, null);
                case CandidReferenceType r:
                    return (r.Id.ToString(), null);
                case CandidVectorType vec:
                    {
                        (string Label, CandidCompoundType? TypeToCreate)? info = this.GetTypeString(vec.Value, 0);
                        if (info == null)
                        {
                            //TODO 
                            throw new Exception();
                        }
                        return ($"List<{info.Value.Label}>", info.Value.TypeToCreate);
                    }
                case CandidOptType opt:
                    {
                        (string Label, CandidCompoundType? TypeToCreate)? info = this.GetTypeString(opt.Value, 0);
                        if (info == null)
                        {
                            //TODO 
                            throw new Exception();
                        }
                        return ($"{info.Value.Label}?", info.Value.TypeToCreate);
                    }
                case CandidRecordType r:
                    {
                        return Next("Record", r);
                    }
                case CandidVariantType v:
                    {
                        return Next("Variant", v);
                    }
                case CandidServiceType s:
                    {
                        return Next("Service", s);
                    }
                case CandidFuncType f:
                    {
                        return Next("Func", f);
                    }
                default:
                    throw new NotImplementedException();
            }
            (string Type, CandidCompoundType TypesToCreate) Next(string prefix, CandidCompoundType t)
            {
                // TODO better naming
                return (prefix + typeCreationIndex, t);
            }
        }


        private string? GetPrimitiveString(PrimitiveType type)
        {
            return type switch
            {
                PrimitiveType.Text => "string",
                PrimitiveType.Nat => nameof(UnboundedUInt),
                PrimitiveType.Nat8 => "byte",
                PrimitiveType.Nat16 => "ushort",
                PrimitiveType.Nat32 => "uint",
                PrimitiveType.Nat64 => "ulong",
                PrimitiveType.Int => nameof(UnboundedInt),
                PrimitiveType.Int8 => "sbyte",
                PrimitiveType.Int16 => "short",
                PrimitiveType.Int32 => "int",
                PrimitiveType.Int64 => "long",
                PrimitiveType.Float32 => "float",
                PrimitiveType.Float64 => "double",
                PrimitiveType.Bool => "bool",
                PrimitiveType.Principal => "Principal",
                PrimitiveType.Reserved => throw new NotImplementedException(),
                PrimitiveType.Empty => throw new NotImplementedException(),
                PrimitiveType.Null => null,
                _ => throw new NotImplementedException(),
            };
        }

        private IEnumerable<AdditionalText> GetDidFiles(GeneratorExecutionContext context)
        {
            foreach (AdditionalText file in context.AdditionalFiles)
            {
                if (Path.GetExtension(file.Path).Equals(".did", StringComparison.OrdinalIgnoreCase))
                {
                    yield return file;
                }
            }
        }
    }
}