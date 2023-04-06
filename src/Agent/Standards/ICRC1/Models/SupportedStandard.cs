using EdjCase.ICP.Candid.Mapping;
using System;

namespace EdjCase.ICP.Agent.Standards.ICRC1.Models
{
	/// <summary>
	/// This class represents a supported standard with a name and URL
	/// </summary>
	public class SupportedStandard
	{
		/// <summary>
		/// The name of the supported standard
		/// </summary>
		[CandidName("name")]
		public string Name { get; set; }

		/// <summary>
		/// The Url of the supported standard
		/// </summary>
		[CandidName("url")]
		public string Url { get; set; }

		/// <summary>
		/// Primary constructor
		/// </summary>
		/// <param name="name">The name of the supported standard</param>
		/// <param name="url">The URL of the supported standard</param>
		public SupportedStandard(string name, string url)
		{
			this.Name = name ?? throw new ArgumentNullException(nameof(name));
			this.Url = url ?? throw new ArgumentNullException(nameof(url));
		}
	}
}
