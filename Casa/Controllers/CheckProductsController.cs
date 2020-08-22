using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Casa;

namespace Casa.Controllers
{
    public class CheckProductsController : Controller
    {
        private readonly RegisterContext _context;

        public CheckProductsController(RegisterContext context)
        {
            _context = context;
        }

        // GET: CheckProducts
        public async Task<IActionResult> Index()
        {
            var registerContext = _context.CheckProduct.Include(c => c.Check).Include(c => c.Product);
            return View(await registerContext.ToListAsync());
        }

        // GET: CheckProducts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var checkProduct = await _context.CheckProduct
                .Include(c => c.Check)
                .Include(c => c.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (checkProduct == null)
            {
                return NotFound();
            }

            return View(checkProduct);
        }

        // GET: CheckProducts/Create
        public IActionResult Create()
        {
            ViewData["CheckId"] = new SelectList(_context.Check, "Id", "Id");
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Name");
            return View();
        }

        // POST: CheckProducts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CheckId,ProductId,Quantity")] CheckProduct checkProduct)
        {
            if (ModelState.IsValid)
            {
                _context.Add(checkProduct);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CheckId"] = new SelectList(_context.Check, "Id", "Id", checkProduct.CheckId);
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Name", checkProduct.ProductId);
            return View(checkProduct);
        }

        // GET: CheckProducts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var checkProduct = await _context.CheckProduct.FindAsync(id);
            if (checkProduct == null)
            {
                return NotFound();
            }
            ViewData["CheckId"] = new SelectList(_context.Check, "Id", "Id", checkProduct.CheckId);
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Name", checkProduct.ProductId);
            return View(checkProduct);
        }

        // POST: CheckProducts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CheckId,ProductId,Quantity")] CheckProduct checkProduct)
        {
            if (id != checkProduct.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(checkProduct);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CheckProductExists(checkProduct.Id))
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
            ViewData["CheckId"] = new SelectList(_context.Check, "Id", "Id", checkProduct.CheckId);
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Name", checkProduct.ProductId);
            return View(checkProduct);
        }

        // GET: CheckProducts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var checkProduct = await _context.CheckProduct
                .Include(c => c.Check)
                .Include(c => c.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (checkProduct == null)
            {
                return NotFound();
            }

            return View(checkProduct);
        }

        // POST: CheckProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var checkProduct = await _context.CheckProduct.FindAsync(id);
            _context.CheckProduct.Remove(checkProduct);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CheckProductExists(int id)
        {
            return _context.CheckProduct.Any(e => e.Id == id);
        }
    }
}
