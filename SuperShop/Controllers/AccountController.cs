using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SuperShop.Data.Entities;
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
                    if (this.Request.Query.Keys.Contains("ReturnUrl"))
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

        public IActionResult Register()
        {
            return View(); // Show the registration view
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterNewUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByEmailAsync(model.Username); // Check if the user already exists
                if (user == null)
                {
                    user = new User
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Email = model.Username,
                        UserName = model.Username
                    };
                    var result = await _userHelper.AddUserAsync(user, model.Password); // Create a new user

                    if (result != IdentityResult.Success)
                    {
                        ModelState.AddModelError(string.Empty, "The user could not be created."); // Add error message for failed registration
                        return View(model); // stay on the registration page if registration fails
                    }

                    var loginViewModel = new LoginViewModel
                    {
                        Password = model.Password,
                        RememberMe = false,
                        Username = model.Username
                    };

                    var result2 = await _userHelper.LoginAsync(loginViewModel); // Log in the user after successful registration

                    if (result2.Succeeded)
                    {
                        return RedirectToAction("Index", "Home"); // Redirect to home page on successful registration
                    }

                    ModelState.AddModelError(string.Empty, "The user could not be created."); // Add error message for failed registration

                }
            }
            return View(model); // stay on the registration page if registration fails or model state is invalid
        }

        public async Task<IActionResult> ChangeUser()
        {
            var user = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name); // Get the currently logged-in user
            var model = new ChangeUserViewModel();

            if (user != null)
            {
                model.FirstName = user.FirstName;
                model.LastName = user.LastName;
            }

            return View(model); // Show the change user view with the current user's information
        }

        [HttpPost]
        public async Task<IActionResult> ChangeUser(ChangeUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name); // Get the currently logged-in user
                if (user != null)
                {
                    user.FirstName = model.FirstName;
                    user.LastName = model.LastName;
                    var response = await _userHelper.UpdateUserAsync(user); // Update the user's information
                    if (response.Succeeded)
                    {
                        ViewBag.UserMessage = "User updated!"; // Show success message
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, response.Errors.FirstOrDefault().Description); // Add error message for failed update
                    }
                }
            }

            return View(model); // stay on the change user page if update fails or model state is invalid

        }

        public IActionResult ChangePassword()
        {
            return View(); // Show the change password view
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name); // Get the currently logged-in user
                if (user != null)
                {
                    var result = await _userHelper.ChangePasswordAsync(user, model.OldPassword, model.NewPassword); // Attempt to change the user's password
                    if (result.Succeeded)
                    {
                        return this.RedirectToAction("ChangeUser"); // Redirect to change user page on successful password change
                    }
                    else
                    {
                        this.ModelState.AddModelError(string.Empty, result.Errors.FirstOrDefault().Description); // Add error message for failed password change
                    }
                }
                else
                {
                    this.ModelState.AddModelError(string.Empty, "User not found."); // Add error message if user is not found
                }
            }
            return this.View(model); // stay on the change password page if update fails or model state is invalid
        }
    }
}
