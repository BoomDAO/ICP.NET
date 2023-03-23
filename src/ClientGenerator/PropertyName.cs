using EdjCase.ICP.Candid.Models;
using EdjCase.ICP.Candid.Models.Values;
using System;
using System.Collections.Generic;

namespace EdjCase.ICP.ClientGenerator
{
	internal class ValueName : IEquatable<ValueName>
	{
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

		public string PropertyName { get; }
		public string VariableName { get; }
		public CandidTag CandidTag { get; }

		public ValueName(string propertyName, string variableName, CandidTag candidTag)
		{
			this.PropertyName = propertyName;
			this.VariableName = variableName;
			this.CandidTag = candidTag;
		}

		public override string ToString()
		{
			return this.CandidTag + "/" + this.PropertyName;
		}

		public override bool Equals(object? obj)
		{
			return this.Equals(obj as ValueName);
		}

		public bool Equals(ValueName? other)
		{
			return this.CandidTag == other?.CandidTag;
		}

		public override int GetHashCode()
		{
			return this.CandidTag.GetHashCode();
		}

		public static ValueName Default(CandidTag tag, bool keepCandidCase)
		{
			string stringValue;
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
			if (char.IsNumber(stringValue[0]))
			{
				// If Its a number, prefix it
				stringValue = "F" + stringValue;
			}
			string propertyName = !keepCandidCase
				? StringUtil.ToPascalCase(stringValue)
				: stringValue;
			string variableName = StringUtil.ToCamelCase(stringValue);
			if (IsKeyword(propertyName))
			{
				// Add @ before reserved words
				propertyName = "@" + propertyName;
			}
			if (IsKeyword(variableName))
			{
				// Add @ before reserved words
				variableName = "@" + variableName;
			}

			return new ValueName(propertyName, variableName, tag);
		}

		private static bool IsKeyword(string value)
		{
			// TODO better way to check for reserved names
			return ReservedWords.Contains(value);
		}
	}

}
