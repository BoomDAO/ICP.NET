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
	internal interface ITypeSourceDescriptor
	{
		public IEnumerable<NamedTypeSourceDescriptor> GetSubTypes();
	}

	internal class NamedTypeSourceDescriptor
	{
		public TypeName Name { get; }
		public ITypeSourceDescriptor Type { get; }

		public NamedTypeSourceDescriptor(TypeName name, ITypeSourceDescriptor type)
		{
			this.Name = name ?? throw new ArgumentNullException(nameof(name));
			this.Type = type ?? throw new ArgumentNullException(nameof(type));
		}
	}


	internal class PrimitiveSourceDescriptor : ITypeSourceDescriptor
	{
		public TypeName? Type { get; set; } // Null means empty, reserved or null
		public PrimitiveSourceDescriptor(TypeName? type)
		{
			this.Type = type;
		}

		public IEnumerable<NamedTypeSourceDescriptor> GetSubTypes()
		{
			return Enumerable.Empty<NamedTypeSourceDescriptor>();
		}
	}
	internal class ReferenceSourceDescriptor : ITypeSourceDescriptor
	{
		public CandidId Reference { get; set; }
		public ReferenceSourceDescriptor(CandidId reference)
		{
			this.Reference = reference;
		}
		public IEnumerable<NamedTypeSourceDescriptor> GetSubTypes()
		{
			return Enumerable.Empty<NamedTypeSourceDescriptor>();
		}
	}
	internal class VectorSourceDescriptor : ITypeSourceDescriptor
	{
		public NamedTypeSourceDescriptor InnerType { get; set; }
		public VectorSourceDescriptor(NamedTypeSourceDescriptor innerType)
		{
			this.InnerType = innerType;
		}

		public IEnumerable<NamedTypeSourceDescriptor> GetSubTypes()
		{
			yield return this.InnerType;
		}
	}

	internal class OptionalSourceDescriptor : ITypeSourceDescriptor
	{
		public NamedTypeSourceDescriptor InnerType { get; set; }
		public OptionalSourceDescriptor(NamedTypeSourceDescriptor innerType)
		{
			this.InnerType = innerType;
		}

		public IEnumerable<NamedTypeSourceDescriptor> GetSubTypes()
		{
			yield return this.InnerType;
		}
	}

	internal class VariantSourceDescriptor : ITypeSourceDescriptor
	{
		public List<(ValueName Name, NamedTypeSourceDescriptor Type)> Options { get; }

		public VariantSourceDescriptor(List<(ValueName Name, NamedTypeSourceDescriptor Type)> options)
		{
			this.Options = options ?? new();
		}

		public IEnumerable<NamedTypeSourceDescriptor> GetSubTypes()
		{
			return this.Options
				.Where(o => o.Type != null)
				.Select(o => o.Type!);
		}
	}

	internal class RecordSourceDescriptor : ITypeSourceDescriptor
	{
		public List<(ValueName Name, NamedTypeSourceDescriptor Type)> Fields { get; }

		public RecordSourceDescriptor(List<(ValueName Name, NamedTypeSourceDescriptor Type)> fields)
		{
			this.Fields = fields ?? new();
		}

		public IEnumerable<NamedTypeSourceDescriptor> GetSubTypes()
		{
			return this.Fields.Select(o => o.Type);
		}
	}

	internal class ServiceSourceDescriptor : ITypeSourceDescriptor
	{
		public List<(string Name, TypeName FuncType, FuncSourceDescriptor FuncDesc)> Methods { get; }

		public ServiceSourceDescriptor(List<(string Name, TypeName FuncType, FuncSourceDescriptor FuncDesc)> methods)
		{
			this.Methods = methods ?? throw new ArgumentNullException(nameof(methods));
		}

		public IEnumerable<NamedTypeSourceDescriptor> GetSubTypes()
		{
			return this.Methods.Select(o => new NamedTypeSourceDescriptor(o.FuncType, o.FuncDesc));
		}
	}

	internal class FuncSourceDescriptor : ITypeSourceDescriptor
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


		public IEnumerable<NamedTypeSourceDescriptor> GetSubTypes()
		{
			return Enumerable.Empty<NamedTypeSourceDescriptor>();
		}

		public class ParameterInfo
		{
			public ValueName Name { get; }
			public NamedTypeSourceDescriptor Type { get; }
			public ParameterInfo(ValueName name, NamedTypeSourceDescriptor type)
			{
				this.Name = name ?? throw new ArgumentNullException(nameof(name));
				this.Type = type;
			}
		}
	}
}
