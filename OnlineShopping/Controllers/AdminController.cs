using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShopping.Areas.Identity.Data;
using OnlineShopping.Data;
using OnlineShopping.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private UserManager<OnlineShoppingUser> _userManager;

        public AdminController(UserManager<OnlineShoppingUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            IQueryable<OnlineShoppingUser> users = _userManager.Users.OrderBy(u => u.Email);
            return View(users);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            OnlineShoppingUser user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                    return RedirectToAction("Index");
            }
            else
                ModelState.AddModelError("", "User Not Found");
            return View("Index", _userManager.Users);
        }

    }
}
