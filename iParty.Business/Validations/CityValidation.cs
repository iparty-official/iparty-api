using FluentValidation;
using iParty.Business.Interfaces;
using iParty.Business.Models.Addresses;
using iParty.Data.Repositories;

namespace iParty.Business.Validations
{
    public class CityValidation : AbstractValidator<City>
    {
        private bool ibgeNumberAlreadyExists(IRepository<City> rep, IFilterBuilder<City> filterBuilder, City city)
        {
            filterBuilder
                .Equal(x => x.IbgeNumber, city.IbgeNumber)
                .Unequal(x => x.Id, city.Id);

            return rep.Recover(filterBuilder).Count > 0;
        }

        private int extractStateIdFromIbgeNumber(int ibgeNumber)
        {
            var stateId = ibgeNumber / 100000; //IBGE has always seven digits, so dividing for 100k, will return the two first digits.

            return stateId;
        }

        public CityValidation(IRepository<City> rep, IFilterBuilder<City> filterBuilder)
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("Nome da cidade não foi informado.");            

            RuleFor(p => p.IbgeNumber).GreaterThan(1000000).WithMessage("O código IBGE precisa ter examente sete dígitos.");

            RuleFor(p => p.IbgeNumber).LessThan(9999999).WithMessage("O código IBGE precisa ter examente sete dígitos.");

            RuleFor(p => ibgeNumberAlreadyExists(rep, filterBuilder, p)).Equal(false).WithMessage("Já existe uma cidade cadastrada com o código IBGE informado.");

            RuleFor(p => p.State).IsInEnum().WithMessage("O estado da cidade é inválido");

            RuleFor(p => (int)p.State).Equal(x => extractStateIdFromIbgeNumber(x.IbgeNumber)).WithMessage("O código IBGE informado não pertece ao estado informado.");
        }        
    }
}