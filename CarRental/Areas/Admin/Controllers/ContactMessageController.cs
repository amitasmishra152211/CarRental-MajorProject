using CarRental_DataAccess.Repository.IRepoSer;
using CarRental_Model.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace CarRental.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "ADMIN", AuthenticationSchemes = "AdminScheme")]
    public class ContactMessageController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ContactMessageController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Admin/ContactMessage
        public IActionResult Index()
        {
            List<ContactMessage> messages = _unitOfWork.ContactMessage
                .GetAll()
                .ToList();

            // सबसे recent messages पहले दिखेंगे
            messages.Reverse();

            return View(messages);
        }

        // DELETE
        [HttpPost]
        public IActionResult Delete(int id)
        {
            ContactMessage message = _unitOfWork.ContactMessage.GetFirstOrDefault(m => m.Id == id);
            if (message == null)
            {
                TempData["error"] = "Message not found!";
                return RedirectToAction("Index");
            }

            _unitOfWork.ContactMessage.Remove(message);
            _unitOfWork.Save();

            TempData["success"] = "Message deleted successfully!";
            return RedirectToAction("Index");
        }
    }
}
