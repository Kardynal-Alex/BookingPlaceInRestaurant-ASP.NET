using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingPlaceInRestaurant.Models.PlacesModel;
namespace BookingPlaceInRestaurant.Models.PlacesModel
{
    public class PlaceDataRepository:IPlaceDataRepository
    {
        private PlaceDBContext context;
        public PlaceDataRepository(PlaceDBContext ctx)
        {
            context = ctx;
        }
    }
}
