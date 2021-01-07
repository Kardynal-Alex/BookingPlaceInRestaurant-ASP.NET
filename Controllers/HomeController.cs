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
        public IActionResult Index(int tableNumber,int numberOfSeats, string dateVisit, bool? isToBook = null, int? placeId = 0)
        {
            ViewBag.DateVisit = dateVisit != null ? Convert.ToDateTime(dateVisit) : DateTime.Now.Date; 
            ViewBag.GetAllPlaces = placeRepository.GetAllPlaces();
            ViewBag.IsToBook = isToBook;
            ViewBag.TableNumber = tableNumber;
            ViewBag.NumberOfSeats = numberOfSeats;
            ViewBag.PlaceId = placeId;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> BookPlace(Guest guest, string dateVisit)
        {
            await guestRepository.AddGuest(guest);
            return RedirectToRoute(new { Controller = "Home", Action = "Index", dateVisit = dateVisit });
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
