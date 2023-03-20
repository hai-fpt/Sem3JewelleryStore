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
    public class CatController : ControllerBase
    {
        private readonly MyDbContext _context;

        public CatController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/Cat
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CatMst>>> GetCatMsts()
        {
          if (_context.CatMsts == null)
          {
              return NotFound();
          }
            return await _context.CatMsts.ToListAsync();
        }

        // GET: api/Cat/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CatMst>> GetCatMst(string id)
        {
          if (_context.CatMsts == null)
          {
              return NotFound();
          }
            var catMst = await _context.CatMsts.FindAsync(id);

            if (catMst == null)
            {
                return NotFound();
            }

            return catMst;
        }

        // PUT: api/Cat/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCatMst(string id, CatMst catMst)
        {
            if (id != catMst.CatId)
            {
                return BadRequest();
            }

            _context.Entry(catMst).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CatMstExists(id))
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

        // POST: api/Cat
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CatMst>> PostCatMst(CatMst catMst)
        {
          if (_context.CatMsts == null)
          {
              return Problem("Entity set 'MyDbContext.CatMsts'  is null.");
          }
            _context.CatMsts.Add(catMst);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CatMstExists(catMst.CatId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCatMst", new { id = catMst.CatId }, catMst);
        }

        // DELETE: api/Cat/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCatMst(string id)
        {
            if (_context.CatMsts == null)
            {
                return NotFound();
            }
            var catMst = await _context.CatMsts.FindAsync(id);
            if (catMst == null)
            {
                return NotFound();
            }

            _context.CatMsts.Remove(catMst);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CatMstExists(string id)
        {
            return (_context.CatMsts?.Any(e => e.CatId == id)).GetValueOrDefault();
        }
    }
}
