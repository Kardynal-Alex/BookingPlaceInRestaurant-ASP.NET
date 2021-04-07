using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using BookingPlaceInRestaurant.Models.GuestsModel;
namespace BookingPlaceInRestaurant.Models.EmailServices
{
    public class SendingEmailAfterBooking
    {
        public async Task SendEmailAsync(Guest guest)
        {
            var mm = new MailMessage("irakardinal@gmail.com", guest.Email);
            mm.Subject = "Booking Place In Restaurant";
            mm.Body = BodyText(guest);
            mm.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;

            NetworkCredential nc = new NetworkCredential("irakardinal@gmail.com", "sasha60327");
            smtp.Credentials = nc;
            await smtp.SendMailAsync(mm);
        }
        private string BodyText(Guest guest)
        {
            StringBuilder text = new StringBuilder();
            text.Append("<html>")
               .Append("<head>")
               .Append("</head>")
               .Append("<body>");
            text.Append("<div class=\"row\">")
                         .Append("<div>")
                             .Append("<h3>" + guest.Time1 + "  ")
                             .Append(guest.Time2 + "   ")
                             .Append("  " + guest.Surname + "</h3>")
                         .Append("</div>");
            text.Append("</div>");
            text.Append("<div class=\"row\">")
                         .Append("<div>")
                             .Append("<h3>Date Visit " + guest.DateVisit.ToShortDateString() + "</h3>")
                         .Append("</div>");
            text.Append("</div>");
            text.Append("<div class=\"col\">")
                        .Append("<h3>Selected Table " + guest.SelectedTable + "</h3>")
                        .Append("<h3>Numbers of People  " + guest.NumberOfGuests + "</h3>")
                    .Append("</div>");
            text.Append("</body>")
               .Append("</html>");
            return text.ToString();
        }
    }
}
