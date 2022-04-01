using Swashbuckle.AspNetCore.Annotations;
using iParty.Api.Dtos.Items;
using iParty.Api.Infra;
using iParty.Api.Interfaces.Mappers;
using iParty.Business.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using iParty.Api.Views.Items;
using System.Collections.Generic;

namespace iParty.Api.Controllers.Items
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ItemController : ControllerBase
    {                
        private readonly IItemService _itemService;

        private readonly IItemMapper _itemMapper;

        public ItemController(IItemService itemService, IItemMapper itemMapper)
        {
            _itemService = itemService;
            _itemMapper = itemMapper;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ItemView), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        [SwaggerOperation(Summary = ItemConstant.CreateSummary, Description = ItemConstant.CreateDescription, Tags = new[] { ItemConstant.Tag })]
        public IActionResult Create([FromBody] ItemDto dto)
        {
            try
            {
                var mapperResult = _itemMapper.Map(dto);

                if (!mapperResult.Success) return BadRequest(mapperResult.Errors);

                var result = _itemService.Create(mapperResult.Entity);

                if (!result.Success) return BadRequest(result.Errors);

                var view = _itemMapper.Map(result.Entity);

                return Ok(view);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [Route("{id}/{version}")]
        [HttpPut]
        [ProducesResponseType(typeof(ItemView), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        [SwaggerOperation(Summary = ItemConstant.CreateSummary, Description = ItemConstant.CreateDescription, Tags = new[] { ItemConstant.Tag })]
        public IActionResult Update([FromRoute] Guid id, [FromRoute] Guid version, [FromBody] ItemDto dto)
        {
            try
            {
                var mapperResult = _itemMapper.Map(dto).DefineIdAndVersion(id, version);

                if (!mapperResult.Success) return BadRequest(mapperResult.Errors);                

                var result = _itemService.Update(id, mapperResult.Entity);

                if (!result.Success) return BadRequest(result.Errors);

                var view = _itemMapper.Map(result.Entity);

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
        [SwaggerOperation(Summary = ItemConstant.CreateSummary, Description = ItemConstant.CreateDescription, Tags = new[] { ItemConstant.Tag })]
        public IActionResult Delete([FromRoute] Guid id)
        {
            try
            {
                var result = _itemService.Delete(id);

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
        [ProducesResponseType(typeof(ItemView), 200)]        
        [ProducesResponseType(typeof(string), 500)]
        [SwaggerOperation(Summary = ItemConstant.CreateSummary, Description = ItemConstant.CreateDescription, Tags = new[] { ItemConstant.Tag })]
        public IActionResult Get([FromRoute] Guid id)
        {
            try
            {
                var entity = _itemService.Get(id);

                var view = _itemMapper.Map(entity);

                return Ok(view);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<ItemView>), 200)]        
        [ProducesResponseType(typeof(string), 500)]
        [SwaggerOperation(Summary = ItemConstant.CreateSummary, Description = ItemConstant.CreateDescription, Tags = new[] { ItemConstant.Tag })]
        public IActionResult Get()
        {
            try
            {
                var entities = _itemService.Get();

                var view = _itemMapper.Map(entities);

                return Ok(view);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

    }
}
