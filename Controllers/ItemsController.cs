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
        public async Task<ActionResult<ItemMst>> GetItem(string id)
        {
            var jewel = _context.JewelTypeMsts.FirstOrDefault(i => i.ItemId == id);
            ItemMst item;
            if (jewel == null)
            {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                item = await _context.ItemMsts
                .Include(i => i.Brand)
                .Include(i => i.Cat)
                .Include(i => i.Certify)
                .Include(i => i.GoldType)
                .Include(i => i.Prod)
                .FirstOrDefaultAsync(i => i.StyleCode == id);
            }
            else
            {
                item = await _context.ItemMsts
                .Include(i => i.Brand)
                .Include(i => i.Cat)
                .Include(i => i.Certify)
                .Include(i => i.GoldType)
                .Include(i => i.Prod)
                .Include(i => i.JewelTypeMsts)
                .FirstOrDefaultAsync(i => i.StyleCode == id);
            }
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            if (item == null)
            {
                return NotFound();
            }

            return item;
        }
        //public async Task<ActionResult<ItemMst>> GetItemMst(string id)
        //{
        //  if (_context.ItemMsts == null)
        //  {
        //      return NotFound();
        //  }
        //    var itemMst = await _context.ItemMsts.FindAsync(id);

        //    if (itemMst == null)
        //    {
        //        return NotFound();
        //    }

        //    return itemMst;
        //}

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
        [HttpPost]
        public IActionResult CreateItem([FromBody] ItemMst item)
        {
            var brand = _context.BrandMsts.FirstOrDefault(b => b.BrandId == item.BrandId);
            if (brand == null)
            {
                return BadRequest("Brand does not exist in the database");
            }

            var cat = _context.CatMsts.FirstOrDefault(c => c.CatId == item.CatId);
            if (cat == null)
            {
                return BadRequest("Cat does not exist in the database");
            }

            var certify = _context.CertifyMsts.FirstOrDefault(c => c.CertifyId == item.CertifyId);
            if (certify == null)
            {
                return BadRequest("Certify does not exist in the database");
            }

            var goldType = _context.GoldKrtMsts.FirstOrDefault(g => g.GoldTypeId == item.GoldTypeId);
            if (goldType == null)
            {
                return BadRequest("GoldKrtMst does not exist in the database");
            }

            var prod = _context.ProdMsts.FirstOrDefault(p => p.ProdId == item.ProdId);
            if (prod == null)
            {
                return BadRequest("ProdMst does not exist in the database");
            }

            var newItem = new ItemMst
            {
                StyleCode = item.StyleCode,
                Pairs = item.Pairs,
                BrandId = item.BrandId,
                Brand = brand,
                Quantity = item.Quantity,
                CatId = item.CatId,
                Cat = cat,
                ProdQuality = item.ProdQuality,
                CertifyId = item.CertifyId,
                Certify = certify,
                ProdId = item.ProdId,
                Prod = prod,
                GoldTypeId = item.GoldTypeId,
                GoldType = goldType,
                GoldWt = item.GoldWt,
                StoneWt = item.StoneWt,
                NetGold = item.NetGold,
                WstgPer = item.WstgPer,
                Wstg = item.Wstg,
                TotGrossWt = item.TotGrossWt,
                GoldRate = item.GoldRate,
                GoldAmt = item.GoldAmt,
                GoldMaking = item.GoldMaking,
                StoneMaking = item.StoneMaking,
                OtherMaking = item.OtherMaking,
                TotMaking = item.TotMaking,
                Mrp = item.Mrp
            };

            _context.ItemMsts.Add(newItem);
            _context.SaveChanges();

            return Ok("Created new item");
        }
        //public async Task<ActionResult<ItemMst>> PostItemMst(ItemMst itemMst, string brandId, string catId, string certifyId, string goldTypeId, string prodId)
        //{
        //    if(_context.ItemMsts == null)
        //    {
        //        return Problem("Entity set 'MyDbContext.ItemMsts' is null.");
        //    }

        //    var brand = await _context.BrandMsts.FindAsync(brandId);
        //    if (brand == null)
        //    {
        //        return BadRequest("Invalid brand");
        //    }
        //    itemMst.Brand = brand;

        //    var cat = await _context.CatMsts.FindAsync(catId);
        //    if (cat == null)
        //    {
        //        return BadRequest("Invalid category");
        //    }
        //    itemMst.Cat = cat;

        //    var certify = await _context.CertifyMsts.FindAsync(certifyId);
        //    if (certify == null)
        //    {
        //        return BadRequest("Invalid certificate");
        //    }
        //    itemMst.Certify = certify;

        //    var goldType = await _context.GoldKrtMsts.FindAsync(goldTypeId);
        //    if (goldType == null)
        //    {
        //        return BadRequest("Invalid goldTypeId.");
        //    }
        //    itemMst.GoldType = goldType;

        //    var prod = await _context.ProdMsts.FindAsync(prodId);
        //    if (prod == null)
        //    {
        //        return BadRequest("Invalid prodId.");
        //    }
        //    itemMst.Prod = prod;

        //    _context.ItemMsts.Add(itemMst);

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException)
        //    {
        //    if (ItemMstExists(itemMst.StyleCode))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtAction("GetItemMst", new { id = itemMst.StyleCode }, itemMst);
        //}

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
