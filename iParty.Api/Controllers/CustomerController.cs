using AutoMapper;
using iParty.Api.Dtos.People;
using iParty.Api.Interfaces.People;
using iParty.Business.Interfaces.People;
using Microsoft.AspNetCore.Mvc;
using System;

namespace iParty.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly IPersonService _personService;

        private readonly IMapper _autoMapper;

        private readonly ICustomerMapper _customerMapper;

        public CustomerController(IPersonService personService, IMapper autoMapper, ICustomerMapper customerMapper)
        {
            _personService = personService;
            _autoMapper = autoMapper;
            _customerMapper = customerMapper;
        }

        [HttpPost]
        public IActionResult Create([FromBody] CustomerDto dto)
        {
            try
            {
                var mapperResult = _customerMapper.Map(dto);

                if (!mapperResult.Success) return BadRequest(mapperResult.Errors);

                var result = _personService.Create(mapperResult.Entity);

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
        [HttpPut]
        public IActionResult Update([FromRoute] Guid id, [FromBody] CustomerDto dto)
        {
            try
            {
                var mapperResult = _customerMapper.Map(dto);

                if (!mapperResult.Success) return BadRequest(mapperResult.Errors);

                mapperResult.Entity.Id = id;

                var result = _personService.Update(id, mapperResult.Entity);

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
                var result = _personService.Delete(id);

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
                var entity = _personService.Get(id);

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
                var entities = _personService.Get();

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
