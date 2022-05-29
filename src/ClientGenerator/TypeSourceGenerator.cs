using EdjCase.ICP.Candid;
using EdjCase.ICP.Candid.Models;
using EdjCase.ICP.Candid.Models.Types;
using EdjCase.ICP.Candid.Models.Values;
using EdjCase.ICP.ClientGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static EdjCase.ICP.ClientGenerator.TypeSourceConverter;

namespace ICP.ClientGenerator
{
    internal static class TypeSourceGenerator
    {
        private static Dictionary<Type, string> systemTypeShorthands = new Dictionary<Type, string>
        {
            { typeof(string), "string" },
            { typeof(byte), "byte" },
            { typeof(ushort), "ushort" },
            { typeof(uint), "uint" },
            { typeof(ulong), "ulong" },
            { typeof(sbyte), "sbyte" },
            { typeof(short), "short" },
            { typeof(int), "int" },
            { typeof(long), "long" },
            { typeof(float), "float" },
            { typeof(double), "double" },
            { typeof(bool), "bool" }
        };

        public static (string FileName, string SourceCode) GenerateSourceCode(DeclaredTypeSourceDescriptor type)
        {
            IndentedStringBuilder builder = new();
            if (type.Namespace == null)
            {
                // TODO
                throw new Exception();
            }


            WriteNamespace(builder, type.Namespace, () =>
            {
                WriteType(builder, type.Type);
            });
            string filePrefix = type.Type switch
            {
                ServiceSourceDescriptor s => "Client_",
                _ => "Type_",
            };
            string source = builder.ToString();
            foreach ((Type systemType, string shortHand) in systemTypeShorthands)
            {
                // Convert system types to shorten versions
                source = source.Replace(systemType.FullName, shortHand);
            }
            return (filePrefix + type.Type.Name, source);
        }

        public static string GenerateAliasSourceCode(Dictionary<string, string> aliases)
        {
            IndentedStringBuilder builder = new();
            foreach ((string id, string aliasedType) in aliases)
            {
                builder.AppendLine($"global using {id} = {aliasedType};");
            }
            return builder.ToString();
        }

        private static void WriteType(IndentedStringBuilder builder, TypeSourceDescriptor type)
        {
            switch (type)
            {
                case VariantSourceDescriptor v:
                    WriteVariant(builder, v);
                    break;
                case RecordSourceDescriptor r:
                    WriteRecord(builder, r);
                    break;
                case ServiceSourceDescriptor s:
                    WriteService(builder, s);
                    break;
                default:
                    throw new NotImplementedException();
            };
        }

