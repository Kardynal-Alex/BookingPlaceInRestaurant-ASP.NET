using System.ComponentModel.DataAnnotations;

namespace BookingPlaceInRestaurant.Models.IdentityModel
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
