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
        public IActionResult Index()
        {
            return View();
        }
    }
}
