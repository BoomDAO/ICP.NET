using EdjCase.ICP.Candid.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdjCase.ICP.Candid.Exceptions
{
	public class CandidTextParseException : Exception
	{
		public string ParseError { get; }
		public CandidTextParseException(string message)
		{
			this.ParseError = message;
		}

		public override string Message
		{

			get
			{
				return "Failed to parse Candid text at . Error: " + this.ParseError;
			}

		}
	}
}
