using System;
using System.Collections.Generic;
using System.Text;

namespace EdjCase.ICP.Agent
{
	public class RequestCleanedUpExcpetion: Exception
	{
		public RequestCleanedUpExcpetion() : base("Request reply/rejected data has timed out and been cleaned up")
		{

		}
	}
}
