using Microsoft.AspNetCore.Mvc;
using SuperShop.Helpers;
using SuperShop.Models;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShop.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserHelper _userHelper;
        public AccountController(IUserHelper userHelper)
        {
            _userHelper = userHelper; // Dependency injection of IUserHelper to manage user-related operations


        }

        public IActionResult Login()
        {
            // If the user is already logged in, redirect them to the home page
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            // Otherwise, show the login view
            return View();
        }

        // Handle the login form submission
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            // if all required fields are valid, attempt to log in the user
            if (ModelState.IsValid)
            {
                var result = await _userHelper.LoginAsync(model); // Attempt to log in the user
                if (result.Succeeded)
                {
                    if(this.Request.Query.Keys.Contains("ReturnUrl"))
                    {
                        // If a return URL is specified, redirect the user to that URL after successful login
                        return Redirect(this.Request.Query["ReturnUrl"].First());
                    }

                    return RedirectToAction("Index", "Home"); // Redirect to home page on successful login
                }

                ModelState.AddModelError(string.Empty, "Failed to log in."); // Add error message for invalid login
            }
            return View(model); // stay on the login page if login fails or model state is invalid
        }

        public async Task<IActionResult> Logout()
        {
            await _userHelper.LogoutAsync(); // Log out the user
            return RedirectToAction("Index", "Home"); // Redirect to home page after logout
        }
    }
}
