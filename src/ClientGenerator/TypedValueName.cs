using System;

namespace EdjCase.ICP.ClientGenerator
{
	internal class TypedValueName
	{
		public TypeName Type { get; }
		public ResolvedName Value { get; }
		public TypedValueName(TypeName type, ResolvedName value)
		{
			this.Type = type;
			this.Value = value ?? throw new ArgumentNullException(nameof(value));
		}
	}

}
