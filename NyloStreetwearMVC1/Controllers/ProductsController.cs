using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NyloStreetwearMVC1.Data;
using NyloStreetwearMVC1.Data.Entities;
using NyloStreetwearMVC1.Models.Product;

namespace NyloStreetwearMVC1.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            return View(await _context.Product.ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(_context.Category, "Id", "Name");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductCreateOrEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                //map viewmodel to entity
                var product = new Product
                {
                    Title = model.Title,
                    Description = model.Description,
                    Price = model.Price,
                    ImageUrl = model.ImageUrl
                };

                _context.Add(product);
                await _context.SaveChangesAsync();

                //process categories
                if (model.SelectedCategoryIds != null)
                {
                    foreach (var categoryId in model.SelectedCategoryIds)
                    {
                        var productCategory = new ProductCategory
                        {
                            ProductId = product.Id,
                            CategoryId = categoryId
                        };
                        _context.Add(productCategory);
                    }
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Categories = new SelectList(_context.Category, "Id", "Name");
            return View(model);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            var model = new ProductCreateOrEditViewModel
            {
                Id = product.Id,
                Title = product.Title,
                Description = product.Description,
                Price = product.Price,
                ImageUrl = product.ImageUrl,
                SelectedCategoryIds = _context.ProductCategories
                    .Where(pc => pc.ProductId == product.Id)
                    .Select(pc => pc.CategoryId)
                    .ToList()
            };

            ViewBag.Categories = new SelectList(_context.Category, "Id", "Name");
            return View(model);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductCreateOrEditViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //map model to entity
                    var product = await _context.Product.FindAsync(id);
                    if (product == null)
                    {
                        return NotFound();
                    }
                    product.Title = model.Title;
                    product.Description = model.Description;
                    product.Price = model.Price;
                    product.ImageUrl = model.ImageUrl;
                    _context.Update(product);
                    await _context.SaveChangesAsync();

                    //update categories
                    var existingCategories = _context.ProductCategories.Where(pc => pc.ProductId == id).ToList();
                    if (model.SelectedCategoryIds != null)
                    {
                        _context.ProductCategories.RemoveRange(existingCategories);
                        foreach (var categoryId in model.SelectedCategoryIds)
                        {
                            var productCategory = new ProductCategory
                            {
                                ProductId = id,
                                CategoryId = categoryId
                            };
                            _context.Add(productCategory);
                        }
                        await _context.SaveChangesAsync();
                    }

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(model.Id))
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

            ViewBag.Categories = new SelectList(_context.Category, "Id", "Name");
            return View(model);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Product.FindAsync(id);
            if (product != null)
            {
                _context.Product.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.Id == id);
        }
    }
}
