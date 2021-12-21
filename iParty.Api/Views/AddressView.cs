using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iParty.Api.Views
{
    public class AddressView : View
    {
        public string ZipCode { get; set; }
        public string Street { get; set; }
        public string District { get; set; }
        public CityView City { get; set; }
    }
}
