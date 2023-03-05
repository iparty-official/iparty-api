using Swashbuckle.AspNetCore.Annotations;
using iParty.Api.Dtos.Addresses;
using iParty.Api.Infra;
using iParty.Api.Interfaces.Mappers;
using iParty.Business.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using iParty.Api.Views.Addresses;
using iParty.Api.Views.People;
using System.Collections.Generic;
using iParty.Business.Models.Addresses;

namespace iParty.Api.Controllers.Customers
{
    [Authorize]
    [ApiController]
    [Route("customer/{customerId}/address")]
    public class CustomerAddressController : ControllerBase
    {
        private readonly ICustomerService _customerService;                

        private readonly IAddressMapper _addressMapper;

        public CustomerAddressController(ICustomerService customerService, IAddressMapper addressMapper)
        {
            _customerService = customerService;                        
            _addressMapper = addressMapper;
        }

        [HttpPost]
        [ProducesResponseType(typeof(AddressView), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        [SwaggerOperation(Summary = CustomerAddressConstant.CreateSummary, Description = CustomerAddressConstant.CreateDescription, Tags = new[] { CustomerAddressConstant.Tag })]
        public IActionResult Create([FromRoute] Guid customerId, [FromBody] AddressDto dto)
        {
            try
            {
                var mapperResult = _addressMapper.Map(dto);

                if (!mapperResult.Success) return BadRequest(mapperResult.Errors);

                var result = _customerService.AddAddress(customerId, mapperResult.Entity);

                if (!result.Success) return BadRequest(result.Errors);

                var view = _addressMapper.Map(result.Entity);

                return Ok(view);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [Route("{id}")]
        [HttpPut]
        [ProducesResponseType(typeof(AddressView), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        [SwaggerOperation(Summary = CustomerAddressConstant.UpdateSummary, Description = CustomerAddressConstant.UpdateDescription, Tags = new[] { CustomerAddressConstant.Tag })]
        public IActionResult Update([FromRoute] Guid customerId, [FromRoute] Guid id, [FromBody] AddressDto dto)
        {
            try
            {
                var mapperResult = _addressMapper.Map(dto).DefineId(id);

                if (!mapperResult.Success) return BadRequest(mapperResult.Errors);                

                var result = _customerService.ReplaceAddress(customerId, id, mapperResult.Entity);

                if (!result.Success) return BadRequest(result.Errors);

                var view = _addressMapper.Map(result.Entity);

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
        [SwaggerOperation(Summary = CustomerAddressConstant.DeleteSummary, Description = CustomerAddressConstant.DeleteDescription, Tags = new[] { CustomerAddressConstant.Tag })]
        public IActionResult Delete([FromRoute] Guid customerId, [FromRoute] Guid id)
        {
            try
            {
                var result = _customerService.RemoveAddress(customerId, id);

                if (!result.Success) return BadRequest(result.Errors);

                var view = _addressMapper.Map(result.Entity);

                return Ok(view);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }

        }

        [Route("{id}")]
        [HttpGet]
        [ProducesResponseType(typeof(AddressView), 200)]
        [ProducesResponseType(typeof(string), 500)]
        [SwaggerOperation(Summary = CustomerAddressConstant.GetByIdSummary, Description = CustomerAddressConstant.GetByIdDescription, Tags = new[] { CustomerAddressConstant.Tag })]
        public IActionResult Get([FromRoute] Guid customerId, [FromRoute] Guid addressId)
        {
            try
            {
                var entity = _customerService.Get(customerId).Addresses.Find(x => x.Id == addressId);

                var view = _addressMapper.Map(entity);

                return Ok(view);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<AddressView>), 200)]
        [ProducesResponseType(typeof(string), 500)]
        [SwaggerOperation(Summary = CustomerAddressConstant.GetAllSummary, Description = CustomerAddressConstant.GetAllDescription, Tags = new[] { CustomerAddressConstant.Tag })]        
        public IActionResult Get([FromRoute] Guid customerId)
        {
            try
            {
                var entities = _customerService.Get(customerId).Addresses;

                var view = _addressMapper.Map(entities);

                return Ok(view);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
