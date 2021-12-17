﻿using iParty.Business.Infra;
using iParty.Business.Interfaces;
using iParty.Business.Models.Addresses;
using iParty.Business.Validations;
using iParty.Data.Repositories;
using System;
using System.Collections.Generic;

namespace iParty.Business.Services.Cities
{
    public class CityService : Service<City, IRepository<City>>, ICityService
    {
        private IFilterBuilder<City> _filterBuilder;   

        public CityService(IRepository<City> rep, IFilterBuilder<City> filterBuilder) : base(rep)
        {
            _filterBuilder = filterBuilder;            
        }

        public ServiceResult<City> Create(City city)
        {                       
            var result = ExecuteValidation(new CityValidation(Rep, _filterBuilder), city);

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

            var result = ExecuteValidation(new CityValidation(Rep, _filterBuilder), city);

            if (!result.IsValid)
                return GetFailureResult(result);

            Rep.Update(id, city);

            return GetSuccessResult(city);
        }      
    }
}
