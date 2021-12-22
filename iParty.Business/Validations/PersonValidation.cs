using FluentValidation;
using iParty.Business.Interfaces.Filters;
using iParty.Business.Models.Addresses;
using iParty.Business.Models.People;
using iParty.Data.Repositories;
using System;
using System.Linq;

namespace iParty.Business.Validations
{
    public class PersonValidation : AbstractValidator<Person>
    {        
        private bool isCPF(string document)
        {
            return document.Length == 11;
        }

        private bool isCNPJ(string document)
        {
            return document.Length == 14;
        }

        private bool cityExists(Guid id, IRepository<City> cityRepository)
        {
            var city = cityRepository.RecoverById(id);

            return city != null;
        }

        private bool documentAlreadyExists(IRepository<Person> personRepository, IFilterBuilder<Person> filterBuilder, Person person)
        {
            if (String.IsNullOrEmpty(person.Document)) return false;
            
            filterBuilder
                .Equal(x => x.Document, person.Document)
                .Unequal(x => x.Id, person.Id);

            return personRepository.Recover(filterBuilder).Count > 0;
        }

        public PersonValidation(IRepository<City> cityRepository, IRepository<Person> personRepository, IFilterBuilder<Person> personFilterBuilder)
        {            
            RuleFor(p => p.Name).NotEmpty().WithMessage("O nome da pessoa não foi informado.");

            RuleFor(p => p.SupplierOrCustomer).IsInEnum().WithMessage("O campo 'Cliente ou Fornecedor' está com um valor inválido.");           

            RuleFor(p => p.Document).Length(x => isCPF(x.Document) ? 11 : isCNPJ(x.Document) ? 14 : 0). WithMessage("O CPF/CNPJ precisa ter 11 ou 14 dígitos.");

            RuleFor(p => true).Equal(x => x.Document.All(char.IsDigit)).WithMessage("O número do documento deve conter apenas números");

            RuleFor(p => documentAlreadyExists(personRepository, personFilterBuilder, p)).Equal(false).WithMessage("Já existe uma pessoa cadastrada com o documento informado.");

            RuleFor(p => p.CustomerInfo.BirthDate).LessThan(DateTime.Today).WithMessage("A data de nascimento não pode ser maior que a data atual.");

            RuleForEach(p => p.Phones).ChildRules(phone => phone.RuleFor(x => x.Prefix).NotEmpty().WithMessage("O prefixo do número do telefone não foi informado."));

            RuleForEach(p => p.Phones).ChildRules(phone => phone.RuleFor(x => x.Number).NotEmpty().WithMessage("O número do telefone não foi informado."));

            RuleForEach(p => p.Phones).ChildRules(phone => phone.RuleFor(x => true).Equal(x => x.Prefix.All(char.IsDigit)).WithMessage("O prefixo do telefone deve conter apenas números."));

            RuleForEach(p => p.Phones).ChildRules(phone => phone.RuleFor(x => true).Equal(x => x.Number.All(char.IsDigit)).WithMessage("O número do telefone deve conter apenas números."));

            RuleForEach(p => p.Addresses).ChildRules(addr => addr.RuleFor(x => true).Equal(x => x.ZipCode.All(char.IsDigit)).WithMessage("O CEP deve conter apenas números."));

            RuleForEach(p => p.Addresses).ChildRules(addr => addr.RuleFor(x => x.ZipCode).Length(8).WithMessage("O CEP deve conter exatamente oito dígitos."));

            RuleForEach(p => p.Addresses).ChildRules(addr => addr.RuleFor(x => x.Street).NotEmpty().WithMessage("O nome da rua não foi informado."));

            RuleForEach(p => p.Addresses).ChildRules(addr => addr.RuleFor(x => x.District).NotEmpty().WithMessage("O nome do bairro não foi informado."));

            RuleForEach(p => p.Addresses).ChildRules(addr => addr.RuleFor(x => cityExists(x.City.Id, cityRepository)).Equal(true).WithMessage("A cidade informada não existe."));
        }        
    }
}
