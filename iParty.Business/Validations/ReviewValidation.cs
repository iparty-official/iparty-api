using FluentValidation;
using iParty.Business.Interfaces.Validations;
using iParty.Business.Models.Review;
using System;

namespace iParty.Business.Validations
{
    public class ReviewValidation : AbstractValidator<Review>, IReviewValidation
    {
        public ReviewValidation()
        {
            RuleFor(p => p.Date).GreaterThan(DateTime.MinValue).WithMessage("A data da avaliação não foi informada.");
            RuleFor(p => p.Description).NotEmpty().WithMessage("A descrição da avaliação não foi informada.");
            RuleFor(p => p.Stars).InclusiveBetween(1, 5).WithMessage("As quantidades de estrelas precisam estar entre 1 e 5.");                                 
        }
    }
}
