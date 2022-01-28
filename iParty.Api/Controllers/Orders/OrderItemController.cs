using iParty.Api.Dtos.Orders;
using iParty.Api.Infra;
using iParty.Api.Interfaces.Mapppers;
using iParty.Business.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

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
        public IActionResult Update([FromRoute] Guid orderId, [FromRoute] Guid id, [FromBody] OrderItemDto dto)
        {
            try
            {
                var itemMapperResult = _orderItemMapper.Map(dto).SetId(id);

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
