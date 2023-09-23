using EdjCase.ICP.Candid.Models;
using EdjCase.ICP.Candid.Models.Values;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace EdjCase.ICP.ClientGenerator
{
	internal class ResolvedName : IEquatable<ResolvedName>
	{
		public string Name { get; }
		public CandidTag CandidTag { get; }

		public ResolvedName(string name, CandidTag candidTag)
		{
			this.Name = name;
			this.CandidTag = candidTag;
		}

		public override string ToString()
		{
			return this.CandidTag + "/" + this.Name;
		}

		public override bool Equals(object? obj)
		{
			return this.Equals(obj as ResolvedName);
		}

		public bool Equals(ResolvedName? other)
		{
			return this.CandidTag == other?.CandidTag;
		}

		public override int GetHashCode()
		{
			return this.CandidTag.GetHashCode();
		}

		internal string ToCamelCase()
		{
			return StringUtil.ToCamelCase(this.Name);
		}
	}

	internal class NameHelper
	{
		public bool KeepCandidCase { get; }
		public NameHelper(bool keepCandidCase)
		{
			this.KeepCandidCase = keepCandidCase;
		}

		public ResolvedName ResolveName(CandidTag tag, string? nameOverride = null)
		{
			string stringValue;
			if (nameOverride != null)
			{
				stringValue = nameOverride;
			}
			else
			{
				if (tag.Name != null)
				{
					stringValue = tag.Name;
					bool isQuoted = stringValue.StartsWith("\"") && stringValue.EndsWith("\"");
					if (isQuoted)
					{
						stringValue = stringValue.Trim('"');
					}
				}
				else
				{
					stringValue = tag.Id.ToString();
				}
			}
			if (char.IsNumber(stringValue[0]))
			{
				// If Its a number, prefix it
				stringValue = "F" + stringValue;
			}
			stringValue = this.KeepCandidCase ? stringValue : StringUtil.ToPascalCase(stringValue);
			stringValue = Escape(stringValue);
			return new ResolvedName(stringValue, tag);
		}

		private static string Escape(string value)
		{
			if (IsKeyword(value))
			{
				// Add @ before reserved words
				value = "@" + value;
			}
			if (value.StartsWith("set_") || value.StartsWith("get_"))
			{
				// Add _ before getters/setters words
				value = "_" + value;
			}
			return value;
		}


		private static bool IsKeyword(string value)
		{
			// TODO better way to check for reserved names
			return ReservedWords.Contains(value);
		}

		internal string ToCamelCase(string name)
		{
			return Escape(StringUtil.ToCamelCase(name));
		}

		internal static readonly HashSet<string> ReservedWords = new()
		{
			"abstract",
			"bool",
			"continue",
			"decimal",
			"default",
			"event",
			"explicit",
			"extern",
			"char",
			"checked",
			"class",
			"const",
			"break",
			"as",
			"base",
			"delegate",
			"is",
			"lock",
			"long",
			"num",
			"byte",
			"case",
			"catch",
			"false",
			"finally",
			"fixed",
			"float",
			"for",
			"as",
			"foreach",
			"goto",
			"if",
			"implicit",
			"in",
			"int",
			"interface",
			"internal",
			"do",
			"double",
			"else",
			"namespace",
			"new",
			"null",
			"object",
			"operator",
			"out",
			"override",
			"params",
			"private",
			"protected",
			"public",
			"readonly",
			"sealed",
			"short",
			"sizeof",
			"ref",
			"return",
			"sbyte",
			"stackalloc",
			"static",
			"string",
			"struct",
			"void",
			"volatile",
			"while",
			"true",
			"try",
			"switch",
			"this",
			"throw",
			"unchecked",
			"unsafe",
			"ushort",
			"using",
			"using static",
			"virtual",
			"typeof",
			"uint",
			"ulong",
			"out"

		};
	}

}
