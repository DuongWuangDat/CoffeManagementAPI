﻿using CoffeeManagementAPI.Interface.StrategyInterface;
using System.Net.Mail;
using System.Net;

namespace CoffeeManagementAPI.Strategy.SendVoucherStrategy
{
    public class SendEmailStrategy : ISendVoucherStrategy
    {
        private readonly IWebHostEnvironment _env;

        public SendEmailStrategy(IWebHostEnvironment env)
        {
            _env = env;
        }
        public async Task<(bool, string)> SendVoucher(string email, string code)
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
                return (true, "");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return (false, ex.Message);
            }
        }

        string CreateBody(string code)
        {
            string body = "";
            using (StreamReader str = new StreamReader(Path.Combine(_env.ContentRootPath, "MailTemplate/emailTemplate.html")))
            {
                body = str.ReadToEnd();
            }

            body = body.Replace("{code}", code);

            return body;
        }
    }
}
