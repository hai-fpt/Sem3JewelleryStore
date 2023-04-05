using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JewelleryStore.Models;
using Newtonsoft.Json;

namespace JewelleryStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DimMstController : ControllerBase
    {
        private readonly MyDbContext _context;

        public DimMstController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/DimMst
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DimMst>>> GetDimMsts()
        {
            //if (_context.DimMsts == null)
            //{
            //    return NotFound();
            //}
            //  return await _context.DimMsts.ToListAsync();
            var dimMsts = await _context.DimMsts.Include(j => j.StyleCodeNavigation).ToListAsync();

            var jsonSettings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            var json = JsonConvert.SerializeObject(dimMsts, Formatting.None, jsonSettings);

            if (_context.DimMsts == null)
            {
                return NotFound();
            }
            return Content(json, "application/json");
        }

        // GET: api/DimMst/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DimMst>> GetDimMst(string id)
        {
          if (_context.DimMsts == null)
          {
              return NotFound();
          }
            var dimMst = await _context.DimMsts.FindAsync(id);

            if (dimMst == null)
            {
                return NotFound();
            }

            return dimMst;
        }

        // PUT: api/DimMst/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDimMst(string id, DimMst dimMst)
        {
            if (id != dimMst.DimId)
            {
                return BadRequest();
            }

            _context.Entry(dimMst).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DimMstExists(id))
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

        // POST: api/DimMst
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DimMst>> PostDimMst(DimMst dimMst)
        {
          if (_context.DimMsts == null)
          {
              return Problem("Entity set 'MyDbContext.DimMsts'  is null.");
          }
            _context.DimMsts.Add(dimMst);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DimMstExists(dimMst.DimId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetDimMst", new { id = dimMst.DimId }, dimMst);
        }

        // DELETE: api/DimMst/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDimMst(string id)
        {
            if (_context.DimMsts == null)
            {
                return NotFound();
            }
            var dimMst = await _context.DimMsts.FindAsync(id);
            if (dimMst == null)
            {
                return NotFound();
            }

            _context.DimMsts.Remove(dimMst);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DimMstExists(string id)
        {
            return (_context.DimMsts?.Any(e => e.DimId == id)).GetValueOrDefault();
        }
    }
}
