using iParty.Business.Infra;
using iParty.Business.Interfaces;
using iParty.Business.Models.Addresses;
using iParty.Business.Validations;
using iParty.Data.Repositories;
using System;

namespace iParty.Business.Services.Citys
{
    public class ServiceCity : Service<City, IRepository<City>>, IServiceCity
    {
        public ServiceCity(IRepository<City> rep) : base(rep)
        {
        }

        public ServiceResult<City> Create(City city)
        {                       
            var result = ExecuteValidation(new CityValidation(), city);

            if (!result.IsValid)
                return GetFailureResult(result);

            Rep.Create(city);

            return GetSuccessResult(city);
        }

        public ServiceResult<City> Update(City city)
        {
            var result = ExecuteValidation(new CityValidation(), city);

            if (!result.IsValid)
                return GetFailureResult(result);

            Rep.Update(city);

            return GetSuccessResult(city);
        }      
    }
}
