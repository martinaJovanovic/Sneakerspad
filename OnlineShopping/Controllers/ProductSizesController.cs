using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineShopping.Data;
using OnlineShopping.Models;
using OnlineShopping.ViewModels;

namespace OnlineShopping
{
    public class ProductSizesController : Controller
    {
        private readonly OnlineShoppingContext _context;

        public ProductSizesController(OnlineShoppingContext context)
        {
            _context = context;
        }

        // GET: ProductSizes
        public async Task<IActionResult> Index()
        {
            var onlineShoppingContext = _context.ProductSize.Include(p => p.Product).Include(p => p.Size);
            return View(await onlineShoppingContext.ToListAsync());
        }

        // GET: ProductSizes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productSize = await _context.ProductSize
                .Include(p => p.Product)
                .Include(p => p.Size)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productSize == null)
            {
                return NotFound();
            }

            return View(productSize);
        }

        // GET: ProductSizes/Create
        public IActionResult Create(int id)
        {
            IEnumerable<Size> sizes = _context.Size.AsEnumerable();
            sizes.OrderBy(s => s.ProductSize);
            if (_context.ProductSize.Where(p => p.ProductId == id).Any())
            {
                return RedirectToAction("Edit", "ProductSizes", new { pId = id });
            }
            else { 
                ProductSizeVM viewmodel = new ProductSizeVM
                {
                    ProductId = id,
                    SizeList = new SelectList(sizes.AsEnumerable(), "Id", "ProductSize"),
                };
                return View(viewmodel);
            }
        }

        // POST: ProductSizes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductSizeVM viewmodel)
        {
            if (ModelState.IsValid)
            {
                int productId = viewmodel.ProductId;
                Product product = _context.Product.Where(p => p.Id == productId).FirstOrDefault();
                string sex = product.Sex;
                IEnumerable<int> listSizes = viewmodel.SelectedSizes;
                foreach (int sizeId in listSizes)
                {
                    _context.ProductSize.Add(new ProductSize { ProductId = productId, SizeId = sizeId });
                }
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Products", new { sex = sex });
            }
            return View(viewmodel);
        }

        // GET: ProductSizes/Edit?pId=5
        public async Task<IActionResult> Edit(int pId)
        {
            var product = _context.Product.Where(p => p.Id == pId).FirstOrDefault();
            if (product == null)
            {
                return NotFound();
            }
            var sizes = _context.Size.AsEnumerable();
            var productsize = _context.ProductSize.Where(ps => ps.ProductId == pId);
            var selectedsizes = productsize.Select(s => s.SizeId).AsEnumerable();
            ProductSizeEditVM viewmodel = new ProductSizeEditVM
            {
                Product = product,
                SizeList = new MultiSelectList(sizes, "Id", "ProductSize"),
                SelectedSizes = selectedsizes
            };
            return View(viewmodel);
        }

        // POST: ProductSizes/Edit?pId=5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductSizeEditVM viewmodel)
        {
            if (ModelState.IsValid)
            {
                var sex = viewmodel.Product.Sex;
                _context.Update(viewmodel.Product);
                await _context.SaveChangesAsync();
                Product product = viewmodel.Product;
                var pId = product.Id;
                IEnumerable<int> listSizes = viewmodel.SelectedSizes;
                IQueryable<ProductSize> toBeRemoved = _context.ProductSize.Where(s => !listSizes.Contains(s.SizeId) && s.ProductId == pId);
                _context.ProductSize.RemoveRange(toBeRemoved);
                IEnumerable<int> existSizes = _context.ProductSize.Where(s => listSizes.Contains(s.SizeId) && s.ProductId == pId).Select(s => s.SizeId);
                IEnumerable<int> newSizes = listSizes.Where(s => !existSizes.Contains(s));
                foreach (int sizeId in newSizes)
                    _context.ProductSize.Add(new ProductSize { SizeId = sizeId, ProductId = pId });
                await _context.SaveChangesAsync();        
                return RedirectToAction("Index", "Products", new { sex = sex });
            }
            return View(viewmodel);
        }

        // GET: ProductSizes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productSize = await _context.ProductSize
                .Include(p => p.Product)
                .Include(p => p.Size)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productSize == null)
            {
                return NotFound();
            }

            return View(productSize);
        }

        // POST: ProductSizes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productSize = await _context.ProductSize.FindAsync(id);
            _context.ProductSize.Remove(productSize);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductSizeExists(int id)
        {
            return _context.ProductSize.Any(e => e.Id == id);
        }
    }
}
