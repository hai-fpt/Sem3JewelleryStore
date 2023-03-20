using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JewelleryStore.Models;
using Microsoft.AspNetCore.Authorization;

namespace JewelleryStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StoneQltyController : ControllerBase
    {
        private readonly MyDbContext _context;

        public StoneQltyController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/StoneQlty
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StoneQltyMst>>> GetStoneQltyMsts()
        {
          if (_context.StoneQltyMsts == null)
          {
              return NotFound();
          }
            return await _context.StoneQltyMsts.ToListAsync();
        }

        // GET: api/StoneQlty/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StoneQltyMst>> GetStoneQltyMst(string id)
        {
          if (_context.StoneQltyMsts == null)
          {
              return NotFound();
          }
            var stoneQltyMst = await _context.StoneQltyMsts.FindAsync(id);

            if (stoneQltyMst == null)
            {
                return NotFound();
            }

            return stoneQltyMst;
        }

        // PUT: api/StoneQlty/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStoneQltyMst(string id, StoneQltyMst stoneQltyMst)
        {
            if (id != stoneQltyMst.StoneQltyId)
            {
                return BadRequest();
            }

            _context.Entry(stoneQltyMst).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StoneQltyMstExists(id))
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

        // POST: api/StoneQlty
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StoneQltyMst>> PostStoneQltyMst(StoneQltyMst stoneQltyMst)
        {
          if (_context.StoneQltyMsts == null)
          {
              return Problem("Entity set 'MyDbContext.StoneQltyMsts'  is null.");
          }
            _context.StoneQltyMsts.Add(stoneQltyMst);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (StoneQltyMstExists(stoneQltyMst.StoneQltyId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetStoneQltyMst", new { id = stoneQltyMst.StoneQltyId }, stoneQltyMst);
        }

        // DELETE: api/StoneQlty/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStoneQltyMst(string id)
        {
            if (_context.StoneQltyMsts == null)
            {
                return NotFound();
            }
            var stoneQltyMst = await _context.StoneQltyMsts.FindAsync(id);
            if (stoneQltyMst == null)
            {
                return NotFound();
            }

            _context.StoneQltyMsts.Remove(stoneQltyMst);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StoneQltyMstExists(string id)
        {
            return (_context.StoneQltyMsts?.Any(e => e.StoneQltyId == id)).GetValueOrDefault();
        }
    }
}
