using iParty.Business.Infra;
using iParty.Business.Services.Citys;
using iParty.Business.Services.Citys.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;

namespace iParty.Api.Controllers
{ 
    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    public class CityController : ControllerBase
    {
        private readonly IServiceCity _serviceCity;
        public CityController(IServiceCity serviceCity)
        {
            _serviceCity = serviceCity;
        }
        
        [HttpPost]
        public NewView Create([FromBody] CityDto dto)
        {
            var view = _serviceCity.Create(dto);
            return view;
        }

        [Route("/{id}")]
        [HttpPut]
        public NewView Update([FromRoute] Guid id, [FromBody] CityDto dto)
        {
            var view = _serviceCity.Update(id, dto);
            return view;
        }

        [Route("/{id}")]
        [HttpDelete]
        public void Delete([FromRoute] Guid id)
        {
            _serviceCity.Delete(id);            
        }
    }
}
