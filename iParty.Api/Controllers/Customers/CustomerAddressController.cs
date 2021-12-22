using AutoMapper;
using iParty.Api.Dtos.Addresses;
using iParty.Api.Interfaces.Mappers;
using iParty.Business.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace iParty.Api.Controllers.Customers
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
                var mapperResult = _addressMapper.Map(dto);

                if (!mapperResult.Success) return BadRequest(mapperResult.Errors);

                var result = _personService.AddAddress(customerId, mapperResult.Entity);

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
                var mapperResult = _addressMapper.Map(dto);

                if (!mapperResult.Success) return BadRequest(mapperResult.Errors);

                mapperResult.Entity.Id = id;

                var result = _personService.ReplaceAddress(customerId, id, mapperResult.Entity);

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
