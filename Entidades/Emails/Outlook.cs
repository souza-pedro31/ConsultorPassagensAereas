using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsultorPassagensAereas.Entidades.Emails
{
    internal class Outlook : Email, IEmail
    {
        public Outlook()
        {
            provedor = "smtp-mail.outlook.com";
        }

        public Outlook(string provedor, string usuario, string senha)
        {
            this.provedor = provedor;
            this.usuario = usuario;
            this.senha = senha;
        }

        public void EnviaEmail(List<string> emails, string assunto, string corpo)
        {
            MailMessage mensagem = MontaEmail(emails, assunto, corpo);
            EnviaEmailComSMTP(mensagem);
        }

        public MailMessage MontaEmail(List<string> emails, string assunto, string corpo)
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(usuario);

            foreach (var email in emails)
            {
                if (ValidaEmail(email))
                {
                    mail.To.Add(email);
                }

                mail.Subject = assunto;
                mail.Body = corpo;
                mail.IsBodyHtml = true;
                mail.SubjectEncoding = Encoding.GetEncoding("UTF-8");
                mail.BodyEncoding = Encoding.GetEncoding("UTF-8");
            }
            return mail;
        }

        public bool ValidaEmail(string email)
        {
            Regex expressao = new Regex(@"\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}");
            if (expressao.IsMatch(email))
            {
                return true;
            }
            return false;
        }

        public void EnviaEmailComSMTP(MailMessage mensagem)
        {
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = provedor;
            smtpClient.Port = 587;
            smtpClient.EnableSsl = true;
            smtpClient.Timeout = 50000;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(usuario, senha);
            smtpClient.Send(mensagem);
            smtpClient.Dispose();
        }
    }
}
