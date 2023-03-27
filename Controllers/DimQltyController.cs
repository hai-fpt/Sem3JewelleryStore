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
    public class DimQltyController : ControllerBase
    {
        private readonly MyDbContext _context;

        public DimQltyController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/DimQlty
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DimQltyMst>>> GetDimQltyMsts()
        {
          if (_context.DimQltyMsts == null)
          {
              return NotFound();
          }
            return await _context.DimQltyMsts.ToListAsync();
        }

        // GET: api/DimQlty/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DimQltyMst>> GetDimQltyMst(string id)
        {
          if (_context.DimQltyMsts == null)
          {
              return NotFound();
          }
            var dimQltyMst = await _context.DimQltyMsts.FindAsync(id);

            if (dimQltyMst == null)
            {
                return NotFound();
            }

            return dimQltyMst;
        }

        // PUT: api/DimQlty/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDimQltyMst(string id, DimQltyMst dimQltyMst)
        {
            if (id != dimQltyMst.DimQltyId)
            {
                return BadRequest();
            }

            _context.Entry(dimQltyMst).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DimQltyMstExists(id))
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

        // POST: api/DimQlty
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DimQltyMst>> PostDimQltyMst(DimQltyMst dimQltyMst)
        {
          if (_context.DimQltyMsts == null)
          {
              return Problem("Entity set 'MyDbContext.DimQltyMsts'  is null.");
          }
            _context.DimQltyMsts.Add(dimQltyMst);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DimQltyMstExists(dimQltyMst.DimQltyId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetDimQltyMst", new { id = dimQltyMst.DimQltyId }, dimQltyMst);
        }

        // DELETE: api/DimQlty/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDimQltyMst(string id)
        {
            if (_context.DimQltyMsts == null)
            {
                return NotFound();
            }
            var dimQltyMst = await _context.DimQltyMsts.FindAsync(id);
            if (dimQltyMst == null)
            {
                return NotFound();
            }

            _context.DimQltyMsts.Remove(dimQltyMst);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DimQltyMstExists(string id)
        {
            return (_context.DimQltyMsts?.Any(e => e.DimQltyId == id)).GetValueOrDefault();
        }
    }
}
