using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using OnlineShopping.Areas.Identity.Data;
using OnlineShopping.Data;
using OnlineShopping.Models;

namespace OnlineShopping.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<OnlineShoppingUser> _signInManager;
        private readonly UserManager<OnlineShoppingUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly OnlineShoppingContext _context;
        

        public RegisterModel(
            UserManager<OnlineShoppingUser> userManager,
            SignInManager<OnlineShoppingUser> signInManager,
            ILogger<RegisterModel> logger, OnlineShoppingContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _context = context;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Display(Name = "Име")]
            public string FirstName { get; set; }
            [Display(Name = "Презиме")]
            public string LastName { get; set; }
            [Required]
            [EmailAddress]
            [Display(Name = "Email адреса")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Лозинка")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Потврди лозинка")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new OnlineShoppingUser
                {
                    UserName = Input.Email,
                    Email = Input.Email,
                    FirstName = Input.FirstName,
                    LastName = Input.LastName
                };
                IdentityResult result = null;

                if (User.Identity.IsAuthenticated && User.IsInRole("Admin"))
                {
                    user.Role = "Менаџер";
                    result = await _userManager.CreateAsync(user, Input.Password);
                    if (result.Succeeded) { var result1 = await _userManager.AddToRoleAsync(user, "Manager"); }
                    Manager manager = new Manager { FirstName = Input.FirstName, LastName = Input.LastName };
                    _context.Manager.Add(manager);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "Managers");
                }
                user.Role = "Клиент";
                result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded) { var result1 = await _userManager.AddToRoleAsync(user, "Client"); }
                Client client = new Client { FirstName = Input.FirstName, LastName = Input.LastName };
                _context.Client.Add(client);
                await _context.SaveChangesAsync();
                
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
