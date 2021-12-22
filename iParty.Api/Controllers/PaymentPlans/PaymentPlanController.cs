﻿using AutoMapper;
using iParty.Api.Dtos;
using iParty.Api.Views.PaymentPlans;
using iParty.Business.Interfaces.Services;
using iParty.Business.Models.PaymentPlans;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace iParty.Api.Controllers.PaymentPlans
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentPlanController : ControllerBase
    {
        private readonly IMapper _autoMapper;

        private readonly IPaymentPlanService _paymentPlanService;
        public PaymentPlanController(IPaymentPlanService paymentPlanService, IMapper autoMapper)
        {
            _paymentPlanService = paymentPlanService;
            _autoMapper = autoMapper;
        }

        [HttpPost]
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

        [Route("{id}")]
        [HttpPut]
        public IActionResult Update([FromRoute] Guid id, [FromBody] PaymentPlanDto dto)
        {
            try
            {
                var paymentPlan = _autoMapper.Map<PaymentPlan>(dto);

                paymentPlan.Id = id;

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
