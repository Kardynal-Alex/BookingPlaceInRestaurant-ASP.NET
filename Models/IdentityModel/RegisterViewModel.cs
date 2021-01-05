using System.ComponentModel.DataAnnotations;

namespace BookingPlaceInRestaurant.Models.IdentityModel
{
    public class RegisterViewModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public int Year { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [Compare("Password",ErrorMessage = "Password do not match")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }
    }
}
