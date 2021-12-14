using AutoMapper;
using iParty.Api.Dtos;
using iParty.Api.Interfaces;
using iParty.Api.Views;
using iParty.Business.Interfaces;
using iParty.Business.Models.Messages;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace iParty.Api.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly IServiceMessage _serviceMessage;

        private readonly IMapper _autoMapper;

        private readonly IMessageMapper _messageMapper;

        public MessageController(IServiceMessage serviceMessage, IMapper autoMapper, IMessageMapper messageMapper)
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
                var message = _messageMapper.Map(dto);

                var result = _serviceMessage.Create(message);

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
                var message = _messageMapper.Map(dto);

                message.Id = id;

                var result = _serviceMessage.Update(id, message);

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
