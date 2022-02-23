using AutoMapper;
using iParty.Api.Dtos.Users;
using iParty.Api.Infra;
using iParty.Api.Views.Users;
using iParty.Business.Interfaces.Services;
using iParty.Business.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace iParty.Api.Controllers.Users
{    
    [ApiController]    
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult Create([FromBody] UserDto dto)
        {
            try
            {                                
                var user = _mapper.Map<User>(dto);

                var result = _userService.Create(user);

                if (!result.Success) return BadRequest(result.Errors);

                var view = _mapper.Map<UserView>(result.Entity);

                return Ok(view);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [Route("Login")]
        [HttpPost]
        public IActionResult Login([FromBody] UserDto dto)
        {
            try
            {
                var user = _userService.Get(dto.EmailAddress, dto.Password);

                if (user == null)
                    return BadRequest("Usuário ou senha inválidos");

                var token = TokenService.GenerateToken(user);

                return Ok(new LoginView() { EmailAddress = dto.EmailAddress, AccessToken = token});
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [Authorize]
        [Route("{id}/ChangePassword")]
        [HttpPut]
        public IActionResult ChangePassword([FromRoute] Guid id, [FromBody] ChangePasswordDto dto)
        {
            try
            {
                var user = _userService.Get(id);

                if (user == null) return BadRequest("Usuário não encontrado.");

                user.ChangeUserPassword(dto.Password);

                var result = _userService.Update(id, user);

                if (!result.Success) return BadRequest(result.Errors);

                var view = _mapper.Map<UserView>(result.Entity);

                return Ok(view);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [Authorize]
        [Route("{id}")]
        [HttpDelete]
        public IActionResult Delete([FromRoute] Guid id)
        {
            try
            {
                var result = _userService.Delete(id);

                if (!result.Success) return BadRequest(result.Errors);

                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }

        }

        [Authorize]
        [Route("{id}")]
        [HttpGet]
        public IActionResult Get([FromRoute] Guid id)
        {
            try
            {
                var entity = _userService.Get(id);

                var view = _mapper.Map<UserView>(entity);

                return Ok(view);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [Authorize]
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var entitys = _userService.Get();

                var view = _mapper.Map<List<UserView>>(entitys);

                return Ok(view);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
