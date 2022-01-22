using iParty.Business.Infra;
using iParty.Business.Interfaces.Services;
using iParty.Business.Interfaces.Validations;
using iParty.Business.Models.Review;
using iParty.Business.Interfaces;
using System;
using System.Collections.Generic;

namespace iParty.Business.Services.Reviews
{
    public class ReviewService : IReviewService
    {
        private BasicService<Review> _basicService;

        public ReviewService(IRepository<Review> repository, IReviewValidation reviewValidation)
        {
            _basicService = new BasicService<Review>(repository, reviewValidation);
        }

        public ServiceResult<Review> Create(Review review)
        {
            return _basicService.Create(review);
        }

        public ServiceResult<Review> Update(Guid id, Review review)
        {
            return _basicService.Update(id, review);
        }

        public ServiceResult<Review> Delete(Guid id)
        {
            return _basicService.Delete(id);
        }

        public Review Get(Guid id)
        {
            return _basicService.Get(id);
        }

        public List<Review> Get()
        {
            return _basicService.Get();
        }
    }
}
