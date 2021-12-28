using FluentValidation;
using iParty.Business.Interfaces.Validations;
using iParty.Business.Models.Review;

namespace iParty.Business.Validations
{
    public class ReviewValidation : AbstractValidator<Review>, IReviewValidation
    {
        public ReviewValidation()
        {
            RuleFor(p => p.Date).NotNull().WithMessage("A data da avaliação não foi informada.");
            RuleFor(p => p.Time).NotNull().WithMessage("O tempo para avaliação não foi informado.");
            RuleFor(p => p.Description).NotEmpty().WithMessage("A descrição da avaliação não foi informada.");
            RuleFor(p => p.Stars).InclusiveBetween(1, 5).WithMessage("As quantidades de estrelas precisam estar entre 1 e 5.");                                 
        }
    }
}
