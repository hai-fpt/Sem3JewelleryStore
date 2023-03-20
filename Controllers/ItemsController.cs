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
    public class ItemsController : ControllerBase
    {
        private readonly MyDbContext _context;

        public ItemsController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/Items
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemMst>>> GetItemMsts()
        {
          if (_context.ItemMsts == null)
          {
              return NotFound();
          }
            return await _context.ItemMsts.ToListAsync();
        }

        // GET: api/Items/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemMst>> GetItemMst(string id)
        {
          if (_context.ItemMsts == null)
          {
              return NotFound();
          }
            var itemMst = await _context.ItemMsts.FindAsync(id);

            if (itemMst == null)
            {
                return NotFound();
            }

            return itemMst;
        }

        // PUT: api/Items/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItemMst(string id, ItemMst itemMst)
        {
            if (id != itemMst.StyleCode)
            {
                return BadRequest();
            }

            _context.Entry(itemMst).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemMstExists(id))
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

        // POST: api/Items
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ItemMst>> PostItemMst(ItemMst itemMst)
        {
          if (_context.ItemMsts == null)
          {
              return Problem("Entity set 'MyDbContext.ItemMsts'  is null.");
          }
            _context.ItemMsts.Add(itemMst);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ItemMstExists(itemMst.StyleCode))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetItemMst", new { id = itemMst.StyleCode }, itemMst);
        }

        // DELETE: api/Items/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItemMst(string id)
        {
            if (_context.ItemMsts == null)
            {
                return NotFound();
            }
            var itemMst = await _context.ItemMsts.FindAsync(id);
            if (itemMst == null)
            {
                return NotFound();
            }

            _context.ItemMsts.Remove(itemMst);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ItemMstExists(string id)
        {
            return (_context.ItemMsts?.Any(e => e.StyleCode == id)).GetValueOrDefault();
        }
    }
}
