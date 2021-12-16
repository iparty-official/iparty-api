using AutoMapper;
using iParty.Api.Dtos;
using iParty.Api.Interfaces;
using iParty.Api.Views;
using iParty.Business.Interfaces;
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
                var notification = _notificationMapper.Map(dto);

                var result = _serviceNotification.Create(notification);

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
                var notification = _notificationMapper.Map(dto);

                notification.Id = id;

                var result = _serviceNotification.Update(id, notification);

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
