using CarRental_DataAccess.Repository.IRepoSer;
using CarRental_Model.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Collections.Generic;

namespace CarRental.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "ADMIN", AuthenticationSchemes = "AdminScheme")]
    public class VehicleController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public VehicleController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: List Vehicles of a Brand
        public IActionResult Index(int? brandId)
        {
            IEnumerable<Vehicle> vehicles;

            if (brandId.HasValue && brandId > 0)
            {
                vehicles = _unitOfWork.Vehicles.GetAll(v => v.BrandId == brandId.Value);

                foreach (Vehicle veh in vehicles)
                {
                    veh.Brand = _unitOfWork.Brands.GetById(veh.BrandId);
                    veh.VehicleType = _unitOfWork.VehicleType.GetById(veh.VehicleTypeId);
                }

                ViewBag.BrandId = brandId.Value;
                ViewBag.BrandName = _unitOfWork.Brands.GetById(brandId.Value)?.BrandName;
            }
            else
            {
                vehicles = _unitOfWork.Vehicles.GetAll();

                foreach (Vehicle veh in vehicles)
                {
                    veh.Brand = _unitOfWork.Brands.GetById(veh.BrandId);
                    veh.VehicleType = _unitOfWork.VehicleType.GetById(veh.VehicleTypeId);
                }

                ViewBag.BrandId = null;
                ViewBag.BrandName = "All Vehicles";
            }

            return View(vehicles);
        }


        // GET: Create Vehicle
        [HttpGet]
        public IActionResult Create(int brandId)
        {
            Brand brand = _unitOfWork.Brands.GetById(brandId);
            if (brand == null) return NotFound();

            Vehicle vehicle = new Vehicle
            {
                BrandId = brandId
            };

            ViewBag.Brand = brand;
            ViewBag.VehicleTypes = new SelectList(_unitOfWork.VehicleType.GetAll(), "Id", "TypeName");

            return View(vehicle);
        }


        // POST: Create Vehicle
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Vehicles.Add(vehicle);
                _unitOfWork.Save();
                TempData["success"] = "Vehicle created successfully!";
                return RedirectToAction("Index", new { brandId = vehicle.BrandId });
            }

            ViewBag.Brand = _unitOfWork.Brands.GetById(vehicle.BrandId);
            ViewBag.VehicleTypes = new SelectList(_unitOfWork.VehicleType.GetAll(), "Id", "TypeName", vehicle.VehicleTypeId);

            return View(vehicle);
        }

        // GET: Edit Vehicle
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Vehicle vehicle = _unitOfWork.Vehicles.GetById(id);
            if (vehicle == null) return NotFound();

            Brand brand = _unitOfWork.Brands.GetById(vehicle.BrandId);
            if (brand == null) return NotFound();

            ViewBag.Brand = brand;
            ViewBag.VehicleTypes = new SelectList(_unitOfWork.VehicleType.GetAll(), "Id", "TypeName", vehicle.VehicleTypeId);

            return View(vehicle);
        }

        // POST: Edit Vehicle
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Vehicles.Update(vehicle);
                _unitOfWork.Save();
                TempData["success"] = "Vehicle updated successfully!";
                return RedirectToAction("Index", new { brandId = vehicle.BrandId });
            }

            ViewBag.Brand = _unitOfWork.Brands.GetById(vehicle.BrandId);
            ViewBag.VehicleTypes = new SelectList(_unitOfWork.VehicleType.GetAll(), "Id", "TypeName", vehicle.VehicleTypeId);

            return View(vehicle);
        }


        // GET: Delete Vehicle
        [HttpGet]
        public IActionResult Delete(int id)
        {
            Vehicle vehicle = _unitOfWork.Vehicles.GetById(id);
            if (vehicle == null) return NotFound();

            ViewBag.Brand = _unitOfWork.Brands.GetById(vehicle.BrandId);
            ViewBag.VehicleType = _unitOfWork.VehicleType.GetById(vehicle.VehicleTypeId);

            return View(vehicle);
        }

        // POST: Delete Vehicle
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Vehicle vehicle = _unitOfWork.Vehicles.GetById(id);
            if (vehicle == null) return NotFound();

            int brandId = vehicle.BrandId;
            _unitOfWork.Vehicles.Remove(vehicle);
            _unitOfWork.Save();

            TempData["success"] = "Vehicle deleted successfully!";
            return RedirectToAction("Index", new { brandId });
        }
    }
}
