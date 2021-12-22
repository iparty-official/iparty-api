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
        private bool cityExists(Guid id, IRepository<City> cityRepository)
        {
            var city = cityRepository.RecoverById(id);

            return city != null;
        }

        public AddressValidation(IRepository<City> cityRepository)
        {
            RuleFor(x => true).Equal(x => x.ZipCode.All(char.IsDigit)).WithMessage("O CEP deve conter apenas números.");

            RuleFor(x => x.ZipCode).Length(8).WithMessage("O CEP deve conter exatamente oito dígitos.");

            RuleFor(x => x.Street).NotEmpty().WithMessage("O nome da rua não foi informado.");

            RuleFor(x => x.District).NotEmpty().WithMessage("O nome do bairro não foi informado.");

            RuleFor(x => cityExists(x.City.Id, cityRepository)).Equal(true).WithMessage("A cidade informada não existe.");
        }        
    }
}