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
    public class DimQltySubController : ControllerBase
    {
        private readonly MyDbContext _context;

        public DimQltySubController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/DimQltySub
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DimQltySubMst>>> GetDimQltySubMsts()
        {
          if (_context.DimQltySubMsts == null)
          {
              return NotFound();
          }
            return await _context.DimQltySubMsts.ToListAsync();
        }

        // GET: api/DimQltySub/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DimQltySubMst>> GetDimQltySubMst(string id)
        {
          if (_context.DimQltySubMsts == null)
          {
              return NotFound();
          }
            var dimQltySubMst = await _context.DimQltySubMsts.FindAsync(id);

            if (dimQltySubMst == null)
            {
                return NotFound();
            }

            return dimQltySubMst;
        }

        // PUT: api/DimQltySub/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDimQltySubMst(string id, DimQltySubMst dimQltySubMst)
        {
            if (id != dimQltySubMst.DimSubTypeId)
            {
                return BadRequest();
            }

            _context.Entry(dimQltySubMst).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DimQltySubMstExists(id))
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

        // POST: api/DimQltySub
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DimQltySubMst>> PostDimQltySubMst(DimQltySubMst dimQltySubMst)
        {
          if (_context.DimQltySubMsts == null)
          {
              return Problem("Entity set 'MyDbContext.DimQltySubMsts'  is null.");
          }
            _context.DimQltySubMsts.Add(dimQltySubMst);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DimQltySubMstExists(dimQltySubMst.DimSubTypeId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetDimQltySubMst", new { id = dimQltySubMst.DimSubTypeId }, dimQltySubMst);
        }

        // DELETE: api/DimQltySub/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDimQltySubMst(string id)
        {
            if (_context.DimQltySubMsts == null)
            {
                return NotFound();
            }
            var dimQltySubMst = await _context.DimQltySubMsts.FindAsync(id);
            if (dimQltySubMst == null)
            {
                return NotFound();
            }

            _context.DimQltySubMsts.Remove(dimQltySubMst);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DimQltySubMstExists(string id)
        {
            return (_context.DimQltySubMsts?.Any(e => e.DimSubTypeId == id)).GetValueOrDefault();
        }
    }
}
