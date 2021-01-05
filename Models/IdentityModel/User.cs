using Microsoft.AspNetCore.Identity;

namespace BookingPlaceInRestaurant.Models.IdentityModel
{
    public class User:IdentityUser
    {
        public int Year { get; set; }
    }
}
