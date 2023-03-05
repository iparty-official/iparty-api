using Swashbuckle.AspNetCore.Annotations;
using AutoMapper;
using iParty.Api.Dtos.People;
using iParty.Api.Interfaces.Mappers;
using iParty.Business.Interfaces.Services;
using iParty.Business.Models.People;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using iParty.Api.Views.People;

namespace iParty.Api.Controllers.Customers
{
    [Authorize]
    [ApiController]
    [Route("customer/{customerId}/phone")]
    public class CustomerPhoneController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        private readonly IMapper _autoMapper;

        private readonly ICustomerMapper _customerMapper;

        public CustomerPhoneController(ICustomerService customerService, IMapper autoMapper, ICustomerMapper customerMapper)
        {
            _customerService = customerService;
            _autoMapper = autoMapper;
            _customerMapper = customerMapper;
        }

        [HttpPost]
        [ProducesResponseType(typeof(PhoneView), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        [SwaggerOperation(Summary = CustomerPhoneConstant.CreateSummary, Description = CustomerPhoneConstant.CreateDescription, Tags = new[] { CustomerPhoneConstant.Tag })]
        public IActionResult Create([FromRoute] Guid customerId, [FromBody] PhoneDto dto)
        {
            try
            {
                var phone = _autoMapper.Map<Phone>(dto);

                var result = _customerService.AddPhone(customerId, phone);

                if (!result.Success) return BadRequest(result.Errors);

                var view = _autoMapper.Map<PhoneView>(result.Entity);

                return Ok(view);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [Route("{id}")]
        [HttpPut]
        [ProducesResponseType(typeof(PhoneView), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        [SwaggerOperation(Summary = CustomerPhoneConstant.UpdateSummary, Description = CustomerPhoneConstant.UpdateDescription, Tags = new[] { CustomerPhoneConstant.Tag })]
        public IActionResult Update([FromRoute] Guid customerId, [FromRoute] Guid id, [FromBody] PhoneDto dto)
        {
            try
            {
                var phone = _autoMapper.Map<Phone>(dto);                               

                var result = _customerService.ReplacePhone(customerId, id, phone);

                if (!result.Success) return BadRequest(result.Errors);

                var view = _autoMapper.Map<PhoneView>(result.Entity);

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
        [SwaggerOperation(Summary = CustomerPhoneConstant.DeleteSummary, Description = CustomerPhoneConstant.DeleteDescription, Tags = new[] { CustomerPhoneConstant.Tag })]
        public IActionResult Delete([FromRoute] Guid customerId, [FromRoute] Guid id)
        {
            try
            {
                var result = _customerService.RemovePhone(customerId, id);

                if (!result.Success) return BadRequest(result.Errors);

                var view = _autoMapper.Map<PhoneView>(result.Entity);

                return Ok(view);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }

        }        
    }
}
