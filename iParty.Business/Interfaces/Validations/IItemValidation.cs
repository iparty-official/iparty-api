using FluentValidation;
using iParty.Business.Models.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iParty.Business.Interfaces.Validations
{
    public interface IItemValidation : IValidator<Item>
    {
    }
}
