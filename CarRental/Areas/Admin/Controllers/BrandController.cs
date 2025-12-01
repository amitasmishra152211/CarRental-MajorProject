using CarRental_DataAccess.Repository.IRepoSer;
using CarRental_Model.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "ADMIN", AuthenticationSchemes = "AdminScheme")]
    public class BrandController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public BrandController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var brands = _unitOfWork.Brands.GetAll();
            return View(brands);
        }

        [HttpGet]
        public IActionResult Create()
        {
            Brand brand = new Brand();
            return View(brand);
        }

        [HttpPost]
        public IActionResult Create(Brand brand)
        {
            if (!ModelState.IsValid)
            {
                TempData["warning"] = "Validation failed!";
                return View("Create", brand);
            }

            try
            {
                _unitOfWork.Brands.Add(brand);
                _unitOfWork.Save();
                TempData["success"] = "Brand Saved Successfully";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["error"] = "Error: " + ex.Message;
                return View("Create", brand);
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var brand = _unitOfWork.Brands.GetById(id);
            if (brand == null) return NotFound();
            return View(brand);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Brand brand)
        {
            if (!ModelState.IsValid) return View(brand);

            _unitOfWork.Brands.Update(brand);
            _unitOfWork.Save();
            TempData["success"] = "Brand updated successfully!";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var brand = _unitOfWork.Brands.GetById(id);
            if (brand == null) return NotFound();
            return View(brand);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var brand = _unitOfWork.Brands.GetById(id);
            if (brand == null) return NotFound();

            _unitOfWork.Brands.Remove(brand);
            _unitOfWork.Save();
            TempData["success"] = "Brand deleted successfully!";
            return RedirectToAction("Index");
        }
    }
}
