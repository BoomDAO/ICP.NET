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

        public string FullTypeName => this.NameSpace + this.Name;
    }
    public class VariantTypez : Typez
    {
        public List<(string Name, Typez InfoType)> Options { get; }
    }
    public class RecordTypez : Typez
    {
        public List<(string Name, Typez Type)> Fields { get; }
    }
    public class ReferenceTypez : Typez
    {

    }
    public class ServiceTypez : Typez
    {
        public List<FuncTypez> Methods { get; }
    }
    public class FuncTypez : Typez
    {
        // TODO name/namespace needed???
        public List<(string Name, Typez Type)> Parameters { get; }
        public List<(string Name, Typez Type)> ReturnParameters { get; }
    }
    internal class SourceGenerationHelper
    {
        private readonly Dictionary<string, string> resolvedTypes = new();

        public string GenerateSourceCode(Typez type)
        {
            IndentedStringBuilder builder = new();
            this.WriteType(builder, type);
            return builder.ToString();
        }

        private void WriteType(IndentedStringBuilder builder, Typez type)
        {
            switch (type)
            {
                case VariantTypez v:
                    this.WriteVariant(builder, v);
                    break;
                case RecordTypez r:
                    this.WriteRecord(builder, r);
                    break;
                case ServiceTypez s:
                    this.WriteService(builder, s);
                    break;
                case FuncTypez f:
                    this.WriteFunc(builder, f);
                    break;
                case ReferenceTypez:
                    break; // Dont write reference types
                default:
                    throw new NotImplementedException();
            };
        }

        private void WriteService(IndentedStringBuilder builder, ServiceTypez service)
        {
            this.WriteClass(builder, $"{service.Name}ApiClient", () =>
            {
                foreach (FuncTypez func in service.Methods)
                {
                    this.WriteFunc(builder, func);
                }
            });
        }

        private void WriteFunc(IndentedStringBuilder builder, FuncTypez func)
        {
            (string, string)[] args = func.Parameters
                .Select((a, i) => (a.Type.FullTypeName, a.Name))
                .ToArray();
            List<(string Type, string Param)> returnTypes = func.ReturnParameters
                .Select(p => (p.Type.FullTypeName, p.Name))
                .ToList();
            this.WriteMethod(
                builder,
                () =>
                {
                    //  TODO

                },
                access: "public",
                isStatic: false,
                returnTypes: returnTypes,
                name: func.Name,
                args
            );

            foreach (Typez paramType in func.Parameters.Select(p => p.Type))
            {
                this.WriteType(builder, paramType);
            }

            foreach (Typez paramType in func.ReturnParameters.Select(p => p.Type))
            {
                this.WriteType(builder, paramType);
            }
        }

        private void WriteRecord(IndentedStringBuilder builder, RecordTypez record)
        {
            string className = record.Name;
            this.WriteClass(builder, className, () =>
            {
                foreach ((string fieldName, Typez fieldType) in record.Fields)
                {
                    builder.AppendLine($"public {className} {fieldName} {{ get; set; }}");
                    builder.AppendLine("");
                    this.WriteType(builder, fieldType);
                }

            });
        }

        private void WriteVariant(IndentedStringBuilder builder, VariantTypez variant)
        {
            string enumName = $"{variant.Name}Type";
            string className = variant.Name;
            List<string> enumValues = variant.Options
                .Select(o => o.Name)
                .ToList();
            this.WriteEnum(builder, enumName, enumValues);
            this.WriteClass(builder, className, () =>
            {
                builder.AppendLine($"public {enumName} Type {{ get; }}");
                builder.AppendLine("private readonly object? value;");
                builder.AppendLine("");

                // Constrcutor
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
                    name: className,
                    (enumName, "type"),
                    ("object?", "value")
                );
                builder.AppendLine("");

                foreach ((string optionName, Typez optionInfo) in variant.Options)
                {
                    if (optionInfo == null)
                    {
                        this.WriteMethod(
                            builder,
                            inner: () =>
                            {
                                builder.AppendLine($"return new {className}({enumName}.{optionName}, null);");
                            },
                            access: "public",
                            isStatic: true,
                            returnType: className,
                            name: optionName
                        );
                    }
                    else
                    {
                        this.WriteMethod(
                            builder,
                            inner: () =>
                            {
                                builder.AppendLine($"return new {className}({enumName}.{optionName}, info);");
                            },
                            access: "public",
                            isStatic: true,
                            returnType: className,
                            name: optionName,
                            (optionInfo.FullTypeName, "info")
                        );
                        builder.AppendLine("");

                        this.WriteMethod(
                            builder,
                            inner: () =>
                            {
                                builder.AppendLine($"this.ValidateType({enumName}.{optionName});");
                                builder.AppendLine($"return ({optionInfo.FullTypeName})this.value!;");
                            },
                            access: "public",
                            isStatic: false,
                            returnType: optionInfo.FullTypeName,
                            name: "As" + optionName
                        );
                        this.WriteType(builder, optionInfo);
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

        private void WriteMethod(IndentedStringBuilder builder, Action inner, string access, bool isStatic, string? returnType, string name, params (string Type, string Param)[] parameters)
        {
            List<(string, string)> returnTypes = new();
            if(returnType != null)
            {
                returnTypes.Add((returnType, returnType));
            }
            this.WriteMethod(builder, inner, access, isStatic, returnTypes, name, parameters);
        }

        private void WriteMethod(IndentedStringBuilder builder, Action inner, string access, bool isStatic, List<(string Type, string Param)> returnTypes, string name, params (string Type, string Param)[] parameters)
        {
            string staticValue = isStatic ? " static" : "";

            string returnValue;
            if (!returnTypes.Any())
            {
                returnValue = "";
            }
            else if(returnTypes.Count == 1)
            {
                returnValue = returnTypes
                    .Select(r => r.Type)
                    .Single();
            }
            else
            {
                returnValue = $"({string.Join(", ", returnTypes.Select(r => $"{r.Type} {r.Param}"))})";
            }

            string parametersString = string.Join(", ", parameters.Select(p => $"{p.Type} {p.Param}"));
            builder.AppendLine($"{access}{staticValue} {returnValue}{name}({parametersString})");
            builder.AppendLine("{");
            using (builder.Indent())
            {
                inner();
            }
            builder.AppendLine("}");
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
    }
}
