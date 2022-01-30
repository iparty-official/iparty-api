using AutoMapper;
using iParty.Api.Dtos.Reviews;
using iParty.Api.Infra;
using iParty.Api.Interfaces.Mapppers;
using iParty.Api.Views.Reviews;
using iParty.Business.Infra.Extensions;
using iParty.Business.Models.Orders;
using iParty.Business.Models.Review;
using iParty.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace iParty.Api.Mappers.Reviews
{
    public class ReviewMapper : BaseMapper<Review>, IReviewMapper
    {
        private IRepository<Order> _orderRepository;
        private readonly IMapper _autoMapper;
        public ReviewMapper(IRepository<Order> orderRepository, IMapper autoMapper)
        {
            _orderRepository = orderRepository;
            _autoMapper = autoMapper;
        }

        public MapperResult<Review> Map(ReviewDto dto)
        {
            var order = getOrder(dto.OrderId);
            var orderItem = getOrderItem(order, dto.OrderItemId);

            if (!SuccessResult()) return GetResult();

            SetEntity(new Review(
                dto.Date,
                dto.Stars,
                dto.Description,                               
                new OrderItemForReview(order.Id, orderItem.Id, new ItemForOrderItemForReview(orderItem.Item.Id, orderItem.Item.Name))));

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
            if (review == null) return null;
            return _autoMapper.Map<ReviewView>(review);
        }

        private OrderItem getOrderItem(Order order, Guid orderItemId)
        {
            if (order == null) return null;
            return order.Items.FirstOrDefault(x => x.Id == orderItemId).IfNull(() => AddError("O item do pedido informado na avaliação não existe."));
        }

        private Order getOrder(Guid orderId)
        {
            return _orderRepository.RecoverById(orderId).IfNull(() => AddError("O pedido informado na avaliação não existe."));
        }
    }
}
