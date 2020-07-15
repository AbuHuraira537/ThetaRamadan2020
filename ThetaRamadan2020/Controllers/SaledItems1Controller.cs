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
    public class SaledItems1Controller : Controller
    {
        private readonly thetaramdan2020Context _context;

        public SaledItems1Controller(thetaramdan2020Context context)
        {
            _context = context;
        }

        // GET: SaledItems1
        public async Task<IActionResult> Index()
        {
            var thetaramdan2020Context = _context.SaledItems.Include(s => s.Category).Include(s => s.Customer).Include(s => s.Item);
            return View(await thetaramdan2020Context.ToListAsync());
        }

        // GET: SaledItems1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var saledItems = await _context.SaledItems
                .Include(s => s.Category)
                .Include(s => s.Customer)
                .Include(s => s.Item)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (saledItems == null)
            {
                return NotFound();
            }

            return View(saledItems);
        }

        // GET: SaledItems1/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Description");
            ViewData["CustomerId"] = new SelectList(_context.Users, "Id", "Name");
            ViewData["ItemId"] = new SelectList(_context.Items, "Id", "Color");
            return View();
        }

        // POST: SaledItems1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CustomerId,SaledDate,CategoryId,ItemId,ItemCount,TotalBill")] SaledItems saledItems)
        {
            if (ModelState.IsValid)
            {
                _context.Add(saledItems);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Description", saledItems.CategoryId);
            ViewData["CustomerId"] = new SelectList(_context.Users, "Id", "Name", saledItems.CustomerId);
            ViewData["ItemId"] = new SelectList(_context.Items, "Id", "Color", saledItems.ItemId);
            return View(saledItems);
        }

        // GET: SaledItems1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var saledItems = await _context.SaledItems.FindAsync(id);
            if (saledItems == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Description", saledItems.CategoryId);
            ViewData["CustomerId"] = new SelectList(_context.Users, "Id", "Name", saledItems.CustomerId);
            ViewData["ItemId"] = new SelectList(_context.Items, "Id", "Color", saledItems.ItemId);
            return View(saledItems);
        }

        // POST: SaledItems1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CustomerId,SaledDate,CategoryId,ItemId,ItemCount,TotalBill")] SaledItems saledItems)
        {
            if (id != saledItems.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(saledItems);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SaledItemsExists(saledItems.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Description", saledItems.CategoryId);
            ViewData["CustomerId"] = new SelectList(_context.Users, "Id", "Name", saledItems.CustomerId);
            ViewData["ItemId"] = new SelectList(_context.Items, "Id", "Color", saledItems.ItemId);
            return View(saledItems);
        }

        // GET: SaledItems1/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var saledItems = await _context.SaledItems
                .Include(s => s.Category)
                .Include(s => s.Customer)
                .Include(s => s.Item)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (saledItems == null)
            {
                return NotFound();
            }

            return View(saledItems);
        }

        // POST: SaledItems1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var saledItems = await _context.SaledItems.FindAsync(id);
            _context.SaledItems.Remove(saledItems);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SaledItemsExists(int id)
        {
            return _context.SaledItems.Any(e => e.Id == id);
        }
    }
}
