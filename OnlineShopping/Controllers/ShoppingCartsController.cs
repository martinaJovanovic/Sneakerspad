using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineShopping.Areas.Identity.Data;
using OnlineShopping.Data;
using OnlineShopping.Models;
using OnlineShopping.ViewModels;

namespace OnlineShopping
{
    public class ShoppingCartsController : Controller
    {
        private readonly OnlineShoppingContext _context;
        private readonly UserManager<OnlineShoppingUser> _userManager;


        public ShoppingCartsController(OnlineShoppingContext context, UserManager<OnlineShoppingUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: ShoppingCarts
        public async Task<IActionResult> Index(int? uId)
        {
            if (uId == null)
            {
                OnlineShoppingUser currentUser = await _userManager.GetUserAsync(User);
                var client = _context.Client.Where(c => c.FirstName.Equals(currentUser.FirstName) && c.LastName.Equals(currentUser.LastName)).FirstOrDefault();
                int? clientId = client.Id;
                if (clientId != null)
                    return RedirectToAction(nameof(Index), new { uId = clientId });
                else
                    return NotFound();
            }
            var carts = _context.ShoppingCart.Include(s => s.Size).Include(s => s.Product);
            OnlineShoppingUser user = await _userManager.GetUserAsync(User);
            string ime = user.FirstName + " " + user.LastName;
            ViewData["Ime"] = ime;
            return View(await carts.ToListAsync());
        }

        // GET: ShoppingCarts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shoppingCart = await _context.ShoppingCart
                .Include(s => s.Client)
                .Include(s => s.ProductSize)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shoppingCart == null)
            {
                return NotFound();
            }

            return View(shoppingCart);
        }

        // GET: ShoppingCarts/Create
        public async Task<IActionResult> Create(int pId, int? uId)
        {
            if (uId == null)
            {
                OnlineShoppingUser currentUser = await _userManager.GetUserAsync(User);
                var client = _context.Client.Where(c => c.FirstName.Equals(currentUser.FirstName) && c.LastName.Equals(currentUser.LastName)).FirstOrDefault();
                int? clientId = client.Id;
                if (clientId != null)
                {
                    return RedirectToAction(nameof(Create), new { uId = clientId, pId = pId });
                }
                else
                    return NotFound();

            }
            var productsizes = _context.ProductSize.Where(p => p.ProductId == pId);
            var sizes = productsizes.Select(p => p.Size);
            var product = _context.Product.Where(p => p.Id == pId).FirstOrDefault();
            ShoppingCartVM viewmodel = new ShoppingCartVM
            {
                Product = product,
                ProductId = pId,
                ClientId = (int)uId,
                ListSizes = new SelectList(sizes.AsEnumerable(), "Id", "ProductSize"),
                ProductSizes = productsizes.AsEnumerable()
            }; 
            return View(viewmodel);
    } 
    

    // POST: ShoppingCarts/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to, for 
    // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ShoppingCartVM viewmodel)
        {
            if (ModelState.IsValid)
            {
                var productId = viewmodel.ProductId;
                var Sizeid = viewmodel.SizeId;
                var size = _context.Size.Where(s => s.Id == Sizeid).FirstOrDefault();
                var quantity = viewmodel.Quantity;
                var productsize = _context.ProductSize.Where(p => p.ProductId == productId && p.SizeId == Sizeid).FirstOrDefault();
                var productsizeId = productsize.Id;
                var shoppingCart = new ShoppingCart { ProductId = productId, SizeId = Sizeid, ClientId = viewmodel.ClientId, ProductSizeId = productsizeId, Quantity = viewmodel.Quantity };
                _context.Add(shoppingCart);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(viewmodel);
        }

        // GET: ShoppingCarts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shoppingCart = await _context.ShoppingCart.FindAsync(id);
            if (shoppingCart == null)
            {
                return NotFound();
            }
            ViewData["ClientId"] = new SelectList(_context.Set<Client>(), "Id", "Id", shoppingCart.ClientId);
            //ViewData["ProductSizeId"] = new SelectList(_context.Set<ProductSize>(), "Id", "Id", shoppingCart.ProductSizeId);
            return View(shoppingCart);
        }

        // POST: ShoppingCarts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ClientId,ProductSizeId,Quantity")] ShoppingCart shoppingCart)
        {
            if (id != shoppingCart.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shoppingCart);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShoppingCartExists(shoppingCart.Id))
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
            ViewData["ClientId"] = new SelectList(_context.Set<Client>(), "Id", "Id", shoppingCart.ClientId);
            //ViewData["ProductSizeId"] = new SelectList(_context.Set<ProductSize>(), "Id", "Id", shoppingCart.ProductSizeId);
            return View(shoppingCart);
        }

        // GET: ShoppingCarts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shoppingCart = await _context.ShoppingCart
                .Include(s => s.Client)
                .Include(s => s.ProductSize)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shoppingCart == null)
            {
                return NotFound();
            }

            return View(shoppingCart);
        }

        // POST: ShoppingCarts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shoppingCart = await _context.ShoppingCart.FindAsync(id);
            _context.ShoppingCart.Remove(shoppingCart);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShoppingCartExists(int id)
        {
            return _context.ShoppingCart.Any(e => e.Id == id);
        }
    }
}
