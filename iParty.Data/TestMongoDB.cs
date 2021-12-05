﻿using iParty.Business.Models.Addresses;
using iParty.Data.Repositories;
using System.Threading.Tasks;

namespace iParty.Data
{
    public class TestMongoDB
    {
        public void Test()
        {                                  
            new CityRepository<City>().Create(
                new City()
                {
                    IbgeNumber = 1234567,
                    Name = "Criciúma"
                }
            );
        }
    }
}
