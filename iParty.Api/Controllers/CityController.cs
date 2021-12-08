using AutoMapper;
using iParty.Api.View;
using iParty.Business.Infra;
using iParty.Business.Models.Addresses;
using iParty.Business.Services.Citys;
using iParty.Business.Services.Citys.Dtos;
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
            var view = _serviceCity.Create(dto);
            return view;
        }

        [Route("{id}")]
        [HttpPut]
        public NewView Update([FromRoute] Guid id, [FromBody] CityDto dto)
        {
            //HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);

            var view = _serviceCity.Update(id, dto);
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
