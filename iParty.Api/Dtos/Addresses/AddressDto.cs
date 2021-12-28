using System;

namespace iParty.Api.Dtos.Addresses
{
    public class AddressDto
    {
        public string ZipCode { get; set; }
        public string Street { get; set; }

        public int Number { get; set; }
        public string District { get; set; }
        public Guid CityId { get; set; }
    }
}