        private static void WriteService(IndentedStringBuilder builder, ServiceSourceDescriptor service)
        {
            string className = $"{service.Name}ApiClient";
            WriteClass(builder, className, () =>
            {
                builder.AppendLine("public IAgent Agent { get; }");
                builder.AppendLine("public Principal CanisterId { get; }");
                // Constrcutor
                WriteMethod(
                    builder,
                    inner: () =>
                    {
                        builder.AppendLine("this.Agent = agent ?? throw new ArgumentNullException(nameof(agent));");
                        builder.AppendLine("this.Can`isterId = canisterId ?? throw new ArgumentNullException(nameof(canisterId));");
                    },
                    access: "public",
                    isStatic: false,
                    isAsync: false,
                    isConstructor: true,
                    returnType: null,
                    name: className,
                    ("IAgent", "agent"),
                    ("Principal", "canisterId")
                );
                foreach (ServiceSourceDescriptor.Method func in service.Methods)
                {
                    (string, string)[] args = func.Parameters
                        .Where(p => p.Type != null) // exclude null/empty/reserved
                        .Select((a, i) => (a.Type!.FullTypeName, a.VariableName))
                        .ToArray();
                    List<(string Type, string Param)> returnTypes;
                    if (func.IsFireAndForget)
                    {
                        // TODO confirm no return types, or even not async?
                        returnTypes = new List<(string Type, string Param)>();
                    }
                    else
                    {
                        returnTypes = func.ReturnParameters
                            .Where(p => p.Type != null) // exclude null/empty/reserved
                            .Select(p => (p.Type!.FullTypeName, p.VariableName))
                            .ToList();
                    }
                    WriteMethod(
                        builder,
                        () =>
                        {
                            builder.AppendLine($"string method = \"{func.UnmodifiedName}\";");

                            var parameterVariables = new List<string>();
                            foreach (ServiceSourceDescriptor.Method.ParameterInfo parameter in func.Parameters)
                            {
                                int index = parameterVariables.Count;
                                string variableName = "p" + index;
                                string valueWithType;
                                if (parameter.Type != null)
                                {
                                    valueWithType = $"CandidValueWithType.FromObject<{parameter.Type.FullTypeName}>({parameter.VariableName})";
                                }
                                else
                                {
                                    valueWithType = "CandidValueWithType.Null()";
                                }
                                builder.AppendLine($"CandidValueWithType {variableName} = {valueWithType};");
                                parameterVariables.Add(variableName);
                            }

                            builder.AppendLine("var candidArgs = new List<CandidValueWithType>");
                            builder.AppendLine("{");
                            using (builder.Indent())
                            {
                                foreach (string v in parameterVariables)
                                {
                                    builder.AppendLine(v + ",");
                                }
                            }
                            builder.AppendLine("};");
                            builder.AppendLine("CandidArg arg = CandidArg.FromCandid(candidArgs);");
                            builder.AppendLine("QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, method, arg, identityOverride: null);");
                            builder.AppendLine("QueryReply reply = response.ThrowOrGetReply();");

                            if (returnTypes.Any())
                            {
                                var returnParamVariables = new List<string>();
                                int i = 0;
                                foreach (ServiceSourceDescriptor.Method.ParameterInfo parameter in func.ReturnParameters)
                                {
                                    // Only include non null/empty/reserved params
                                    if (parameter.Type != null)
                                    {
                                        string variableName = "r" + i;
                                        builder.AppendLine($"{parameter.Type.FullTypeName} {variableName} = reply.Arg.Values[{i}].ToObject<{parameter.Type.FullTypeName}>();");
                                        returnParamVariables.Add(variableName);
                                    }
                                    i++;
                                }
                                string returnString = string.Join(", ", returnParamVariables);
                                builder.AppendLine($"return ({returnString});");
                            }

                        },
                        access: "public",
                        isStatic: false,
                        isAsync: true,
                        isConstructor: false,
                        returnTypes: returnTypes,
                        name: func.Name,
                        args
                    );

                }
            });
        }


        private static string BuildCandidId(CandidId? id)
        {
            if (id == null)
            {
                return "null";
            }
            return $"CandidId.Parse(\"{id}\")";
        }

