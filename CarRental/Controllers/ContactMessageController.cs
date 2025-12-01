using CarRental_DataAccess.Repository.IRepoSer;
using CarRental_Model.Domain;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Controllers
{
    public class ContactMessageController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ContactMessageController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpPost]
        public IActionResult Submit(ContactMessage contactMessage)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.ContactMessage.Add(contactMessage);
                _unitOfWork.Save();

                TempData["Success"] = "Message sent successfully!";
                return RedirectToAction("AboutUs", "Home");
            }

            TempData["Error"] = "Please fill all required fields!";
            return RedirectToAction("AboutUs", "Home");
        }
    }
}
