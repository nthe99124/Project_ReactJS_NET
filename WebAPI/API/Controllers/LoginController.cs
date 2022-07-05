using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Model.ViewModel;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using MyDbContext = API.Common.MyDbContext;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class LoginController : ControllerBase
    {
        private readonly MyDbContext _context;
        private readonly IConfiguration _config;
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
            var userRole = _context.Users.SingleOrDefault(ur => ur.UserName == user.UserName);
            var jwtToken = new JwtSecurityTokenHandler();
            var secretKeyByte = Encoding.UTF8.GetBytes(_config["AppSettings:SecretKey"]);
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("UserName", user.UserName),
                    new Claim("TokenId",Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Role,userRole.UserRole.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyByte), SecurityAlgorithms.HmacSha512Signature)
            };
            var token = jwtToken.CreateToken(tokenDescription);
            return jwtToken.WriteToken(token);
        }
    }
}
