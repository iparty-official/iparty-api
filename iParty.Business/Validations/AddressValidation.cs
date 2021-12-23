using FluentValidation;
using iParty.Business.Interfaces.Validations;
using iParty.Business.Models.Addresses;
using iParty.Business.Models.People;
using iParty.Data.Repositories;
using System;
using System.Linq;

namespace iParty.Business.Validations
{
    public class AddressValidation : AbstractValidator<Address>, IAddressValidation
    {       
        public AddressValidation(IRepository<City> cityRepository)
        {
            //TODO: Impedir endereço duplicado. Talvez eu tenha que criar o campo Number. Daí posso validar se há repetição de ZipCode + Number

            RuleFor(x => true).Equal(x => x.ZipCode.All(char.IsDigit)).WithMessage("O CEP deve conter apenas números.");

            RuleFor(x => x.ZipCode).Length(8).WithMessage("O CEP deve conter exatamente oito dígitos.");

            RuleFor(x => x.Street).NotEmpty().WithMessage("O nome da rua não foi informado.");

            RuleFor(x => x.District).NotEmpty().WithMessage("O nome do bairro não foi informado.");

            RuleFor(x => cityRepository.RecoverById(x.City.Id)).NotNull().WithMessage("A cidade informada não existe.");
        }        
    }
}