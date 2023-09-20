using Dict = System.Collections.Generic.List<(System.String, EdjCase.ICP.Candid.Models.UnboundedUInt)>;
using EdjCase.ICP.Candid.Models;

namespace Sample.Shared.AddressBook
{
	public class Address
	{
		public string street { get; set; }

		public string city { get; set; }

		public UnboundedUInt zip_code { get; set; }

		public string country { get; set; }

		public Address(string street, string city, UnboundedUInt zip_code, string country)
		{
			this.street = street;
			this.city = city;
			this.zip_code = zip_code;
			this.country = country;
		}

		public Address()
		{
		}
	}
}