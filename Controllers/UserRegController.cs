using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JewelleryStore.Models;

namespace JewelleryStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRegController : ControllerBase
    {
        private readonly MyDbContext _context;

        public UserRegController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/UserReg
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserRegMst>>> GetUserRegMsts()
        {
          if (_context.UserRegMsts == null)
          {
              return NotFound();
          }
            return await _context.UserRegMsts.ToListAsync();
        }

        // GET: api/UserReg/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserRegMst>> GetUserRegMst(string id)
        {
          if (_context.UserRegMsts == null)
          {
              return NotFound();
          }
            var userRegMst = await _context.UserRegMsts.FindAsync(id);

            if (userRegMst == null)
            {
                return NotFound();
            }

            return userRegMst;
        }

        // PUT: api/UserReg/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserRegMst(string id, UserRegMst userRegMst)
        {
            if (id != userRegMst.UserId)
            {
                return BadRequest();
            }

            _context.Entry(userRegMst).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserRegMstExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/UserReg
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserRegMst>> PostUserRegMst(UserRegMst userRegMst)
        {
          if (_context.UserRegMsts == null)
          {
              return Problem("Entity set 'MyDbContext.UserRegMsts'  is null.");
          }
            _context.UserRegMsts.Add(userRegMst);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserRegMstExists(userRegMst.UserId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUserRegMst", new { id = userRegMst.UserId }, userRegMst);
        }

        // DELETE: api/UserReg/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserRegMst(string id)
        {
            if (_context.UserRegMsts == null)
            {
                return NotFound();
            }
            var userRegMst = await _context.UserRegMsts.FindAsync(id);
            if (userRegMst == null)
            {
                return NotFound();
            }

            _context.UserRegMsts.Remove(userRegMst);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserRegMstExists(string id)
        {
            return (_context.UserRegMsts?.Any(e => e.UserId == id)).GetValueOrDefault();
        }
    }
}
