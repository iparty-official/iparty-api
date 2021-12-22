using AutoMapper;
using iParty.Api.Dtos.Messages;
using iParty.Api.Interfaces.Mappers;
using iParty.Api.Views.Messages;
using iParty.Business.Interfaces.Messages;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace iParty.Api.Controllers.Messages
{
    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _serviceMessage;

        private readonly IMapper _autoMapper;

        private readonly IMessageMapper _messageMapper;

        public MessageController(IMessageService serviceMessage, IMapper autoMapper, IMessageMapper messageMapper)
        {
            _serviceMessage = serviceMessage;
            _autoMapper = autoMapper;
            _messageMapper = messageMapper;
        }

        [HttpPost]
        public IActionResult Create([FromBody] MessageDto dto)
        {
            try
            {
                var mapperResult = _messageMapper.Map(dto);

                if (!mapperResult.Success) return BadRequest(mapperResult.Errors);

                var result = _serviceMessage.Create(mapperResult.Entity);

                if (!result.Success) return BadRequest(result.Errors);

                var view = _autoMapper.Map<MessageView>(result.Entity);

                return Ok(view);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [Route("{id}")]
        [HttpPut]
        public IActionResult Update([FromRoute] Guid id, [FromBody] MessageDto dto)
        {            
            try
            {
                var mapperResult = _messageMapper.Map(dto);

                if (!mapperResult.Success) return BadRequest(mapperResult.Errors);

                mapperResult.Entity.Id = id;

                var result = _serviceMessage.Update(id, mapperResult.Entity);

                if (!result.Success) return BadRequest(result.Errors);

                var view = _autoMapper.Map<MessageView>(result.Entity);

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
                var result = _serviceMessage.Delete(id);

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
                var entity = _serviceMessage.Get(id);

                var view = _autoMapper.Map<MessageView>(entity);

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
                var entitys = _serviceMessage.Get();

                var view = _autoMapper.Map<List<MessageView>>(entitys);

                return Ok(view);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

    }
}
