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
    public class InquiryController : ControllerBase
    {
        private readonly MyDbContext _context;

        public InquiryController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/Inquiry
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Inquiry>>> GetInquiries()
        {
          if (_context.Inquiries == null)
          {
              return NotFound();
          }
            return await _context.Inquiries.ToListAsync();
        }

        // GET: api/Inquiry/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Inquiry>> GetInquiry(string id)
        {
          if (_context.Inquiries == null)
          {
              return NotFound();
          }
            var inquiry = await _context.Inquiries.FindAsync(id);

            if (inquiry == null)
            {
                return NotFound();
            }

            return inquiry;
        }

        // PUT: api/Inquiry/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInquiry(string id, Inquiry inquiry)
        {
            if (id != inquiry.Id)
            {
                return BadRequest();
            }

            _context.Entry(inquiry).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InquiryExists(id))
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

        // POST: api/Inquiry
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Inquiry>> PostInquiry(Inquiry inquiry)
        {
          if (_context.Inquiries == null)
          {
              return Problem("Entity set 'MyDbContext.Inquiries'  is null.");
          }
            _context.Inquiries.Add(inquiry);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (InquiryExists(inquiry.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetInquiry", new { id = inquiry.Id }, inquiry);
        }

        // DELETE: api/Inquiry/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInquiry(string id)
        {
            if (_context.Inquiries == null)
            {
                return NotFound();
            }
            var inquiry = await _context.Inquiries.FindAsync(id);
            if (inquiry == null)
            {
                return NotFound();
            }

            _context.Inquiries.Remove(inquiry);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InquiryExists(string id)
        {
            return (_context.Inquiries?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
