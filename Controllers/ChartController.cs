using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using BookingPlaceInRestaurant.Models.GuestsModel;
using BookingPlaceInRestaurant.Models.Chart;
using Newtonsoft.Json;

namespace BookingPlaceInRestaurant.Controllers
{
    public class ChartController : Controller
    {
        private GuestDBContext context;
        public ChartController(GuestDBContext ctx)
        {
            context = ctx;
        }
        public IActionResult Index()
        {
            var List = context.Guests.Where(x => x.DateVisit.Date.Month == DateTime.Now.Date.Month).ToList();
            var filtredList = List.Select(x => x).OrderBy(x => x.DateVisit).GroupBy(x => x.DateVisit);
            List<DataPoint> dataPoints = new List<DataPoint>();
            foreach (var item in filtredList)
            {
                int k = item.Count();
                dataPoints.Add(new DataPoint(item.Key.ToShortDateString(), k));
            }
            ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);
            return View();
        }
    }
}
