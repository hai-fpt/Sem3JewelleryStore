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
    public class CertifyController : ControllerBase
    {
        private readonly MyDbContext _context;

        public CertifyController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/Certify
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CertifyMst>>> GetCertifyMsts()
        {
          if (_context.CertifyMsts == null)
          {
              return NotFound();
          }
            return await _context.CertifyMsts.ToListAsync();
        }

        // GET: api/Certify/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CertifyMst>> GetCertifyMst(string id)
        {
          if (_context.CertifyMsts == null)
          {
              return NotFound();
          }
            var certifyMst = await _context.CertifyMsts.FindAsync(id);

            if (certifyMst == null)
            {
                return NotFound();
            }

            return certifyMst;
        }

        // PUT: api/Certify/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCertifyMst(string id, CertifyMst certifyMst)
        {
            if (id != certifyMst.CertifyId)
            {
                return BadRequest();
            }

            _context.Entry(certifyMst).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CertifyMstExists(id))
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

        // POST: api/Certify
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CertifyMst>> PostCertifyMst(CertifyMst certifyMst)
        {
          if (_context.CertifyMsts == null)
          {
              return Problem("Entity set 'MyDbContext.CertifyMsts'  is null.");
          }
            _context.CertifyMsts.Add(certifyMst);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CertifyMstExists(certifyMst.CertifyId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCertifyMst", new { id = certifyMst.CertifyId }, certifyMst);
        }

        // DELETE: api/Certify/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCertifyMst(string id)
        {
            if (_context.CertifyMsts == null)
            {
                return NotFound();
            }
            var certifyMst = await _context.CertifyMsts.FindAsync(id);
            if (certifyMst == null)
            {
                return NotFound();
            }

            _context.CertifyMsts.Remove(certifyMst);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CertifyMstExists(string id)
        {
            return (_context.CertifyMsts?.Any(e => e.CertifyId == id)).GetValueOrDefault();
        }
    }
}
