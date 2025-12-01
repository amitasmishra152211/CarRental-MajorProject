using CarRental_DataAccess.Repository.IRepoSer;
using CarRental_Model.Domain;
using Microsoft.AspNetCore.Authorization;
using CarRental_DataAccess.Repository.IRepoSer;
using CarRental_Model.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace CarRental.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "ADMIN", AuthenticationSchemes = "AdminScheme")]
    public class VehicleTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public VehicleTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: List Vehicle Types
        public IActionResult Index()
        {
            IEnumerable<VehicleType> vehicleTypes = _unitOfWork.VehicleType.GetAll();
            return View(vehicleTypes);
        }

        // GET: Create Vehicle Type
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Create Vehicle Type
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(VehicleType vehicleType)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.VehicleType.Add(vehicleType);
                _unitOfWork.Save();
                TempData["success"] = "Vehicle type added successfully!";
                return RedirectToAction("Index");
            }
            return View(vehicleType);
        }

        // GET: Edit Vehicle Type
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var vehicleType = _unitOfWork.VehicleType.GetById(id);
            if (vehicleType == null) return NotFound();

            return View(vehicleType);
        }

        // POST: Edit Vehicle Type
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(VehicleType vehicleType)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.VehicleType.Update(vehicleType);
                _unitOfWork.Save();
                TempData["success"] = "Vehicle type updated successfully!";
                return RedirectToAction("Index");
            }
            return View(vehicleType);
        }

        // GET: Delete Vehicle Type
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var vehicleType = _unitOfWork.VehicleType.GetById(id);
            if (vehicleType == null) return NotFound();

            return View(vehicleType);
        }

        // POST: Delete Vehicle Type
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var vehicleType = _unitOfWork.VehicleType.GetById(id);
            if (vehicleType == null) return NotFound();

            _unitOfWork.VehicleType.Remove(vehicleType);
            _unitOfWork.Save();
            TempData["success"] = "Vehicle type deleted successfully!";
            return RedirectToAction("Index");
        }
    }
}
