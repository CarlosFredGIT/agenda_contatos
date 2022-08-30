using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace AgendaContatos.Messages
{
    //Classe para envio de e-mails
    public class EmailMessage
    {
        private string _conta = "cotinaoresponda@outlook.com";
        private string _senha = "@Admin123456";
        private string _smtp = "smtp-mail.outlook.com";
        private int _porta = 587;

        public void SendMail(string emailTo, string subject, string body)
        {
            var mailMessage = new MailMessage(_conta, emailTo);
            mailMessage.Subject = subject;
            mailMessage.Body = body;
            mailMessage.IsBodyHtml = true;

            var smtpClient = new SmtpClient(_smtp, _porta);
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new NetworkCredential(_conta, _senha);
            smtpClient.Send(mailMessage);
        }
    }
}
