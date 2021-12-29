using iParty.Business.Models.Addresses;

namespace iParty.Api.Dtos.Addresses
{
    public class CityDto
    {
        public int IbgeNumber { get; set; }
        public string Name { get; set; }
        public UfEnum State { get; set; }
    }
}
