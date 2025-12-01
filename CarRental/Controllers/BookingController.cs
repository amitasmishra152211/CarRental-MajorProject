using CarRental_DataAccess.Repository.IRepoSer;
using CarRental_Model.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarRental.Controllers
{
    [Authorize(AuthenticationSchemes = "UserScheme", Policy = "UserPolicy")]
    public class BookingController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookingController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Checkout Page (Vehicle Details + Availability)
        [HttpGet]
        public IActionResult Checkout(int vehicleId)
        {
            Vehicle vehicle = _unitOfWork.Vehicles.GetById(vehicleId);
            if (vehicle == null)
                return NotFound();

            // Populate related entities
            vehicle.Brand = _unitOfWork.Brands.GetById(vehicle.BrandId);
            vehicle.VehicleType = _unitOfWork.VehicleType.GetById(vehicle.VehicleTypeId);

            // Check how many bookings exist for this vehicle today or in future
            int bookedCount = _unitOfWork.Booking
                .GetAll(b => b.VehicleId == vehicleId && b.EndDate >= DateTime.Today)
                .Count();

            // Assuming 1 vehicle per type
            vehicle.AvailableQuantity = 1 - bookedCount;
            if (vehicle.AvailableQuantity < 0)
                vehicle.AvailableQuantity = 0;

            return View(vehicle);
        }

        // POST: Add Booking (from Checkout form)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddBooking(int vehicleId, string FullName, string MobileNumber, string Email,
                                        string Locality, string Area, string LandMark, string City,
                                        string Pincode, string State)
        {
            Vehicle vehicle = _unitOfWork.Vehicles.GetById(vehicleId);
            if (vehicle == null)
                return NotFound();

            int bookedCount = _unitOfWork.Booking
                .GetAll(b => b.VehicleId == vehicleId && b.EndDate >= DateTime.Today)
                .Count();

            if (bookedCount >= 1)
            {
                TempData["Error"] = "Car is not available currently. Please try later.";
                return RedirectToAction("Checkout", new { vehicleId = vehicleId });
            }

            int userId = _unitOfWork.Users.GetFirstOrDefault(u => u.Username == User.Identity.Name).Id;

            Booking booking = new Booking
            {
                VehicleId = vehicleId,
                UserId = userId,
                FullName = FullName,
                MobileNumber = MobileNumber,
                Email = Email,
                Locality = Locality,
                Area = Area,
                LandMark = LandMark,
                City = City,
                Pincode = Pincode,
                State = State,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(1), // 1-day booking for simplicity
                Status = "Confirmed"
            };

            _unitOfWork.Booking.Add(booking);
            _unitOfWork.Save();

            TempData["Message"] = "Booking confirmed! Car will arrive in 1 hour.";
            return RedirectToAction("Index", "Home");
        }

        // GET: User's Booking List (My Bookings)
        [HttpGet]
        public IActionResult Index()
        {
            int userId = _unitOfWork.Users.GetFirstOrDefault(u => u.Username == User.Identity.Name).Id;
            List<Booking> bookings = _unitOfWork.Booking.GetAll(b => b.UserId == userId).ToList();

            foreach (Booking booking in bookings)
            {
                booking.Vehicle = _unitOfWork.Vehicles.GetById(booking.VehicleId);
            }

            return View(bookings);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult PlaceOrder(int bookingId, string paymentMethod)
        {
            Booking booking = _unitOfWork.Booking.GetById(bookingId);
            if (booking == null)
            {
                return NotFound();
            }

            booking.Status = "Order Placed (" + paymentMethod + ")";
            _unitOfWork.Booking.Update(booking);
            _unitOfWork.Save();
            TempData["Message"] = "Order placed successfully!";
            return RedirectToAction("OrderReceived", new { bookingId = booking.Id });
        }

        // GET: Order Received
        [HttpGet]
        public IActionResult OrderReceived(int bookingId)
        {
            Booking booking = _unitOfWork.Booking.GetById(bookingId);
            if (booking == null)
            {
                return NotFound();
            }

            booking.Vehicle = _unitOfWork.Vehicles.GetById(booking.VehicleId);
            return View(booking);
        }

        // POST: Remove Pending Booking
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveBooking(int bookingId)
        {
            Booking booking = _unitOfWork.Booking.GetById(bookingId);
            if (booking == null)
            {
                return NotFound();
            }

            _unitOfWork.Booking.Remove(booking);
            _unitOfWork.Save();

            TempData["Message"] = "Booking removed successfully!";
            return RedirectToAction("Index");
        }

        // POST: Cancel Placed Order
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CancelOrder(int bookingId)
        {
            Booking booking = _unitOfWork.Booking.GetById(bookingId);
            if (booking == null)
            {
                return NotFound();
            }

            booking.Status = "Cancelled";
            _unitOfWork.Booking.Update(booking);
            _unitOfWork.Save();

            TempData["Message"] = "Order cancelled successfully!";
            return RedirectToAction("Index");
        }

        // GET: Invoice (show invoice page in browser)
        [HttpGet]
        public IActionResult GenerateInvoice(int bookingId)
        {
            Booking booking = _unitOfWork.Booking.GetById(bookingId);
            if (booking == null)
            {
                return NotFound();
            }

            booking.Vehicle = _unitOfWork.Vehicles.GetById(booking.VehicleId);

            return View(booking);
        }
    }
}