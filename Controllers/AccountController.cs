using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webapp.Models;
using webapp.Services.Interfaces;
using webapp.ViewModels;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace webapp.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ICityService _cityService;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ICityService cityService, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _cityService = cityService;
            _roleManager = roleManager;
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            var model = new RegisterModel();

            ViewData["CityId"] = new SelectList(_cityService.GetAll(), "Id", "Name", model.CityId);
            ViewData["Role"] = new SelectList(_roleManager.Roles, "Name", "Name", model.RoleId);
            ViewData["Gender"] = new SelectList(new List<char> { 'M', 'F' }, model.Gender);

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewData["CityId"] = new SelectList(_cityService.GetAll(), "Id", "Name", model.CityId);
                ViewData["Role"] = new SelectList(_roleManager.Roles, "Name", "Name", model.RoleId);
                ViewData["Gender"] = new SelectList(new List<char> { 'M', 'F' }, model.Gender);

                return View(model);
            }

            AppUser user = new AppUser
            {
                UserName = model.UserName,
                Email = model.Email,
                CityId = model.CityId,
                Gender = model.Gender,
                FirstName = model.FirstName,
                LastName = model.LastName
            };

            IdentityResult result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, model.Role);

                var roles = await _userManager.GetRolesAsync(user);

                if (roles.FirstOrDefault().Equals("Organizer"))
                {
                    return RedirectToAction("Index", "Event", new { Area = "Organizer" });
                }
                return RedirectToAction("Index", "Home");
            }

            foreach (var err in result.Errors)
            {
                ModelState.AddModelError("", err.Description);
            }

            ViewData["CityId"] = new SelectList(_cityService.GetAll(), "Id", "Name", model.CityId);
            ViewData["Role"] = new SelectList(_roleManager.Roles, "Name", "Name", model.RoleId);
            ViewData["Gender"] = new SelectList(new List<char> { 'M', 'F' }, model.Gender);

            return View(model);
        }

        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    await _signInManager.SignOutAsync();
                    SignInResult result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);

                    if (result.Succeeded)
                    {
                        var roles = await _userManager.GetRolesAsync(user);
                        if (roles.FirstOrDefault().Equals("Organizer"))
                        {
                            return RedirectToAction("Index", "Event", new { Area = "Organizer" });
                        }
                        return RedirectToAction("Index", "Home");
                    }
                }

                ModelState.AddModelError(nameof(LoginModel.Email), "Invalid user or password");
            }

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        public ActionResult Error() => View("Unauthorized");
    }
}