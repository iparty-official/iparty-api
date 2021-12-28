using iParty.Business.Infra;
using iParty.Business.Models.Review;
using System;

namespace iParty.Business.Interfaces.Services
{
    public interface IReviewService : IService<Review>
    {
        ServiceResult<Review> Create(Review review);
        ServiceResult<Review> Update(Guid id, Review review);
    }
}
