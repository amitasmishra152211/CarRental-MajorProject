using CarRental_DataAccess.Repository.IRepoSer;
using CarRental_Model.Domain;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace CarRental.Controllers
{
    public class CarTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CarTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // Optional: List all vehicle types
        public IActionResult Index()
        {
            IEnumerable<VehicleType> vehicleTypes = _unitOfWork.VehicleType.GetAll();
            return View(vehicleTypes);
        }

        // Show all vehicles of a specific type
        public IActionResult Details(int id)
        {
            VehicleType vehicleType = _unitOfWork.VehicleType.GetById(id);
            if (vehicleType == null) return NotFound();

            List<Vehicle> vehicles = _unitOfWork.Vehicles.GetAll()
                .Where(v => v.VehicleTypeId == id)
                .ToList();

            foreach (Vehicle vehicle in vehicles)
            {
                vehicle.Brand = _unitOfWork.Brands.GetById(vehicle.BrandId);
                vehicle.VehicleType = vehicleType;
            }

            ViewBag.VehicleTypeName = vehicleType.TypeName;
            ViewBag.VehicleTypeImg = vehicleType.VehicleTypeImg;
            return View(vehicles);
        }
    }
}
