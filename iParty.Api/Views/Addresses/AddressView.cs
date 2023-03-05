using iParty.Api.Views.Cities;
using System;

namespace iParty.Api.Views.Addresses
{
    public class AddressView : View
    {
        public string ZipCode { get; set; }
        public string Street { get; set; }
        public string District { get; set; }
        public int Number { get; set; }
        public Guid CityId { get; set; }
    }
}
