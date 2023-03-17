using EdjCase.ICP.Candid.Models;
using EdjCase.ICP.ClientGenerator;
using System;
using System.Collections.Generic;

namespace EdjCase.ICP.ClientGenerator
{
	internal abstract class SourceCodeType
	{
	}

	internal class CompiledTypeSourceCodeType : SourceCodeType
	{
		public Type Type { get; }
		public SourceCodeType? GenericType { get; }

		public CompiledTypeSourceCodeType(Type type, SourceCodeType? genericType = null)
		{
			this.Type = type ?? throw new ArgumentNullException(nameof(type));
			this.GenericType = genericType;
		}
	}

	internal class ReferenceSourceCodeType : SourceCodeType
	{
		public CandidId Id { get; }
		public ReferenceSourceCodeType(CandidId id)
		{
			this.Id = id ?? throw new ArgumentNullException(nameof(id));
		}
	}


	internal class RecordSourceCodeType : SourceCodeType
	{
		public List<(ValueName Tag, SourceCodeType Type)> Fields { get; }


		public RecordSourceCodeType(List<(ValueName Tag, SourceCodeType Type)> fields)
		{
			this.Fields = fields ?? throw new ArgumentNullException(nameof(fields));
		}
	}

	internal class VariantSourceCodeType : SourceCodeType
	{
		public List<(ValueName Tag, SourceCodeType? Type)> Options { get; }

		public VariantSourceCodeType(List<(ValueName Tag, SourceCodeType? Type)> options)
		{
			this.Options = options ?? throw new ArgumentNullException(nameof(options));
		}
	}

	internal class ServiceSourceCodeType : SourceCodeType
	{
		public List<(ValueName Name, Func FuncInfo)> Methods { get; set; }
		public ServiceSourceCodeType(List<(ValueName Name, Func FuncInfo)> methods)
		{
			this.Methods = methods ?? throw new ArgumentNullException(nameof(methods));
		}

		public class Func
		{
			public List<(ValueName Name, SourceCodeType Type)> ArgTypes { get; internal set; }
			public List<(ValueName Name, SourceCodeType Type)> ReturnTypes { get; internal set; }
			public bool IsOneway { get; set; }
			public bool IsQuery { get; set; }

			public Func(
				List<(ValueName Name, SourceCodeType Type)> argTypes,
				List<(ValueName Name, SourceCodeType Type)> returnTypes,
				bool isOneway,
				bool isQuery
			)
			{
				this.ArgTypes = argTypes ?? throw new ArgumentNullException(nameof(argTypes));
				this.ReturnTypes = returnTypes ?? throw new ArgumentNullException(nameof(returnTypes));
				this.IsOneway = isOneway;
				this.IsQuery = isQuery;
			}
		}
	}
}
