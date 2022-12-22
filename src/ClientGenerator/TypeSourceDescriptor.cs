using EdjCase.ICP.Candid.Models;
using EdjCase.ICP.Candid.Models.Types;
using EdjCase.ICP.Candid.Models.Values;
using EdjCase.ICP.ClientGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICP.ClientGenerator
{
	internal abstract class TypeSourceDescriptor
	{
	}

	internal abstract class GenericSourceDescriptor : TypeSourceDescriptor
	{
		public TypeName InnerType { get; set; }
		public GenericSourceDescriptor(TypeName innerType)
		{
			this.InnerType = innerType;
		}
	}

	internal class PrimitiveSourceDescriptor : TypeSourceDescriptor
	{
		public TypeName? Type { get; set; } // Null means empty, reserved or null
		public PrimitiveSourceDescriptor(TypeName? type)
		{
			this.Type = type;
		}
	}
	internal class ReferenceSourceDescriptor : TypeSourceDescriptor
	{
		public TypeName Reference { get; set; }
		public ReferenceSourceDescriptor(TypeName reference)
		{
			this.Reference = reference;
		}
	}
	internal class VectorSourceDescriptor : GenericSourceDescriptor
	{
		public VectorSourceDescriptor(TypeName innerType) : base(innerType)
		{
		}
	}

	internal class OptionalSourceDescriptor : GenericSourceDescriptor
	{
		public OptionalSourceDescriptor(TypeName innerType) : base(innerType)
		{
		}
	}

	internal class VariantSourceDescriptor : TypeSourceDescriptor
	{
		public List<(ValueName Name, TypeName? Type)> Options { get; }

		public VariantSourceDescriptor(List<(ValueName Name, TypeName? Type)> options)
		{
			this.Options = options ?? new();
		}
	}

	internal class RecordSourceDescriptor : TypeSourceDescriptor
	{
		public List<(ValueName Name, TypeName Type)> Fields { get; }

		public RecordSourceDescriptor(List<(ValueName Name, TypeName Type)> fields)
		{
			this.Fields = fields ?? new();
		}
	}

	internal class ServiceSourceDescriptor : TypeSourceDescriptor
	{
		public List<(string Name, TypeName FuncType, FuncSourceDescriptor FuncDesc)> Methods { get; }

		public ServiceSourceDescriptor(List<(string Name, TypeName FuncType, FuncSourceDescriptor FuncDesc)> methods)
		{
			this.Methods = methods ?? throw new ArgumentNullException(nameof(methods));
		}
	}

	internal class FuncSourceDescriptor : TypeSourceDescriptor
	{
		public FuncSourceDescriptor(
			bool isFireAndForget,
			bool isQuery,
			List<ParameterInfo> parameters,
			List<ParameterInfo> returnParameters)
		{
			this.IsFireAndForget = isFireAndForget;
			this.IsQuery = isQuery;
			this.Parameters = parameters ?? throw new ArgumentNullException(nameof(parameters));
			this.ReturnParameters = returnParameters ?? throw new ArgumentNullException(nameof(returnParameters));
		}

		public bool IsFireAndForget { get; }
		public bool IsQuery { get; }
		public List<ParameterInfo> Parameters { get; }
		public List<ParameterInfo> ReturnParameters { get; }


		public class ParameterInfo
		{
			public ValueName Name { get; }
			public TypeName? Type { get; }
			public bool IsOpt { get; }
			public ParameterInfo(ValueName name, TypeName? type, bool isOpt)
			{
				this.Name = name ?? throw new ArgumentNullException(nameof(name));
				this.Type = type;
				this.IsOpt = isOpt;
			}
		}
	}
}
