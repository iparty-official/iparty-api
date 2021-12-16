namespace iParty.Business.Models.Addresses
{
    public class City: Entity
    {
        public int IbgeNumber { get; set; }
        public string Name { get; set; }
        public UfEnum State { get; set; }
    }
}
