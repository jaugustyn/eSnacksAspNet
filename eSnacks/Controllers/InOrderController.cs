using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using eSnacks.Data;
using eSnacks.Models;

namespace eSnacks.Controllers
{
    public class InOrderController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InOrderController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: InOrder
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.InOrders.Include(i => i.MenuItem).Include(i => i.PlacedOrder).ThenInclude(po => po.Restaurant);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: InOrder/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.InOrders == null)
            {
                return NotFound();
            }

            var inOrder = await _context.InOrders
                .Include(i => i.MenuItem)
                .Include(i => i.PlacedOrder)
                .ThenInclude(po => po.Restaurant)
                .FirstOrDefaultAsync(m => m.InOrderId == id);
            if (inOrder == null)
            {
                return NotFound();
            }

            return View(inOrder);
        }

        // GET: InOrder/Create
        public IActionResult Create()
        {
            ViewData["MenuItemId"] = new SelectList(_context.MenuItems, "Id", "ItemName");
            ViewData["PlacedOrderId"] = new SelectList(_context.PlacedOrders, "PlacedOrderId", "PlacedOrderId");
            return View();
        }

        // POST: InOrder/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InOrderId,Quantity,Price,Comment,PlacedOrderId,Id")] InOrder inOrder)
        {
            if (ModelState.IsValid)
            {
                _context.Add(inOrder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MenuItemId"] = new SelectList(_context.MenuItems, "Id", "ItemName", inOrder.MenuItemId);
            ViewData["PlacedOrderId"] = new SelectList(_context.PlacedOrders, "PlacedOrderId", "PlacedOrderId", inOrder.PlacedOrderId);
            return View(inOrder);
        }

        // GET: InOrder/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.InOrders == null)
            {
                return NotFound();
            }

            var inOrder = await _context.InOrders.FindAsync(id);
            if (inOrder == null)
            {
                return NotFound();
            }
            ViewData["MenuItemId"] = new SelectList(_context.MenuItems, "Id", "ItemName", inOrder.MenuItemId);
            ViewData["PlacedOrderId"] = new SelectList(_context.PlacedOrders, "PlacedOrderId", "PlacedOrderId", inOrder.PlacedOrderId);
            return View(inOrder);
        }

        // POST: InOrder/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("InOrderId,Quantity,Price,Comment,PlacedOrderId,Id")] InOrder inOrder)
        {
            if (id != inOrder.InOrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(inOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InOrderExists(inOrder.InOrderId))
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
            ViewData["MenuItemId"] = new SelectList(_context.MenuItems, "Id", "ItemName", inOrder.MenuItemId);
            ViewData["PlacedOrderId"] = new SelectList(_context.PlacedOrders, "PlacedOrderId", "PlacedOrderId", inOrder.PlacedOrderId);
            return View(inOrder);
        }

        // GET: InOrder/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.InOrders == null)
            {
                return NotFound();
            }

            var inOrder = await _context.InOrders
                .Include(i => i.MenuItem)
                .Include(i => i.PlacedOrder)
                .ThenInclude(po => po.Restaurant)
                .FirstOrDefaultAsync(m => m.InOrderId == id);
            if (inOrder == null)
            {
                return NotFound();
            }

            return View(inOrder);
        }

        // POST: InOrder/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.InOrders == null)
            {
                return Problem("Entity set 'ApplicationDbContext.InOrders'  is null.");
            }
            var inOrder = await _context.InOrders.FindAsync(id);
            if (inOrder != null)
            {
                _context.InOrders.Remove(inOrder);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InOrderExists(int id)
        {
          return _context.InOrders.Any(e => e.InOrderId == id);
        }
    }
}
