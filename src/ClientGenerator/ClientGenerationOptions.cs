using EdjCase.ICP.Candid.Models.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdjCase.ICP.ClientGenerator
{
	/// <summary>
	/// Options for generating a client
	/// </summary>
	public class ClientGenerationOptions
	{
		/// <summary>
		/// The name of the client class and file to use
		/// </summary>
		public string Name { get; }
		/// <summary>
		/// The base namespace to use in the generated files
		/// </summary>
		public string Namespace { get; }
		/// <summary>
		/// If true, there will be no folders, all files will be in the same directory
		/// </summary>
		public bool NoFolders { get; }

		/// <param name="name">The name of the client class and file to use</param>
		/// <param name="namespace">The base namespace to use in the generated files</param>
		/// <param name="noFolders">If true, there will be no folders, all files will be in the same directory</param>
		public ClientGenerationOptions(string name, string @namespace, bool noFolders)
		{
			if (string.IsNullOrWhiteSpace(name))
			{
				throw new ArgumentNullException(nameof(name));
			}
			// Trim whitespace and replace spaces with underscores
			this.Name = StringUtil.ToPascalCase(name.Trim().Replace(' ', '_'));
			this.Namespace = @namespace ?? throw new ArgumentNullException(nameof(@namespace));
			this.NoFolders = noFolders;
		}
	}
}