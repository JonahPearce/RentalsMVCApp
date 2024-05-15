using EntityLibrary.Entities;
using LogicLibrary.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PROG73020_M3_Group10.Models;
using System.Numerics;

namespace PROG73020_M3_Group10.Controllers
{
    public class AccountController : Controller
    {
        public AccountController(SignInManager<Customer> signInManager, UserManager<Customer> userManager, IPropertyManager propertyManager, PropertyDBContext propertyDBContext)
        {
            _propertyManager = propertyManager;
            _propertyContext = propertyDBContext;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new Customer
                {
                    UserName = model.Username,
                    Email = model.Email,
                    PhoneNumber = model.Phone,
                    FirstName = model.FirstName,
                    LastName = model.LastName
                };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult LogIn(string returnURL = "")
        {
            var model = new LoginViewModel { ReturnUrl = returnURL };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password,
                            isPersistent: model.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }

            ModelState.AddModelError("", "Invalid username/password.");
            return View(model);
        }

        [HttpGet]
        public ViewResult AccessDenied()
        {
            return View();
        }

        [Authorize]
        [HttpGet]
        public IActionResult Profile()
        {
            //  await _signInManager.

            Customer customer = _propertyManager.GetCustomerByName(User.Identity.Name);

            if (customer == null)
            {
                return NotFound();
            }

            return View("Profile", customer);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Edit()
        {
            Customer customer = _propertyManager.GetCustomerByName(User.Identity.Name);

            if (customer == null)
            {
                return NotFound();
            }

            EditProfileViewModel model = new EditProfileViewModel
            {
                Username = customer.UserName,
                Email = customer.Email,
                Phone = customer.PhoneNumber,
                FirstName = customer.FirstName,
                LastName = customer.LastName
            };

            return View("Edit", model);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(EditProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                Customer customer = _propertyManager.GetCustomerByName(User.Identity.Name);
                customer.UserName = model.Username;
                customer.Email = model.Email;
                customer.PhoneNumber = model.Phone;
                customer.FirstName = model.FirstName;
                customer.LastName = model.LastName;

                var result = await _userManager.UpdateAsync(customer);

                if (result.Succeeded)
                {
                    await _signInManager.RefreshSignInAsync(customer);
                        return RedirectToAction("Profile", customer);
                }
            }

            return View(model);
        }

        [Authorize]
        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                Customer customer = _propertyManager.GetCustomerByName(User.Identity.Name);
                if (customer == null)
                {
                return NotFound();
                }

                var changePasswordResult = await _userManager.ChangePasswordAsync(customer, model.CurrentPassword, model.Password);
                if (changePasswordResult.Succeeded)
                {
                    await _signInManager.RefreshSignInAsync(customer);
                    return RedirectToAction("Profile", customer);
                }
                else
                {
                    foreach (var error in changePasswordResult.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(model);
        }

        private UserManager<Customer> _userManager;
        private SignInManager<Customer> _signInManager;
        private IPropertyManager _propertyManager;
        private PropertyDBContext _propertyContext;
    }
}
