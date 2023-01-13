using System;
using System.Collections.Generic;
using System.Text;

namespace EdjCase.ICP.Agent
{
	public class RequestCleanedUpException : Exception
	{
		public RequestCleanedUpException() : base("Request reply/rejected data has timed out and has been cleaned up")
		{

		}
	}
}
