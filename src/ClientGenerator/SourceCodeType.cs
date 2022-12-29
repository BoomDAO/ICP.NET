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
	public abstract class SourceCodeType
	{
	}

	public class NullEmptyOrReservedSourceCodeType : SourceCodeType
	{

	}


	public class CsharpTypeSourceCodeType : SourceCodeType
	{
		public Type Type { get; }
		public List<SourceCodeType> GenericTypes { get; }

		public CsharpTypeSourceCodeType(Type type, SourceCodeType? genericType = null)
		{
			this.Type = type ?? throw new ArgumentNullException(nameof(type));
			this.GenericTypes = genericType == null ? new List<SourceCodeType>() : new List<SourceCodeType> { genericType };
		}
		public CsharpTypeSourceCodeType(Type type, List<SourceCodeType> genericTypes)
		{
			this.Type = type ?? throw new ArgumentNullException(nameof(type));
			this.GenericTypes = genericTypes ?? new List<SourceCodeType>();
		}
	}

	public class ReferenceSourceCodeType : SourceCodeType
	{
		public CandidId Id { get; }
		public ReferenceSourceCodeType(CandidId id)
		{
			this.Id = id ?? throw new ArgumentNullException(nameof(id));
		}
	}


	public class RecordSourceCodeType : SourceCodeType
	{
		public List<(ValueName Tag, SourceCodeType Type)> Fields { get; }


		public RecordSourceCodeType(List<(ValueName Tag, SourceCodeType Type)> fields)
		{
			this.Fields = fields ?? throw new ArgumentNullException(nameof(fields));
		}
	}

	public class VariantSourceCodeType : SourceCodeType
	{
		public List<(ValueName Tag, SourceCodeType Type)> Options { get; }

		public VariantSourceCodeType(List<(ValueName Tag, SourceCodeType Type)> options)
		{
			this.Options = options ?? throw new ArgumentNullException(nameof(options));
		}
	}

	public class ServiceSourceCodeType : SourceCodeType
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
			public bool IsFireAndForget { get; set; }
			public bool IsQuery { get; set; }

			public Func(
				List<(ValueName Name, SourceCodeType Type)> argTypes,
				List<(ValueName Name, SourceCodeType Type)> returnTypes,
				bool isFireAndForget,
				bool isQuery
			)
			{
				this.ArgTypes = argTypes ?? throw new ArgumentNullException(nameof(argTypes));
				this.ReturnTypes = returnTypes ?? throw new ArgumentNullException(nameof(returnTypes));
				this.IsFireAndForget = isFireAndForget;
				this.IsQuery = isQuery;
			}
		}
	}
}
