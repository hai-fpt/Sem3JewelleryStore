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
    public class JewelTypeController : ControllerBase
    {
        private readonly MyDbContext _context;

        public JewelTypeController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/JewelType
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JewelTypeMst>>> GetJewelTypeMsts()
        {
          if (_context.JewelTypeMsts == null)
          {
              return NotFound();
          }
            return await _context.JewelTypeMsts.ToListAsync();
        }

        // GET: api/JewelType/5
        [HttpGet("{id}")]
        public async Task<ActionResult<JewelTypeMst>> GetJewelTypeMst(string id)
        {
          if (_context.JewelTypeMsts == null)
          {
              return NotFound();
          }
            var jewelTypeMst = await _context.JewelTypeMsts.FindAsync(id);

            if (jewelTypeMst == null)
            {
                return NotFound();
            }

            return jewelTypeMst;
        }

        // PUT: api/JewelType/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJewelTypeMst(string id, JewelTypeMst jewelTypeMst)
        {
            if (id != jewelTypeMst.Id)
            {
                return BadRequest();
            }

            _context.Entry(jewelTypeMst).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JewelTypeMstExists(id))
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

        // POST: api/JewelType
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<JewelTypeMst>> PostJewelTypeMst(JewelTypeMst jewelTypeMst)
        {
          if (_context.JewelTypeMsts == null)
          {
              return Problem("Entity set 'MyDbContext.JewelTypeMsts'  is null.");
          }
            _context.JewelTypeMsts.Add(jewelTypeMst);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (JewelTypeMstExists(jewelTypeMst.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetJewelTypeMst", new { id = jewelTypeMst.Id }, jewelTypeMst);
        }

        // DELETE: api/JewelType/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJewelTypeMst(string id)
        {
            if (_context.JewelTypeMsts == null)
            {
                return NotFound();
            }
            var jewelTypeMst = await _context.JewelTypeMsts.FindAsync(id);
            if (jewelTypeMst == null)
            {
                return NotFound();
            }

            _context.JewelTypeMsts.Remove(jewelTypeMst);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool JewelTypeMstExists(string id)
        {
            return (_context.JewelTypeMsts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
