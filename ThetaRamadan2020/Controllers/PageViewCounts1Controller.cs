using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ThetaRamadan2020.Models;

namespace ThetaRamadan2020.Controllers
{
    public class PageViewCounts1Controller : Controller
    {
        private readonly thetaramdan2020Context _context;

        public PageViewCounts1Controller(thetaramdan2020Context context)
        {
            _context = context;
        }

        // GET: PageViewCounts1
        public async Task<IActionResult> Index()
        {
            return View(await _context.PageViewCount.ToListAsync());
        }

        // GET: PageViewCounts1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pageViewCount = await _context.PageViewCount
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pageViewCount == null)
            {
                return NotFound();
            }

            return View(pageViewCount);
        }

        // GET: PageViewCounts1/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PageViewCounts1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Url,Count")] PageViewCount pageViewCount)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pageViewCount);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pageViewCount);
        }

        // GET: PageViewCounts1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pageViewCount = await _context.PageViewCount.FindAsync(id);
            if (pageViewCount == null)
            {
                return NotFound();
            }
            return View(pageViewCount);
        }

        // POST: PageViewCounts1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Url,Count")] PageViewCount pageViewCount)
        {
            if (id != pageViewCount.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pageViewCount);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PageViewCountExists(pageViewCount.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(pageViewCount);
        }

        // GET: PageViewCounts1/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pageViewCount = await _context.PageViewCount
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pageViewCount == null)
            {
                return NotFound();
            }

            return View(pageViewCount);
        }

        // POST: PageViewCounts1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pageViewCount = await _context.PageViewCount.FindAsync(id);
            _context.PageViewCount.Remove(pageViewCount);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PageViewCountExists(int id)
        {
            return _context.PageViewCount.Any(e => e.Id == id);
        }
    }
}
