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
        public IActionResult Index(int? tableNumber = 0, int? numberOfSeats = 0, int? placeId = 0, bool? isEdit = null)
        {
            ViewBag.GetAllPlaces = repository.GetAllPlaces();
            ViewBag.TableNumber = tableNumber;
            ViewBag.NumberOfSeats = numberOfSeats;
            ViewBag.PlaceId = placeId;
            ViewBag.IsEdit = isEdit;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreatePlace(Place createPlace)
        {
            await repository.CreatePlace(createPlace);
            return RedirectToAction("Index", "Places");
        }
        [HttpPost]
        public async Task<IActionResult> EditPlace(Place updatePlace)
        {
            await repository.UpdatePlace(updatePlace);
            return RedirectToAction("Index", "Places");
        }
        [HttpPost]
        public async Task<IActionResult> DeletePlace(int placeId)
        {
            await repository.DeletePlace(placeId);
            return RedirectToAction("Index", "Places");
        }
    }
}
