using AutoMapper;
using iParty.Api.Dtos.People;
using iParty.Api.Interfaces.Mappers;
using iParty.Business.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace iParty.Api.Controllers.Suppliers
{
    [ApiController]
    [Route("[controller]")]
    public class SupplierController : ControllerBase
    {
        //TODO: Esse controller está retornando tantos clientes quanto fornecedores. Isso está errado.

        private readonly IPersonService _personService;

        private readonly IMapper _autoMapper;

        private readonly ISupplierMapper _supplierMapper;

        public SupplierController(IPersonService personService, IMapper autoMapper, ISupplierMapper supplierMapper)
        {
            _personService = personService;
            _autoMapper = autoMapper;
            _supplierMapper = supplierMapper;
        }

        [HttpPost]
        public IActionResult Create([FromBody] SupplierDto dto)
        {
            try
            {
                var mapperResult = _supplierMapper.Map(dto);

                if (!mapperResult.Success) return BadRequest(mapperResult.Errors);

                var result = _personService.Create(mapperResult.Entity);

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
        public IActionResult Update([FromRoute] Guid id, [FromBody] SupplierDto dto)
        {
            try
            {
                var mapperResult = _supplierMapper.Map(dto);

                if (!mapperResult.Success) return BadRequest(mapperResult.Errors);

                mapperResult.Entity.Id = id;

                var result = _personService.Update(id, mapperResult.Entity);

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
                var result = _personService.Delete(id);

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
                var entity = _personService.Get(id);

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
                var entities = _personService.Get();

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
