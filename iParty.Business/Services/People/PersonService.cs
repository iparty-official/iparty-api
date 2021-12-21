using iParty.Business.Infra;
using iParty.Business.Interfaces;
using iParty.Business.Models.Addresses;
using iParty.Business.Models.People;
using iParty.Business.Validations;
using iParty.Data.Repositories;
using System;
using System.Collections.Generic;

namespace iParty.Business.Services.Cities
{
    public class PersonService : Service<Person, IRepository<Person>>, IPersonService
    {
        private IRepository<City> _cityRepository;

        private IFilterBuilder<Person> _personFilterBuilder;

        public PersonService(IRepository<Person> rep, IRepository<City> cityRepository, IFilterBuilder<Person> personFilterBuilder) : base(rep)
        {
            _cityRepository = cityRepository;
            _personFilterBuilder = personFilterBuilder;
        }

        public ServiceResult<Person> Create(Person person)
        {
            var result = ExecuteValidation(new PersonValidation(_cityRepository, Rep, _personFilterBuilder), person);

            if (!result.IsValid)
                return GetFailureResult(result);

            Rep.Create(person);

            return GetSuccessResult(person);
        }

        public ServiceResult<Person> Update(Guid id, Person person)
        {
            var currentPerson = Get(id);

            if (currentPerson == null)
                return GetFailureResult("Não foi possível localizar a pessoa informada.");               

            var result = ExecuteValidation(new PersonValidation(_cityRepository, Rep, _personFilterBuilder), person);

            if (!result.IsValid)
                return GetFailureResult(result);

            Rep.Update(id, person);

            return GetSuccessResult(person);
        }
    }
}
