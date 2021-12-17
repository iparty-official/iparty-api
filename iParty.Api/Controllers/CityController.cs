using AutoMapper;
using iParty.Api.Dtos;
using iParty.Api.Views;
using iParty.Business.Interfaces;
using iParty.Business.Models.Addresses;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace iParty.Api.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    public class CityController : ControllerBase
    {
        private readonly ICityService _serviceCity;

        private readonly IMapper _mapper;

        public CityController(ICityService serviceCity, IMapper mapper)
        {
            _serviceCity = serviceCity;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult Create([FromBody] CityDto dto)
        {
            try
            {
                var city = _mapper.Map<City>(dto);

                var result = _serviceCity.Create(city);

                if (!result.Success) return BadRequest(result.Errors);

                var view = _mapper.Map<CityView>(result.Entity);

                return Ok(view);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [Route("{id}")]
        [HttpPut]
        public IActionResult Update([FromRoute] Guid id, [FromBody] CityDto dto)
        {            
            try
            {               
                var city = _mapper.Map<City>(dto);
                city.Id = id;

                var result = _serviceCity.Update(id, city);

                if (!result.Success) return BadRequest(result.Errors);

                var view = _mapper.Map<CityView>(result.Entity);

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
                var result = _serviceCity.Delete(id);

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
                var entity = _serviceCity.Get(id);

                var view  = _mapper.Map<CityView>(entity);

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
                var entitys = _serviceCity.Get();

                var view = _mapper.Map<List<CityView>>(entitys);

                return Ok(view);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);                
            }
        }
      
    }
}
