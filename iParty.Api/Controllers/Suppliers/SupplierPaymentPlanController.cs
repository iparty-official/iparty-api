using iParty.Api.Dtos;
using iParty.Api.Interfaces.Mappers;
using iParty.Business.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace iParty.Api.Controllers.Suppliers
{
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
        public IActionResult Delete([FromRoute] Guid supplierId, [FromRoute] Guid id)
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
