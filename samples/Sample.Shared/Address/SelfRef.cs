using EdjCase.ICP.Candid.Models;
using Sample.Shared.AddressBook;
using System.Collections.Generic;

namespace Sample.Shared.AddressBook
{
	public class SelfRef : OptionalValue<SelfRef.SelfRefValue>
	{
		public SelfRef()
		{
		}

		public SelfRef(SelfRef.SelfRefValue value) : base(value)
		{
		}

		public class SelfRefValue : List<SelfRef>
		{
			public SelfRefValue()
			{
			}
		}
	}
}