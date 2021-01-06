using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingPlaceInRestaurant.Models.PlacesModel;
namespace BookingPlaceInRestaurant.Models.PlacesModel
{
    public interface IPlaceDataRepository
    {
        public IQueryable<Place> GetAllPlaces();
        public Task UpdatePlace(Place updatePlace);
        public Task DeletePlace(int placeId);
        public Task CreatePlace(Place createPlace);
    }
}
