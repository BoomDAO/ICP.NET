using EdjCase.ICP.Candid.Models;
using Sample.Shared.AddressBook;
using System.Collections.Generic;

namespace Sample.Shared.AddressBook
{
	public class SelfRef : OptionalValue<SelfRef.SelfRefValue>
	{
		protected SelfRef()
		{
		}

		public class SelfRefValue : List<SelfRef>
		{
			protected SelfRefValue()
			{
			}
		}
	}
}