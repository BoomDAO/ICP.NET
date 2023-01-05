using EdjCase.ICP.Candid.Models.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ICP.Candid.Tests
{
	public class StringUtilTests
	{
		[Theory]
		[InlineData("A", "a")]
		[InlineData("AA", "aA")]
		[InlineData("camelCase", "camelCase")]
		[InlineData("PascalCase", "pascalCase")]
		[InlineData("snake_case", "snakeCase")]
		[InlineData("spinal-case", "spinalCase")]
		[InlineData("TestAAATest", "testAAATest")]
		public void ToCamelCase(string text, string expected)
		{
			string actual = StringUtil.ToCamelCase(text);
			Assert.Equal(expected, actual);
		}

		[Theory]
		[InlineData("A", "A")]
		[InlineData("AA", "AA")]
		[InlineData("camelCase", "CamelCase")]
		[InlineData("PascalCase", "PascalCase")]
		[InlineData("snake_case", "SnakeCase")]
		[InlineData("spinal-case", "SpinalCase")]
		[InlineData("TestAAATest", "TestAAATest")]
		public void ToPascalCase(string text, string expected)
		{
			string actual = StringUtil.ToPascalCase(text);
			Assert.Equal(expected, actual);
		}

		[Theory]
		[InlineData("A", "a")]
		[InlineData("AA", "a_a")]
		[InlineData("camelCase", "camel_case")]
		[InlineData("PascalCase", "pascal_case")]
		[InlineData("snake_case", "snake_case")]
		[InlineData("spinal-case", "spinal_case")]
		[InlineData("TestAAATest", "test_a_a_a_test")]
		public void ToSnakeCase(string text, string expected)
		{
			string actual = StringUtil.ToSnakeCase(text);
			Assert.Equal(expected, actual);
		}
	}
}
