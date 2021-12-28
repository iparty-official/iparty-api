using FluentValidation;
using iParty.Business.Interfaces.Validations;
using iParty.Business.Models.InventoryStatements;
using iParty.Business.Models.Items;
using iParty.Data.Repositories;
using System;

namespace iParty.Business.Validations
{
    public class InventoryStatementValidation : AbstractValidator<InventoryStatement>, IInventoryStatementValidation
    {
        public InventoryStatementValidation(IRepository<Item> itemRepository)
        {
            RuleFor(x => x.Product).NotNull().WithMessage("O item não foi informado.");

            RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("A quantidade precisa ser maior que zero.");

            RuleFor(x => x.InOrOut).IsInEnum().WithMessage("O valor do campo 'Entrada ou Saída' é inválido.");            

            RuleFor(x => x.DateTime).GreaterThan(DateTime.MinValue).WithMessage("A data/hora do lançamento não foi informada.");

            RuleFor(x => itemRepository.RecoverById(x.Product.Id)).NotNull().WithMessage("O item informado não existe.");            
        }
    }
}
