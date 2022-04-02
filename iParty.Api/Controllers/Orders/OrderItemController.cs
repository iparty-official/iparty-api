using Swashbuckle.AspNetCore.Annotations;
using iParty.Api.Dtos.Orders;
using iParty.Api.Infra;
using iParty.Api.Interfaces.Mapppers;
using iParty.Business.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using iParty.Api.Views.Orders;

namespace iParty.Api.Controllers.Orders
{
    [Authorize]
    [ApiController]
    [Route("order/{orderId}/item")]
    public class OrderItemController : Controller
    {
        private IOrderMapper _orderMapper;

        private IOrderItemMapper _orderItemMapper;

        private IOrderService _orderService;

        public OrderItemController(IOrderMapper orderMapper, IOrderItemMapper orderItemMapper, IOrderService orderService)
        {
            _orderMapper = orderMapper;
            _orderItemMapper = orderItemMapper;
            _orderService = orderService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(OrderItemView), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        [SwaggerOperation(Summary = OrderItemConstant.CreateSummary, Description = OrderItemConstant.CreateDescription, Tags = new[] { OrderItemConstant.Tag })]
        public IActionResult Create([FromRoute] Guid orderId, [FromBody] OrderItemDto dto)
        {
            try
            {
                var itemMapperResult = _orderItemMapper.Map(dto);

                if (!itemMapperResult.Success) return BadRequest(itemMapperResult.Errors);

                var result = _orderService.AddOrderItem(orderId, itemMapperResult.Entity);

                if (!result.Success) return BadRequest(result.Errors);

                var view = _orderMapper.Map(result.Entity);

                return Ok(view);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [Route("{id}")]
        [HttpPut]
        [ProducesResponseType(typeof(OrderItemView), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        [SwaggerOperation(Summary = OrderItemConstant.UpdateSummary, Description = OrderItemConstant.UpdateDescription, Tags = new[] { OrderItemConstant.Tag })]
        public IActionResult Update([FromRoute] Guid orderId, [FromRoute] Guid id, [FromBody] OrderItemDto dto)
        {
            try
            {
                var itemMapperResult = _orderItemMapper.Map(dto).DefineId(id);

                if (!itemMapperResult.Success) return BadRequest(itemMapperResult.Errors);                

                var result = _orderService.ReplaceOrderItem(orderId, id, itemMapperResult.Entity);

                if (!result.Success) return BadRequest(result.Errors);

                var view = _orderMapper.Map(result.Entity);

                return Ok(view);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [Route("{id}")]
        [HttpDelete]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        [SwaggerOperation(Summary = OrderItemConstant.DeleteSummary, Description = OrderItemConstant.DeleteDescription, Tags = new[] { OrderItemConstant.Tag })]
        public IActionResult Delete([FromRoute] Guid orderId, [FromRoute] Guid id)
        {
            try
            {
                var result = _orderService.RemoveOrderItem(orderId, id);

                if (!result.Success) return BadRequest(result.Errors);

                var view = _orderMapper.Map(result.Entity);

                return Ok(view);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }

        }
    }
}
