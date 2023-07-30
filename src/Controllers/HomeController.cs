using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace SMSHandler.Controllers
{
    public class HomeController : Controller
    {
        [Route("new-sms")]
        public async Task<IActionResult> NewSms(string from, string to, string message)
        {
            var apiKey = Config.SendgridApiKey;
            var emailFromAddress = Config.EmailFrom;
            var emailToAddress = Config.EmailTo;
            var emailToName = Config.EmailToName;

            var client = new SendGridClient(apiKey);
            var emaiFrom = new EmailAddress(emailFromAddress, "New SMS");
            var toEmail = new EmailAddress(emailToAddress, emailToName);

            var subject = "New SMS received";
            var plainTextContent = $@"From: {from}
To: {to}
Message: {message}";

            var htmlContent = $@"<strong>From: </strong>{from}<br />
<strong>To: </strong>{to}<br />
<strong>Message: </strong>{message}<br />";

            var msg = MailHelper.CreateSingleEmail(emaiFrom, toEmail, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
            return Content("Yes!");
        }
    }
}
