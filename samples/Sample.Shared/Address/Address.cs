using Dict = System.Collections.Generic.List<(System.String, EdjCase.ICP.Candid.Models.UnboundedUInt)>;
using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid.Models;

namespace Sample.Shared.AddressBook
{
	public class Address
	{
		[CandidName("street")]
		public string Street { get; set; }

		[CandidName("city")]
		public string City { get; set; }

		[CandidName("zip_code")]
		public UnboundedUInt ZipCode { get; set; }

		[CandidName("country")]
		public string Country { get; set; }

		public Address(string street, string city, UnboundedUInt zipCode, string country)
		{
			this.Street = street;
			this.City = city;
			this.ZipCode = zipCode;
			this.Country = country;
		}

		public Address()
		{
		}
	}
}