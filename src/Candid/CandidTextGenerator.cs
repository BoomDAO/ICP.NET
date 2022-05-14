using ICP.Candid.Models;
using ICP.Candid.Models.Types;
using ICP.Candid.Models.Values;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICP.Candid
{
    public static class CandidTextGenerator
    {
        public enum IndentType
        {
            None,
            Spaces_2,
            Spaces_3,
            Spaces_4,
            Tab
        }


        public static string Generate(CandidTypeDefinition t, IndentType indentType = IndentType.None)
        {
            var textComponent = CandidTextGenerator.GenerateInternal(t);
            string? tabString = indentType switch
            {
                IndentType.None => "",
                IndentType.Spaces_2 => "  ",
                IndentType.Spaces_3 => "   ",
                IndentType.Spaces_4 => "    ",
                IndentType.Tab => "\t",
                _ => throw new NotImplementedException()
            };

            using (var stringWriter = new StringWriter())
            {
                using (var writer = new IndentedTextWriter(stringWriter, tabString))
                {
                    textComponent.WriteText(writer, indentType != IndentType.None);
                }
                return stringWriter.ToString();
            }

        }

        private static TextComponentBase GenerateInternal(CandidTypeDefinition t)
        {
            return t switch
            {
                FuncCandidTypeDefinition f => GenerateFunc(f),
                OptCandidTypeDefinition o => GenerateOpt(o),
                VectorCandidTypeDefinition ve => GeneratorVec(ve),
                RecordCandidTypeDefinition r => GenerateRecord(r),
                VariantCandidTypeDefinition va => GenerateVariant(va),
                ServiceCandidTypeDefinition s => GenerateService(s),
                ReferenceCandidTypeDefinition r => GenerateRecursive(r),
                PrimitiveCandidTypeDefinition p => GeneratePrimitive(p),
                _ => throw new NotImplementedException()
            };
        }

        private static ConstantTextComponent GenerateRecursive(ReferenceCandidTypeDefinition r)
        {
            return new ConstantTextComponent(r.Id.ToString());
        }

        private static ConstantTextComponent GeneratePrimitive(PrimitiveCandidTypeDefinition p)
        {
            string type = p.PrimitiveType switch
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
            return new ConstantTextComponent(type);
        }

        private static CompoundTypeTextComponent GenerateService(ServiceCandidTypeDefinition s)
        {
            // TODO dynamic single vs multi line based on width
            List<KeyValueTextComponent> methods = s.Methods
                .Select(f => new KeyValueTextComponent(f.Key, GenerateInternal(f.Value)))
                .ToList();
            var type = new KeyValueTextComponent("service", new ConstantTextComponent($"({s.Id})"));
            return new CompoundTypeTextComponent(type, " -> ", new CurlyBraceTypeTextComponent<KeyValueTextComponent>(methods), s.RecursiveId);
        }

        private static CompoundTypeTextComponent GenerateVariant(VariantCandidTypeDefinition va)
        {
            List<KeyValueTextComponent> fields = va.Fields
                .Select(f => new KeyValueTextComponent(GenerateTag(f.Key), GenerateInternal(f.Value)))
                .ToList();
            var innerValue = new CurlyBraceTypeTextComponent<KeyValueTextComponent>(fields);
            return new CompoundTypeTextComponent(new ConstantTextComponent("variant"), " ", innerValue, va.RecursiveId);
        }

        private static CompoundTypeTextComponent GenerateRecord(RecordCandidTypeDefinition r)
        {
            bool tagsMatchIndex = r.Fields
                .Select((f, i) => (Tag: f.Key, Index: i))
                .All((f => f.Tag == f.Index));
            TextComponentBase innerValue;
            // If tags are indicies, use shorthand
            if (tagsMatchIndex)
            {
                List<TextComponentBase> fields = r.Fields
                    .Select(f => GenerateInternal(f.Value))
                    .ToList();
                innerValue = new CurlyBraceTypeTextComponent<TextComponentBase>(fields);
            }
            else
            {
                List<KeyValueTextComponent> fields = r.Fields
                    .Select(f => new KeyValueTextComponent(GenerateTag(f.Key), GenerateInternal(f.Value)))
                    .ToList();
                innerValue = new CurlyBraceTypeTextComponent<KeyValueTextComponent>(fields);
            }
            return new CompoundTypeTextComponent(new ConstantTextComponent("record"), " ", innerValue, r.RecursiveId);
        }

        private static string GenerateTag(CandidTag key)
        {
            return key.Name ?? key.Id.ToString();
        }

        private static CompoundTypeTextComponent GeneratorVec(VectorCandidTypeDefinition ve)
        {
            TextComponentBase innerValue = GenerateInternal(ve.Value);
            return new CompoundTypeTextComponent(new ConstantTextComponent("vec"), " ", innerValue, ve.RecursiveId);
        }

        private static CompoundTypeTextComponent GenerateOpt(OptCandidTypeDefinition o)
        {
            TextComponentBase innerValue = GenerateInternal(o.Value);
            return new CompoundTypeTextComponent(new ConstantTextComponent("opt"), " ", innerValue, o.RecursiveId);
        }

        private static CompoundTypeTextComponent GenerateFunc(FuncCandidTypeDefinition func)
        {
            List<TextComponentBase> argTypes = func.ArgTypes
                .Select(t => CandidTextGenerator.GenerateInternal(t))
                .ToList();
            List<TextComponentBase> returnTypes = func.ReturnTypes
                .Select(t => CandidTextGenerator.GenerateInternal(t))
                .ToList();

            List<string> modes = func.Modes
                .Select(t => CandidTextGenerator.GenerateMode(t))
                .ToList();

            var innerValue = new TupleWithSuffixTextComponent(new TupleTextComponent(returnTypes), modes);
            return new CompoundTypeTextComponent(new TupleTextComponent(argTypes), " -> ", innerValue, func.RecursiveId);
        }

        private static string GenerateMode(FuncMode t)
        {
            return t switch
            {
                FuncMode.Oneway => "oneway",
                FuncMode.Query => "query",
                _ => throw new NotImplementedException(),
            };
        }

        private abstract record TextComponentBase
        {
            public abstract int UnIndentedWidth { get; }
            public abstract void WriteText(IndentedTextWriter writer, bool indented);

            protected void WriteMultiple<T>(IndentedTextWriter writer, List<T> items, string delimiter, bool indented)
                where T : TextComponentBase
            {
                if (indented)
                {
                    writer.Indent += 2;
                    writer.WriteLine();
                }
                else
                {
                    writer.Write(" ");
                }
                for (int i = 0; i < items.Count; i++)
                {
                    TextComponentBase item = items[i];
                    item.WriteText(writer, indented);
                    if (i != items.Count - 1)
                    {
                        // Add seperator to all but last
                        writer.Write(delimiter);
                    }
                    if (indented)
                    {
                        writer.WriteLine();
                    }
                    else
                    {
                        writer.Write(" ");
                    }
                }
                if (indented)
                {
                    writer.Indent -= 2;
                }
            }
        }

        private record TupleTextComponent(List<TextComponentBase> Items) : TextComponentBase
        {
            public override int UnIndentedWidth =>
                1 // "("
                + this.Items.Sum(i => i.UnIndentedWidth) // '{item}'
                + (this.Items.Count - 1) * 2 // ', ' between each item
                + 1; // ")"


            public override void WriteText(IndentedTextWriter writer, bool indented)
            {
                writer.Write($"(");
                this.WriteMultiple(writer, this.Items, ",", indented);
                writer.Write(")");
            }
        }

        private record TupleWithSuffixTextComponent(TupleTextComponent Tuple, List<string> Suffixes) : TextComponentBase
        {
            public override int UnIndentedWidth =>
                this.Tuple.UnIndentedWidth // items
                + this.Suffixes.Sum(s => s.Length) // Suffix length
                + (this.Suffixes.Count - 1); // Gaps between suffixes


            public override void WriteText(IndentedTextWriter writer, bool indented)
            {
                this.Tuple.WriteText(writer, indented);
                foreach(string suffix in this.Suffixes)
                {
                    writer.Write(" ");
                    writer.Write(suffix);
                }
            }
        }

        private record KeyValueTextComponent(string Key, TextComponentBase Value) : TextComponentBase
        {
            public override int UnIndentedWidth =>
                this.Key.Length // '{key}'
                + 1 // ':'
                + this.Value.UnIndentedWidth; // '{value}'

            public override void WriteText(IndentedTextWriter writer, bool indented)
            {
                writer.Write($"{this.Key}:");
                this.Value.WriteText(writer, indented);
            }
        }

        private record CurlyBraceTypeTextComponent<T>(List<T> Fields) : TextComponentBase
            where T : TextComponentBase
        {
            public override int UnIndentedWidth =>
                2 // '{ '
                + this.Fields.Sum(p => p.UnIndentedWidth) // '{key} : {value}'
                + (this.Fields.Count - 1) * 2 // '; ' (kv seperators)
                + 2; // ' }'

            public override void WriteText(IndentedTextWriter writer, bool indented)
            {
                writer.Write($"{{");
                this.WriteMultiple(writer, this.Fields, ";", indented);
                writer.Write("}");
            }
        }

        private record CompoundTypeTextComponent(TextComponentBase Type, string Seperator, TextComponentBase InnerValue, CandidId? RecursiveId) : TextComponentBase
        {
            public override int UnIndentedWidth =>
                this.RecursiveId != null ?  this.RecursiveId.Value.Length + 2 : 0 // 'μ{recursiveId}.' or ''
                + this.Type.UnIndentedWidth // '{type}'
                + this.Seperator.Length // '{seperator}'
                + this.InnerValue.UnIndentedWidth; // '{value}'

            public override void WriteText(IndentedTextWriter writer, bool indented)
            {
                bool b = this.InnerValue is CurlyBraceTypeTextComponent<KeyValueTextComponent> c && c.Fields.Count == 4;
                if (this.RecursiveId != null)
                {
                    writer.Write("μ");
                    writer.Write(this.RecursiveId.ToString());
                    writer.Write(".");
                }

                this.Type.WriteText(writer, indented);
                writer.Write(this.Seperator);
                this.InnerValue.WriteText(writer, indented);
            }
        }

        private record ConstantTextComponent(string Value) : TextComponentBase
        {
            public override int UnIndentedWidth => this.Value.Length;

            public override void WriteText(IndentedTextWriter writer, bool indented)
            {
                writer.Write(this.Value);
            }
        }
    }
}
