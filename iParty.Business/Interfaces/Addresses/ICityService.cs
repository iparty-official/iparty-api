using iParty.Business.Infra;
using iParty.Business.Models.Addresses;
using System;

namespace iParty.Business.Interfaces.Addresses
{
    public interface ICityService : IService<City>
    {
        public ServiceResult<City> Create(City city);

        public ServiceResult<City> Update(Guid id, City city);
    }
}
