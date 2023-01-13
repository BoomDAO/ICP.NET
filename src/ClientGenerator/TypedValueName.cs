using System;

namespace EdjCase.ICP.ClientGenerator
{
	internal class TypedValueName
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
