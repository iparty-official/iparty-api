using iParty.Business.Infra;
using iParty.Business.Interfaces.Filters;
using iParty.Business.Interfaces.Services;
using iParty.Business.Interfaces.Validations;
using iParty.Business.Models.Addresses;
using iParty.Business.Models.PaymentPlans;
using iParty.Business.Models.People;
using iParty.Business.Interfaces;
using System;
using System.Collections.Generic;

namespace iParty.Business.Services.People
{
    public class PersonService : IPersonService
    {
        private BasicService<Person> _basicService;      

        private IRepository<PaymentPlan> _paymentPlanRepository;        
        
        private IPersonValidation _personValidation;
        
        private IPhoneValidation _phoneValidation;

        private IPersonPhoneValidation _personPhoneValidation;

        private IAddressValidation _addressValidation;

        private IPersonAddressValidation _personAddressValidation;

        protected IFilterBuilder<Person> PersonFilterBuilder;

        protected IRepository<Person> Repository;

        public PersonService(IRepository<Person> repository,
                             IFilterBuilder<Person> personFilterBuilder, 
                             IPersonValidation personValidation,
                             IPhoneValidation phoneValidation,
                             IPersonPhoneValidation personPhoneValidation,
                             IAddressValidation addressValidation,
                             IPersonAddressValidation personAddressValidation,
                             IRepository<PaymentPlan> paymentPlanRepository)
        {                        
            _personValidation = personValidation;
            _phoneValidation = phoneValidation;
            _personPhoneValidation = personPhoneValidation;
            _addressValidation = addressValidation;
            _personAddressValidation = personAddressValidation;
            _paymentPlanRepository = paymentPlanRepository;            
            _basicService = new BasicService<Person>(repository, personValidation);

            PersonFilterBuilder = personFilterBuilder;
            Repository = repository;
        }

        public ServiceResult<Person> Create(Person person)
        {
            return _basicService.Create(person);
        }

        public ServiceResult<Person> Update(Guid id, Person person)
        {
            return _basicService.Update(id, person);
        }

        public ServiceResult<Person> Delete(Guid id)
        {
            return _basicService.Delete(id);
        }

        public virtual Person Get(Guid id)
        {
            return _basicService.Get(id);
        }

        public virtual List<Person> Get()
        {
            return _basicService.Get();
        }

        public ServiceResult<Person> AddPhone(Guid personId, Phone phone)
        {
            var person = Get(personId);

            if (person == null)
                return ServiceResult<Person>.FailureResult("Não foi possível localizar a pessoa informada.");

            var result = _phoneValidation.Validate(phone);

            if (!result.IsValid)
                return ServiceResult<Person>.FailureResult(result);            

            person.Phones.Add(phone);

            result = _personPhoneValidation.Validate(person);

            if (!result.IsValid)
                return ServiceResult<Person>.FailureResult(result);

            Repository.Update(personId, person);

            return ServiceResult<Person>.SuccessResult(person);
        }

        public ServiceResult<Person> ReplacePhone(Guid personId, Guid phoneId, Phone phone)
        {
            var person = Get(personId);

            if (person == null)
                return ServiceResult<Person>.FailureResult("Não foi possível localizar a pessoa informada.");

            var result = _phoneValidation.Validate(phone);

            if (!result.IsValid)
                return ServiceResult<Person>.FailureResult(result);

            person.ReplacePhone(phoneId, phone);

            result = _personPhoneValidation.Validate(person);

            if (!result.IsValid)
                return ServiceResult<Person>.FailureResult(result);

            Repository.Update(personId, person);

            return ServiceResult<Person>.SuccessResult(person);
        }

        public ServiceResult<Person> RemovePhone(Guid personId, Guid phoneId)
        {
            var person = Get(personId);

            if (person == null)
                return ServiceResult<Person>.FailureResult("Não foi possível localizar a pessoa informada.");                        

            person.RemovePhone(phoneId);

            var result = _personPhoneValidation.Validate(person);

            if (!result.IsValid)
                return ServiceResult<Person>.FailureResult(result);

            Repository.Update(personId, person);

            return ServiceResult<Person>.SuccessResult(person);
        }        

        public ServiceResult<Person> AddAddress(Guid personId, Address address)
        {
            var person = Get(personId);

            if (person == null)
                return ServiceResult<Person>.FailureResult("Não foi possível localizar a pessoa informada.");

            var result = _addressValidation.Validate(address);

            if (!result.IsValid)
                return ServiceResult<Person>.FailureResult(result);

            result = _personAddressValidation.Validate(person);

            if (!result.IsValid)
                return ServiceResult<Person>.FailureResult(result);

            person.Addresses.Add(address);

            Repository.Update(personId, person);

            return ServiceResult<Person>.SuccessResult(person);
        }

        public ServiceResult<Person> ReplaceAddress(Guid personId, Guid addressId, Address address)
        {
            var person = Get(personId);

            if (person == null)
                return ServiceResult<Person>.FailureResult("Não foi possível localizar a pessoa informada.");

            var result = _addressValidation.Validate(address);

            if (!result.IsValid)
                return ServiceResult<Person>.FailureResult(result);

            result = _personAddressValidation.Validate(person);

            if (!result.IsValid)
                return ServiceResult<Person>.FailureResult(result);

            person.ReplaceAddress(addressId, address);

            Repository.Update(personId, person);

            return ServiceResult<Person>.SuccessResult(person);
        }

        public ServiceResult<Person> RemoveAddress(Guid personId, Guid addressId)
        {
            var person = Get(personId);

            if (person == null)
                return ServiceResult<Person>.FailureResult("Não foi possível localizar a pessoa informada.");

            person.RemoveAddress(addressId);

            Repository.Update(personId, person);

            return ServiceResult<Person>.SuccessResult(person);
        }

        public ServiceResult<Person> AddPaymentPlan(Guid personId, Guid paymentPlanId)
        {
            var person = Get(personId);

            if (person == null)
                return ServiceResult<Person>.FailureResult("Não foi possível localizar a pessoa informada.");

            var paymentPlan = _paymentPlanRepository.RecoverById(paymentPlanId);

            if (paymentPlan == null)
                return ServiceResult<Person>.FailureResult("Não foi possível localizar o plano de pagamento informado.");

            if (person.SupplierInfo.PaymentPlans.Exists(x => x.Id == paymentPlan.Id))
                return ServiceResult<Person>.FailureResult("O plano de pagamento informado já está vinculado ao fornecedor.");

            person.SupplierInfo.PaymentPlans.Add(paymentPlan);

            Repository.Update(personId, person);

            return ServiceResult<Person>.SuccessResult(person);
        }        

        public ServiceResult<Person> RemovePaymentPlan(Guid personId, Guid paymentPlanId)
        {
            var person = Get(personId);

            if (person == null)
                return ServiceResult<Person>.FailureResult("Não foi possível localizar a pessoa informada.");

            person.RemovePaymentPlan(paymentPlanId);

            Repository.Update(personId, person);

            return ServiceResult<Person>.SuccessResult(person);
        }       
    }
}
