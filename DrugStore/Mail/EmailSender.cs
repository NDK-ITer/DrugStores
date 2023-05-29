using System.Net.Mail;
using System.Net;


namespace DrugStore.Mail
{
    public class EmailSender:IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("hkcdrugstore@gmail.com", "tqtehjhpdyvlzdto"),
                
            };
           
            return client.SendMailAsync(
                new MailMessage(from: "hkcdrugstore@gmail.com",
                                to: email,
                                subject,
                                message
                                )
                {
                    IsBodyHtml=true
                });
        }

      
    }
}
