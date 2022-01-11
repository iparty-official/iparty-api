using iParty.Business.Models.Addresses;

namespace iParty.Api.Views.Addresses
{
    public class CityView : View
    {
        public int IbgeNumber { get; set; }
        public string Name { get; set; }
        public UfEnum State { get; set; }
    }
}
