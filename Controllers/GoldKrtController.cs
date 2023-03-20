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
    public class GoldKrtController : ControllerBase
    {
        private readonly MyDbContext _context;

        public GoldKrtController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/GoldKrt
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GoldKrtMst>>> GetGoldKrtMsts()
        {
          if (_context.GoldKrtMsts == null)
          {
              return NotFound();
          }
            return await _context.GoldKrtMsts.ToListAsync();
        }

        // GET: api/GoldKrt/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GoldKrtMst>> GetGoldKrtMst(string id)
        {
          if (_context.GoldKrtMsts == null)
          {
              return NotFound();
          }
            var goldKrtMst = await _context.GoldKrtMsts.FindAsync(id);

            if (goldKrtMst == null)
            {
                return NotFound();
            }

            return goldKrtMst;
        }

        // PUT: api/GoldKrt/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGoldKrtMst(string id, GoldKrtMst goldKrtMst)
        {
            if (id != goldKrtMst.GoldTypeId)
            {
                return BadRequest();
            }

            _context.Entry(goldKrtMst).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GoldKrtMstExists(id))
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

        // POST: api/GoldKrt
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GoldKrtMst>> PostGoldKrtMst(GoldKrtMst goldKrtMst)
        {
          if (_context.GoldKrtMsts == null)
          {
              return Problem("Entity set 'MyDbContext.GoldKrtMsts'  is null.");
          }
            _context.GoldKrtMsts.Add(goldKrtMst);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (GoldKrtMstExists(goldKrtMst.GoldTypeId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetGoldKrtMst", new { id = goldKrtMst.GoldTypeId }, goldKrtMst);
        }

        // DELETE: api/GoldKrt/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGoldKrtMst(string id)
        {
            if (_context.GoldKrtMsts == null)
            {
                return NotFound();
            }
            var goldKrtMst = await _context.GoldKrtMsts.FindAsync(id);
            if (goldKrtMst == null)
            {
                return NotFound();
            }

            _context.GoldKrtMsts.Remove(goldKrtMst);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GoldKrtMstExists(string id)
        {
            return (_context.GoldKrtMsts?.Any(e => e.GoldTypeId == id)).GetValueOrDefault();
        }
    }
}
