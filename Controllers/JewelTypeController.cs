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
    public class JewelTypeController : ControllerBase
    {
        private readonly MyDbContext _context;

        public JewelTypeController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/JewelType
        [HttpGet]
        //public async Task<ActionResult<IEnumerable<JewelTypeMst>>> GetJewelTypeMsts()
        //{
        //    var jewelTypeMsts = await _context.JewelTypeMsts
        //        .Include(j => j.Item)
        //            .ThenInclude(i => i.Brand)
        //        .Include(j => j.Item)
        //            .ThenInclude(i => i.Cat)
        //        .Include(j => j.Item)
        //            .ThenInclude(i => i.Certify)
        //        .Include(j => j.Item)
        //            .ThenInclude(i => i.GoldType)
        //        .ToListAsync();

        //    if (jewelTypeMsts == null)
        //    {
        //        return NotFound();
        //    }

        //    return jewelTypeMsts;
        //}
        public async Task<ActionResult<IEnumerable<JewelTypeMst>>> GetJewelTypeMsts()
        {
            var jewelTypeMsts = await _context.JewelTypeMsts.Include(j => j.Item).ToListAsync();

            var jsonSettings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            var json = JsonConvert.SerializeObject(jewelTypeMsts, Formatting.None, jsonSettings);

            if (_context.JewelTypeMsts == null)
            {
                return NotFound();
            }
            return Content(json, "application/json");
        }

        //GET: api/JewelType/category
        [HttpGet("category")]
        public async Task<ActionResult<IEnumerable<JewelTypeMst>>> GetJewelTypeMstForCat(string category)
        {
            var jewelTypeMsts = await _context.JewelTypeMsts
                .Include(j => j.Item)
                .Where(j => j.JewelleryType.ToLower() == category.ToLower())
                .ToListAsync();

            if (jewelTypeMsts == null)
            {
                jewelTypeMsts = await _context.JewelTypeMsts.Include(j => j.Item).ToListAsync();
            }

            var jsonSetings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            var json = JsonConvert.SerializeObject(jewelTypeMsts, Formatting.None, jsonSetings);

            return Content(json, "application/json");
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

        // GET: search
        [HttpGet("/search")]
        public async Task<IActionResult> SearchItem(string query, string filter)
        {
            //var jewelType = await _context.JewelTypeMsts
            //    .Include(j => j.Item)
            //        .ThenInclude(i => i.Brand)
            //    .Include(j => j.Item)
            //        .ThenInclude(i => i.Cat)
            //    .Include(j => j.Item)
            //        .ThenInclude(i => i.Certify)
            //    .Include(j => j.Item)
            //        .ThenInclude(i => i.GoldType)
            //    .Include(j => j.Item)
            //        .ThenInclude(i => i.Prod)
            //    .Where(j => j.Id.Contains(query) ||
            //                j.Item.StyleCode.Contains(query) ||
            //                j.Item.Brand.BrandId.Contains(query) ||
            //                j.Item.Cat.CatName.Contains(query) ||
            //                j.Item.Certify.CertifyType.Contains(query) ||
            //                j.Item.GoldType.GoldCrt.Contains(query) ||
            //                j.Item.Prod.ProdType.Contains(query))
            //    .ToListAsync();

            IQueryable<JewelTypeMst> queryable = _context.JewelTypeMsts
                .Include(j => j.Item)
                    .ThenInclude(i => i.Brand)
                .Include(j => j.Item)
                    .ThenInclude(i => i.Cat)
                .Include(j => j.Item)
                    .ThenInclude(i => i.Certify)
                .Include(j => j.Item)
                    .ThenInclude(i => i.GoldType)
                .Include(j => j.Item)
                    .ThenInclude(i => i.Prod);
            if (!string.IsNullOrEmpty(filter))
            {
                switch (filter)
                {
                    case "Brand":
                        queryable = queryable.Where(j => j.Item.Brand.BrandId.Contains(query) || j.Item.Brand.BrandType.Contains(query));
                        break;
                    case "Cat":
                        queryable = queryable.Where(j => j.Item.Cat.CatId.Contains(query) || j.Item.Cat.CatName.Contains(query));
                        break;
                    case "Certify":
                        queryable = queryable.Where(j => j.Item.Certify.CertifyId.Contains(query) || j.Item.Certify.CertifyType.Contains(query));
                        break;
                    case "Gold":
                        queryable = queryable.Where(j => j.Item.GoldType.GoldTypeId.Contains(query) || j.Item.GoldType.GoldCrt.Contains(query));
                        break;
                    default:
                        break;
                }
            }

            var jewelType = await queryable
                .Where(j => j.Id.Contains(query) ||
                            j.Item.StyleCode.Contains(query) ||
                            j.Item.Brand.BrandId.Contains(query) || j.Item.Brand.BrandType.Contains(query) ||
                            j.Item.Cat.CatId.Contains(query) || j.Item.Cat.CatName.Contains(query) ||
                            j.Item.Certify.CertifyId.Contains(query) || j.Item.Certify.CertifyType.Contains(query) ||
                            j.Item.GoldType.GoldTypeId.Contains(query) || j.Item.GoldType.GoldCrt.Contains(query))
                .ToListAsync();


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
