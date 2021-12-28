using iParty.Business.Infra;
using iParty.Business.Interfaces.Services;
using iParty.Business.Interfaces.Validations;
using iParty.Business.Models.Review;
using iParty.Data.Repositories;
using System;

namespace iParty.Business.Services.Reviews
{
    public class ReviewService : Service<Review, IRepository<Review>>, IReviewService
    {
        private readonly IReviewValidation _reviewValidation;
        public ReviewService(IRepository<Review> rep, IReviewValidation reviewValidation) : base(rep)
        {
            _reviewValidation = reviewValidation;
        }

        public ServiceResult<Review> Create(Review review)
        {
            var result = _reviewValidation.Validate(review);

            if (!result.IsValid)
                return GetFailureResult(result);

            Rep.Create(review);

            return GetSuccessResult(review);
        }

        public ServiceResult<Review> Update(Guid id, Review review)
        {
            var currentReview = Get(id);

            if (currentReview == null)
                return GetFailureResult("Não foi possível localizar a avaliação informada.");

            var result = _reviewValidation.Validate(review);

            if (!result.IsValid)
                return GetFailureResult(result);

            Rep.Update(id, review);

            return GetSuccessResult(review);
        }
    }
}
