using AutoMapper;
using AutoMapper.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TodoApplication.Helpers;
using TodoApplication.Models;
using TodoApplication.Services.UserListServices;

namespace TodoApplication.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserListService _userListService;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;

        public UserController(IUserListService userListService, IMapper mapper, IOptions<AppSettings> appSettings)
        {
            _userListService = userListService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
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
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                Console.WriteLine("Key is " + key);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, user.Id.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                //return basic user info
                return Ok(new
                {
                    Token = tokenString
                });
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
