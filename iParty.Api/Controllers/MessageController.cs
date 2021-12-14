using AutoMapper;
using iParty.Api.Dtos;
using iParty.Api.Views;
using iParty.Business.Interfaces;
using iParty.Business.Models.Messages;
using iParty.Business.Models.Orders;
using iParty.Business.Models.People;
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
            var message = _mapper.Map<Message>(dto);
            
            message.From = new Person() { Id = dto.FromId };
            message.To = new Person() { Id = dto.ToId };
            message.Order = new Order() { Id = dto.OrderId };

            var result = _serviceMessage.Create(message);

            if (!result.Success) return BadRequest(result.Errors);

            var view = _mapper.Map<MessageView>(result.Entity);

            return Ok(view);
        }

        [Route("{id}")]
        [HttpPut]
        public IActionResult Update([FromRoute] Guid id, [FromBody] MessageDto dto)
        {
            //HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);

            var message = _mapper.Map<Message>(dto);

            message.Id = id;
            message.From = new Person() { Id = dto.FromId };
            message.To = new Person() { Id = dto.ToId };
            message.Order = new Order() { Id = dto.OrderId };

            var result = _serviceMessage.Update(message);

            if (!result.Success) return BadRequest(result.Errors);

            var view = _mapper.Map<MessageView>(result.Entity);

            return Ok(view);
        }

        [Route("{id}")]
        [HttpDelete]
        public void Delete([FromRoute] Guid id)
        {
            _serviceMessage.Delete(id);
        }

        [Route("{id}")]
        [HttpGet]
        public MessageView Get([FromRoute] Guid id)
        {
            var entity = _serviceMessage.Get(id);

            return _mapper.Map<MessageView>(entity);
        }

        [HttpGet]
        public List<MessageView> Get()
        {
            var entitys = _serviceMessage.Get();

            return _mapper.Map<List<MessageView>>(entitys);
        }

    }
}
