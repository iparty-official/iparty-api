using FluentValidation;
using FluentValidation.Results;
using iParty.Business.Interfaces.Validations;
using iParty.Business.Models.Items;
using iParty.Business.Models.People;
using iParty.Data.Repositories;

namespace iParty.Business.Validations
{
    public class ItemValidation : AbstractValidator<Item>, IItemValidation
    {
        private IScheduleValidation _scheduleValidation;

        public ItemValidation(IRepository<Person> personRepository, IScheduleValidation scheduleValidation)
        {
            //TODO: Adicionar SKU
            //TODO: Impedir duplicidade do item
            //TODO: Impedir duplicidade do schedule

            _scheduleValidation = scheduleValidation;

            RuleFor(x => x.Supplier).NotNull().WithMessage("O fornecedor precisa ser informado");

            RuleFor(x => personRepository.RecoverById(x.Supplier.Id)).NotNull().WithMessage("O fornecedor informado não existe.");

            RuleFor(x => x.Name).NotEmpty().WithMessage("O nome do item precisa ser informado.");

            RuleFor(x => x.Details).NotEmpty().WithMessage("Os detalhes do item precisam ser informados.");

            RuleFor(x => x.Price).GreaterThan(0).WithMessage("O preço do item precisa ser informado.");

            RuleFor(x => x.Unit).IsInEnum().WithMessage("A unidade de medida informada é inválida.");

            RuleFor(x => x.ProductOrService).IsInEnum().WithMessage("O valor do campo 'Produto ou Serviço' é inválido.");

            RuleFor(x => x.ProductInfo.ForRentOrSale).IsInEnum().WithMessage("O valor do campo 'Aluguel ou Venda' é inválido.");

            RuleFor(x => x.ProductInfo.AvailableQuantity).GreaterThanOrEqualTo(0).WithMessage("A quantidade disponível em estoque não pode ser negativa");            
        }

        public ValidationResult CustomValidate(Item item)
        {
            var result = this.Validate(item);

            if (!result.IsValid) return result;

            foreach (var schedule in item.Schedules)
            {
                result = _scheduleValidation.Validate(schedule);
                
                if (!result.IsValid) return result;
            }

            return result;
        }
    }
}
