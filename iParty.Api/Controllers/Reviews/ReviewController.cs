using iParty.Api.Dtos.Reviews;
using iParty.Api.Infra;
using iParty.Api.Interfaces.Mapppers;
using iParty.Business.Interfaces;
using iParty.Business.Interfaces.Validations;
using iParty.Business.Models.Review;
using iParty.Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace iParty.Api.Controllers.Reviews
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewMapper _reviewMapper;
        private readonly BasicService<Review> _reviewService;

        public ReviewController(IReviewMapper reviewMapper, IRepository<Review> repository, IReviewValidation validation)
        {
            _reviewMapper = reviewMapper;
            _reviewService = new BasicService<Review>(repository, validation);
        }

        [HttpPost]
        public IActionResult Create([FromBody] ReviewDto dto)
        {
            try
            {
                var mapperResult = _reviewMapper.Map(dto);

                if (!mapperResult.Success) return BadRequest(mapperResult.Errors);

                var result = _reviewService.Create(mapperResult.Entity);

                if (!result.Success) return BadRequest(result.Errors);

                var view = _reviewMapper.Map(result.Entity);

                return Ok(view);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [Route("{id}/{version}")]
        [HttpPut]
        public IActionResult Update([FromRoute] Guid id, [FromRoute] Guid version, [FromBody] ReviewDto dto)
        {
            try
            {
                var mapperResult = _reviewMapper.Map(dto).DefineIdAndVersion(id, version);

                if (!mapperResult.Success) return BadRequest(mapperResult.Errors);                

                var result = _reviewService.Update(id, mapperResult.Entity);

                if (!result.Success) return BadRequest(result.Errors);

                var view = _reviewMapper.Map(result.Entity);

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
                var result = _reviewService.Delete(id);

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
                var entity = _reviewService.Get(id);

                var view = _reviewMapper.Map(entity);

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
                var entities = _reviewService.Get();

                var view = _reviewMapper.Map(entities);

                return Ok(view);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
