using CoffeeManagementAPI.Interface;
using System.Net;
using System.Net.Mail;

namespace CoffeeManagementAPI.Services
{
    public class SendEmailService : ISendMailService
    {
        public async Task<bool> SendMail(string email, string code)
        {
            try
            {
                var smtpClient = new SmtpClient("smtp.gmail.com");
                smtpClient.EnableSsl = true;
                smtpClient.Port = 587;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential("coffeetime2510@gmail.com", "pwizvsfmfgakszju");


                MailMessage mail = new MailMessage();
                mail.IsBodyHtml = true;
                mail.From = new MailAddress("coffeetime2510@gmail.com");
                mail.To.Add(email);
                mail.Subject = "COFFEE XIN THÔNG BÁO";
                mail.Body = CreateBody(code);

                await smtpClient.SendMailAsync(mail);
                return true;

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        string CreateBody(string code)
        {
            string body = "";
            using (StreamReader str = new StreamReader("MailTemplate/emailTemplate.html"))
            {
                body = str.ReadToEnd();
            }

            body = body.Replace("{code}", code);

            return body;
        }
    }
}
