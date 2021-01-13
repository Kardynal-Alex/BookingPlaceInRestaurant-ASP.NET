using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingPlaceInRestaurant.Models.GuestsModel;
namespace BookingPlaceInRestaurant.Controllers
{
    public class GuestsController : Controller
    {
        private IGuestDataRepository repository;
        public GuestsController(IGuestDataRepository repo)
        {
            repository = repo;
        }
        public IActionResult Index(DateTime? dateVisit = null, string surname = null, string phone = null)
        {
            ViewBag.SearchGuests = new Guest
            {
                DateVisit = dateVisit.GetValueOrDefault(),
                Surname = surname,
                Phone = phone
            };
            return View(repository.GetFiltredGuests(dateVisit, surname, phone));
        }
        public IActionResult GuestCabinet()
        {
            return View(repository.GuestBookingInfo(User.Identity.Name));
        }
        [HttpPost]
        public async Task<IActionResult> DeleteGuestInCabinet(int id)
        {
            await repository.DeleteGuest(id);
            return RedirectToAction("GuestCabinet", "Guests");
        }
        public async Task<IActionResult> DeleteGuestsOutOfDate()
        {
            await repository.DeleteGuestsOutOfDate();
            return RedirectToAction("Index", "Guests");
        }
        public async Task<IActionResult> EditGuest(int id)
        {
            var guest = await repository.GetGuestById(id);
            ViewBag.NumberOfSeats = repository.GetNumberOfSeatsByPlaceId(guest.PlaceId);
            return View(guest);
        }
        [HttpPost]
        public async Task<IActionResult> EditGuest(Guest editGuest)
        {
            long num = 0;
            ViewBag.NumberOfSeats = repository.GetNumberOfSeatsByPlaceId(editGuest.PlaceId);
            if (editGuest.DateVisit < DateTime.Now.Date)
            {
                ModelState.AddModelError("DateVisit", "Enter correct Date");
            }
            else
            if (editGuest.Time1 > editGuest.Time2)
            {
                ModelState.AddModelError("Time1", "Time1 must be less than Time2");
            }
            else
            if (long.TryParse(editGuest.Phone, out num) == false)  
            {
                ModelState.AddModelError("Phone", "Phone must contains only digits");
            }
            else
            if (ModelState.IsValid) 
            {
                await repository.EditGuest(editGuest);
                return RedirectToAction("Index", "Guests");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> DeleteGuest(int id)
        {
            await repository.DeleteGuest(id);
            return RedirectToAction("Index", "Guests");
        }
    }
}
