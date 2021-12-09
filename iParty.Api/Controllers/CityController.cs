using AutoMapper;
using iParty.Api.Dtos;
using iParty.Api.Views;
using iParty.Business.Infra;
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
        private readonly IServiceCity _serviceCity;

        private readonly IMapper _mapper;

        public CityController(IServiceCity serviceCity, IMapper mapper)
        {
            _serviceCity = serviceCity;
            _mapper = mapper;
        }

        [HttpPost]
        public NewView Create([FromBody] CityDto dto)
        {
            var city = _mapper.Map<City>(dto);
            
            var result = _serviceCity.Create(city);

            var view = _mapper.Map<NewView>(result.Entity);
            
            return view;
        }

        [Route("{id}")]
        [HttpPut]
        public NewView Update([FromRoute] Guid id, [FromBody] CityDto dto)
        {
            //HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);

            var city = _mapper.Map<City>(dto);

            city.Id = id;

            var result = _serviceCity.Update(city);

            var view = _mapper.Map<NewView>(result.Entity);

            return view;
        }

        [Route("{id}")]
        [HttpDelete]
        public void Delete([FromRoute] Guid id)
        {
            _serviceCity.Delete(id);            
        }

        [Route("{id}")]
        [HttpGet]
        public CityView Get([FromRoute] Guid id)
        {
            var entity = _serviceCity.Get(id);

            return _mapper.Map<CityView>(entity);
        }
        
        [HttpGet]
        public List<CityView> Get()
        {
            var entitys = _serviceCity.Get();

            return _mapper.Map<List<CityView>>(entitys);
        }
      
    }
}
