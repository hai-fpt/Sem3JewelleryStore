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
    public class IncomeController : ControllerBase
    {
        private readonly MyDbContext _context;

        public IncomeController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/Income
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Income>>> GetIncomes()
        {
          if (_context.Incomes == null)
          {
              return NotFound();
          }
            return await _context.Incomes.ToListAsync();
        }

        // GET: api/Income/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Income>> GetIncome(string id)
        {
          if (_context.Incomes == null)
          {
              return NotFound();
          }
            var income = await _context.Incomes.FindAsync(id);

            if (income == null)
            {
                return NotFound();
            }

            return income;
        }

        // PUT: api/Income/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIncome(string id, Income income)
        {
            if (id != income.OrderId)
            {
                return BadRequest();
            }

            _context.Entry(income).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IncomeExists(id))
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

        // POST: api/Income
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Income>> PostIncome(Income income)
        {
          if (_context.Incomes == null)
          {
              return Problem("Entity set 'MyDbContext.Incomes'  is null.");
          }
            _context.Incomes.Add(income);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (IncomeExists(income.OrderId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetIncome", new { id = income.OrderId }, income);
        }

        // DELETE: api/Income/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIncome(string id)
        {
            if (_context.Incomes == null)
            {
                return NotFound();
            }
            var income = await _context.Incomes.FindAsync(id);
            if (income == null)
            {
                return NotFound();
            }

            _context.Incomes.Remove(income);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool IncomeExists(string id)
        {
            return (_context.Incomes?.Any(e => e.OrderId == id)).GetValueOrDefault();
        }
    }
}
