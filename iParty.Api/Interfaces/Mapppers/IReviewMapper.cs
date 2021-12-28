using iParty.Api.Dtos.Reviews;
using iParty.Api.Infra;
using iParty.Api.Views.Reviews;
using iParty.Business.Models.Review;
using System.Collections.Generic;

namespace iParty.Api.Interfaces.Mapppers
{
    public interface IReviewMapper
    {
        MapperResult<Review> Map(ReviewDto dto);
        ReviewView Map(Review review);
        List<ReviewView> Map(List<Review> reviews);
    }
}
