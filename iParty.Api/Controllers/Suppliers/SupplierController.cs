﻿using iParty.Api.Dtos.People;
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
    [Route("[controller]")]
    public class SupplierController : ControllerBase
    {       
        private readonly ISupplierService _supplierService;        

        private readonly ISupplierMapper _supplierMapper;

        public SupplierController(ISupplierService supplierService, ISupplierMapper supplierMapper)
        {
            _supplierService = supplierService;            
            _supplierMapper = supplierMapper;
        }

        [HttpPost]
        public IActionResult Create([FromBody] SupplierDto dto)
        {
            try
            {
                var mapperResult = _supplierMapper.Map(dto);

                if (!mapperResult.Success) return BadRequest(mapperResult.Errors);

                var result = _supplierService.Create(mapperResult.Entity);

                if (!result.Success) return BadRequest(result.Errors);

                var view = _supplierMapper.Map(result.Entity);

                return Ok(view);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [Route("{id}/{version}")]
        [HttpPut]
        public IActionResult Update([FromRoute] Guid id, [FromRoute] Guid version, [FromBody] SupplierDto dto)
        {
            try
            {
                var mapperResult = _supplierMapper.Map(dto).DefineIdAndVersion(id, version);

                if (!mapperResult.Success) return BadRequest(mapperResult.Errors);                

                var result = _supplierService.Update(id, mapperResult.Entity);

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
        public IActionResult Delete([FromRoute] Guid id)
        {
            try
            {
                var result = _supplierService.Delete(id);

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
                var entity = _supplierService.Get(id);

                var view = _supplierMapper.Map(entity);

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
                var entities = _supplierService.Get();

                var view = _supplierMapper.Map(entities);

                return Ok(view);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

    }
}
