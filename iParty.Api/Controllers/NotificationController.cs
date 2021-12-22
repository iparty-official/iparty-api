using AutoMapper;
using iParty.Api.Dtos.Notifications;
using iParty.Api.Interfaces.Notifications;
using iParty.Api.Views.Notifications;
using iParty.Business.Interfaces.Notifications;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace iParty.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _serviceNotification;
        private readonly IMapper _autoMapper;
        private readonly INotificationMapper _notificationMapper;
        public NotificationController(INotificationService serviceNotification,
                                      IMapper autoMapper, 
                                      INotificationMapper notificationMapper)
        {
            _serviceNotification = serviceNotification;
            _autoMapper = autoMapper;
            _notificationMapper = notificationMapper;
        }

        [HttpPost]
        public IActionResult Create([FromBody] NotificationDto dto)
        {
            try
            {
                var mapperResult = _notificationMapper.Map(dto);

                if (!mapperResult.Success) return BadRequest(mapperResult.Errors);

                var result = _serviceNotification.Create(mapperResult.Entity);

                if (!result.Success) return BadRequest(result.Errors);

                var view = _autoMapper.Map<NotificationView>(result.Entity);

                return Ok(view);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [Route("{id}")]
        [HttpPut]
        public IActionResult Update([FromRoute] Guid id, [FromBody] NotificationDto dto)
        {
            try
            {
                var mapperResult = _notificationMapper.Map(dto);

                if (!mapperResult.Success) return BadRequest(mapperResult.Errors);

                mapperResult.Entity.Id = id;

                var result = _serviceNotification.Update(id, mapperResult.Entity);

                if (!result.Success) return BadRequest(result.Errors);

                var view = _autoMapper.Map<NotificationView>(result.Entity);

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
                var result = _serviceNotification.Delete(id);

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
                var entity = _serviceNotification.Get(id);

                var view = _autoMapper.Map<NotificationView>(entity);

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
                var entitys = _serviceNotification.Get();

                var view = _autoMapper.Map<List<NotificationView>>(entitys);

                return Ok(view);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
