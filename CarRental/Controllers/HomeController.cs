using System.Diagnostics;
using CarRental.Models;
using CarRental_DataAccess.Repository.IRepoSer;
using CarRental_Model.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace CarRental.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Brand> brands = _unitOfWork.Brands.GetAll();
            IEnumerable<VehicleType> vehicleTypes = _unitOfWork.VehicleType.GetAll();

            ViewBag.Brands = brands;
            ViewBag.VehicleTypes = vehicleTypes;

            return View();
        }


        [HttpGet]
        public IActionResult SearchResults(string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                return RedirectToAction("Index");
            }

            // Get all vehicles
            IEnumerable<Vehicle> vehicles = _unitOfWork.Vehicles.GetAll();

            // Manually populate Brand and VehicleType for each vehicle
            foreach (Vehicle vehicle in vehicles)
            {
                vehicle.Brand = _unitOfWork.Brands.GetById(vehicle.BrandId);
                vehicle.VehicleType = _unitOfWork.VehicleType.GetById(vehicle.VehicleTypeId);
            }

            // Filter vehicles by search query
            IEnumerable<Vehicle> filteredVehicles = vehicles
                .Where(v => v.VehicleName.ToLower().Contains(query.ToLower())
                         || v.Brand.BrandName.ToLower().Contains(query.ToLower())
                         || v.VehicleType.TypeName.ToLower().Contains(query.ToLower()));
            ViewBag.Query = query;

            return View(filteredVehicles);
        }

        public IActionResult AboutUs()
        {
            return View();
        }

        public IActionResult Services()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
