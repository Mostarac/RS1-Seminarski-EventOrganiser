using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using webapp.Models;
using webapp.Services.Interfaces;
using webapp.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace webapp.Controllers
{
    [Authorize(Roles = "User")]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        private readonly ICityService _cityService;
        private readonly UserManager<AppUser> _userManager;

        public UsersController(IUserService userService, ICityService cityService, UserManager<AppUser> userManager)
        {
            _userService = userService;
            _cityService = cityService;
            _userManager = userManager;
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userService.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            ViewData["CityId"] = new SelectList(_cityService.GetAll(), "Id", "Name", user.CityId);
            ViewData["Gender"] = new SelectList(new List<char> { 'M', 'F' }, user.Gender);

            return View(user);
        }

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,FirstName,LastName,Email,EmailConfirmed,PhoneNumber,PasswordHash,Gender,Username,CityId")] UserVm model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.FindByIdAsync(model.Id);

                    user.UserName = model.Username;
                    user.CityId = model.CityId;
                    user.Gender = model.Gender;
                    user.FirstName = model.FirstName;
                    user.LastName = model.LastName;

                    await _userManager.UpdateAsync(user);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(model.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Home");
            }
            ViewData["CityId"] = new SelectList(_cityService.GetAll(), "Id", "Name", model.CityId);
            ViewData["Gender"] = new SelectList(new List<char> { 'M', 'F' }, model.Gender);

            return View(model);
        }

        public bool UserExists(string id)
        {
            return _userManager.Users.Any(x => x.Id == id);
        }

    }
}
