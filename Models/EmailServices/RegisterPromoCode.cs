using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using BookingPlaceInRestaurant.Models.GuestsModel;

namespace BookingPlaceInRestaurant.Models.EmailServices
{
    public class RegisterPromoCode
    {
        private GuestDBContext context;
        public RegisterPromoCode(GuestDBContext ctx)
        {
            context = ctx;
        }
        public async Task SendEmailAsync(PromoCode promoCode)
        {
            var mm = new MailMessage("email", promoCode.Email);
            mm.Subject = "System PromoCode";
            mm.Body = BodyText(promoCode);
            mm.IsBodyHtml = true;

            context.PromoCodes.Add(promoCode);
            await context.SaveChangesAsync();

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;
            
            NetworkCredential nc = new NetworkCredential("email", "password");
            smtp.Credentials = nc;
            await smtp.SendMailAsync(mm);
        }
        private string BodyText(PromoCode promoCode)
        {
            StringBuilder text = new StringBuilder();
            text.Append("<html>")
               .Append("<head>")
               .Append("<h3>Dear, Sir or Madam "+"</h3>")
               .Append("</head>")
               .Append("<body>");
            text.Append("<div class=\"row\">")
                         .Append("<div>")
                             .Append("<h3>For registered users there is a dicount " + promoCode.Discount + "% on the first visit.</h3>")
                             .Append("<h3>Time is limited up to " + promoCode.EndDate.ToShortDateString() + "</h3>")
                             .Append("<h3>Time is limited. For using promocode show this message to your waiter.</h3>")
                         .Append("</div>");
            text.Append("</div>");
            text.Append("</body>")
               .Append("</html>");
            return text.ToString();
        }
    }
}
