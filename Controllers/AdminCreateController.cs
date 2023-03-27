using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JewelleryStore.Models;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JewelleryStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminCreateController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly MyDbContext _context;

        public AdminCreateController(MyDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] AdminLoginMst user)
        {
            if (_context.AdminLoginMsts.Any(u => u.UserName == user.UserName))
            {
                return BadRequest("Username already exists");
            }

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);
            
           
            var newUser = new AdminLoginMst
            {
                UserName = user.UserName,
                Password = hashedPassword
            };

            _context.AdminLoginMsts.Add(newUser);
            _context.SaveChanges();

            return Ok("New user created successfully");
        }

        //private string EncryptPassword(string password)
        //{

        //    var issuer = _config["Jwt:Issuer"];
        //    var audience = _config["Jwt:Audience"];
        //    var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);
        //    var claimsIdentity = new ClaimsIdentity(new Claim[] { new Claim("password", password) });
        //    var securityTokenDescriptor = new SecurityTokenDescriptor
        //    {
        //        Subject = claimsIdentity,
        //        Expires = DateTime.UtcNow.AddDays(7),
        //        Issuer = issuer,
        //        Audience = audience,
        //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        //    };
        //    var securityTokenHandler = new JwtSecurityTokenHandler();

        //    var securityToken = securityTokenHandler.CreateToken(securityTokenDescriptor);

        //    return securityTokenHandler.WriteToken(securityToken);
        //}
    }
}
