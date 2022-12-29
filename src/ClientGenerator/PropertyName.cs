using EdjCase.ICP.Candid.Models;
using EdjCase.ICP.Candid.Models.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdjCase.ICP.ClientGenerator
{
	public class ValueName
	{
		public string PascalCaseValue { get; }
		public string CamelCaseValue { get; }
		public string CandidName { get; }

		private ValueName(string propertyName, string variableName, string candidName)
		{
			this.PascalCaseValue = propertyName;
			this.CamelCaseValue = variableName;
			this.CandidName = candidName;
		}

		public static ValueName Default(CandidTag value)
		{
			return Default(value.Name ?? value.Id.ToString());
		}
		public static ValueName Default(string value)
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
			string propertyName = StringUtil.ToPascalCase(value);
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
			switch (value)
			{
				case "short":
				case "int":
				case "long":
				case "ushort":
				case "uint":
				case "ulong":
				case "string":
				case "new":
				case "bool":
					return true;
				default:
					return false;
			}
		}
	}

}
