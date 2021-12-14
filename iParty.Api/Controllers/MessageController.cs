using AutoMapper;
using iParty.Api.Dtos;
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

        private readonly IMapper _mapper;

        public MessageController(IServiceMessage serviceMessage, IMapper mapper)
        {
            _serviceMessage = serviceMessage;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult Create([FromBody] MessageDto dto)
        {
            try
            {
                var message = _mapper.Map<Message>(dto);

                var result = _serviceMessage.Create(message);

                if (!result.Success) return BadRequest(result.Errors);

                var view = _mapper.Map<MessageView>(result.Entity);

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
                var message = _mapper.Map<Message>(dto);

                message.Id = id;

                var result = _serviceMessage.Update(message);

                if (!result.Success) return BadRequest(result.Errors);

                var view = _mapper.Map<MessageView>(result.Entity);

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

                var view = _mapper.Map<MessageView>(entity);

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

                var view = _mapper.Map<List<MessageView>>(entitys);

                return Ok(view);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

    }
}
