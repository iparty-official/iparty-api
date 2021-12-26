using AutoMapper;
using iParty.Api.Dtos.InventoryStatements;
using iParty.Api.Interfaces.Mappers;
using iParty.Api.Views.InventoryStatements;
using iParty.Business.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace iParty.Api.Controllers.InventoryStatements
{
    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    public class InventoryStatementController : ControllerBase
    {
        private readonly IInventoryStatementService _serviceInventoryStatement;        

        private readonly IInventoryStatementMapper _inventoryStatementMapper;

        public InventoryStatementController(IInventoryStatementService serviceInventoryStatement, IInventoryStatementMapper inventoryStatementMapper)
        {
            _serviceInventoryStatement = serviceInventoryStatement;            
            _inventoryStatementMapper = inventoryStatementMapper;
        }

        [HttpPost]
        public IActionResult Create([FromBody] InventoryStatementDto dto)
        {
            try
            {
                var mapperResult = _inventoryStatementMapper.Map(dto);

                if (!mapperResult.Success) return BadRequest(mapperResult.Errors);

                var result = _serviceInventoryStatement.Create(mapperResult.Entity);

                if (!result.Success) return BadRequest(result.Errors);

                var view = _inventoryStatementMapper.Map(result.Entity);

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
                var result = _serviceInventoryStatement.Delete(id);

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
                var entity = _serviceInventoryStatement.Get(id);

                var view = _inventoryStatementMapper.Map(entity);

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
                var entitys = _serviceInventoryStatement.Get();

                var view = _inventoryStatementMapper.Map(entitys);

                return Ok(view);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

    }
}
