using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingPlaceInRestaurant.Models.PlacesModel;
namespace BookingPlaceInRestaurant.Controllers
{
    public class PlacesController : Controller
    {
        private IPlaceDataRepository repository;
        public PlacesController(IPlaceDataRepository repo)
        {
            repository = repo;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
