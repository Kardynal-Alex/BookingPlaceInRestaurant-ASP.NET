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

        public IActionResult Index()
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
