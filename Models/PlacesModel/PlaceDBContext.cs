using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingPlaceInRestaurant.Models.PlacesModel;
using Microsoft.EntityFrameworkCore;
namespace BookingPlaceInRestaurant.Models.PlacesModel
{
    public class PlaceDBContext:DbContext
    {
        public PlaceDBContext(DbContextOptions<PlaceDBContext> opt) : base(opt) { }

        public DbSet<Place> Places { get; set; }
    }
}
