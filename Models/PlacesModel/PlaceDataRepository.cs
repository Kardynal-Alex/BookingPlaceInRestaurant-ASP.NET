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
        public IQueryable<Place> GetAllPlaces() => context.Places;
        public async Task UpdatePlace(Place updatePlace)
        {
            var UpdatePlace = context.Places.Find(updatePlace.Id);
            UpdatePlace.TableNumber = updatePlace.TableNumber;
            UpdatePlace.NumberOfSeats = updatePlace.NumberOfSeats;
            context.Places.Update(UpdatePlace);
            await context.SaveChangesAsync();
        }
        public async Task DeletePlace(int placeId)
        {
            context.Places.Remove(new Place { Id = placeId });
            await context.SaveChangesAsync();
        }
        public async Task CreatePlace(Place createPlace)
        {
            context.Places.Add(createPlace);
            await context.SaveChangesAsync();
        }
    }
}
