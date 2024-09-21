using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultorPassagensAereas.Entities
{
    internal class SolicitacaoConsulta
    {
        public List<string> emails = new List<string>();
        public string AeroportoPartida;
        public string AeroportoChegada;
        public DateTime dataIda;
        public DateTime dataVolta;
        public bool dispararEmail = true;

        public void adicionarEmail(string email)
        {
            emails.Add(email);
        }
    }
}
