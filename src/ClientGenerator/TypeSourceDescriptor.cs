using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICP.ClientGenerator
{
    internal class DeclaredTypeSourceDescriptor
    {
        public string Namespace { get; }
        public TypeSourceDescriptor Type { get; }

        public DeclaredTypeSourceDescriptor(string @namespace, TypeSourceDescriptor type)
        {
            this.Namespace = @namespace ?? throw new ArgumentNullException(nameof(@namespace));
            this.Type = type ?? throw new ArgumentNullException(nameof(type));
        }
    }

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
            List<(string Name, string? InfoFullTypeName)> options,
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
        public List<Method> Methods { get; }

        public ServiceSourceDescriptor(string name, List<Method> methods, List<TypeSourceDescriptor> subTypesToCreate)
            : base(name, subTypesToCreate)
        {
            this.Methods = methods ?? throw new ArgumentNullException(nameof(methods));
        }


        public class Method
        {
            public string Name { get; }
            public bool IsFireAndForget { get; }
            public bool IsQuery { get; }
            public List<(string Name, string FullTypeName)> Parameters { get; }
            public List<(string Name, string FullTypeName)> ReturnParameters { get; }

            public Method(
                string name,
                bool isFireAndForget,
                bool isQuery,
                List<(string Name, string FullTypeName)> parameters,
                List<(string Name, string FullTypeName)> returnParameters)
            {
                this.Name = name ?? throw new ArgumentNullException(nameof(name));
                this.IsFireAndForget = isFireAndForget;
                this.IsQuery = isQuery;
                this.Parameters = parameters ?? throw new ArgumentNullException(nameof(parameters));
                this.ReturnParameters = returnParameters ?? throw new ArgumentNullException(nameof(returnParameters));
            }
        }
    }

}
