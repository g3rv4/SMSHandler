using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace SMSHandler.Controllers
{
    public class HomeController : Controller
    {
        private IConfiguration Configuration;
        public HomeController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        [Route("new-sms")]
        public async Task<IActionResult> NewSms(string from, string phonenumber, string message)
        {
            var apiKey = Configuration.GetValue<string>("SENDGRID_API_KEY");
            var emailFromAddress = Configuration.GetValue<string>("EMAIL_FROM_ADDRESS");
            var emailToAddress = Configuration.GetValue<string>("EMAIL_TO_ADDRESS");
            var emailToName = Configuration.GetValue<string>("EMAIL_TO_NAME");

            var client = new SendGridClient(apiKey);
            var emaiFrom = new EmailAddress(emailFromAddress, "New SMS");
            var to = new EmailAddress(emailToAddress, emailToName);

            var subject = "New SMS received";
            var plainTextContent = $@"From: {from}
To: {phonenumber}
Message: {message}";

            var htmlContent = $@"<strong>From: </strong>{from}<br />
<strong>To: </strong>{phonenumber}<br />
<strong>Message: </strong>{message}<br />";

            var msg = MailHelper.CreateSingleEmail(emaiFrom, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
            return Content("Yes!");
        }
    }
}
