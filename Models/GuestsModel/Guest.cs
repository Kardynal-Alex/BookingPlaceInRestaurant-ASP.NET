using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingPlaceInRestaurant.Models.GuestsModel
{
    public class Guest
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public DateTime DateVisit { get; set; }
        public TimeSpan Time1 { get; set; }
        public TimeSpan Time2 { get; set; }
        public int NumberOfGuests { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public int PlaceId { get; set; }
    }
}
