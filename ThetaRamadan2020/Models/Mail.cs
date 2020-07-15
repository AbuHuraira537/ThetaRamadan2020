using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace ThetaRamadan2020.Models
{
    public class Mail
    {
        private string fromemail;
        private string emailname;
        private string host;
        private string pass;
        private int port;
        private string wellcome;
        public string desc;
        public Mail()
        {
            fromemail = "kalyanainfo967@gmail.com";
            emailname = "ThetaRamadan2020";
            host = "smtp.gmail.com";
            pass = "bsef17m537";
            port = 587;
            wellcome = "WellCome to theta Online Store";
            desc = "";
        }
        public  Boolean sendmail(string to)
        {
            try
            {
                MailMessage mail = new MailMessage
                {
                    From = new MailAddress(fromemail, emailname)
                };
                mail.To.Add(to);
                mail.Subject = wellcome;
                if(desc == "")
                { 
                mail.Body = "<h3>Well Come To ThetaOnline Store :</h3></br>" +
                    "<h3>Thanks for hoining us! </h3></br><h4>you can now contact us through our email,live chat..</h4>";
                }
                else
                {
                    mail.Body = "<h3>Well Come To ThetaOnline Store :</h3></br> your password is:" + desc;
                }
                mail.IsBodyHtml = true;

                SmtpClient server = new SmtpClient
                {
                    Host = host,
                    UseDefaultCredentials = false,
                    Credentials = new System.Net.NetworkCredential(fromemail, pass),
                    Port = 587,
                    EnableSsl = true

                };
                server.Send(mail);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }



        }
    }
}
