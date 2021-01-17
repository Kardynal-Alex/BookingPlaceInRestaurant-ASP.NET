using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingPlaceInRestaurant.Models
{
    public class PromoCode
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public int Discount { get; set; }
        public DateTime EndDate { get; set; }
    }
}
