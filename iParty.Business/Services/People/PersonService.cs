using iParty.Business.Infra;
using iParty.Business.Interfaces.Filters;
using iParty.Business.Interfaces.Services;
using iParty.Business.Interfaces.Validations;
using iParty.Business.Models.Addresses;
using iParty.Business.Models.People;
using iParty.Data.Repositories;
using System;

namespace iParty.Business.Services.Cities
{
    public class PersonService : Service<Person, IRepository<Person>>, IPersonService
    {
        private IRepository<City> _cityRepository;

        private IFilterBuilder<Person> _personFilterBuilder;
        
        private IPersonValidation _personValidation;
        
        private IPhoneValidation _phoneValidation;

        private IAddressValidation _addressValidation;

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
        
        private ServiceResult<Person> replaceAddress(Person person, Guid addressId, Address newAddress)
        {
            var currentAddress = person.Addresses.Find(x => x.Id == addressId);

            if (currentAddress == null)
                return GetFailureResult("Não foi possível localizar o endereço informado");

            var index = person.Addresses.IndexOf(currentAddress);

            person.Addresses.Remove(currentAddress);

            newAddress.Id = addressId;

            person.Addresses.Insert(index, newAddress);

            return GetSuccessResult(person);
        }

        private ServiceResult<Person> removeAddress(Person person, Guid addressId)
        {
            var currentAddress = person.Addresses.Find(x => x.Id == addressId);

            if (currentAddress == null)
                return GetFailureResult("Não foi possível localizar o endereço informado");

            var index = person.Addresses.IndexOf(currentAddress);

            person.Addresses.Remove(currentAddress);

            return GetSuccessResult(person);
        }        

        public PersonService(IRepository<Person> rep, 
                             IRepository<City> cityRepository, 
                             IFilterBuilder<Person> personFilterBuilder, 
                             IPersonValidation personValidation,
                             IPhoneValidation phoneValidation,
                             IAddressValidation addressValidation) : base(rep)
        {
            _cityRepository = cityRepository;
            _personFilterBuilder = personFilterBuilder;
            _personValidation = personValidation;
            _phoneValidation = phoneValidation;
            _addressValidation = addressValidation;
        }

        public ServiceResult<Person> Create(Person person)
        {
            var result = ExecuteValidation(_personValidation, person);

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

            var result = ExecuteValidation(_personValidation, person);

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

            var result = _phoneValidation.Validate(phone);

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

            var result = _phoneValidation.Validate(phone);

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

        public ServiceResult<Person> AddAddress(Guid personId, Address address)
        {
            var person = Get(personId);

            if (person == null)
                return GetFailureResult("Não foi possível localizar a pessoa informada.");

            var result = _addressValidation.Validate(address);

            if (!result.IsValid)
                return GetFailureResult(result);

            person.Addresses.Add(address);

            Rep.Update(personId, person);

            return GetSuccessResult(person);
        }

        public ServiceResult<Person> ReplaceAddress(Guid personId, Guid addressId, Address address)
        {
            var person = Get(personId);

            if (person == null)
                return GetFailureResult("Não foi possível localizar a pessoa informada.");

            var result = _addressValidation.Validate(address);

            if (!result.IsValid)
                return GetFailureResult(result);

            var replaceResult = replaceAddress(person, addressId, address);

            if (!replaceResult.Success) return replaceResult;

            Rep.Update(personId, person);

            return GetSuccessResult(person);
        }

        public ServiceResult<Person> RemoveAddress(Guid personId, Guid addressId)
        {
            var person = Get(personId);

            if (person == null)
                return GetFailureResult("Não foi possível localizar a pessoa informada.");

            var removeResult = removeAddress(person, addressId);

            if (!removeResult.Success) return removeResult;

            Rep.Update(personId, person);

            return GetSuccessResult(person);
        }
    }
}