        private static string BuildCandidTag(CandidTag tag)
        {
            return $"new CandidTag(\"{tag.Id}\", {$"\"tag.Name\"" ?? "null"})";
        }

        private static string BuildDictionaryString(string genericType1, string genericType2, IEnumerable<(string, string)> values)
        {
            string valuesString = string.Join(", ", values.Select(v => $"{{ {v.Item1}, {v.Item2} }}"));
            return $"new Dictionary<CandidTag, CandidType > {{ {valuesString} }}";
        }

        private static string BuildListString(string genericType, IEnumerable<string> values)
        {
            string valuesString = string.Join(", ", values);
            return $"new List<{genericType}> {{ {valuesString} }}";
        }

        private static void WriteRecord(IndentedStringBuilder builder, RecordSourceDescriptor record)
        {
            string className = record.Name;
            WriteClass(builder, className, () =>
            {
                foreach ((string fieldName, string fieldFullTypeName) in record.Fields)
                {
                    builder.AppendLine($"public {fieldFullTypeName} {fieldName} {{ get; set; }}");
                    builder.AppendLine("");
                }

                foreach (TypeSourceDescriptor paramType in record.SubTypesToCreate)
                {
                    WriteType(builder, paramType);
                }

            });

        }

        private static void WriteVariant(IndentedStringBuilder builder, VariantSourceDescriptor variant)
        {
            string enumName = $"{variant.Name}Type";
            string className = variant.Name;
            List<string> enumValues = variant.Options
                .Select(o => o.Name)
                .ToList();
            WriteEnum(builder, enumName, enumValues);
            WriteClass(builder, className, () =>
            {
                builder.AppendLine($"public {enumName} Type {{ get; }}");
                builder.AppendLine("private readonly object? value;");
                builder.AppendLine("");

                // Constrcutor
                WriteMethod(
                    builder,
                    inner: () =>
                    {
                        builder.AppendLine("this.Type = type;");
                        builder.AppendLine("this.value = value;");
                    },
                    access: "public",
                    isStatic: false,
                    isAsync: false,
                    isConstructor: true,
                    returnType: null,
                    name: className,
                    (enumName, "type"),
                    ("object?", "value")
                );
                builder.AppendLine("");

                foreach ((string optionName, string? infoFullTypeName) in variant.Options)
                {
                    if (infoFullTypeName == null)
                    {
                        WriteMethod(
                            builder,
                            inner: () =>
                            {
                                builder.AppendLine($"return new {className}({enumName}.{optionName}, null);");
                            },
                            access: "public",
                            isStatic: true,
                            isAsync: false,
                            isConstructor: false,
                            returnType: className,
                            name: optionName
                        );
                    }
                    else
                    {
                        WriteMethod(
                            builder,
                            inner: () =>
                            {
                                builder.AppendLine($"return new {className}({enumName}.{optionName}, info);");
                            },
                            access: "public",
                            isStatic: true,
                            isAsync: false,
                            isConstructor: false,
                            returnType: className,
                            name: optionName,
                            (infoFullTypeName, "info")
                        );
                        builder.AppendLine("");

                        WriteMethod(
                            builder,
                            inner: () =>
                            {
                                builder.AppendLine($"this.ValidateType({enumName}.{optionName});");
                                builder.AppendLine($"return ({infoFullTypeName})this.value!;");
                            },
                            access: "public",
                            isStatic: false,
                            isAsync: false,
                            isConstructor: false,
                            returnType: infoFullTypeName,
                            name: "As" + optionName
                        );
                    }
                    builder.AppendLine("");

                }


                WriteMethod(
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
                    isAsync: false,
                    isConstructor: false,
                    returnType: "void",
                    name: "ValidateType",
                    (enumName, "type")
                );



                foreach (TypeSourceDescriptor paramType in variant.SubTypesToCreate)
                {
                    WriteType(builder, paramType);
                }

            });
        }



        private static void WriteNamespace(IndentedStringBuilder builder, string name, Action inner)
        {
            builder.AppendLine($"namespace {name}");
            builder.AppendLine("{");
            using (builder.Indent())
            {
                inner();
            }
            builder.AppendLine("}");
        }

        private static void WriteMethod(
            IndentedStringBuilder builder,
            Action inner,
            string? access,
            bool isStatic,
            bool isAsync,
            bool isConstructor,
            string? returnType,
            string name,
            params (string Type, string Param)[] parameters)
        {
            List<(string, string)> returnTypes = new();
            if (returnType != null)
            {
                returnTypes.Add((returnType, returnType));
            }
            WriteMethod(builder, inner, access, isStatic, isAsync, isConstructor, returnTypes, name, parameters);
        }

        private static void WriteMethod(
            IndentedStringBuilder builder,
            Action inner,
            string? access,
            bool isStatic,
            bool isAsync,
            bool isConstructor,
            List<(string Type, string Param)> returnTypes,
            string name,
            params (string Type, string Param)[] parameters)
        {
            var methodItems = new List<string>();
            if (access != null)
            {
                methodItems.Add(access);
            }
            if (!isConstructor)
            {
                if (isStatic)
                {
                    methodItems.Add("static");
                }

                string returnValue;
                if (!returnTypes.Any())
                {
                    returnValue = "void";
                }
                else if (returnTypes.Count == 1)
                {
                    returnValue = returnTypes
                        .Select(r => r.Type)
                        .Single();
                }
                else
                {
                    returnValue = $"({string.Join(", ", returnTypes.Select(r => $"{r.Type} {r.Param}"))})";
                }
                if (isAsync)
                {
                    if (!returnTypes.Any())
                    {
                        returnValue = "async Task";
                    }
                    else
                    {
                        returnValue = $"async Task<{returnValue}>";
                    }
                }

                methodItems.Add(returnValue);
            }

            methodItems.Add(name);

            string parametersString = string.Join(", ", parameters.Select(p => $"{p.Type} {p.Param}"));

            builder.AppendLine($"{string.Join(" ", methodItems)}({parametersString})");
            builder.AppendLine("{");
            using (builder.Indent())
            {
                inner();
            }
            builder.AppendLine("}");
        }


        private static void WriteEnum(IndentedStringBuilder builder, string name, List<string> values)
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

        private static void WriteClass(IndentedStringBuilder builder, string name, Action inner)
        {
            builder.AppendLine($"public class {name}");
            builder.AppendLine("{");
            using (builder.Indent())
            {
                inner();
            }
            builder.AppendLine("}");
        }
    }
}
