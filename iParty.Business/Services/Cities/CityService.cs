using iParty.Business.Infra;
using iParty.Business.Interfaces.Services;
using iParty.Business.Interfaces.Validations;
using iParty.Business.Models.Addresses;
using iParty.Business.Interfaces;
using System;
using System.Collections.Generic;

namespace iParty.Business.Services.Cities
{
    public class CityService : ICityService
    {
        private BasicService<City> _basicService;

        public CityService(IRepository<City> repository, ICityValidation cityValidation)
        {
            _basicService = new BasicService<City>(repository, cityValidation);
        }

        public ServiceResult<City> Create(City city)
        {
            return _basicService.Create(city);
        }

        public ServiceResult<City> Update(Guid id, City city)
        {
            return _basicService.Update(id, city);
        }

        public ServiceResult<City> Delete(Guid id)
        {
            return _basicService.Delete(id);
        }

        public City Get(Guid id)
        {
            return _basicService.Get(id);
        }

        public List<City> Get()
        {
            return _basicService.Get();
        }
    }
}
