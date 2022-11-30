using EdjCase.ICP.Candid.Models.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static EdjCase.ICP.ClientGenerator.TypeSourceConverter;
namespace ICP.ClientGenerator
{
    internal abstract class TypeSourceDescriptor
    {
        public string Name { get; }
        public List<TypeSourceDescriptor> SubTypesToCreate { get; }

        protected TypeSourceDescriptor(string name, TypeSourceDescriptor? subTypesToCreate)
        {
            this.Name = name ?? throw new ArgumentNullException(nameof(name));
            this.SubTypesToCreate = new List<TypeSourceDescriptor>();
            if (subTypesToCreate != null)
            {
                this.SubTypesToCreate.Add(subTypesToCreate);
            }
        }
        protected TypeSourceDescriptor(string name, List<TypeSourceDescriptor> subTypesToCreate)
        {
            this.Name = name ?? throw new ArgumentNullException(nameof(name));
            this.SubTypesToCreate = subTypesToCreate ?? throw new ArgumentNullException(nameof(subTypesToCreate));
        }
    }

    internal class VariantSourceDescriptor : TypeSourceDescriptor
    {
        public List<(string Name, string? InfoFullTypeName)> Options { get; }

        public VariantSourceDescriptor(
            string name,
            List<(string Name, string? InfoTypeName)> options,
            List<TypeSourceDescriptor> subTypesToCreate)
            : base(name, subTypesToCreate)
        {
            if (options?.Any() != true)
            {
                throw new ArgumentNullException(nameof(options));
            }
            this.Options = options;
        }
    }

    internal class RecordSourceDescriptor : TypeSourceDescriptor
    {
        public List<(string Name, string FullTypeName)> Fields { get; }

        public RecordSourceDescriptor(string name, List<(string Name, string FullTypeName)> fields, List<TypeSourceDescriptor> subTypesToCreate)
            : base(name, subTypesToCreate)
        {
            this.Fields = fields ?? throw new ArgumentNullException(nameof(fields));
        }
    }

    internal class ServiceSourceDescriptor : TypeSourceDescriptor
    {
        public Dictionary<string, FuncSourceDescriptor> Methods { get; }

        public ServiceSourceDescriptor(string name, Dictionary<string, FuncSourceDescriptor> methods, List<TypeSourceDescriptor> subTypesToCreate)
            : base(name, subTypesToCreate)
        {
            this.Methods = methods ?? throw new ArgumentNullException(nameof(methods));
        }
    }

	internal class FuncSourceDescriptor : TypeSourceDescriptor
	{
		public FuncSourceDescriptor(
			string name,
			bool isFireAndForget,
			bool isQuery,
			List<ParameterInfo> parameters,
			List<ParameterInfo> returnParameters,
			List<TypeSourceDescriptor> subTypesToCreate)
			: base(name, subTypesToCreate)
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

		//(ServiceSourceDescriptor.Method Method, List<TypeSourceDescriptor> SubTypesToCreate)
		public class ParameterInfo
		{
			public string VariableName { get; }
			public string? TypeName { get; }
			public ParameterInfo(string variableName, string? typeName)
			{
				this.VariableName = variableName ?? throw new ArgumentNullException(nameof(variableName));
				this.TypeName = typeName;
			}
		}
	}
}
