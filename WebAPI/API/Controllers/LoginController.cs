using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Model.BaseEntity;
using Model.DTOs;
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
            user.Password = MD5Hash(user.Password);
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

        [HttpPost("Register")]
        public IActionResult Register(Login user)
        {
            if (user == null)
            {
                return BadRequest();
            }
            else if (!CheckPassWord(user.Password))
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    Success = false,
                    Message = "Register false",
                });
            }
            else
            {
                user.Password = MD5Hash(user.Password);
                return Ok(new
                {
                    Success = true,
                    Message = "Register success",
                });
            }
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

        private string MD5Hash(string passWord)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(passWord));
            var lstByteHash = md5.Hash;
            StringBuilder passWordHash = new StringBuilder();
            //convert byte to hexadecimal
            foreach (var item in lstByteHash)
            {

                passWordHash.Append(item.ToString("x2"));
            }
            return passWordHash.ToString();
        }

        private bool CheckPassWord(string passWord)
        {
            //Regex gồm chữ cái thường, chữ hoa, số và kí tự đặc biệt
            Regex regex = new Regex("(?=.*[A-Z])(?=.*[!@#$&*])(?=.*[0-9])(?=.*[a-z]).{8}");
            Match match = regex.Match(passWord);
            return match.Success;
        }
    }
}
