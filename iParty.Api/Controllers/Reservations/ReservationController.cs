using iParty.Api.Dtos.Reservations;
using iParty.Api.Interfaces.Mappers;
using iParty.Business.Interfaces;
using iParty.Business.Interfaces.Validations;
using iParty.Business.Models.Reservation;
using iParty.Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using Swashbuckle.AspNetCore.Annotations;
using iParty.Api.Views.Reservations;
using System.Collections.Generic;

namespace iParty.Api.Controllers.Reservations
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ReservationController : ControllerBase
    {
        private readonly BasicService<Reservation> _serviceReservation;

        private readonly IReservationMapper _reservationMapper;

        public ReservationController(IReservationMapper reservationMapper, IReservationValidation validation, IRepository<Reservation> repository)
        {
            _serviceReservation = new BasicService<Reservation>(repository, validation);
            _reservationMapper = reservationMapper;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ReservationView), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        [SwaggerOperation(Summary = ReservationConstant.CreateSummary, Description = ReservationConstant.CreateDescription, Tags = new[] { ReservationConstant.Tag })]
        public IActionResult Create([FromBody] ReservationDto dto)
        {
            try
            {
                var mapperResult = _reservationMapper.Map(dto);

                if (!mapperResult.Success) return BadRequest(mapperResult.Errors);

                var result = _serviceReservation.Create(mapperResult.Entity);

                if (!result.Success) return BadRequest(result.Errors);

                var view = _reservationMapper.Map(result.Entity);

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
        [SwaggerOperation(Summary = ReservationConstant.CreateSummary, Description = ReservationConstant.CreateDescription, Tags = new[] { ReservationConstant.Tag })]
        public IActionResult Delete([FromRoute] Guid id)
        {
            try
            {
                var result = _serviceReservation.Delete(id);

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
        [ProducesResponseType(typeof(ReservationView), 200)]        
        [ProducesResponseType(typeof(string), 500)]
        [SwaggerOperation(Summary = ReservationConstant.CreateSummary, Description = ReservationConstant.CreateDescription, Tags = new[] { ReservationConstant.Tag })]
        public IActionResult Get([FromRoute] Guid id)
        {
            try
            {
                var entity = _serviceReservation.Get(id);

                var view = _reservationMapper.Map(entity);

                return Ok(view);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<ReservationView>), 200)]        
        [ProducesResponseType(typeof(string), 500)]
        [SwaggerOperation(Summary = ReservationConstant.CreateSummary, Description = ReservationConstant.CreateDescription, Tags = new[] { ReservationConstant.Tag })]
        public IActionResult Get()
        {
            try
            {
                var entities = _serviceReservation.Get();

                var view = _reservationMapper.Map(entities);

                return Ok(view);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

    }
}
