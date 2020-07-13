using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ThetaRamadan2020.Models;

namespace ThetaRamadan2020.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchasedItemsController : ControllerBase
    {
        private readonly thetaramdan2020Context _context;

        public PurchasedItemsController(thetaramdan2020Context context)
        {
            _context = context;
        }

        // GET: api/PurchasedItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PurchasedItems>>> GetPurchasedItems()
        {
            return await _context.PurchasedItems.ToListAsync();
        }

        // GET: api/PurchasedItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PurchasedItems>> GetPurchasedItems(int id)
        {
            var purchasedItems = await _context.PurchasedItems.FindAsync(id);

            if (purchasedItems == null)
            {
                return NotFound();
            }

            return purchasedItems;
        }

        // PUT: api/PurchasedItems/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPurchasedItems(int id, PurchasedItems purchasedItems)
        {
            if (id != purchasedItems.Id)
            {
                return BadRequest();
            }

            _context.Entry(purchasedItems).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PurchasedItemsExists(id))
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

        // POST: api/PurchasedItems
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<PurchasedItems>> PostPurchasedItems(PurchasedItems purchasedItems)
        {
            _context.PurchasedItems.Add(purchasedItems);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPurchasedItems", new { id = purchasedItems.Id }, purchasedItems);
        }

        // DELETE: api/PurchasedItems/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PurchasedItems>> DeletePurchasedItems(int id)
        {
            var purchasedItems = await _context.PurchasedItems.FindAsync(id);
            if (purchasedItems == null)
            {
                return NotFound();
            }

            _context.PurchasedItems.Remove(purchasedItems);
            await _context.SaveChangesAsync();

            return purchasedItems;
        }

        private bool PurchasedItemsExists(int id)
        {
            return _context.PurchasedItems.Any(e => e.Id == id);
        }
    }
}
