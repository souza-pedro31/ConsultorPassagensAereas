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
    interface IEmail
    {
        public void EnviaEmail(List<string> emails, string assunto, string corpo);

        public MailMessage MontaEmail(List<string> emails, string assunto, string corpo);

        public bool ValidaEmail(string email);

        public void EnviaEmailComSMTP(MailMessage mensagem);
    }
}
