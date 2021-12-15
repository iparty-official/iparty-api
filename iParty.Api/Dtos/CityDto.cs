using iParty.Business.Models.Addresses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iParty.Api.Dtos
{
    public class CityDto
    {
        public int IbgeNumber { get; set; }
        public string Name { get; set; }
        public UfEnum State { get; set; }
    }
}
