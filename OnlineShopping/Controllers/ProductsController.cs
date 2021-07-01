using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineShopping.Data;
using OnlineShopping.Models;
using OnlineShopping.ViewModels;

namespace OnlineShopping
{
    public class ProductsController : Controller
    {
        private readonly OnlineShoppingContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductsController(OnlineShoppingContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Products

        public async Task<IActionResult> Index(string? color, string? sex, string? brand, decimal? cenaOd, decimal? cenaDo)
        {
            IQueryable<Product> products = _context.Product.AsQueryable();
            if (!string.IsNullOrEmpty(color))
            {
                products = products.Where(p => p.Color.Contains(color));
            }

            if (!string.IsNullOrEmpty(sex))
            {
                if (sex.Equals("Женски"))
                {
                    products = products.Where(p => p.Sex.Equals("Женски") || p.Sex.Equals("Унисекс"));
                }
                if (sex.Equals("Машки"))
                {
                    products = products.Where(p => p.Sex.Equals("Машки") || p.Sex.Equals("Унисекс"));
                }
                if (sex.Equals("Деца"))
                {
                    products = products.Where(p => p.Sex.Equals("Деца"));
                }
            }

            if (!string.IsNullOrEmpty(brand))
            {
                products = products.Where(p => p.Name.Contains(brand));
            }

            if (cenaOd != null)
            {
                products = products.Where(p => p.Price >= cenaOd);
            }

            if (cenaDo != null)
            {
                products = products.Where(p => p.Price <= cenaDo);
            }

            if (cenaDo != null)
            {
                products = products.Where(p => p.Price <= cenaDo);
            }

            List<string> colors = new List<string>();
            List<string> brands = new List<string>();
            List<decimal> ceniOd = new List<decimal>();
            List<decimal> ceniDo = new List<decimal>();
            colors.Add("Бела");
            colors.Add("Црна");
            colors.Add("Сива");
            colors.Add("Мултиколор");
            colors.Add("Жолта");
            colors.Add("Розева");
            colors.Add("Зелена");
            colors.Add("Сина");
            brands.Add("Adidas");
            brands.Add("Nike");
            brands.Add("Puma");
            brands.Add("Reebok");
            brands.Add("Vans");
            brands.Add("Fila");
            ceniOd.Add(500);
            ceniOd.Add(1000);
            ceniOd.Add(2000);
            ceniOd.Add(3000);
            ceniOd.Add(4000);
            ceniDo.Add(1000);
            ceniDo.Add(2000);
            ceniDo.Add(3000);
            ceniDo.Add(4000);
            ceniDo.Add(5000);
            ceniDo.Add(6000);
            ceniDo.Add(7000);
            ceniDo.Add(8000);

            var viewmodel = new ProductsFilter
            {
                Products = products.AsEnumerable(),
                Brands = new SelectList(brands.AsEnumerable()),
                Colors = new SelectList(colors.AsEnumerable()),
                CeniOd = new SelectList(ceniOd.AsEnumerable()),
                CeniDo = new SelectList(ceniDo.AsEnumerable()),
                Sex = sex
            };
            return View(viewmodel);
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
            var productsize = _context.ProductSize.Where(p => p.ProductId == id).AsEnumerable();
            var sizes = productsize.Select(p => p.SizeId).AsEnumerable();
            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductAddVM viewmodel)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = UploadedFile(viewmodel);

                Product product = new Product
                {
                    Name = viewmodel.Name,
                    Price = viewmodel.Price,
                    Description = viewmodel.Description,
                    PictureUrl = uniqueFileName,
                    Sex = viewmodel.Sex,
                    Color = viewmodel.Color,
                };

                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        private string UploadedFile(ProductAddVM model)
        {
            string uniqueFileName = null;

            if (model.Picture != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(model.Picture.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Picture.CopyTo(fileStream);
                }
                uniqueFileName = "/images/" + uniqueFileName;
            }
            return uniqueFileName;
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
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,Description,PictureUrl,Color,Sex,CenaPopust")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                String sex = product.Sex;
                return RedirectToAction(nameof(Index), new { sex = sex });
            }
            return View(product);
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
            _context.Product.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.Id == id);
        }

        private string UploadedFile(ProductsVM viewmodel)
        {
            string uniqueFileName = null;

            if (viewmodel.Picture != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(viewmodel.Picture.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    viewmodel.Picture.CopyTo(fileStream);
                }
                uniqueFileName = "/images/" + uniqueFileName;
            }
            return uniqueFileName;
        }

        public async Task<IActionResult> Search(string search)
        {
            IQueryable<Product> products = _context.Product.AsQueryable();
            if (!string.IsNullOrEmpty(search))
            {
                products = products.Where(p => p.Name.Contains(search));
            }
            else
            {
                products = null;
            }
            var viewmodel = new ProductsSearch
            {
                Products = products
            };
            return View(viewmodel);
        }

        // GET: Products/Popust/5
        public async Task<IActionResult> Popust(int? id)
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

            PopustVM popustVM = new PopustVM
            {
                Product = product
            };
            return View(popustVM);
        }

        // POST: Products/Popust/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Popust(int id, PopustVM viewmodel)
        {
            if (id != viewmodel.Product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Product product = viewmodel.Product;
                    decimal price = product.Price;
                    int popust = viewmodel.Popust;
                    price = price - (price * popust / 100);
                    viewmodel.Product.CenaPopust = price;
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(viewmodel.Product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                string sex = viewmodel.Product.Sex;
                ViewData["Sex"] = sex;
                return RedirectToAction(nameof(Index), new { sex = sex });
            }
            return View(viewmodel);
        }
    }
}
