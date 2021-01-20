using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingPlaceInRestaurant.Models.GuestsModel
{
    public interface IGuestDataRepository
    {
        public Task AddGuest(Guest addGuest);
        public IQueryable<Guest> GetAllGuests();
        public Task DeleteGuestsOutOfDate();
        public Task<Guest> GetGuestById(int id);
        public Task EditGuest(Guest editGuest);
        public Task DeleteGuest(int id);
        public IQueryable<Guest> GetFiltredGuests(DateTime? dateVisit, string surname = null, int table = 0);
        public int GetNumberOfSeatsByPlaceId(int placeId);
        public List<Guest> GetGuestsByDate(DateTime date);
        public List<Guest> GetGuestsByDateAndTable(DateTime date, int tableNumber);
        public IQueryable<Guest> GuestBookingInfo(string Email);
        public IQueryable<PromoCode> GetAllPromoCodes();
        public Task DeleteOldPromoCodes();
    }
}
