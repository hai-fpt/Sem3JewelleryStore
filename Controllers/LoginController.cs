using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using JewelleryStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JewelleryStore.Controllers
{
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _config;

        private readonly MyDbContext _context;

        public LoginController(IConfiguration config)
        {
            _config = config;
        }
        public LoginController(MyDbContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] AdminLoginMst model)
        {
            bool isValid = ValidateUserCredentails(model.UserName, model.Password);

            if (!isValid)
            {
                return Unauthorized();
            }

            //var tokenHandler = new JwtSecurityTokenHandler();
            //var key = Encoding.ASCII.GetBytes(_config["Jwt:Secret"]);
            //var tokenDescriptor = new SecurityTokenDescriptor
            //{
            //    Subject = new ClaimsIdentity(new Claim[] {
            //        new Claim(ClaimTypes.Name, model.UserName)
            //    }),
            //    Expires = DateTime.UtcNow.AddDays(7),
            //    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            //};
            //var token = tokenHandler.CreateToken(tokenDescriptor);

            //return Ok(new { Token = tokenHandler.WriteToken(token) });

            var issuer = _config["Jwt:Issuer"];
            var audience = _config["Jwt:Audience"];
            var key = Encoding.ASCII.GetBytes(_config["Jwt:Secret"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim("Id", Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Sub, model.UserName),
                    new Claim(JwtRegisteredClaimNames.Email, model.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);
            var stringToken = tokenHandler.WriteToken(token);

            return Ok(stringToken);
        }

        private bool ValidateUserCredentails(string userName, string password)
        {
            var user = _context.AdminLoginMsts.FirstOrDefault(u => u.UserName == userName);

            if (user != null)
            {
                using var sha256 = SHA256.Create();
                var hashedPasswordBytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                var hashedPassword = BitConverter.ToString(hashedPasswordBytes).Replace("-", "");

                return hashedPassword == user.Password;
            }

            return false;
        }
    }
}

