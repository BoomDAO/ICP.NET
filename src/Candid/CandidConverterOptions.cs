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
		public List<ICandidValueMapper>? CustomMappers { get; set; } 

		public static CandidConverterOptions Default() => new CandidConverterOptions();
	}
}
