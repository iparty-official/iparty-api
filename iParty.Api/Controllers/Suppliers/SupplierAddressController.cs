using AutoMapper;
using iParty.Api.Dtos.Addresses;
using iParty.Api.Infra;
using iParty.Api.Interfaces.Mappers;
using iParty.Business.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace iParty.Api.Controllers.Suppliers
{
    [Authorize]
    [ApiController]
    [Route("supplier/{supplierId}/address")]
    public class SupplierAddressController : ControllerBase
    {
        private readonly ISupplierService _supplierService;

        private readonly IMapper _autoMapper;

        private readonly ISupplierMapper _supplierMapper;

        private readonly IAddressMapper _addressMapper;

        public SupplierAddressController(ISupplierService supplierService, IMapper autoMapper, ISupplierMapper supplierMapper, IAddressMapper addressMapper)
        {
            _supplierService = supplierService;
            _autoMapper = autoMapper;
            _supplierMapper = supplierMapper;
            _addressMapper = addressMapper;
        }

        [HttpPost]
        public IActionResult Create([FromRoute] Guid supplierId, [FromBody] AddressDto dto)
        {
            try
            {
                var mapperResult = _addressMapper.Map(dto);

                if (!mapperResult.Success) return BadRequest(mapperResult.Errors);

                var result = _supplierService.AddAddress(supplierId, mapperResult.Entity);

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
        public IActionResult Update([FromRoute] Guid supplierId, [FromRoute] Guid id, [FromBody] AddressDto dto)
        {
            try
            {
                var mapperResult = _addressMapper.Map(dto).SetId(id);

                if (!mapperResult.Success) return BadRequest(mapperResult.Errors);                

                var result = _supplierService.ReplaceAddress(supplierId, id, mapperResult.Entity);

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
        public IActionResult Delete([FromRoute] Guid supplierId, [FromRoute] Guid id)
        {
            try
            {
                var result = _supplierService.RemoveAddress(supplierId, id);

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
