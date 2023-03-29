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
            var jewelTypeMsts = await _context.JewelTypeMsts.Include(j => j.Item).ToListAsync();
            
          if (_context.JewelTypeMsts == null)
          {
              return NotFound();
          }
            return await _context.JewelTypeMsts.ToListAsync();
        }

        // GET: api/JewelType/5
        [HttpGet("{id}")]
        public IActionResult GetJewelTypeMst(string id)
        {
            //var jewelType = _context.JewelTypeMsts.Include(j => j.Item).FirstOrDefault(j => j.Id == id);
            var jewelType = _context.JewelTypeMsts
                .Include(j => j.Item)
                    .ThenInclude(i => i.Brand)
                .Include(j => j.Item)
                    .ThenInclude(i => i.Cat)
                .Include(j => j.Item)
                    .ThenInclude(i => i.Certify)
                .Include(j => j.Item)
                    .ThenInclude(i => i.GoldType)
                .Include(j => j.Item)
                    .ThenInclude(i => i.Prod)
                .FirstOrDefault(j => j.Id == id);

            if (jewelType == null)
            {
                return NotFound();
            }

            return Ok(jewelType);
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
        public IActionResult CreateJewelType([FromBody] JewelTypeMst jewelType)
        {
            var item = _context.ItemMsts.FirstOrDefault(i => i.StyleCode == jewelType.ItemId);
            if (item == null)
            {
                return BadRequest("Item does not exists");
            }

            var newJewelType = new JewelTypeMst
            {
                Id = jewelType.Id,
                JewelleryType = jewelType.JewelleryType,
                ImgPath = jewelType.ImgPath,
                ItemId = jewelType.ItemId,
                Item = item
            };

            _context.JewelTypeMsts.Add(newJewelType);
            _context.SaveChanges();

            return Ok("New jewel created successfully");
        }
        //public async Task<ActionResult<JewelTypeMst>> PostJewelTypeMst(JewelTypeMst jewelTypeMst, string itemId)
        //{
        //    if (_context.JewelTypeMsts == null)
        //    {
        //        return Problem("Entity set 'MyDbContext.JewelTypeMsts'  is null.");
        //    }
        //    var item = await _context.ItemMsts.FindAsync(itemId);
        //    if (item == null)
        //    {
        //        return NotFound($"Item with ID {itemId} not found");
        //    }
        //    jewelTypeMst.Item = item;
            
        //    _context.JewelTypeMsts.Add(jewelTypeMst);
        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (JewelTypeMstExists(jewelTypeMst.Id))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtAction("GetJewelTypeMst", new { id = jewelTypeMst.Id }, jewelTypeMst);
        //}

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
