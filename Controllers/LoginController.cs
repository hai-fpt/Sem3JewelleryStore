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


        public LoginController(IConfiguration config, MyDbContext context)
        {
            _config = config;
            _context = context;
        }
        

        [HttpPost("login")]
        public IActionResult Login([FromBody] AdminLoginMst model)
        {
            bool isValid = ValidateAdminCredentails(model.UserName, model.Password);

            if (!isValid)
            {
                return Unauthorized();
            }

            var issuer = _config["Jwt:Issuer"];
            var audience = _config["Jwt:Audience"];
            var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);
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

        private bool ValidateAdminCredentails(string userName, string password)
        {

            var user = _context.AdminLoginMsts.FirstOrDefault(u => u.UserName == userName);

            if (user != null)
            {
                return BCrypt.Net.BCrypt.Verify(password, user.Password);
            }

            return false;
        }

        //[HttpPost]
        //public IActionResult UserLogin([FromBody] UserRegMst user)
        //{
        //    bool isValid = ValidateUserCredentials(user.Username, user.Password);

        //    if (!isValid)
        //    {
        //        return Unauthorized();
        //    }

        //    var issuer = _config["Jwt:Issuer"];
        //    var audience = _config["Jwt:Audience"];
        //    var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);
        //    var tokenDescriptor = new SecurityTokenDescriptor
        //    {
        //        Subject = new ClaimsIdentity(new[] {
        //            new Claim("Id", Guid.NewGuid().ToString()),
        //            new Claim(JwtRegisteredClaimNames.Sub, user.Username),
        //            new Claim(JwtRegisteredClaimNames.Email, user.Username),
        //            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        //        }),
        //        Expires = DateTime.UtcNow.AddDays(7),
        //        Issuer = issuer,
        //        Audience = audience,
        //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        //    };

        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var token = tokenHandler.CreateToken(tokenDescriptor);
        //    var jwtToken = tokenHandler.WriteToken(token);
        //    var stringToken = tokenHandler.WriteToken(token);

        //    return Ok(stringToken);
        //}

        //private bool ValidateUserCredentials(string username, string password)
        //{
        //    var user = _context.UserRegMsts.FirstOrDefault(u => u.Username == username);

        //    if (user != null)
        //    {
        //        return BCrypt.Net.BCrypt.Verify(password, user.Password);
        //    }

        //    return false;
        //}
    }
}

