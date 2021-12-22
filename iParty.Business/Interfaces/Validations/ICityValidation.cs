using FluentValidation;
using iParty.Business.Models.Addresses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iParty.Business.Interfaces.Validations
{
    public interface ICityValidation : IValidator<City>
    {
    }
}
