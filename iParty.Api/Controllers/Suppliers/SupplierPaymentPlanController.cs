using iParty.Api.Dtos;
using iParty.Api.Interfaces.Mappers;
using iParty.Business.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using Swashbuckle.AspNetCore.Annotations;
using iParty.Api.Views.PaymentPlans;

namespace iParty.Api.Controllers.Suppliers
{
    [Authorize]
    [ApiController]
    [Route("supplier/{supplierId}/paymentplan")]
    public class SupplierPaymentPlanController : ControllerBase
    {
        private readonly ISupplierService _supplierService;        

        private readonly ISupplierMapper _supplierMapper;      

        public SupplierPaymentPlanController(ISupplierService supplierService, ISupplierMapper supplierMapper)
        {
            _supplierService = supplierService;            
            _supplierMapper = supplierMapper;            
        }
        
        [HttpPost]
        [ProducesResponseType(typeof(PaymentPlanView), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        [SwaggerOperation(Summary = SupplierPaymentPlanConstant.CreateSummary, Description = SupplierPaymentPlanConstant.CreateDescription, Tags = new[] { SupplierPaymentPlanConstant.Tag })]
        public IActionResult Create([FromRoute] Guid supplierId, [FromBody] GuidDto dto)
        {
            try
            {               
                var result = _supplierService.AddPaymentPlan(supplierId, dto.Id);

                if (!result.Success) return BadRequest(result.Errors);

                var view = _supplierMapper.Map(result.Entity);

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
        [SwaggerOperation(Summary = SupplierPaymentPlanConstant.DeleteSummary, Description = SupplierPaymentPlanConstant.DeleteDescription, Tags = new[] { SupplierPaymentPlanConstant.Tag })]
        public IActionResult Delete([FromRoute] Guid supplierId, [FromRoute] Guid id, [FromRoute] Guid version)
        {
            try
            {
                var result = _supplierService.RemovePaymentPlan(supplierId, id);

                if (!result.Success) return BadRequest(result.Errors);

                var view = _supplierMapper.Map(result.Entity);

                return Ok(view);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }

        }        
    }
}
