using iParty.Business.Infra;
using iParty.Business.Interfaces.Filters;
using iParty.Business.Interfaces.Services;
using iParty.Business.Interfaces.Validations;
using iParty.Business.Models.Addresses;
using iParty.Data.Repositories;
using System;

namespace iParty.Business.Services.Cities
{
    public class CityService : Service<City, IRepository<City>>, ICityService
    {
        private IFilterBuilder<City> _filterBuilder;

        private ICityValidation _cityValidation;

        public CityService(IRepository<City> rep, IFilterBuilder<City> filterBuilder, ICityValidation cityValidation) : base(rep)
        {
            _filterBuilder = filterBuilder;
            _cityValidation = cityValidation;
        }

        public ServiceResult<City> Create(City city)
        {                       
            var result = ExecuteValidation(_cityValidation, city);

            if (!result.IsValid)
                return GetFailureResult(result);           

            Rep.Create(city);

            return GetSuccessResult(city);
        }

        public ServiceResult<City> Update(Guid id, City city)
        {
            var currentCity = Get(city.Id);

            if (currentCity == null)
                return GetFailureResult("Não foi possível localizar a cidade informada.");            

            var result = ExecuteValidation(_cityValidation, city);

            if (!result.IsValid)
                return GetFailureResult(result);

            Rep.Update(id, city);

            return GetSuccessResult(city);
        }      
    }
}
