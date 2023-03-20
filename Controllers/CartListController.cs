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
    public class CartListController : ControllerBase
    {
        private readonly MyDbContext _context;

        public CartListController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/CartList
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CartList>>> GetCartLists()
        {
          if (_context.CartLists == null)
          {
              return NotFound();
          }
            return await _context.CartLists.ToListAsync();
        }

        // GET: api/CartList/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CartList>> GetCartList(string id)
        {
          if (_context.CartLists == null)
          {
              return NotFound();
          }
            var cartList = await _context.CartLists.FindAsync(id);

            if (cartList == null)
            {
                return NotFound();
            }

            return cartList;
        }

        // PUT: api/CartList/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCartList(string id, CartList cartList)
        {
            if (id != cartList.Id)
            {
                return BadRequest();
            }

            _context.Entry(cartList).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CartListExists(id))
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

        // POST: api/CartList
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CartList>> PostCartList(CartList cartList)
        {
          if (_context.CartLists == null)
          {
              return Problem("Entity set 'MyDbContext.CartLists'  is null.");
          }
            _context.CartLists.Add(cartList);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CartListExists(cartList.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCartList", new { id = cartList.Id }, cartList);
        }

        // DELETE: api/CartList/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCartList(string id)
        {
            if (_context.CartLists == null)
            {
                return NotFound();
            }
            var cartList = await _context.CartLists.FindAsync(id);
            if (cartList == null)
            {
                return NotFound();
            }

            _context.CartLists.Remove(cartList);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CartListExists(string id)
        {
            return (_context.CartLists?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
