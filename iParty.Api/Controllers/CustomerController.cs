using AutoMapper;
using iParty.Api.Dtos;
using iParty.Api.Views;
using iParty.Business.Interfaces;
using iParty.Business.Models.People;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace iParty.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly IPersonService _personService;

        private readonly IMapper _autoMapper;

        public CustomerController(IPersonService personService, IMapper autoMapper)
        {
            _personService = personService;
            _autoMapper = autoMapper;
        }

        [HttpPost]
        public IActionResult Create([FromBody] CustomerDto dto)
        {
            try
            {
                var customer = _autoMapper.Map<Person>(dto);

                var result = _personService.Create(customer);

                if (!result.Success) return BadRequest(result.Errors);

                var view = _autoMapper.Map<CustomerView>(result.Entity);

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
                var customer = _autoMapper.Map<Person>(dto);
                
                customer.Id = id;

                var result = _personService.Update(id, customer);

                if (!result.Success) return BadRequest(result.Errors);

                var view = _autoMapper.Map<CustomerView>(result.Entity);

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

                var view = _autoMapper.Map<CustomerView>(entity);

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
                var entitys = _personService.Get();

                var view = _autoMapper.Map<List<CustomerView>>(entitys);

                return Ok(view);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

    }
}
