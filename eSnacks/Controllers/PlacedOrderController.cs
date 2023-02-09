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
    public class PlacedOrderController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PlacedOrderController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PlacedOrder
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PlacedOrders.Include(p => p.OrderStatus).Include(p => p.Restaurant).Include(p => p.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PlacedOrder/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PlacedOrders == null)
            {
                return NotFound();
            }

            var placedOrder = await _context.PlacedOrders
                .Include(p => p.OrderStatus)
                .Include(p => p.Restaurant)
                .Include(p => p.User)
                .Include(x => x.InOrders).ThenInclude(x => x.MenuItem)
                .FirstOrDefaultAsync(m => m.PlacedOrderId == id);
            if (placedOrder == null)
            {
                return NotFound();
            }

            return View(placedOrder);
        }

        // GET: PlacedOrder/Create
        public IActionResult Create()
        {
            ViewData["OrderStatusId"] = new SelectList(_context.OrderStatuses, "OrderStatusId", "OrderStatusId");
            ViewData["RestaurantId"] = new SelectList(_context.Restaurants, "RestaurantId", "RestaurantId");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: PlacedOrder/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PlacedOrderId,OrderTime,EstimatedDeliveryTime,DeliveryAddress,Price,Discount,FinalPrice,Comment,OrderStatusId,UserId,RestaurantId")] PlacedOrder placedOrder)
        {
            if (ModelState.IsValid)
            {
                _context.Add(placedOrder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrderStatusId"] = new SelectList(_context.OrderStatuses, "OrderStatusId", "OrderStatusId", placedOrder.OrderStatusId);
            ViewData["RestaurantId"] = new SelectList(_context.Restaurants, "RestaurantId", "RestaurantId", placedOrder.RestaurantId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", placedOrder.UserId);
            return View(placedOrder);
        }

        // GET: PlacedOrder/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PlacedOrders == null)
            {
                return NotFound();
            }

            var placedOrder = await _context.PlacedOrders.FindAsync(id);
            if (placedOrder == null)
            {
                return NotFound();
            }
            ViewData["OrderStatusId"] = new SelectList(_context.OrderStatuses, "OrderStatusId", "Status", placedOrder.OrderStatusId);
            ViewData["RestaurantId"] = new SelectList(_context.Restaurants, "RestaurantId", "RestaurantName", placedOrder.RestaurantId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", placedOrder.UserId);
            return View(placedOrder);
        }

        // POST: PlacedOrder/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PlacedOrderId,OrderTime,EstimatedDeliveryTime,DeliveryAddress,Price,Discount,FinalPrice,Comment,OrderStatusId,UserId,RestaurantId")] PlacedOrder placedOrder)
        {
            if (id != placedOrder.PlacedOrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(placedOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlacedOrderExists(placedOrder.PlacedOrderId))
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
            ViewData["OrderStatusId"] = new SelectList(_context.OrderStatuses, "OrderStatusId", "Status", placedOrder.OrderStatusId);
            ViewData["RestaurantId"] = new SelectList(_context.Restaurants, "RestaurantId", "RestaurantName", placedOrder.RestaurantId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", placedOrder.UserId);
            return View(placedOrder);
        }

        // GET: PlacedOrder/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PlacedOrders == null)
            {
                return NotFound();
            }

            var placedOrder = await _context.PlacedOrders
                .Include(p => p.OrderStatus)
                .Include(p => p.Restaurant)
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.PlacedOrderId == id);
            if (placedOrder == null)
            {
                return NotFound();
            }

            return View(placedOrder);
        }

        // POST: PlacedOrder/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PlacedOrders == null)
            {
                return Problem("Entity set 'ApplicationDbContext.PlacedOrders'  is null.");
            }
            var placedOrder = await _context.PlacedOrders.FindAsync(id);
            if (placedOrder != null)
            {
                _context.PlacedOrders.Remove(placedOrder);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlacedOrderExists(int id)
        {
          return _context.PlacedOrders.Any(e => e.PlacedOrderId == id);
        }
    }
}
