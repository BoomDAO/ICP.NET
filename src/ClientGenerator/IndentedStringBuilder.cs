using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICP.ClientGenerator
{
    internal class IndentedStringBuilder
    {
        private readonly StringBuilder stringBuilder = new StringBuilder();

        public void AppendLine(string value)
        {
            this.stringBuilder.AppendLine(value);
        }

        public override string ToString()
        {
            return this.stringBuilder.ToString();
        }

    }
}
