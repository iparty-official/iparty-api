using iParty.Api.Dtos.Reservations;
using iParty.Api.Interfaces.Mappers;
using iParty.Business.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace iParty.Api.Controllers.Reservations
{
    //TODO: Revisar validações do tipo NotNull() em todos os serviços.

    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _serviceReservation;

        private readonly IReservationMapper _reservationMapper;

        public ReservationController(IReservationService serviceReservation, IReservationMapper reservationMapper)
        {
            _serviceReservation = serviceReservation;
            _reservationMapper = reservationMapper;
        }

        [HttpPost]
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
