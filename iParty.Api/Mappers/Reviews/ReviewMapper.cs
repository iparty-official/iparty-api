using AutoMapper;
using iParty.Api.Dtos.Reviews;
using iParty.Api.Infra;
using iParty.Api.Interfaces.Mapppers;
using iParty.Api.Views.Orders;
using iParty.Api.Views.Reviews;
using iParty.Business.Infra.Extensions;
using iParty.Business.Interfaces;
using iParty.Business.Models.Orders;
using iParty.Business.Models.Review;
using System;
using System.Collections.Generic;
using System.Linq;

namespace iParty.Api.Mappers.Reviews
{
    public class ReviewMapper : BaseMapper<Review>, IReviewMapper
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly IMapper _autoMapper;
        public ReviewMapper(IRepository<Order> orderRepository, IMapper autoMapper)
        {
            _orderRepository = orderRepository;
            _autoMapper = autoMapper;
        }

        public MapperResult<Review> Map(ReviewDto dto)
        {
            var orderItem = getOrderItem(dto.OrderId, dto.OrderItemId);

            if (!SuccessResult())
            {
                return GetResult();
            }

            SetEntity(new Review
            {
                Date = dto.Date,
                Description = dto.Description,
                Stars = dto.Stars,
                Time = dto.Time,
                OrderItem = orderItem
            });

            return GetResult();
        }

        public ReviewView Map(Review review)
        {
            return mapToView(review);
        }

        public List<ReviewView> Map(List<Review> reviews)
        {
            var reviewsView = new List<ReviewView>();

            foreach (Review review in reviews)
            {
                reviewsView.Add(mapToView(review));
            }

            return reviewsView;
        }

        private ReviewView mapToView(Review review)
        {
            if (review == null)
            {
                return null;
            }

            return new ReviewView
            {
                Date = review.Date,
                Description = review.Description,
                Stars = review.Stars,
                Time = review.Time,
                OrderItem = _autoMapper.Map<OrderItemView>(review.OrderItem)
            };
        }

        private OrderItem getOrderItem(Guid orderId, Guid orderItemId)
        {
            var order = getOrder(orderId);

            if (order == null)
            {
                return null;
            }

            return order.Items.FirstOrDefault(x => x.Id == orderItemId).IfNull(() => AddError("O item do pedido informado na avaliação não existe."));
        }

        private Order getOrder(Guid orderId)
        {
            return _orderRepository.RecoverById(orderId).IfNull(() => AddError("O pedido informado na avaliação não existe."));
        }
    }
}
