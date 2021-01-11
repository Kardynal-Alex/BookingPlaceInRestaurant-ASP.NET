using System;
using System.ComponentModel.DataAnnotations;

namespace BookingPlaceInRestaurant.Models.GuestsModel
{
    public class Guest
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Enter surname")]
        [Display(Name = "Surname")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Select Date Visit")]
        public DateTime DateVisit { get; set; }
        [Required(ErrorMessage = "Enter date of arrival")]
        [Display(Name = "Date of arrival")]
        public TimeSpan Time1 { get; set; }
        [Required(ErrorMessage = "Enter time of dispatch")]
        [Display(Name = "Time of dispatch")]
        public TimeSpan Time2 { get; set; }
        public int NumberOfGuests { get; set; }
        [Required(ErrorMessage = "Enter Email")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Incorect email")]
        public string Email { get; set; }
        [MaxLength(10)]
        [Required(ErrorMessage = "Enter Yhour Phone")]
        public string Phone { get; set; }
        public int SelectedTable { get; set; }
        public int PlaceId { get; set; }
    }
}
