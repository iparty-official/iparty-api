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

        private ServiceResult<Person> replacePhone(Person person, Guid phoneId, Phone newPhone)
        {
            var currentPhone = person.Phones.Find(x => x.Id == phoneId);

            if (currentPhone == null)
                return GetFailureResult("Não foi possível localizar o telefone informado");

            var index = person.Phones.IndexOf(currentPhone);

            person.Phones.Remove(currentPhone);

            newPhone.Id = phoneId;

            person.Phones.Insert(index, newPhone);

            return GetSuccessResult(person);
        }

        private ServiceResult<Person> removePhone(Person person, Guid phoneId)
        {
            var currentPhone = person.Phones.Find(x => x.Id == phoneId);

            if (currentPhone == null)
                return GetFailureResult("Não foi possível localizar o telefone informado");

            var index = person.Phones.IndexOf(currentPhone);

            person.Phones.Remove(currentPhone);

            return GetSuccessResult(person);
        }

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

        public ServiceResult<Person> AddPhone(Guid personId, Phone phone)
        {
            var person = Get(personId);

            if (person == null)
                return GetFailureResult("Não foi possível localizar a pessoa informada.");

            var result = new PhoneValidation().Validate(phone);

            if (!result.IsValid)
                return GetFailureResult(result);

            person.Phones.Add(phone);

            Rep.Update(personId, person);

            return GetSuccessResult(person);
        }

        public ServiceResult<Person> ReplacePhone(Guid personId, Guid phoneId, Phone phone)
        {
            var person = Get(personId);

            if (person == null)
                return GetFailureResult("Não foi possível localizar a pessoa informada.");

            var result = new PhoneValidation().Validate(phone);

            if (!result.IsValid)
                return GetFailureResult(result);

            var replaceResult = replacePhone(person, phoneId, phone);

            if (!replaceResult.Success) return replaceResult;

            Rep.Update(personId, person);

            return GetSuccessResult(person);
        }

        public ServiceResult<Person> RemovePhone(Guid personId, Guid phoneId)
        {
            var person = Get(personId);

            if (person == null)
                return GetFailureResult("Não foi possível localizar a pessoa informada.");                        

            var removeResult = removePhone(person, phoneId);

            if (!removeResult.Success) return removeResult;

            Rep.Update(personId, person);

            return GetSuccessResult(person);
        }        
    }
}
