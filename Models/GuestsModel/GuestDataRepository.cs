using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingPlaceInRestaurant.Models.GuestsModel;
namespace BookingPlaceInRestaurant.Models.GuestsModel
{
    public class GuestDataRepository:IGuestDataRepository
    {
        private GuestDBContext context;
        public GuestDataRepository(GuestDBContext ctx)
        {
            context = ctx;
        }
    }
}
