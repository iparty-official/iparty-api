using AutoMapper;
using iParty.Api.Dtos.PaymentPlans;
using iParty.Api.Views.PaymentPlans;
using iParty.Business.Interfaces;
using iParty.Business.Interfaces.Validations;
using iParty.Business.Models.PaymentPlans;
using iParty.Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;

namespace iParty.Api.Controllers.PaymentPlans
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class PaymentPlanController : ControllerBase
    {
        private readonly IMapper _autoMapper;

        private readonly BasicService<PaymentPlan> _paymentPlanService;
        public PaymentPlanController(IMapper autoMapper, IPaymentPlanValidation validation, IRepository<PaymentPlan> repository)
        {
            _paymentPlanService = new BasicService<PaymentPlan>(repository, validation);
            _autoMapper = autoMapper;
        }

        [HttpPost]
        [ProducesResponseType(typeof(PaymentPlanView), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        [SwaggerOperation(Summary = PaymentPlanConstant.CreateSummary, Description = PaymentPlanConstant.CreateDescription, Tags = new[] { PaymentPlanConstant.Tag })]
        public IActionResult Create([FromBody] PaymentPlanDto dto)
        {
            try
            {
                var paymentPlan = _autoMapper.Map<PaymentPlan>(dto);

                var result = _paymentPlanService.Create(paymentPlan);

                if (!result.Success) return BadRequest(result.Errors);

                var view = _autoMapper.Map<PaymentPlanView>(result.Entity);

                return Ok(view);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [Route("{id}/{version}")]
        [HttpPut]
        [ProducesResponseType(typeof(PaymentPlanView), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        [SwaggerOperation(Summary = PaymentPlanConstant.UpdateSummary, Description = PaymentPlanConstant.UpdateDescription, Tags = new[] { PaymentPlanConstant.Tag })]
        public IActionResult Update([FromRoute] Guid id, [FromRoute] Guid version, [FromBody] PaymentPlanDto dto)
        {
            try
            {
                var paymentPlan = _autoMapper.Map<PaymentPlan>(dto);
                
                paymentPlan.DefineIdAndVersion(id, version);                

                var result = _paymentPlanService.Update(id, paymentPlan);

                if (!result.Success) return BadRequest(result.Errors);

                var view = _autoMapper.Map<PaymentPlanView>(result.Entity);

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
        [SwaggerOperation(Summary = PaymentPlanConstant.DeleteSummary, Description = PaymentPlanConstant.DeleteDescription, Tags = new[] { PaymentPlanConstant.Tag })]
        public IActionResult Delete([FromRoute] Guid id)
        {
            try
            {
                var result = _paymentPlanService.Delete(id);

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
        [ProducesResponseType(typeof(PaymentPlanView), 200)]        
        [ProducesResponseType(typeof(string), 500)]
        [SwaggerOperation(Summary = PaymentPlanConstant.GetByIdSummary, Description = PaymentPlanConstant.GetByIdDescription, Tags = new[] { PaymentPlanConstant.Tag })]
        public IActionResult Get([FromRoute] Guid id)
        {
            try
            {
                var entity = _paymentPlanService.Get(id);

                var view = _autoMapper.Map<PaymentPlanView>(entity);

                return Ok(view);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<PaymentPlanView>), 200)]        
        [ProducesResponseType(typeof(string), 500)]
        [SwaggerOperation(Summary = PaymentPlanConstant.GetAllSummary, Description = PaymentPlanConstant.GetAllDescription, Tags = new[] { PaymentPlanConstant.Tag })]
        public IActionResult Get()
        {
            try
            {
                var entitys = _paymentPlanService.Get();

                var view = _autoMapper.Map<List<PaymentPlanView>>(entitys);

                return Ok(view);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
