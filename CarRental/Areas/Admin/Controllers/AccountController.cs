using CarRental_DataAccess.Repository.IRepoSer;
using CarRental_Model.Domain;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CarRental.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public AccountController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated && User.IsInRole("ADMIN"))
                return RedirectToAction("Index", "Home", new { area = "Admin" });
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            User user = _unitOfWork.Users.GetByUsernameWithRole(username);

            if (user != null && user.Password == password &&
                user.Role.RoleName.Equals("ADMIN", System.StringComparison.OrdinalIgnoreCase))
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, "ADMIN")
                };

                var identity = new ClaimsIdentity(claims, "AdminScheme");
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync("AdminScheme", principal);
                return RedirectToAction("Index", "Home", new { area = "Admin" });
            }

            ViewBag.Error = "Invalid username or password";
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("AdminScheme");
            return RedirectToAction("Login");
        }
    }
}
