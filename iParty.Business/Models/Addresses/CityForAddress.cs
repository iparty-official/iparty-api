using iParty.Business.Models.Cities;
using System;

namespace iParty.Business.Models.Addresses
{
    public class CityForAddress
    {
        public CityForAddress()
        {
        }

        public CityForAddress(Guid id, string name, UfEnum state)
        {
            Id = id;
            Name = name;
            State = state;
        }
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public  UfEnum State { get; private set; }
    }
}
