using System;
using System.Collections.Generic;
using System.Text;

namespace EdjCase.ICP.Agent
{
	public static class Constants
	{
		// Default delta for ingress expiry is 5 minutes.
		public const int DEFAULT_INGRESS_EXPIRY_DELTA_IN_MSECS = 5 * 60 * 1000;
	}
}
