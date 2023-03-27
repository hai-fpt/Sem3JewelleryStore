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
    public class BrandController : ControllerBase
    {
        private readonly MyDbContext _context;

        public BrandController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/Brand
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BrandMst>>> GetBrandMsts()
        {
          if (_context.BrandMsts == null)
          {
              return NotFound();
          }
            return await _context.BrandMsts.ToListAsync();
        }

        // GET: api/Brand/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BrandMst>> GetBrandMst(string id)
        {
          if (_context.BrandMsts == null)
          {
              return NotFound();
          }
            var brandMst = await _context.BrandMsts.FindAsync(id);

            if (brandMst == null)
            {
                return NotFound();
            }

            return brandMst;
        }

        // PUT: api/Brand/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBrandMst(string id, BrandMst brandMst)
        {
            if (id != brandMst.BrandId)
            {
                return BadRequest();
            }

            _context.Entry(brandMst).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BrandMstExists(id))
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

        // POST: api/Brand
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BrandMst>> PostBrandMst(BrandMst brandMst)
        {
          if (_context.BrandMsts == null)
          {
              return Problem("Entity set 'MyDbContext.BrandMsts'  is null.");
          }
            _context.BrandMsts.Add(brandMst);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (BrandMstExists(brandMst.BrandId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetBrandMst", new { id = brandMst.BrandId }, brandMst);
        }

        // DELETE: api/Brand/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBrandMst(string id)
        {
            if (_context.BrandMsts == null)
            {
                return NotFound();
            }
            var brandMst = await _context.BrandMsts.FindAsync(id);
            if (brandMst == null)
            {
                return NotFound();
            }

            _context.BrandMsts.Remove(brandMst);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BrandMstExists(string id)
        {
            return (_context.BrandMsts?.Any(e => e.BrandId == id)).GetValueOrDefault();
        }
    }
}
