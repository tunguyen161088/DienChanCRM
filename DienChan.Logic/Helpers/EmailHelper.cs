using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using DienChan.DataAccess;
using DienChan.Entities;

namespace DienChan.Logic.Helpers
{
    public class EmailHelper
    {
        public void SendReportEmail(Order order, Stream report, string email)
        {
            var attach = new Attachment(report, $"Receipt_{order.orderId}.pdf", System.Net.Mime.MediaTypeNames.Application.Octet);

            var fromAddress = new MailAddress(Configuration.FromEmail, "Academie Dien Chan");
            var toAddress = new MailAddress(order.customer.email, $"{order.customer.firstName} {order.customer.lastName}");
            string subject = $"Receipt of order {order.orderId}";
            string body = "Please see attachment your recent order! Thank you for your business!";

            var smtp = new SmtpClient
            {
                Host = Configuration.HostSmtp,
                Port = Configuration.HostPort,
                EnableSsl = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, Configuration.FromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body,
                Attachments = { attach },
                CC = { email }
            })
            {
                smtp.Send(message);
            }
        }
    }
}
