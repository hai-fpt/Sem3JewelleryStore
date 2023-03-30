using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JewelleryStore.Models;
using Newtonsoft.Json.Linq;

namespace JewelleryStore.Controllers
{
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly MyDbContext _context;

        public CardController(MyDbContext context)
        {
            _context = context;
        }

        [HttpPost("api/verify")]
        public IActionResult VerifyPaymentAndReduceQuantity([FromBody] CreditCard creditCard)
        {
            var existingCard = _context.CreditCards
                .FirstOrDefault(c => c.CardNumber == creditCard.CardNumber
                && c.CardName == creditCard.CardName
                && c.CardExpiration == creditCard.CardExpiration
                && c.CardCvv == creditCard.CardCvv);
            if (existingCard == null)
            {
                return BadRequest("Invalid card credentials");
            }
            return StatusCode(200);
        }

        [HttpPost("api/oms")]
        public IActionResult InventoryManagement([FromBody] CartList cart)
        {
            return Ok();
        }

    }
}
