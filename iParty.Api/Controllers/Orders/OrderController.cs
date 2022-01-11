using iParty.Api.Dtos.Orders;
using iParty.Api.Interfaces.Mapppers;
using iParty.Business.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace iParty.Api.Controllers.Orders
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {        
        private IOrderMapper _orderMapper;

        private IOrderService _orderService;

        public OrderController(IOrderMapper orderMapper, IOrderService orderService)
        {
            _orderMapper = orderMapper;
            _orderService = orderService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] OrderDto dto)
        {                        
            try
            {
                var mapperResult = _orderMapper.Map(dto);

                if (!mapperResult.Success) return BadRequest(mapperResult.Errors);

                var result = _orderService.Create(mapperResult.Entity);

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
        public IActionResult Update([FromRoute] Guid id, [FromBody] OrderDto dto)
        {
            try
            {
                var mapperResult = _orderMapper.Map(dto);

                if (!mapperResult.Success) return BadRequest(mapperResult.Errors);

                mapperResult.Entity.Id = id;

                var result = _orderService.Update(id, mapperResult.Entity);

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
        public IActionResult Delete([FromRoute] Guid id)
        {
            try
            {
                var result = _orderService.Delete(id);

                if (!result.Success) return BadRequest(result.Errors);

                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }

        }

        [Route("{id}")]
        [HttpGet]
        public IActionResult Get([FromRoute] Guid id)
        {
            try
            {
                var entity = _orderService.Get(id);

                var view = _orderMapper.Map(entity);

                return Ok(view);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var entities = _orderService.Get();

                var view = _orderMapper.Map(entities);

                return Ok(view);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
