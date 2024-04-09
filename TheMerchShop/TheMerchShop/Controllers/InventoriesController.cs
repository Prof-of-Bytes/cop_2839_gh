using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TheMerchShop.DataAccess;
using TheMerchShop.Models;

namespace TheMerchShop.Controllers
{
    public class InventoriesController : Controller
    {
        private readonly MerchShopContext _context;

        public InventoriesController(MerchShopContext context)
        {
            _context = context;
        }

        // GET: Inventories
        public async Task<IActionResult> Index()
        {
            TempData["message"] = "Hello from the Inventory Controller";
            var merchShopContext = _context.Inventories.Include(i => i.Merch).Include(i => i.Warehouse);
            return View(await merchShopContext.ToListAsync());
        }

        // GET: Inventories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventory = await _context.Inventories
                .Include(i => i.Merch)
                .Include(i => i.Warehouse)
                .FirstOrDefaultAsync(m => m.InventoryId == id);
            if (inventory == null)
            {
                return NotFound();
            }

            return View(inventory);
        }

        // GET: Inventories/Create
        public IActionResult Create()
        {
            ViewData["MerchID"] = new SelectList(_context.Merch, "MerchID", "MerchID");
            ViewData["WarehouseID"] = new SelectList(_context.Warehouses, "WarehouseID", "WarehouseID");
            return View();
        }

        // POST: Inventories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InventoryId,MerchID,WarehouseID,Quantity,PurchaseDate,PurchasePrice,SalePrice")] Inventory inventory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(inventory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MerchID"] = new SelectList(_context.Merch, "MerchID", "MerchID", inventory.MerchID);
            ViewData["WarehouseID"] = new SelectList(_context.Warehouses, "WarehouseID", "WarehouseID", inventory.WarehouseID);
            return View(inventory);
        }

        // GET: Inventories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventory = await _context.Inventories.FindAsync(id);
            if (inventory == null)
            {
                return NotFound();
            }
            ViewData["MerchID"] = new SelectList(_context.Merch, "MerchID", "MerchID", inventory.MerchID);
            ViewData["WarehouseID"] = new SelectList(_context.Warehouses, "WarehouseID", "WarehouseID", inventory.WarehouseID);
            return View(inventory);
        }

        // POST: Inventories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("InventoryId,MerchID,WarehouseID,Quantity,PurchaseDate,PurchasePrice,SalePrice")] Inventory inventory)
        {
            if (id != inventory.InventoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(inventory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InventoryExists(inventory.InventoryId))
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
            ViewData["MerchID"] = new SelectList(_context.Merch, "MerchID", "MerchID", inventory.MerchID);
            ViewData["WarehouseID"] = new SelectList(_context.Warehouses, "WarehouseID", "WarehouseID", inventory.WarehouseID);
            return View(inventory);
        }

        // GET: Inventories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventory = await _context.Inventories
                .Include(i => i.Merch)
                .Include(i => i.Warehouse)
                .FirstOrDefaultAsync(m => m.InventoryId == id);
            if (inventory == null)
            {
                return NotFound();
            }

            return View(inventory);
        }

        // POST: Inventories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var inventory = await _context.Inventories.FindAsync(id);
            if (inventory != null)
            {
                _context.Inventories.Remove(inventory);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InventoryExists(int id)
        {
            return _context.Inventories.Any(e => e.InventoryId == id);
        }
    }
}
