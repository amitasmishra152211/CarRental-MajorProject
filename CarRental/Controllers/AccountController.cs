using CarRental_DataAccess.Repository.IRepoSer;
using CarRental_Model.Domain;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CarRental.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public AccountController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public IActionResult Register(string username, string firstName, string middleName, string lastName, string email, string password, string confirmPassword)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(firstName) ||
                string.IsNullOrWhiteSpace(lastName) || string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(confirmPassword))
            {
                ViewBag.Error = "All required fields must be filled.";
                return View();
            }

            if (password != confirmPassword)
            {
                ViewBag.Error = "Password and Confirm Password do not match.";
                return View();
            }

            if (!new EmailAddressAttribute().IsValid(email))
            {
                ViewBag.Error = "This is not a valid email. Please enter a valid email.";
                return View();
            }

            if (_unitOfWork.Users.GetFirstOrDefault(u => u.Email == email) != null)
            {
                ViewBag.Error = "This email is already taken.";
                return View();
            }

            if (_unitOfWork.Users.GetFirstOrDefault(u => u.Username == username) != null)
            {
                ViewBag.Error = "Username already exists.";
                return View();
            }

            var role = _unitOfWork.Roles.GetFirstOrDefault(r => r.RoleName == "USER");
            if (role == null)
            {
                role = new Role { RoleName = "USER" };
                _unitOfWork.Roles.Add(role);
                _unitOfWork.Save();
            }

            var newUser = new User
            {
                Username = username,
                FirstName = firstName,
                MiddleName = middleName,
                LastName = lastName,
                Email = email,
                Password = password,
                RoleId = role.Id
            };

            _unitOfWork.Users.Add(newUser);
            _unitOfWork.Save();

            TempData["Message"] = "Registration successful! Please login.";
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            User user = _unitOfWork.Users.GetByUsernameWithRole(username);
            if (user != null && user.Password == password)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.Role.RoleName)
                };
                var identity = new ClaimsIdentity(claims, "UserScheme");
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync("UserScheme", principal);
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "Invalid username or password.";
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("UserScheme");
            return RedirectToAction("Login");
        }
    }
}
