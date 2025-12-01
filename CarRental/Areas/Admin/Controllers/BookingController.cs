using CarRental_DataAccess.Repository.IRepoSer;
using CarRental_Model.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CarRental.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "ADMIN", AuthenticationSchemes = "AdminScheme")]
    public class BookingController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookingController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Admin/Booking
        public IActionResult Index()
        {
            List<Booking> bookings = _unitOfWork.Booking.GetAll().ToList();

            foreach (Booking booking in bookings)
            {
                booking.Vehicle = _unitOfWork.Vehicles.GetById(booking.VehicleId);
                booking.User = _unitOfWork.Users.GetById(booking.UserId);
            }

            return View(bookings);
        }

        // GET: Admin/Booking/Details/5
        public IActionResult Details(int id)
        {
            Booking booking = _unitOfWork.Booking.GetById(id);

            if (booking == null)
            {
                return NotFound();
            }

            booking.Vehicle = _unitOfWork.Vehicles.GetById(booking.VehicleId);
            booking.User = _unitOfWork.Users.GetById(booking.UserId);

            return View(booking);
        }

        // GET: Admin/Booking/Delete/5
        public IActionResult Delete(int id)
        {
            Booking booking = _unitOfWork.Booking.GetById(id);

            if (booking == null)
            {
                return NotFound();
            }

            booking.Vehicle = _unitOfWork.Vehicles.GetById(booking.VehicleId);
            booking.User = _unitOfWork.Users.GetById(booking.UserId);

            return View(booking);
        }

        // POST: Admin/Booking/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Booking booking = _unitOfWork.Booking.GetById(id);

            if (booking == null)
            {
                return NotFound();
            }

            _unitOfWork.Booking.Remove(booking);
            _unitOfWork.Save();

            TempData["Message"] = "Booking deleted successfully!";
            return RedirectToAction(nameof(Index));
        }
    }
}
