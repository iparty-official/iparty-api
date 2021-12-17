using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iParty.Api.Dtos
{
    public class AddressDto
    {
        public string ZipCode { get; set; }
        public string Street { get; set; }
        public string District { get; set; }
        public CityDto City { get; set; }
    }
}
