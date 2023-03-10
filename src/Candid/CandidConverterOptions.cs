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
		/// <summary>
		/// List of custom mappers to use instead of the default mappers provided.
		/// Order does matter, FIFO
		/// </summary>
		public List<ICandidValueMapper> CustomMappers { get; set; } = new List<ICandidValueMapper>();

		/// <summary>
		/// Helper method to add a custom mapper
		/// </summary>
		/// <param name="mapper">Candid mapper to add</param>
		public void AddCustomMapper(ICandidValueMapper mapper)
		{
			this.CustomMappers.Add(mapper);
		}

		/// <summary>
		/// Helper method to add a custom mapper by type. Requires the type to 
		/// be `ICandidValueMapper` and it has an empty constrcutor
		/// </summary>
		/// <typeparam name="T"></typeparam>
		public void AddCustomMapper<T>()
			where T : ICandidValueMapper, new()
		{
			this.AddCustomMapper(new T());
		}
	}
}
