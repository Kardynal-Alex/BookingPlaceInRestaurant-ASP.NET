using BookingPlaceInRestaurant.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BookingPlaceInRestaurant.Models.GuestsModel;
using BookingPlaceInRestaurant.Models.PlacesModel;
namespace BookingPlaceInRestaurant.Controllers
{
    public class HomeController : Controller
    {
        private IGuestDataRepository guestRepository;
        private IPlaceDataRepository placeRepository;
        public HomeController(IGuestDataRepository guestRepo, IPlaceDataRepository placeRepo)
        {
            guestRepository = guestRepo;
            placeRepository = placeRepo;
        }
        public IActionResult Index(Place place, string dateVisit, bool? isToBook = null)
        {
            var DateVisit = dateVisit != null ? Convert.ToDateTime(dateVisit) : DateTime.Now.Date;
            ViewBag.DateVisit = DateVisit; 
            ViewBag.GetAllPlaces = placeRepository.GetAllPlaces();
            ViewBag.IsToBook = isToBook;
            ViewBag.Place = new Place
            {
                Id = place.Id,
                TableNumber = place.TableNumber,
                NumberOfSeats = place.NumberOfSeats
            };
            ViewBag.GuestsList = guestRepository.GetGuestsByDate(DateVisit);
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> BookPlace(Guest guest, string dateVisit, Place place, int placeId)
        {
            long num = 0;
            ViewBag.DateVisit = Convert.ToDateTime(dateVisit);
            ViewBag.Place = new Place
            {
                Id = placeId,
                NumberOfSeats = place.NumberOfSeats,
                TableNumber = place.TableNumber
            };
            if (guest.DateVisit < DateTime.Now.Date)
            {
                ModelState.AddModelError("DateVisit", "Enter correct Date");
            }
            else
            if (guest.Time1 > guest.Time2)
            {
                ModelState.AddModelError("Time1", "Time1 must be less than Time2");
            }
            else
            if (long.TryParse(guest.Phone, out num) == false)
            {
                ModelState.AddModelError("Phone", "Phone must contains only digits");
            }
            else
            if (ModelState.IsValid)
            {
                await guestRepository.AddGuest(guest);
                return RedirectToRoute(new { Controller = "Home", Action = "Index", dateVisit = dateVisit });
            }
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
