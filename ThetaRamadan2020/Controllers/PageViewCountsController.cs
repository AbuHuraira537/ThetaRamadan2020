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
    public class PageViewCountsController : ControllerBase
    {
        private readonly thetaramdan2020Context _context;

        public PageViewCountsController(thetaramdan2020Context context)
        {
            _context = context;
        }

        // GET: api/PageViewCounts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PageViewCount>>> GetPageViewCount()
        {
            return await _context.PageViewCount.ToListAsync();
        }

        // GET: api/PageViewCounts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PageViewCount>> GetPageViewCount(int id)
        {
            var pageViewCount = await _context.PageViewCount.FindAsync(id);

            if (pageViewCount == null)
            {
                return NotFound();
            }

            return pageViewCount;
        }

        // PUT: api/PageViewCounts/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPageViewCount(int id, PageViewCount pageViewCount)
        {
            if (id != pageViewCount.Id)
            {
                return BadRequest();
            }

            _context.Entry(pageViewCount).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PageViewCountExists(id))
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

        // POST: api/PageViewCounts
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<PageViewCount>> PostPageViewCount(PageViewCount pageViewCount)
        {
            _context.PageViewCount.Add(pageViewCount);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPageViewCount", new { id = pageViewCount.Id }, pageViewCount);
        }

        // DELETE: api/PageViewCounts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PageViewCount>> DeletePageViewCount(int id)
        {
            var pageViewCount = await _context.PageViewCount.FindAsync(id);
            if (pageViewCount == null)
            {
                return NotFound();
            }

            _context.PageViewCount.Remove(pageViewCount);
            await _context.SaveChangesAsync();

            return pageViewCount;
        }

        private bool PageViewCountExists(int id)
        {
            return _context.PageViewCount.Any(e => e.Id == id);
        }
    }
}
