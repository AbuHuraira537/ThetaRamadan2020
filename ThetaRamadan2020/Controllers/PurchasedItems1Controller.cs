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
    public class PurchasedItems1Controller : Controller
    {
        private readonly thetaramdan2020Context _context;

        public PurchasedItems1Controller(thetaramdan2020Context context)
        {
            _context = context;
        }

        // GET: PurchasedItems1
        public async Task<IActionResult> Index()
        {
            var thetaramdan2020Context = _context.PurchasedItems.Include(p => p.ItemCategoryNavigation).Include(p => p.ItemNavigation);
            return View(await thetaramdan2020Context.ToListAsync());
        }

        // GET: PurchasedItems1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchasedItems = await _context.PurchasedItems
                .Include(p => p.ItemCategoryNavigation)
                .Include(p => p.ItemNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (purchasedItems == null)
            {
                return NotFound();
            }

            return View(purchasedItems);
        }

        // GET: PurchasedItems1/Create
        public IActionResult Create()
        {
            ViewData["ItemCategory"] = new SelectList(_context.Categories, "Id", "Description");
            ViewData["Item"] = new SelectList(_context.Items, "Id", "Color");
            return View();
        }

        // POST: PurchasedItems1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ItemCategory,Item,ItemCount,PurchasedDate,PerItemPrice")] PurchasedItems purchasedItems)
        {
            if (ModelState.IsValid)
            {
                _context.Add(purchasedItems);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ItemCategory"] = new SelectList(_context.Categories, "Id", "Description", purchasedItems.ItemCategory);
            ViewData["Item"] = new SelectList(_context.Items, "Id", "Color", purchasedItems.Item);
            return View(purchasedItems);
        }

        // GET: PurchasedItems1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchasedItems = await _context.PurchasedItems.FindAsync(id);
            if (purchasedItems == null)
            {
                return NotFound();
            }
            ViewData["ItemCategory"] = new SelectList(_context.Categories, "Id", "Description", purchasedItems.ItemCategory);
            ViewData["Item"] = new SelectList(_context.Items, "Id", "Color", purchasedItems.Item);
            return View(purchasedItems);
        }

        // POST: PurchasedItems1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ItemCategory,Item,ItemCount,PurchasedDate,PerItemPrice")] PurchasedItems purchasedItems)
        {
            if (id != purchasedItems.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(purchasedItems);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PurchasedItemsExists(purchasedItems.Id))
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
            ViewData["ItemCategory"] = new SelectList(_context.Categories, "Id", "Description", purchasedItems.ItemCategory);
            ViewData["Item"] = new SelectList(_context.Items, "Id", "Color", purchasedItems.Item);
            return View(purchasedItems);
        }

        // GET: PurchasedItems1/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchasedItems = await _context.PurchasedItems
                .Include(p => p.ItemCategoryNavigation)
                .Include(p => p.ItemNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (purchasedItems == null)
            {
                return NotFound();
            }

            return View(purchasedItems);
        }

        // POST: PurchasedItems1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var purchasedItems = await _context.PurchasedItems.FindAsync(id);
            _context.PurchasedItems.Remove(purchasedItems);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PurchasedItemsExists(int id)
        {
            return _context.PurchasedItems.Any(e => e.Id == id);
        }
    }
}
