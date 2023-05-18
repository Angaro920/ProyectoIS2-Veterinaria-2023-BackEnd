using System;
using System.Net;
using System.Net.Mail;

namespace API_Log.Utilities
{
    public class EmailService
    {
        private SmtpClient cliente;
        private MailMessage email;
        private string Host = "smtp.gmail.com";
        private int Port = 587;
        private string User = "acruzardila23@gmail.com";
        private string Password = "vnucooikapcvqnkz";//Contraseña de Aplicación
        private bool EnabledSSL = true;


        public EmailService()
        {
            cliente = new SmtpClient()
            {
                Host = Host,
                Port = Port,
                EnableSsl = EnabledSSL,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = true,
                Credentials = new NetworkCredential(User, Password)
            };
        }
        public void EnviarCorreo(string destinatario, string asunto, string mensaje, bool esHtlm = false)
        {
            email = new MailMessage(User, destinatario, asunto, mensaje);
            email.IsBodyHtml = esHtlm;
            cliente.Send(email);
        }
    }
}
