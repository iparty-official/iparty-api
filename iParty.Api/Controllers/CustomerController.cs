using AutoMapper;
using iParty.Api.Dtos;
using iParty.Api.Views;
using iParty.Business.Interfaces;
using iParty.Business.Models.People;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace iParty.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly IServicePerson _servicePerson;
        private readonly IMapper _mapper;

        public CustomerController(IServicePerson servicePerson, IMapper mapper)
        {
            _servicePerson = servicePerson;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult Create([FromBody] CustomerDto dto)
        {
            try
            {
                var customer = _mapper.Map<Person>(dto);
                customer.SupplierOrCustomer = SupplierOrCustomer.Customer;
                customer.CustomerInfo = new Customer
                {
                    BirthDate = dto.BirthDate
                };
                
                //var result = _servicePerson.Create(customer);

                //if (!result.Success) return BadRequest(result.Errors);

                //var view = _mapper.Map<CustomerView>(result.Entity);
                //view.BirthDate = result.Entity.CustomerInfo.BirthDate;

                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [Route("{id}")]
        [HttpPut]
        public IActionResult Update([FromRoute] Guid id, [FromBody] CustomerDto dto)
        {         
            try
            {
                var customer = _servicePerson.Get(id);
                if (customer == null) return BadRequest(new List<string> { "Não foi possível localizar o cliente informado." });

                customer = _mapper.Map<Person>(dto);
                customer.SupplierOrCustomer = SupplierOrCustomer.Customer;
                customer.CustomerInfo = new Customer
                {
                    BirthDate = dto.BirthDate
                };

                //var result = _servicePerson.Update(customer);

                //if (!result.Success) return BadRequest(result.Errors);

                //var view = _mapper.Map<CustomerView>(result.Entity);
                //view.BirthDate = result.Entity.CustomerInfo.BirthDate;

                return Ok();
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
                var result = _servicePerson.Delete(id);

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
                var entity = _servicePerson.Get(id);

                var view = _mapper.Map<CustomerView>(entity);

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
                var entitys = _servicePerson.Get();

                var view = _mapper.Map<List<CustomerView>>(entitys);

                return Ok(view);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
