using System.Net.Mail;
using System.Net;

namespace Demo.Presentation.Utilities
{
    public class EmailSettings
    {
        public static void SendEmail(Email email)
        {
            var Client = new SmtpClient("smtp.gmail.com", 587);
            Client.EnableSsl = true;
            Client.Credentials = new NetworkCredential("ahmedosama12467@gmail.com", "krqfcpdaddvjfgxm");
            Client.Send("ahmedosama12467@gmail.com", email.To, email.Subject, email.Body);

        }
    }
}
 