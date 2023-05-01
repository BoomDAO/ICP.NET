using EdjCase.ICP.Candid.Models;

namespace Sample.Shared.AddressBook
{
	public class address
	{
		public string street { get; set; }

		public string city { get; set; }

		public UnboundedUInt zip_code { get; set; }

		public string country { get; set; }

		public address(string street, string city, UnboundedUInt zip_code, string country)
		{
			this.street = street;
			this.city = city;
			this.zip_code = zip_code;
			this.country = country;
		}

		public address()
		{
		}
	}
}