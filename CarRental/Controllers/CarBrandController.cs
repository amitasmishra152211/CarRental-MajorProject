using CarRental_DataAccess.Repository.IRepoSer;
using Microsoft.EntityFrameworkCore;
using CarRental_Model.Domain;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace CarRental.Controllers
{
    public class CarBrandController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CarBrandController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // Brand list page (all brands)
        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<Brand> brands = _unitOfWork.Brands.GetAll();
            return View(brands);
        }

        // Brand Details page (all vehicles under a brand)
        [HttpGet]
        public IActionResult Details(int id)
        {
            Brand brand = _unitOfWork.Brands.GetById(id);
            if (brand == null) return NotFound();

            List<Vehicle> vehicles = _unitOfWork.Vehicles.GetAll()
                .Where(v => v.BrandId == id)
                .ToList();

            foreach (Vehicle vehicle in vehicles)
            {
                vehicle.VehicleType = _unitOfWork.VehicleType.GetById(vehicle.VehicleTypeId);
            }

            ViewBag.BrandImage = brand.BrandImage;
            ViewBag.BrandName = brand.BrandName;
            ViewBag.Description = brand.Description;

            return View(vehicles);
        }
    }
}
