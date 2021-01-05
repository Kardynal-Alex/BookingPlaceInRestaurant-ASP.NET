using Microsoft.EntityFrameworkCore;

namespace BookingPlaceInRestaurant.Models.GuestsModel
{
    public class GuestDBContext:DbContext
    {
        public GuestDBContext(DbContextOptions<GuestDBContext> opt) : base(opt) { }

        public DbSet<Guest> Guests { get; set; }
    }
}
