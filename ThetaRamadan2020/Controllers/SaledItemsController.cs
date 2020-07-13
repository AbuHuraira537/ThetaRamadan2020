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
    public class SaledItemsController : ControllerBase
    {
        private readonly thetaramdan2020Context _context;

        public SaledItemsController(thetaramdan2020Context context)
        {
            _context = context;
        }

        // GET: api/SaledItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SaledItems>>> GetSaledItems()
        {
            return await _context.SaledItems.ToListAsync();
        }

        // GET: api/SaledItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SaledItems>> GetSaledItems(int id)
        {
            var saledItems = await _context.SaledItems.FindAsync(id);

            if (saledItems == null)
            {
                return NotFound();
            }

            return saledItems;
        }

        // PUT: api/SaledItems/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSaledItems(int id, SaledItems saledItems)
        {
            if (id != saledItems.Id)
            {
                return BadRequest();
            }

            _context.Entry(saledItems).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SaledItemsExists(id))
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

        // POST: api/SaledItems
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<SaledItems>> PostSaledItems(SaledItems saledItems)
        {
            _context.SaledItems.Add(saledItems);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSaledItems", new { id = saledItems.Id }, saledItems);
        }

        // DELETE: api/SaledItems/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<SaledItems>> DeleteSaledItems(int id)
        {
            var saledItems = await _context.SaledItems.FindAsync(id);
            if (saledItems == null)
            {
                return NotFound();
            }

            _context.SaledItems.Remove(saledItems);
            await _context.SaveChangesAsync();

            return saledItems;
        }

        private bool SaledItemsExists(int id)
        {
            return _context.SaledItems.Any(e => e.Id == id);
        }
    }
}
