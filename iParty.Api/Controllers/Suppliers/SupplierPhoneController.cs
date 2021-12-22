using AutoMapper;
using iParty.Api.Dtos.People;
using iParty.Api.Interfaces.Mappers;
using iParty.Business.Interfaces.Services;
using iParty.Business.Models.People;
using Microsoft.AspNetCore.Mvc;
using System;

namespace iParty.Api.Controllers.Suppliers
{
    [ApiController]
    [Route("supplier/{supplierId}/phone")]
    public class SupplierPhoneController : ControllerBase
    {
        private readonly IPersonService _personService;

        private readonly IMapper _autoMapper;

        private readonly ISupplierMapper _supplierMapper;

        public SupplierPhoneController(IPersonService personService, IMapper autoMapper, ISupplierMapper supplierMapper)
        {
            _personService = personService;
            _autoMapper = autoMapper;
            _supplierMapper = supplierMapper;
        }

        [HttpPost]
        public IActionResult Create([FromRoute] Guid customerId, [FromBody] PhoneDto dto)
        {
            try
            {
                var phone = _autoMapper.Map<Phone>(dto);

                var result = _personService.AddPhone(customerId, phone);

                if (!result.Success) return BadRequest(result.Errors);

                var view = _supplierMapper.Map(result.Entity);

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

                var view = _supplierMapper.Map(result.Entity);

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

                var view = _supplierMapper.Map(result.Entity);

                return Ok(view);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }

        }        
    }
}
