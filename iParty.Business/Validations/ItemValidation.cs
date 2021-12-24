using FluentValidation;
using iParty.Business.Interfaces.Validations;
using iParty.Business.Models.Items;
using iParty.Business.Models.People;
using iParty.Data.Repositories;

namespace iParty.Business.Validations
{
    public class ItemValidation : AbstractValidator<Item>, IItemValidation
    {
        public ItemValidation(IRepository<Person> personRepository)
        {
            //TODO: Adicionar SKU
            //TODO: Impedir duplicidade do item
            //TODO: Impedir duplicidade do schedule

            RuleFor(x => x.Supplier).NotNull().WithMessage("O fornecedor precisa ser informado");

            RuleFor(x => personRepository.RecoverById(x.Supplier.Id)).NotNull().WithMessage("O fornecedor informado não existe.");

            RuleFor(x => x.Name).NotEmpty().WithMessage("O nome do item precisa ser informado.");

            RuleFor(x => x.Details).NotEmpty().WithMessage("Os detalhes do item precisam ser informados.");

            RuleFor(x => x.Price).GreaterThan(0).WithMessage("O preço do item precisa ser informado.");

            RuleFor(x => x.Unit).IsInEnum().WithMessage("A unidade de medida informada é inválida.");

            RuleFor(x => x.ProductOrService).IsInEnum().WithMessage("O valor do campo 'Produto ou Serviço' é inválido.");

            RuleFor(x => x.ProductInfo.ForRentOrSale).IsInEnum().WithMessage("O valor do campo 'Aluguel ou Venda' é inválido.");

            RuleFor(x => x.ProductInfo.AvailableQuantity).GreaterThanOrEqualTo(0).WithMessage("A quantidade disponível em estoque não pode ser negativa");

            RuleForEach(x => x.Schedules).ChildRules(y => y.RuleFor(sch => sch.DayOfWeek).IsInEnum().WithMessage("O dia da semana informado é inválido."));
            
            RuleForEach(x => x.Schedules).ChildRules(y => y.RuleFor(sch => sch.Hours).NotEmpty().WithMessage("Nenhum horário foi informado."));                       

            RuleForEach(x => x.Schedules).ChildRules(y => y.RuleForEach(sch => sch.Hours).Must(hour => hour.InitialHour < hour.FinalHour).WithMessage("A hora inicial precisa ser menor que a hora final"));
        }       
    }
}
