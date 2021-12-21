using AutoMapper;
using iParty.Api.Dtos;
using iParty.Api.Interfaces;
using iParty.Api.Views;
using iParty.Business.Interfaces;
using iParty.Business.Models.People;
using Microsoft.AspNetCore.Mvc;
using System;

//TODO: Injetar validadores nos serviços
//TODO: No validador de pessoas, chamar validador de telefone e endereços

namespace iParty.Api.Controllers
{
    [ApiController]
    [Route("customer/{customerId}/phone")]
    public class CustomerPhoneController : ControllerBase
    {
        private readonly IPersonService _personService;

        private readonly IMapper _autoMapper;

        private readonly ICustomerMapper _customerMapper;

        public CustomerPhoneController(IPersonService personService, IMapper autoMapper, ICustomerMapper customerMapper)
        {
            _personService = personService;
            _autoMapper = autoMapper;
            _customerMapper = customerMapper;
        }

        [HttpPost]
        public IActionResult Create([FromRoute] Guid customerId, [FromBody] PhoneDto dto)
        {
            try
            {
                var phone = _autoMapper.Map<Phone>(dto);

                var result = _personService.AddPhone(customerId, phone);

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
        public IActionResult Update([FromRoute] Guid customerId, [FromRoute] Guid id, [FromBody] PhoneDto dto)
        {
            try
            {
                var phone = _autoMapper.Map<Phone>(dto);

                phone.Id = id;

                var result = _personService.ReplacePhone(customerId, id, phone);

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
        public IActionResult Delete([FromRoute] Guid customerId, [FromRoute] Guid id)
        {
            try
            {
                var result = _personService.RemovePhone(customerId, id);

                if (!result.Success) return BadRequest(result.Errors);

                var view = _customerMapper.Map(result.Entity);

                return Ok(view);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }

        }        
    }
}
