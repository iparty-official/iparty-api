using FluentValidation;
using iParty.Business.Models.Addresses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iParty.Business.Validations
{
    public class CityValidation : AbstractValidator<City>
    {
        public CityValidation()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("Nome da cidade não foi informado.");

            RuleFor(p => p.IbgeNumber).GreaterThan(1000000).WithMessage("O código IBGE precisa ter examente sete dígitos.");

            RuleFor(p => p.IbgeNumber).LessThan(9999999).WithMessage("O código IBGE precisa ter examente sete dígitos.");            
        }
    }
}
