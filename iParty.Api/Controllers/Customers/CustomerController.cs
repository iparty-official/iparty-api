using iParty.Api.Dtos.People;
using iParty.Api.Infra;
using iParty.Api.Interfaces.Mappers;
using iParty.Business.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace iParty.Api.Controllers.Customers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {        
        private readonly ICustomerService _customerService;        

        private readonly ICustomerMapper _customerMapper;

        public CustomerController(ICustomerService customerService, ICustomerMapper customerMapper)
        {
            _customerService = customerService;            
            _customerMapper = customerMapper;
        }

        [HttpPost]
        public IActionResult Create([FromBody] CustomerDto dto)
        {
            try
            {
                var mapperResult = _customerMapper.Map(dto);

                if (!mapperResult.Success) return BadRequest(mapperResult.Errors);

                var result = _customerService.Create(mapperResult.Entity);

                if (!result.Success) return BadRequest(result.Errors);

                var view = _customerMapper.Map(result.Entity);

                return Ok(view);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [Route("{id}/{version}")]
        [HttpPut]
        public IActionResult Update([FromRoute] Guid id, [FromRoute] Guid version, [FromBody] CustomerDto dto)
        {
            try
            {
                var mapperResult = _customerMapper.Map(dto).SetIdAndVersion(id, version);

                if (!mapperResult.Success) return BadRequest(mapperResult.Errors);                

                var result = _customerService.Update(id, mapperResult.Entity);

                if (!result.Success) return BadRequest(result.Errors);

                var view = _customerMapper.Map(result.Entity);

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
                var result = _customerService.Delete(id);

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
                var entity = _customerService.Get(id);

                var view = _customerMapper.Map(entity);

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
                var entities = _customerService.Get();

                var view = _customerMapper.Map(entities);

                return Ok(view);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

    }
}
