using Swashbuckle.AspNetCore.Annotations;
using AutoMapper;
using iParty.Api.Dtos.Items;
using iParty.Api.Interfaces.Mappers;
using iParty.Business.Interfaces.Services;
using iParty.Business.Models.Items;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using iParty.Api.Views.Items;

namespace iParty.Api.Controllers.Items
{
    [Authorize]
    [ApiController]
    [Route("item/{itemId}/schedule")]
    public class ItemScheduleController : ControllerBase
    {
        private readonly IItemService _itemService;        

        private readonly IItemMapper _itemMapper;

        private readonly IMapper _autoMapper;

        public ItemScheduleController(IItemService itemService, IItemMapper itemMapper, IMapper autoMapper)
        {
            _itemService = itemService;            
            _itemMapper = itemMapper;
            _autoMapper = autoMapper;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ScheduleView), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        [SwaggerOperation(Summary = ScheduleConstant.CreateSummary, Description = ScheduleConstant.CreateDescription, Tags = new[] { ScheduleConstant.Tag })]
        public IActionResult Create([FromRoute] Guid itemId, [FromBody] ScheduleDto dto)
        {
            try
            {
                var schedule = _autoMapper.Map<Schedule>(dto);

                var result = _itemService.AddSchedule(itemId, schedule);

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
        [HttpPut]
        [ProducesResponseType(typeof(ScheduleView), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        [SwaggerOperation(Summary = ScheduleConstant.UpdateSummary, Description = ScheduleConstant.UpdateDescription, Tags = new[] { ScheduleConstant.Tag })]
        public IActionResult Update([FromRoute] Guid itemId, [FromRoute] Guid id, [FromBody] ScheduleDto dto)
        {
            try
            {
                var schedule = _autoMapper.Map<Schedule>(dto);
                
                schedule.DefineId(id);

                var result = _itemService.ReplaceSchedule(itemId, id, schedule);

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
        [SwaggerOperation(Summary = ScheduleConstant.DeleteSummary, Description = ScheduleConstant.DeleteDescription, Tags = new[] { ScheduleConstant.Tag })]
        public IActionResult Delete([FromRoute] Guid itemId, [FromRoute] Guid id)
        {
            try
            {
                var result = _itemService.RemoveSchedule(itemId, id);

                if (!result.Success) return BadRequest(result.Errors);

                var view = _itemMapper.Map(result.Entity);

                return Ok(view);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }

        }
    }
}
