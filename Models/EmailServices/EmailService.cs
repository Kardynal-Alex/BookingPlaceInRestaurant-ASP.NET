using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace BookingPlaceInRestaurant.Models.EmailServices
{
    public class EmailService
    {
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var mm = new MailMessage("email", email);
            mm.Subject = subject;
            mm.Body = message;
            mm.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;

            NetworkCredential nc = new NetworkCredential("email", "password");
            smtp.Credentials = nc;
            await smtp.SendMailAsync(mm);
        }
    }
}
