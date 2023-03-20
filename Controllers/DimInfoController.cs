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
    public class DimInfoController : ControllerBase
    {
        private readonly MyDbContext _context;

        public DimInfoController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/DimInfo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DimInfoMst>>> GetDimInfoMsts()
        {
          if (_context.DimInfoMsts == null)
          {
              return NotFound();
          }
            return await _context.DimInfoMsts.ToListAsync();
        }

        // GET: api/DimInfo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DimInfoMst>> GetDimInfoMst(string id)
        {
          if (_context.DimInfoMsts == null)
          {
              return NotFound();
          }
            var dimInfoMst = await _context.DimInfoMsts.FindAsync(id);

            if (dimInfoMst == null)
            {
                return NotFound();
            }

            return dimInfoMst;
        }

        // PUT: api/DimInfo/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDimInfoMst(string id, DimInfoMst dimInfoMst)
        {
            if (id != dimInfoMst.DimId)
            {
                return BadRequest();
            }

            _context.Entry(dimInfoMst).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DimInfoMstExists(id))
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

        // POST: api/DimInfo
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DimInfoMst>> PostDimInfoMst(DimInfoMst dimInfoMst)
        {
          if (_context.DimInfoMsts == null)
          {
              return Problem("Entity set 'MyDbContext.DimInfoMsts'  is null.");
          }
            _context.DimInfoMsts.Add(dimInfoMst);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DimInfoMstExists(dimInfoMst.DimId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetDimInfoMst", new { id = dimInfoMst.DimId }, dimInfoMst);
        }

        // DELETE: api/DimInfo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDimInfoMst(string id)
        {
            if (_context.DimInfoMsts == null)
            {
                return NotFound();
            }
            var dimInfoMst = await _context.DimInfoMsts.FindAsync(id);
            if (dimInfoMst == null)
            {
                return NotFound();
            }

            _context.DimInfoMsts.Remove(dimInfoMst);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DimInfoMstExists(string id)
        {
            return (_context.DimInfoMsts?.Any(e => e.DimId == id)).GetValueOrDefault();
        }
    }
}
