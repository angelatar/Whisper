using System;
using System.Net;
using System.Net.Mail;

namespace UserAPI.Businness
{
    public static class ValidationCode
    {

        public static bool Send(string receiver, string code)
        {
            string fromaddr = "whisperscdministration@gmail.com";
            string toaddr = receiver;//TO ADDRESS HERE
            string password = "whisper123.0";

            try
            {
                MailMessage msg = new MailMessage();
                msg.Subject = "Validation code";
                msg.From = new MailAddress(fromaddr);
                msg.Body = string.Format("Your validation code is {0}", code);
                msg.To.Add(new MailAddress(toaddr));
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.UseDefaultCredentials = false;
                smtp.EnableSsl = true;
                NetworkCredential nc = new NetworkCredential(fromaddr, password);
                smtp.Credentials = nc;
                smtp.Send(msg);
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }

        public static string CodeGenerator()
        {
            Guid g = Guid.NewGuid();
            string code = Convert.ToBase64String(g.ToByteArray());
            code = code.Replace("=", "");
            code = code.Replace("+", "");
            return code;
        }
    }
}
