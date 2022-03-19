using API.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Model.Common;
using Model.ViewModel;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using MyDbContext = API.Common.MyDbContext;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class LoginController : ControllerBase
    {
        private readonly MyDbContext _context;
        private IConfiguration _config;
        public LoginController(MyDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }
        [HttpPost("Login")]
        public IActionResult Login(Login user)
        {
            var userLog = _context.Users.SingleOrDefault(u => u.UserName == user.UserName && u.PassWord == user.Password);
            if (userLog == null)
            {
                return Ok(new
                {
                    Success = false,
                    Message = "Invalid username/password"
                });
            }
            // grant token
            return Ok(new
            {
                Success = true,
                Message = "Authenticate success",
                Data = GenerateToken(user)
            });
        }
        private string GenerateToken(Login user)
        {
            var jwtToken = new JwtSecurityTokenHandler();
            var secretKeyByte = Encoding.UTF8.GetBytes(_config["AppSettings:SecretKey"]);
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    //new Claim(ClaimTypes.Email,user.Email),
                    new Claim("UserName", user.UserName),
                    //new Claim("Id",user.Id.ToString()),
                    new Claim("TokenId",Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyByte), SecurityAlgorithms.HmacSha512Signature)
            };
            var token = jwtToken.CreateToken(tokenDescription);
            return jwtToken.WriteToken(token);
        }
    }
}
