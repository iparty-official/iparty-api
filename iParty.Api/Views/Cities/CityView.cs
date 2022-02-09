using iParty.Business.Models.Cities;

namespace iParty.Api.Views.Cities
{
    public class CityView : View
    {
        public int IbgeNumber { get; set; }
        public string Name { get; set; }
        public UfEnum State { get; set; }
    }
}
