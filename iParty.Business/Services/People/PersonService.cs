using iParty.Business.Infra;
using iParty.Business.Interfaces.Filters;
using iParty.Business.Interfaces.Services;
using iParty.Business.Interfaces.Validations;
using iParty.Business.Models.Addresses;
using iParty.Business.Models.PaymentPlans;
using iParty.Business.Models.People;
using iParty.Business.Interfaces;
using System;

namespace iParty.Business.Services.People
{
    public class PersonService : Service<Person, IRepository<Person>>, IPersonService
    {       
        private IRepository<PaymentPlan> _paymentPlanRepository;        
        
        private IPersonValidation _personValidation;
        
        private IPhoneValidation _phoneValidation;

        private IPersonPhoneValidation _personPhoneValidation;

        private IAddressValidation _addressValidation;

        private IPersonAddressValidation _personAddressValidation;

        protected IFilterBuilder<Person> PersonFilterBuilder;

        public PersonService(IRepository<Person> rep,                              
                             IFilterBuilder<Person> personFilterBuilder, 
                             IPersonValidation personValidation,
                             IPhoneValidation phoneValidation,
                             IPersonPhoneValidation personPhoneValidation,
                             IAddressValidation addressValidation,
                             IPersonAddressValidation personAddressValidation,
                             IRepository<PaymentPlan> paymentPlanRepository) : base(rep)
        {                        
            _personValidation = personValidation;
            _phoneValidation = phoneValidation;
            _personPhoneValidation = personPhoneValidation;
            _addressValidation = addressValidation;
            _personAddressValidation = personAddressValidation;
            _paymentPlanRepository = paymentPlanRepository;
            
            PersonFilterBuilder = personFilterBuilder;
        }

        public ServiceResult<Person> Create(Person person)
        {
            var result = _personValidation.CustomValidate(person);

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

            var result = _personValidation.CustomValidate(person);

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

            result = _personPhoneValidation.Validate(person);

            if (!result.IsValid)
                return GetFailureResult(result);

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

            person.ReplacePhone(phoneId, phone);

            result = _personPhoneValidation.Validate(person);

            if (!result.IsValid)
                return GetFailureResult(result);

            Rep.Update(personId, person);

            return GetSuccessResult(person);
        }

        public ServiceResult<Person> RemovePhone(Guid personId, Guid phoneId)
        {
            var person = Get(personId);

            if (person == null)
                return GetFailureResult("Não foi possível localizar a pessoa informada.");                        

            person.RemovePhone(phoneId);

            var result = _personPhoneValidation.Validate(person);

            if (!result.IsValid)
                return GetFailureResult(result);

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

            result = _personAddressValidation.Validate(person);

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

            result = _personAddressValidation.Validate(person);

            if (!result.IsValid)
                return GetFailureResult(result);

            person.ReplaceAddress(addressId, address);            

            Rep.Update(personId, person);

            return GetSuccessResult(person);
        }

        public ServiceResult<Person> RemoveAddress(Guid personId, Guid addressId)
        {
            var person = Get(personId);

            if (person == null)
                return GetFailureResult("Não foi possível localizar a pessoa informada.");

            person.RemoveAddress(addressId);            

            Rep.Update(personId, person);

            return GetSuccessResult(person);
        }

        public ServiceResult<Person> AddPaymentPlan(Guid personId, Guid paymentPlanId)
        {
            var person = Get(personId);

            if (person == null)
                return GetFailureResult("Não foi possível localizar a pessoa informada.");

            var paymentPlan = _paymentPlanRepository.RecoverById(paymentPlanId);

            if (paymentPlan == null)
                return GetFailureResult("Não foi possível localizar o plano de pagamento informado.");

            if (person.SupplierInfo.PaymentPlans.Exists(x => x.Id == paymentPlan.Id))
                return GetFailureResult("O plano de pagamento informado já está vinculado ao fornecedor.");

            person.SupplierInfo.PaymentPlans.Add(paymentPlan);

            Rep.Update(personId, person);

            return GetSuccessResult(person);
        }        

        public ServiceResult<Person> RemovePaymentPlan(Guid personId, Guid paymentPlanId)
        {
            var person = Get(personId);

            if (person == null)
                return GetFailureResult("Não foi possível localizar a pessoa informada.");

            person.RemovePaymentPlan(paymentPlanId);            

            Rep.Update(personId, person);

            return GetSuccessResult(person);
        }       
    }
}
