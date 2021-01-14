using BookingPlaceInRestaurant.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using BookingPlaceInRestaurant.Models.GuestsModel;
using BookingPlaceInRestaurant.Models.PlacesModel;
using BookingPlaceInRestaurant.Models.EmailServices;
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
            if (User.IsInRole("user")) 
            {
                ViewBag.GuestEmail = User.Identity.Name;
            }
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
            if (User.IsInRole("user")) 
            {
                ViewBag.GuestEmail = User.Identity.Name;
            }
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
                var list = guestRepository.GetGuestsByDateAndTable(guest.DateVisit, guest.SelectedTable);
                bool f = false;
                foreach (var item in list)
                {
                    if ((item.Time1 < guest.Time1 && guest.Time2 < item.Time2) ||
                        (!(guest.Time1 <= item.Time1 && guest.Time2 <= item.Time1)) &&
                        (!(item.Time2 <= guest.Time1 && item.Time2 <= guest.Time2)))
                    {
                        f = true;
                    }
                    if (f) break;
                }
                if (f) ModelState.AddModelError("Time1", "Current Time is booked");
                if (ModelState.IsValid)
                {
                    await guestRepository.AddGuest(guest);
                    SendingEmailAfterBooking Email = new SendingEmailAfterBooking();
                    await Email.SendEmailAsync(guest);
                    return RedirectToRoute(new { Controller = "Home", Action = "Index", dateVisit = dateVisit });
                }
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
