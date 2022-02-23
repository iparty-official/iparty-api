using System;

namespace iParty.Business.Models.Addresses
{
    public class Address: Entity
    {
        public Address() : base() {}

        public Address(string zipCode, string street, int number, string district, CityForAddress city)
        {
            ZipCode = zipCode;
            Street = street;
            Number = number;
            District = district;
            City = city;
        }

        public string ZipCode { get; private set; }
        public string Street { get; private set; }
        public int Number { get; private set; }
        public string District { get; private set; }
        public CityForAddress City { get; private set; }
    }
}
