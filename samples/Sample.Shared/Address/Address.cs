using EdjCase.ICP.Candid.Models;

namespace Sample.Shared.AddressBook
{
	public class Address
	{
		public string street { get; set; }

		public string city { get; set; }

		public UnboundedUInt zip_code { get; set; }

		public string country { get; set; }

		public Address(string street, string city, UnboundedUInt zipCode, string country)
		{
			this.street = street;
			this.city = city;
			this.zip_code = zipCode;
			this.country = country;
		}

		public Address()
		{
		}
	}
}