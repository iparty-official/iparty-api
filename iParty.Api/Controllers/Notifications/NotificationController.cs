using AutoMapper;
using iParty.Api.Dtos.Notifications;
using iParty.Api.Interfaces.Mappers;
using iParty.Api.Views.Notifications;
using iParty.Business.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace iParty.Api.Controllers.Notifications
{
    [ApiController]
    [Route("[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        private readonly IMapper _autoMapper;
        private readonly INotificationMapper _notificationMapper;
        public NotificationController(INotificationService serviceNotification,
                                      IMapper autoMapper, 
                                      INotificationMapper notificationMapper)
        {
            _notificationService = serviceNotification;
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

                var result = _notificationService.Create(mapperResult.Entity);

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

                var result = _notificationService.Update(id, mapperResult.Entity);

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
                var result = _notificationService.Delete(id);

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
                var entity = _notificationService.Get(id);

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
                var entitys = _notificationService.Get();

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
