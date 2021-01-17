using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingPlaceInRestaurant.Models.PlacesModel;
namespace BookingPlaceInRestaurant.Models.GuestsModel
{
    public class GuestDataRepository : IGuestDataRepository
    {
        private GuestDBContext context;
        private PlaceDBContext placeContext;
        public GuestDataRepository(GuestDBContext ctx, PlaceDBContext ctx1)
        {
            context = ctx;
            placeContext = ctx1;
        }
        public async Task AddGuest(Guest addGuest)
        {
            context.Guests.Add(addGuest);
            await context.SaveChangesAsync();
        }
        public IQueryable<Guest> GetAllGuests() => context.Guests;
        public async Task DeleteGuestsOutOfDate()
        {
            var selectedList = context.Guests.Where(x => x.DateVisit < DateTime.Now.Date).ToList();
            context.Guests.RemoveRange(selectedList);
            await context.SaveChangesAsync();
        }
        public async Task<Guest> GetGuestById(int id) => await context.Guests.FindAsync(id);
        public async Task EditGuest(Guest editGuest)
        {
            var guest = context.Guests.Find(editGuest.Id);
            guest.Surname = editGuest.Surname;
            guest.DateVisit = editGuest.DateVisit;
            guest.Time1 = editGuest.Time1;
            guest.Time2 = editGuest.Time2;
            guest.NumberOfGuests = editGuest.NumberOfGuests;
            guest.Email = editGuest.Email;
            guest.Phone = editGuest.Phone;
            guest.SelectedTable = editGuest.SelectedTable;
            guest.PlaceId = editGuest.PlaceId;
            context.Guests.Update(guest);
            await context.SaveChangesAsync();
        }
        public async Task DeleteGuest(int id)
        {
            context.Guests.Remove(new Guest { Id = id });
            await context.SaveChangesAsync();
        }
        public IQueryable<Guest> GetFiltredGuests(DateTime? dateVisit = null, string surname = null, int table = 0)
        {
            IQueryable<Guest> FiltredGuests = context.Guests; ;
            if (dateVisit.HasValue)
            {
                FiltredGuests = FiltredGuests.Where(x => x.DateVisit == dateVisit);
            }
            if (surname != null)
            {
                FiltredGuests = FiltredGuests.Where(x => x.Surname == surname);
            }
            if (table > 0) 
            {
                FiltredGuests = FiltredGuests.Where(x => x.SelectedTable == table);
            }
            return FiltredGuests;
        }
        public int GetNumberOfSeatsByPlaceId(int placeId) => placeContext.Places.Find(placeId).NumberOfSeats;
        public List<Guest> GetGuestsByDate(DateTime date) =>
            context.Guests.Where(x => x.DateVisit == date.Date &&
            (x.Time1 >= DateTime.Now.TimeOfDay ||
            (x.Time1 <= DateTime.Now.TimeOfDay && DateTime.Now.TimeOfDay <= x.Time2)))
            .OrderBy(x => x.Time1).ToList();
        public List<Guest> GetGuestsByDateAndTable(DateTime date, int tableNumber) =>
            context.Guests.Where(x => x.DateVisit == date.Date && x.PlaceId == tableNumber &&
            (x.Time1 >= DateTime.Now.TimeOfDay ||
            (x.Time1 <= DateTime.Now.TimeOfDay && DateTime.Now.TimeOfDay <= x.Time2)))
            .OrderBy(x => x.Time1).ToList();
        public IQueryable<Guest> GuestBookingInfo(string Email)
        {
            return context.Guests.Where(x => x.Email == Email);
        }
        public IQueryable<PromoCode> GetAllPromoCodes() => context.PromoCodes;
    }
}
