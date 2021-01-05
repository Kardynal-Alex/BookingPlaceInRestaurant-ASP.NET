using BookingPlaceInRestaurant.Models.IdentityModel;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookingPlaceInRestaurant.Models.IdentityModel
{
    public class ApplicationContext:IdentityDbContext<User>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> opt) : base(opt) { }
    }
}
