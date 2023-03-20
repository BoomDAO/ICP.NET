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
		public string CandidName { get; }

		public ValueName(string propertyName, string variableName, string candidName)
		{
			this.PropertyName = propertyName;
			this.VariableName = variableName;
			this.CandidName = candidName;
		}

		public override string ToString()
		{
			return this.CandidName + "/" + this.PropertyName;
		}

		public override bool Equals(object? obj)
		{
			return this.Equals(obj as ValueName);
		}

		public bool Equals(ValueName? other)
		{
			return this.CandidName == other?.CandidName;
		}

		public override int GetHashCode()
		{
			return this.CandidName.GetHashCode();
		}

		public static ValueName Default(CandidTag value, bool keepCandidCase)
		{
			return Default(value.Name ?? value.Id.ToString(), keepCandidCase);
		}
		public static ValueName Default(string value, bool keepCandidCase)
		{
			bool isQuoted = value.StartsWith("\"") && value.EndsWith("\"");
			if (isQuoted)
			{
				value = value.Trim('"');
			}
			string candidName = value;
			if (char.IsNumber(value[0]))
			{
				// If Its a number, prefix it
				value = "F" + value;
			}
			string propertyName = !keepCandidCase
				? StringUtil.ToPascalCase(value)
				: value;
			string variableName = StringUtil.ToCamelCase(value);
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

			return new ValueName(propertyName, variableName, candidName);
		}

		private static bool IsKeyword(string value)
		{
			// TODO better way to check for reserved names
			return ReservedWords.Contains(value);
		}
	}

}
