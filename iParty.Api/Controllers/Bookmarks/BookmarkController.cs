using Swashbuckle.AspNetCore.Annotations;
using iParty.Api.Dtos.Bookmarks;
using iParty.Api.Interfaces.Mappers;
using iParty.Business.Interfaces;
using iParty.Business.Interfaces.Validations;
using iParty.Business.Models.Bookmark;
using iParty.Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using iParty.Api.Views.Bookmarks;
using System.Collections.Generic;

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
        [ProducesResponseType(typeof(BookmarkView), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        [SwaggerOperation(Summary = BookmarkConstant.CreateSummary, Description = BookmarkConstant.CreateDescription, Tags = new[] { BookmarkConstant.Tag })]
        //TODO: Esse método recebe um DTO e este DTO possui o ID do cliente. Não deveríamos receber o ID do cliente. Em vez disso deveríamos obter o ID do cliente a partir do usuário que consta no token de autenticação
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
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        [SwaggerOperation(Summary = BookmarkConstant.DeleteSummary, Description = BookmarkConstant.DeleteDescription, Tags = new[] { BookmarkConstant.Tag })]
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
        [ProducesResponseType(typeof(BookmarkView), 200)]        
        [ProducesResponseType(typeof(string), 500)]
        [SwaggerOperation(Summary = BookmarkConstant.GetByIdSummary, Description = BookmarkConstant.GetByIdDescription, Tags = new[] { BookmarkConstant.Tag })]
        //TODO: Este método me parece inútil e acredito que ele deveria ser removido. Ver texto da constante GetByIdDescription.
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
        [ProducesResponseType(typeof(List<BookmarkView>), 200)]
        [ProducesResponseType(typeof(string), 500)]
        [SwaggerOperation(Summary = BookmarkConstant.GetAllSummary, Description = BookmarkConstant.GetAllDescription, Tags = new[] { BookmarkConstant.Tag })]
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
