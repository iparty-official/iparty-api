using iParty.Business.Models.Addresses;
using iParty.Business.Models.Cities;

namespace iParty.Api.Dtos.Cities
{
    public class CityDto
    {
        public int IbgeNumber { get; set; }
        public string Name { get; set; }
        public UfEnum State { get; set; }
    }
}
