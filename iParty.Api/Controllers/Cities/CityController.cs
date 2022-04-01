using Swashbuckle.AspNetCore.Annotations;
using AutoMapper;
using iParty.Api.Dtos.Cities;
using iParty.Api.Views.Cities;
using iParty.Business.Interfaces;
using iParty.Business.Interfaces.Validations;
using iParty.Business.Models.Cities;
using iParty.Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace iParty.Api.Controllers.Cities
{
    [Authorize]
    [ApiController]    
    [Route("[controller]")]
    public class CityController : ControllerBase
    {
        private readonly BasicService<City> _cityService;

        private readonly IMapper _mapper;

        public CityController(IMapper mapper, IRepository<City> repository, ICityValidation validation)
        {
            _cityService = new BasicService<City>(repository, validation);
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(typeof(CityView), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        [SwaggerOperation(Summary = CityConstant.CreateSummary, Description = CityConstant.CreateDescription, Tags = new[] { CityConstant.Tag })]
        public IActionResult Create([FromBody] CityDto dto)
        {
            try
            {
                var city = _mapper.Map<City>(dto);

                var result = _cityService.Create(city);

                if (!result.Success) return BadRequest(result.Errors);

                var view = _mapper.Map<CityView>(result.Entity);

                return Ok(view);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [Route("{id}/{version}")]
        [HttpPut]
        [ProducesResponseType(typeof(CityView), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        [SwaggerOperation(Summary = CityConstant.CreateSummary, Description = CityConstant.CreateDescription, Tags = new[] { CityConstant.Tag })]
        public IActionResult Update([FromRoute] Guid id, [FromRoute] Guid version, [FromBody] CityDto dto)
        {            
            try
            {
                var city = _mapper.Map<City>(dto);
                    
                city.DefineIdAndVersion(id, version);

                var result = _cityService.Update(id, city);

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
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        [SwaggerOperation(Summary = CityConstant.CreateSummary, Description = CityConstant.CreateDescription, Tags = new[] { CityConstant.Tag })]
        public IActionResult Delete([FromRoute] Guid id)
        {
            try
            {
                var result = _cityService.Delete(id);

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
        [ProducesResponseType(typeof(CityView), 200)]        
        [ProducesResponseType(typeof(string), 500)]
        [SwaggerOperation(Summary = CityConstant.CreateSummary, Description = CityConstant.CreateDescription, Tags = new[] { CityConstant.Tag })]
        public IActionResult Get([FromRoute] Guid id)
        {
            try
            {
                var entity = _cityService.Get(id);

                var view  = _mapper.Map<CityView>(entity);

                return Ok(view); 
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);  
            }
        }
        
        [HttpGet]
        [ProducesResponseType(typeof(List<CityView>), 200)]        
        [ProducesResponseType(typeof(string), 500)]
        [SwaggerOperation(Summary = CityConstant.CreateSummary, Description = CityConstant.CreateDescription, Tags = new[] { CityConstant.Tag })]
        public IActionResult Get()
        {
            try
            {
                var entitys = _cityService.Get();

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
