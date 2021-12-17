using iParty.Business.Infra;
using iParty.Business.Interfaces;
using iParty.Business.Models.People;
using iParty.Business.Validations;
using iParty.Data.Repositories;
using System;
using System.Collections.Generic;

namespace iParty.Business.Services.Cities
{
    public class PersonService : Service<Person, IRepository<Person>>, IPersonService
    {
        public PersonService(IRepository<Person> rep) : base(rep)
        {
        }

        public ServiceResult<Person> Create(Person Person)
        {
            var result = ExecuteValidation(new PersonValidation(), Person);

            if (!result.IsValid)
                return GetFailureResult(result);

            Rep.Create(Person);

            return GetSuccessResult(Person);
        }

        public ServiceResult<Person> Update(Guid id, Person Person)
        {
            var currentPerson = Get(id);

            if (currentPerson == null)
                GetFailureResult("Não foi possível localizar a pessoa informada.");               

            var result = ExecuteValidation(new PersonValidation(), Person);

            if (!result.IsValid)
                return GetFailureResult(result);

            Rep.Update(id, Person);

            return GetSuccessResult(Person);
        }
    }
}
