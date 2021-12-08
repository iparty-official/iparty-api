using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iParty.Business.Models.Addresses
{
    public class City: Entity
    {
        public City()
        {
        }

        public City(string name, int ibgeNumber)
        {
            Name = name;
            IbgeNumber = ibgeNumber;
        }

        public int IbgeNumber { get; private set; }
        public string Name { get; private set; }

        public static City Create(string name, int ibgeNumber)
        {
            return new City(name, ibgeNumber);
        }

        public void Update(string name, int ibgeNumber)
        {
            Name = name;
            IbgeNumber = ibgeNumber;
        }
    }
}
