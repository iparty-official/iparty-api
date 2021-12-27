namespace iParty.Business.Models.Addresses
{
    public class Address: Entity
    {        
        public string ZipCode { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }
        public string District { get; set; }
        public City City { get; set; }
    }
}
