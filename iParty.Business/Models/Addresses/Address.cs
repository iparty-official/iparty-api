namespace iParty.Business.Models.Addresses
{
    public class Address: Entity
    {
        public Address() { }
        public Address(string zipCode, string street, int number, string district, City city)
        {
            ZipCode = zipCode;
            Street = street;
            Number = number;
            District = district;
            City = city;
        }
        public string ZipCode { get; private set; }
        public string Street { get; private set; }
        public int Number { get; private set; }
        public string District { get; private set; }
        public City City { get; private set; }
    }
}
