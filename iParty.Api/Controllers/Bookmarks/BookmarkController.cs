using iParty.Api.Dtos.Bookmarks;
using iParty.Api.Interfaces.Mappers;
using iParty.Business.Interfaces;
using iParty.Business.Interfaces.Validations;
using iParty.Business.Models.Bookmark;
using iParty.Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace iParty.Api.Controllers.Bookmarks
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class BookmarkController : ControllerBase
    {
        private readonly BasicService<Bookmark> _serviceBookmark;

        private readonly IBookmarkMapper _bookmarkMapper;

        public BookmarkController(IBookmarkMapper bookmarkMapper, IRepository<Bookmark> repository, IBookmarkValidation validation)
        {
            _serviceBookmark = new BasicService<Bookmark>(repository, validation);
            _bookmarkMapper = bookmarkMapper;            
        }

        [HttpPost]
        public IActionResult Create([FromBody] BookmarkDto dto)
        {
            try
            {
                var mapperResult = _bookmarkMapper.Map(dto);

                if (!mapperResult.Success)
                {
                    return BadRequest(mapperResult.Errors);
                }

                var result = _serviceBookmark.Create(mapperResult.Entity);

                if (!result.Success)
                {
                    return BadRequest(result.Errors);
                }

                var view = _bookmarkMapper.Map(result.Entity);

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
                var result = _serviceBookmark.Delete(id);

                if (!result.Success)
                {
                    return BadRequest(result.Errors);
                }

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
                var entity = _serviceBookmark.Get(id);

                var view = _bookmarkMapper.Map(entity);

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
                var entities = _serviceBookmark.Get();

                var view = _bookmarkMapper.Map(entities);

                return Ok(view);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

    }
}
