using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdjCase.ICP.ClientGenerator
{
	public class TypedValueName
	{
		public TypeName Type { get; }
		public ValueName Value { get; }
		public TypedValueName(TypeName type, ValueName value)
		{
			this.Type = type ?? throw new ArgumentNullException(nameof(type));
			this.Value = value ?? throw new ArgumentNullException(nameof(value));
		}
	}

}
