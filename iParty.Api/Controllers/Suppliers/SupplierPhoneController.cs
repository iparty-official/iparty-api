using AutoMapper;
using iParty.Api.Dtos.People;
using iParty.Api.Interfaces.Mappers;
using iParty.Api.Views.People;
using iParty.Business.Interfaces.Services;
using iParty.Business.Models.People;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;

namespace iParty.Api.Controllers.Suppliers
{
    [Authorize]
    [ApiController]
    [Route("supplier/{supplierId}/phone")]
    public class SupplierPhoneController : ControllerBase
    {
        private readonly ISupplierService _supplierService;

        private readonly IMapper _autoMapper;

        private readonly ISupplierMapper _supplierMapper;

        public SupplierPhoneController(ISupplierService supplierService, IMapper autoMapper, ISupplierMapper supplierMapper)
        {
            _supplierService = supplierService;
            _autoMapper = autoMapper;
            _supplierMapper = supplierMapper;
        }

        [HttpPost]
        [ProducesResponseType(typeof(PhoneView), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        [SwaggerOperation(Summary = SupplierPhoneConstant.CreateSummary, Description = SupplierPhoneConstant.CreateDescription, Tags = new[] { SupplierPhoneConstant.Tag })]
        public IActionResult Create([FromRoute] Guid supplierId, [FromBody] PhoneDto dto)
        {
            try
            {
                var phone = _autoMapper.Map<Phone>(dto);

                var result = _supplierService.AddPhone(supplierId, phone);

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
        [ProducesResponseType(typeof(PhoneView), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        [SwaggerOperation(Summary = SupplierPhoneConstant.UpdateSummary, Description = SupplierPhoneConstant.UpdateDescription, Tags = new[] { SupplierPhoneConstant.Tag })]
        public IActionResult Update([FromRoute] Guid supplierId, [FromRoute] Guid id, [FromBody] PhoneDto dto)
        {
            try
            {
                var phone = _autoMapper.Map<Phone>(dto);                               

                var result = _supplierService.ReplacePhone(supplierId, id, phone);

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
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        [SwaggerOperation(Summary = SupplierPhoneConstant.DeleteSummary, Description = SupplierPhoneConstant.DeleteDescription, Tags = new[] { SupplierPhoneConstant.Tag })]
        public IActionResult Delete([FromRoute] Guid supplierId, [FromRoute] Guid id)
        {
            try
            {
                var result = _supplierService.RemovePhone(supplierId, id);

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
