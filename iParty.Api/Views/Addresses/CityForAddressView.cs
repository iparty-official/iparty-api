using iParty.Business.Models.Cities;

namespace iParty.Api.Views.Addresses
{
    public class CityForAddressView : View
    {     
        public string Name { get; set; }
        public UfEnum State { get; set; }
    }
}
