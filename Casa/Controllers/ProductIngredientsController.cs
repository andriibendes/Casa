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
    public class ProductIngredientsController : Controller
    {
        private readonly RegisterContext _context;

        public ProductIngredientsController(RegisterContext context)
        {
            _context = context;
        }

        // GET: ProductIngredients
        public async Task<IActionResult> Index()
        {
            var registerContext = _context.ProductIngredient.Include(p => p.Ingredient).Include(p => p.Product);
            return View(await registerContext.ToListAsync());
        }

        // GET: ProductIngredients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productIngredient = await _context.ProductIngredient
                .Include(p => p.Ingredient)
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productIngredient == null)
            {
                return NotFound();
            }

            return View(productIngredient);
        }

        // GET: ProductIngredients/Create
        public IActionResult Create()
        {
            ViewData["IngredientId"] = new SelectList(_context.Ingredient, "Id", "Name");
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Name");
            return View();
        }

        // POST: ProductIngredients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductId,IngredientId,Quantity")] ProductIngredient productIngredient)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productIngredient);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IngredientId"] = new SelectList(_context.Ingredient, "Id", "Name", productIngredient.IngredientId);
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Name", productIngredient.ProductId);
            return View(productIngredient);
        }

        // GET: ProductIngredients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productIngredient = await _context.ProductIngredient.FindAsync(id);
            if (productIngredient == null)
            {
                return NotFound();
            }
            ViewData["IngredientId"] = new SelectList(_context.Ingredient, "Id", "Name", productIngredient.IngredientId);
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Name", productIngredient.ProductId);
            return View(productIngredient);
        }

        // POST: ProductIngredients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductId,IngredientId,Quantity")] ProductIngredient productIngredient)
        {
            if (id != productIngredient.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productIngredient);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductIngredientExists(productIngredient.Id))
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
            ViewData["IngredientId"] = new SelectList(_context.Ingredient, "Id", "Name", productIngredient.IngredientId);
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Name", productIngredient.ProductId);
            return View(productIngredient);
        }

        // GET: ProductIngredients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productIngredient = await _context.ProductIngredient
                .Include(p => p.Ingredient)
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productIngredient == null)
            {
                return NotFound();
            }

            return View(productIngredient);
        }

        // POST: ProductIngredients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productIngredient = await _context.ProductIngredient.FindAsync(id);
            _context.ProductIngredient.Remove(productIngredient);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductIngredientExists(int id)
        {
            return _context.ProductIngredient.Any(e => e.Id == id);
        }
    }
}
