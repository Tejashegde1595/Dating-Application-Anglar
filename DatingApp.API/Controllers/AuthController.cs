using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DatingApp.API.Data;
using DatingApp.API.Models;
using DatingApp.API.Dtos;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController: ControllerBase
    {
        private readonly IAuthRepository _context;

        private readonly IConfiguration _config;
        public AuthController(IAuthRepository context,IConfiguration config){
          _context=context;
          _config=config;
        }
        // GET api/values
        [HttpPost("register")]

        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
            userForRegisterDto.username=userForRegisterDto.username.ToLower();

            if(await _context.UserExists(userForRegisterDto.username))
            {
                return BadRequest("username already exists");
            }

            var  usertocreate=new User{
                username=userForRegisterDto.username
            };

            var createduser= await _context.Register(usertocreate,userForRegisterDto.Password);
            return StatusCode(201);
        }
        
         [HttpPost("login")]

        public async Task<IActionResult> login(UserForLoginDto userForLoginDto)
        {
            var userfromrepo=await _context.Login(userForLoginDto.username.ToLower(),userForLoginDto.Password);

            if(userfromrepo==null)
                return Unauthorized();

            var claims=new[] 
            {
                new Claim(ClaimTypes.NameIdentifier,userfromrepo.id.ToString()),
                new Claim(ClaimTypes.Name,userfromrepo.username)

            };

            var key=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds=new SigningCredentials(key,SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor=new SecurityTokenDescriptor {
                Subject=new ClaimsIdentity(claims),
                Expires=DateTime.Now.AddDays(1),
                SigningCredentials=creds
            };

            var tokenhandler=new JwtSecurityTokenHandler();

            var token=tokenhandler.CreateToken(tokenDescriptor);

            return Ok(new{
                token=tokenhandler.WriteToken(token)
            });

        }
        
    }
}
