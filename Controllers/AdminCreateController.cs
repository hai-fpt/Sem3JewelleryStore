using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JewelleryStore.Models;
using System.Security.Cryptography;

namespace JewelleryStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminCreateController : ControllerBase
    {
        private readonly MyDbContext _context;

        public AdminCreateController(MyDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] AdminLoginMst user)
        {
            if (_context.AdminLoginMsts.Any(u => u.UserName == user.UserName))
            {
                return BadRequest("Username already exists");
            }

            using var sha256 = SHA256.Create();
            var hashedPasswordBytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(user.Password));
            var hashedPassword = BitConverter.ToString(hashedPasswordBytes).Replace("-", "");

            var newUser = new AdminLoginMst
            {
                UserName = user.UserName,
                Password = hashedPassword
            };

            _context.AdminLoginMsts.Add(newUser);
            _context.SaveChanges();

            return Ok("New user created successfully");
        }
    }
}
