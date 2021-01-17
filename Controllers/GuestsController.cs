using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using BookingPlaceInRestaurant.Models.GuestsModel;
using Microsoft.AspNetCore.Identity;
using BookingPlaceInRestaurant.Models.IdentityModel;
namespace BookingPlaceInRestaurant.Controllers
{
    public class GuestsController : Controller
    {
        private IGuestDataRepository repository;
        private UserManager<User> userManager;
        private RoleManager<IdentityRole> roleManager;
        public GuestsController(IGuestDataRepository repo, UserManager<User> _userManager, RoleManager<IdentityRole> _roleManager)
        {
            repository = repo;
            userManager = _userManager;
            roleManager = _roleManager;
        }
        public IActionResult Index(DateTime? dateVisit = null, string surname = null, int table = 0)
        {
            ViewBag.SearchGuests = new Guest
            {
                DateVisit = dateVisit.GetValueOrDefault(),
                Surname = surname,
                SelectedTable = table
            };
            return View(repository.GetFiltredGuests(dateVisit, surname, table));
        }
        public async Task<IActionResult> ShowAllRegisteredUsers()
        {
            return View(userManager.Users);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteUser(string id)
        {
            User user = await userManager.FindByIdAsync(id);
            if(user!=null)
            {
                var result = await userManager.DeleteAsync(user);
                return RedirectToAction("ShowAllRegisteredUsers", "Guests");
            }
            return View();
        }
        public IActionResult ShowAllPromoCode()
        {
            return View(repository.GetAllPromoCodes());
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
