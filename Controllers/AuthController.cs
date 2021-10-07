using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using webapp.Models;
using webapp.ViewModels;

namespace webapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [HttpPost, Route("login")]
        public async Task<IActionResult> Login([FromBody] ApiLoginModel model)
        {

            if (model == null)
            {
                return BadRequest("Invalid client request");
            }

            AppUser user = await _userManager.FindByEmailAsync(model.Email);


            if (user == null || ! await _userManager.CheckPasswordAsync(user, model.Password))
                return Unauthorized();

            //if (user.UserName == "johndoe" && model.Password == "def@123")
            //{
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@3456"));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, roles.FirstOrDefault())
                };

            var tokeOptions = new JwtSecurityToken(
                issuer: "https://p1801.app.fit.ba",
                audience: "https://angular.p1801.app.fit.ba",
                claims: claims,
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: signinCredentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
            return Ok(new { Token = tokenString });
            //}
            //else
            //{
            //return Unauthorized();
            //}
        }
    }
}
