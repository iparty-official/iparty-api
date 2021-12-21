using AutoMapper;
using iParty.Api.Dtos;
using iParty.Api.Interfaces;
using iParty.Api.Views;
using iParty.Business.Interfaces;
using iParty.Business.Models.People;
using Microsoft.AspNetCore.Mvc;
using System;

namespace iParty.Api.Controllers
{
    [ApiController]
    [Route("customer/{customerId}/address")]
    public class CustomerAddressController : ControllerBase
    {
        private readonly IPersonService _personService;

        private readonly IMapper _autoMapper;

        private readonly ICustomerMapper _customerMapper;

        private readonly IAddressMapper _addressMapper;

        public CustomerAddressController(IPersonService personService, IMapper autoMapper, ICustomerMapper customerMapper, IAddressMapper addressMapper)
        {
            _personService = personService;
            _autoMapper = autoMapper;
            _customerMapper = customerMapper;
            _addressMapper = addressMapper;
        }

        [HttpPost]
        public IActionResult Create([FromRoute] Guid customerId, [FromBody] AddressDto dto)
        {
            try
            {
                var address = _addressMapper.Map(dto);

                var result = _personService.AddAddress(customerId, address);

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
        public IActionResult Update([FromRoute] Guid customerId, [FromRoute] Guid id, [FromBody] AddressDto dto)
        {
            try
            {
                var address = _addressMapper.Map(dto);

                address.Id = id;

                var result = _personService.ReplaceAddress(customerId, id, address);

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
                var result = _personService.RemoveAddress(customerId, id);

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
