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
    public class ProdController : ControllerBase
    {
        private readonly MyDbContext _context;

        public ProdController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/Prod
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProdMst>>> GetProdMsts()
        {
          if (_context.ProdMsts == null)
          {
              return NotFound();
          }
            return await _context.ProdMsts.ToListAsync();
        }

        // GET: api/Prod/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProdMst>> GetProdMst(string id)
        {
          if (_context.ProdMsts == null)
          {
              return NotFound();
          }
            var prodMst = await _context.ProdMsts.FindAsync(id);

            if (prodMst == null)
            {
                return NotFound();
            }

            return prodMst;
        }

        // PUT: api/Prod/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProdMst(string id, ProdMst prodMst)
        {
            if (id != prodMst.ProdId)
            {
                return BadRequest();
            }

            _context.Entry(prodMst).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProdMstExists(id))
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

        // POST: api/Prod
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProdMst>> PostProdMst(ProdMst prodMst)
        {
          if (_context.ProdMsts == null)
          {
              return Problem("Entity set 'MyDbContext.ProdMsts'  is null.");
          }
            _context.ProdMsts.Add(prodMst);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ProdMstExists(prodMst.ProdId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetProdMst", new { id = prodMst.ProdId }, prodMst);
        }

        // DELETE: api/Prod/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProdMst(string id)
        {
            if (_context.ProdMsts == null)
            {
                return NotFound();
            }
            var prodMst = await _context.ProdMsts.FindAsync(id);
            if (prodMst == null)
            {
                return NotFound();
            }

            _context.ProdMsts.Remove(prodMst);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProdMstExists(string id)
        {
            return (_context.ProdMsts?.Any(e => e.ProdId == id)).GetValueOrDefault();
        }
    }
}
