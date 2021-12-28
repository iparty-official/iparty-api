using iParty.Api.Dtos.Reviews;
using iParty.Api.Interfaces.Mapppers;
using iParty.Business.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace iParty.Api.Controllers.Reviews
{
    [ApiController]
    [Route("[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewMapper _reviewMapper;
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewMapper reviewMapper, IReviewService reviewService)
        {
            _reviewMapper = reviewMapper;
            _reviewService = reviewService;
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

        [Route("{id}")]
        [HttpPut]
        public IActionResult Update([FromRoute] Guid id, [FromBody] ReviewDto dto)
        {
            try
            {
                var mapperResult = _reviewMapper.Map(dto);

                if (!mapperResult.Success) return BadRequest(mapperResult.Errors);

                mapperResult.Entity.Id = id;

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
