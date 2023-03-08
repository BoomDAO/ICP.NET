using EdjCase.ICP.Candid.Mapping;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace EdjCase.ICP.Candid
{
	/// <summary>
	/// Options for configuring how candid is convertered
	/// </summary>
	public class CandidConverterOptions
	{
		public List<ICandidValueMapper> CustomMappers { get; set; } = new List<ICandidValueMapper>();

		public void AddCustomMapper(ICandidValueMapper mapper)
		{
			this.CustomMappers.Add(mapper);
		}

		public void AddCustomMapper<T>()
			where T : ICandidValueMapper, new()
		{
			this.AddCustomMapper(new T());
		}

		public static CandidConverterOptions Default() => new CandidConverterOptions();
	}
}
