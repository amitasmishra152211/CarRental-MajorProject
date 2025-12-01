using CarRental_DataAccess.Repository.IRepoSer;
using CarRental_Model.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "ADMIN", AuthenticationSchemes = "AdminScheme")]
    public class UserController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: List all users
        public IActionResult Index()
        {
            var users = _unitOfWork.Users.GetAll().AsQueryable().Include(u => u.Role).ToList();
            return View(users);
        }

        // GET: Edit User
        public IActionResult Edit(int id)
        {
            var user = _unitOfWork.Users.GetFirstOrDefault(u => u.Id == id);
            if (user == null) return NotFound();

            ViewBag.Roles = _unitOfWork.Roles.GetAll();
            return View(user);
        }

        // GET: Delete confirmation
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var user = _unitOfWork.Users.GetFirstOrDefault(u => u.Id == id);
            if (user == null) return NotFound();
            return View(user);
        }

        // POST: Delete User
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var user = _unitOfWork.Users.GetFirstOrDefault(u => u.Id == id);
            if (user == null) return NotFound();

            _unitOfWork.Users.Remove(user);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }
    }
}
