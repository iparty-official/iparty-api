using FluentValidation;
using FluentValidation.Results;
using iParty.Business.Interfaces.Filters;
using iParty.Business.Interfaces.Validations;
using iParty.Business.Models.Addresses;
using iParty.Business.Models.PaymentPlans;
using iParty.Business.Models.People;
using iParty.Data.Repositories;
using System;
using System.Linq;

namespace iParty.Business.Validations
{
    public class PersonValidation : AbstractValidator<Person>, IPersonValidation
    {
        private IPhoneValidation _phoneValidation;

        private IPersonPhoneValidation _personPhoneValidation;

        private IAddressValidation _addressValidation;

        private IPaymentPlanValidation _paymentPlanValidation;

        public PersonValidation(IRepository<City> cityRepository, 
                                IRepository<Person> personRepository, 
                                IRepository<PaymentPlan> paymentPlanRepository,                                 
                                IFilterBuilder<Person> personFilterBuilder,
                                IPhoneValidation phoneValidation,
                                IPersonPhoneValidation personPhoneValidation,
                                IAddressValidation addressValidation,
                                IPaymentPlanValidation paymentPlanValidation)
        {
            //TODO: Validar DV do CPF/CNPJ
            _phoneValidation = phoneValidation;

            _personPhoneValidation = personPhoneValidation;

            _addressValidation = addressValidation;

            _paymentPlanValidation = paymentPlanValidation;

            RuleFor(p => p.Name).NotEmpty().WithMessage("O nome da pessoa não foi informado.");

            RuleFor(p => p.SupplierOrCustomer).IsInEnum().WithMessage("O campo 'Cliente ou Fornecedor' está com um valor inválido.");           

            RuleFor(p => p.Document).Length(x => isCPF(x.Document) ? 11 : isCNPJ(x.Document) ? 14 : 0). WithMessage("O CPF/CNPJ precisa ter 11 ou 14 dígitos.");

            RuleFor(p => true).Equal(x => x.Document.All(char.IsDigit)).WithMessage("O número do documento deve conter apenas números");

            RuleFor(p => documentAlreadyExists(personRepository, personFilterBuilder, p)).Equal(false).WithMessage("Já existe uma pessoa cadastrada com o documento informado.");

            RuleFor(p => p.CustomerInfo.BirthDate).LessThan(DateTime.Today).WithMessage("A data de nascimento não pode ser maior que a data atual.");            

            RuleFor(p => p.SupplierOrCustomer == SupplierOrCustomer.Supplier && string.IsNullOrEmpty(p.SupplierInfo.BusinessDescription)).Equal(false).WithMessage("A descrição do negócio não foi informada");

            RuleForEach(p => p.SupplierInfo.PaymentPlans).ChildRules(pay => pay.RuleFor(x => paymentPlanRepository.RecoverById(x.Id)).NotNull().WithMessage("O plano de pagamento informado não existe."));
        }

        public ValidationResult CustomValidate(Person person)        
        {
            var result = this.Validate(person);

            if (!result.IsValid) return result;

            foreach (var phone in person.Phones)
            {
                result = _phoneValidation.Validate(phone);

                if (!result.IsValid) return result;
            }

            foreach (var address in person.Addresses)
            {
                result = _addressValidation.Validate(address);

                if (!result.IsValid) return result;
            }

            foreach (var paymentPlan in person.SupplierInfo.PaymentPlans)
            {
                result = _paymentPlanValidation.Validate(paymentPlan);

                if (!result.IsValid) return result;
            }

            result = _personPhoneValidation.Validate(person);            

            return result;
        }    

        private bool isCPF(string document)
        {
            return document.Length == 11;
        }

        private bool isCNPJ(string document)
        {
            return document.Length == 14;
        }               

        private bool documentAlreadyExists(IRepository<Person> personRepository, IFilterBuilder<Person> filterBuilder, Person person)
        {
            if (String.IsNullOrEmpty(person.Document)) return false;

            filterBuilder
                .Equal(x => x.Document, person.Document)
                .Unequal(x => x.Id, person.Id);

            return personRepository.Recover(filterBuilder).Count > 0;
        }
    }
}
