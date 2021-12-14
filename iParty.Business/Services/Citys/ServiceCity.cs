﻿using iParty.Business.Infra;
using iParty.Business.Interfaces;
using iParty.Business.Models.Addresses;
using iParty.Business.Validations;
using iParty.Data.Repositories;
using System.Collections.Generic;

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
            var currentCity = Get(city.Id);

            if (currentCity == null)
                return new ServiceResult<City>
                {
                    Success = false,
                    Entity = null,
                    Errors = new List<string> { "Não foi possível localizar a cidade informada." }
                };

            var result = ExecuteValidation(new CityValidation(), city);

            if (!result.IsValid)
                return GetFailureResult(result);

            Rep.Update(city);

            return GetSuccessResult(city);
        }      
    }
}
