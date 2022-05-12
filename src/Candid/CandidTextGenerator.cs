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
    public static class CandidTextGenerator
    {
        public static string Generate(CandidTypeDefinition t, bool indented = false)
        {
            return t switch
            {
                CompoundCandidTypeDefinition c => GenerateCompound(c, indented),
                RecursiveReferenceCandidTypeDefinition r => GenerateRecursive(r),
                PrimitiveCandidTypeDefinition p => GeneratePrimitive(p),
                _ => throw new NotImplementedException()
            };
        }

        private static string GenerateCompound(CompoundCandidTypeDefinition c, bool indented)
        {
            string value = c switch
            {
                FuncCandidTypeDefinition f => GenerateFunc(f, indented),
                OptCandidTypeDefinition o => GenerateOpt(o, indented),
                VectorCandidTypeDefinition ve => GeneratorVec(ve, indented),
                RecordCandidTypeDefinition r => GenerateRecord(r, indented),
                VariantCandidTypeDefinition va => GenerateVariant(va, indented),
                ServiceCandidTypeDefinition s => GenerateService(s, indented),
                _ => throw new NotImplementedException()
            };
            if(c.RecursiveId != null)
            {
                value = $"μ{c.RecursiveId}.{value}";
            }
            return value;
        }

        private static string GenerateRecursive(RecursiveReferenceCandidTypeDefinition r)
        {
            return $"rec {r.RecursiveId}";
        }

        private static string GeneratePrimitive(PrimitiveCandidTypeDefinition p)
        {
            return p.PrimitiveType switch
            {
                CandidPrimitiveType.Text => "text",
                CandidPrimitiveType.Nat => "nat",
                CandidPrimitiveType.Nat8 => "nat8",
                CandidPrimitiveType.Nat16 => "nat16",
                CandidPrimitiveType.Nat32 => "nat32",
                CandidPrimitiveType.Nat64 => "nat64",
                CandidPrimitiveType.Int => "int",
                CandidPrimitiveType.Int8 => "int8",
                CandidPrimitiveType.Int16 => "int16",
                CandidPrimitiveType.Int32 => "int32",
                CandidPrimitiveType.Int64 => "int64",
                CandidPrimitiveType.Float32 => "float32",
                CandidPrimitiveType.Float64 => "float64",
                CandidPrimitiveType.Bool => "bool",
                CandidPrimitiveType.Principal => "principal",
                CandidPrimitiveType.Reserved => "reserved",
                CandidPrimitiveType.Empty => "empty",
                CandidPrimitiveType.Null => "null",
                _ => throw new NotImplementedException(),
            };
        }

        private static string GenerateService(ServiceCandidTypeDefinition s, bool indented)
        {
            // TODO dynamic single vs multi line based on width
            List<string> methods = s.Methods
                .Select(f => $"{f.Key}:{CandidTextGenerator.Generate(f.Value, indented)}")
                .ToList();

            if (!indented)
            {
                return $"service : ({s.Name}) -> {{{string.Join("; ", methods)}}}";
            }
            return $"service : ({s.Name}) -> {{{string.Join(";", methods.Select(t => $"\n\t{t}"))}}}";
        }

        private static string GenerateVariant(VariantCandidTypeDefinition va, bool indented)
        {
            // TODO dynamic single vs multi line based on width
            IEnumerable<string> fields = va.Fields.Select(f => $"{CandidTextGenerator.Generate(f.Key)} : {CandidTextGenerator.Generate(f.Value, indented)}");

            if (!indented)
            {
                return $"variant : {{{string.Join("; ", fields)}}}";
            }
            return $"variant : {{{string.Join(";", fields.Select(t => $"\n\t{t}"))}}}";
        }

        private static string GenerateRecord(RecordCandidTypeDefinition r, bool indented)
        {
            // TODO dynamic single vs multi line based on width
            IEnumerable<string> fields = r.Fields.Select(f => $"{CandidTextGenerator.Generate(f.Key)} : {CandidTextGenerator.Generate(f.Value, indented)}");

            if (!indented)
            {
                return $" record {{ {string.Join("; ", fields)} }}";
            }
            return $" record {{{string.Join(";", fields.Select(f => $"\n\t{f}"))} }}";
        }

        private static object Generate(CandidTag key)
        {
            return key.Name ?? key.Id.ToString();
        }

        private static string GeneratorVec(VectorCandidTypeDefinition ve, bool indented)
        {
            return $"vec {CandidTextGenerator.Generate(ve.Value, indented)}";
        }

        private static string GenerateOpt(OptCandidTypeDefinition o, bool indented)
        {
            return $"opt {CandidTextGenerator.Generate(o.Value, indented)}";
        }

        private static string GenerateFunc(FuncCandidTypeDefinition func, bool indented)
        {
            // TODO optimize
            List<string> argTypes = func.ArgTypes
                .Select(t => CandidTextGenerator.Generate(t, indented))
                .ToList();
            List<string> returnTypes = func.ReturnTypes
                .Select(t => CandidTextGenerator.Generate(t, indented))
                .ToList();

            List<string> modes = func.Modes
                .Select(t => CandidTextGenerator.Generate(t))
                .ToList();


            if (!indented)
            {
                return $"({string.Join(", ", argTypes)}) -> ({string.Join(", ", returnTypes)}) {string.Join(", ", modes)}";
            }
            return $"({string.Join(", ", argTypes)}) -> ({string.Join(",", returnTypes.Select(t => $"\n\t{t}"))}) {string.Join(", ", modes)}";
        }

        private static string Generate(FuncMode t)
        {
            return t switch
            {
                FuncMode.Oneway => "oneway",
                FuncMode.Query => "query",
                _ => throw new NotImplementedException(),
            };
        }
    }
}
