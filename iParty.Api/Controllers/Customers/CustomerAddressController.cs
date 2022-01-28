using iParty.Api.Dtos.Addresses;
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
    [Route("customer/{customerId}/address")]
    public class CustomerAddressController : ControllerBase
    {
        private readonly ICustomerService _customerService;        

        private readonly ICustomerMapper _customerMapper;

        private readonly IAddressMapper _addressMapper;

        public CustomerAddressController(ICustomerService customerService, ICustomerMapper customerMapper, IAddressMapper addressMapper)
        {
            _customerService = customerService;            
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

                var result = _customerService.AddAddress(customerId, mapperResult.Entity);

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
                var mapperResult = _addressMapper.Map(dto).SetId(id);

                if (!mapperResult.Success) return BadRequest(mapperResult.Errors);                

                var result = _customerService.ReplaceAddress(customerId, id, mapperResult.Entity);

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
                var result = _customerService.RemoveAddress(customerId, id);

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
