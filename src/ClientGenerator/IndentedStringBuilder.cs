using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdjCase.ICP.ClientGenerator
{
	internal class IndentedStringBuilder
	{
		private readonly StringBuilder stringBuilder = new StringBuilder();
		private int indent = 0;

		public void AppendLine(string value)
		{
			foreach (int _ in Enumerable.Range(0, this.indent))
			{
				this.stringBuilder.Append("\t");
			}
			this.stringBuilder.Append(value);
			this.stringBuilder.Append('\n');
		}

		public IDisposable Indent()
		{
			this.indent += 1;
			return new Disposable(this);
		}

		public override string ToString()
		{
			return this.stringBuilder.ToString();
		}

		private class Disposable : IDisposable
		{
			private readonly IndentedStringBuilder builder;
			public Disposable(IndentedStringBuilder builder)
			{
				this.builder = builder;
			}

			public void Dispose()
			{
				this.builder.indent -= 1;
			}
		}
	}
}
