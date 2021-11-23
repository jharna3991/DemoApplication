using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApplication.Helpers;
using TodoApplication.Models;
using TodoApplication.Services.UserListServices;

namespace TodoApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserListService _userListService;
        private readonly IMapper _mapper;

        public UserController(IUserListService userListService, IMapper mapper)
        {
            _userListService = userListService;
            _mapper = mapper;
        }
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] LoginModel model)
        {
            //map UserModel to Registermodel
            var user = _mapper.Map<UserModel>(model);

            try
            {
                var obj = _userListService.Authenticate(model.UserName, model.Password);
                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterUser model)
        {
            //map UserModel to Registermodel
            var user = _mapper.Map<UserModel>(model);
            try
            {
                var obj = _userListService.CreateUser(user, model.Password);
                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }

    }
}
