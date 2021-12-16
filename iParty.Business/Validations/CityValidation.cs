using FluentValidation;
using iParty.Business.Interfaces;
using iParty.Business.Models.Addresses;
using iParty.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iParty.Business.Validations
{
    public class CityValidation : AbstractValidator<City>
    {
        public CityValidation(IRepository<City> rep, IFilterBuilder filterBuilder)
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("Nome da cidade não foi informado.");

            RuleFor(p => p.State).IsInEnum().WithMessage("O estado da cidade é inválido");

            RuleFor(p => p.IbgeNumber).GreaterThan(1000000).WithMessage("O código IBGE precisa ter examente sete dígitos.");

            RuleFor(p => p.IbgeNumber).LessThan(9999999).WithMessage("O código IBGE precisa ter examente sete dígitos.");

            RuleFor(p => IbgeNumberAlreadyExists(rep, filterBuilder, p)).Equal(false).WithMessage("Já existe uma cidade cadastrada com o código IBGE informado.");
        }

        private bool IbgeNumberAlreadyExists(IRepository<City> rep, IFilterBuilder filterBuilder, City city)
        {
            filterBuilder
                .Equal(city.IbgeNumber.GetType(), city.IbgeNumber)
                
                ;

            return rep.Recover(filterBuilder).Count > 0;
        }
    }
}
