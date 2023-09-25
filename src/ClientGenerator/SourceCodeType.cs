using EdjCase.ICP.Candid.Models;
using EdjCase.ICP.ClientGenerator;
using System;
using System.Collections.Generic;

namespace EdjCase.ICP.ClientGenerator
{
	internal abstract class SourceCodeType
	{
		public abstract bool IsPredefinedType { get; }

	}

	internal class NonGenericSourceCodeType : SourceCodeType
	{
		public Type Type { get; }
		public override bool IsPredefinedType { get; } = true;

		public NonGenericSourceCodeType(Type primitiveType)
		{
			this.Type = primitiveType;
		}
	}

	internal class ArraySourceCodeType : SourceCodeType
	{
		public SourceCodeType? GenericType { get; }

		public override bool IsPredefinedType { get; } = true;

		public ArraySourceCodeType(SourceCodeType? genericType)
		{
			this.GenericType = genericType;
		}
	}

	internal class ListSourceCodeType : SourceCodeType
	{
		public SourceCodeType GenericType { get; }

		public override bool IsPredefinedType { get; } = true;

		public ListSourceCodeType(SourceCodeType genericType)
		{
			this.GenericType = genericType;
		}
	}

	internal class OptionalValueSourceCodeType : SourceCodeType
	{
		public SourceCodeType GenericType { get; }

		public override bool IsPredefinedType { get; } = true;

		public OptionalValueSourceCodeType(SourceCodeType genericType)
		{
			this.GenericType = genericType;
		}
	}

	internal class DictionarySourceCodeType : SourceCodeType
	{
		public SourceCodeType KeyType { get; }
		public SourceCodeType ValueType { get; }

		public override bool IsPredefinedType { get; } = true;

		public DictionarySourceCodeType(SourceCodeType keyType, SourceCodeType valueType)
		{
			this.KeyType = keyType;
			this.ValueType = valueType;
		}
	}

	internal class ReferenceSourceCodeType : SourceCodeType
	{
		public CandidId Id { get; }

		public override bool IsPredefinedType { get; } = true;
		public ReferenceSourceCodeType(CandidId id)
		{
			this.Id = id ?? throw new ArgumentNullException(nameof(id));
		}


	}

	internal class TupleSourceCodeType : SourceCodeType
	{
		public List<SourceCodeType> Fields { get; }

		public override bool IsPredefinedType { get; } = true;

		public TupleSourceCodeType(List<SourceCodeType> fields)
		{
			this.Fields = fields ?? throw new ArgumentNullException(nameof(fields));
		}
	}

	internal class RecordSourceCodeType : SourceCodeType
	{
		public List<(ResolvedName Tag, SourceCodeType Type)> Fields { get; }


		public override bool IsPredefinedType { get; } = false;

		public RecordSourceCodeType(List<(ResolvedName Tag, SourceCodeType Type)> fields)
		{
			this.Fields = fields ?? throw new ArgumentNullException(nameof(fields));
		}
	}

	internal class VariantSourceCodeType : SourceCodeType
	{
		public List<(ResolvedName Tag, SourceCodeType? Type)> Options { get; }
		public override bool IsPredefinedType { get; } = false;

		public VariantSourceCodeType(
			List<(ResolvedName Tag, SourceCodeType? Type)> options
		)
		{
			this.Options = options ?? throw new ArgumentNullException(nameof(options));
		}
	}

	internal class ServiceSourceCodeType : SourceCodeType
	{
		public List<(string CsharpName, string CandidName, Func FuncInfo)> Methods { get; set; }
		public override bool IsPredefinedType { get; } = false;
		public ServiceSourceCodeType(List<(string CsharpName, string CandidName, Func FuncInfo)> methods)
		{
			this.Methods = methods ?? throw new ArgumentNullException(nameof(methods));
		}

		public class Func
		{
			public List<(ResolvedName Name, SourceCodeType Type)> ArgTypes { get; internal set; }
			public List<(ResolvedName Name, SourceCodeType Type)> ReturnTypes { get; internal set; }
			public bool IsOneway { get; set; }
			public bool IsQuery { get; set; }

			public Func(
				List<(ResolvedName Name, SourceCodeType Type)> argTypes,
				List<(ResolvedName Name, SourceCodeType Type)> returnTypes,
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
