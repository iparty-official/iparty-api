using FluentValidation;
using iParty.Business.Models.Review;

namespace iParty.Business.Interfaces.Validations
{
    public interface IReviewValidation : IValidator<Review>
    {
    }
}
