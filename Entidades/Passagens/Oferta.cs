using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultorPassagensAereas.Entidades.Passagens
{
    internal class Oferta
    {
        public DateTime dataIda { get; set; }
        public DateTime dataVolta { get; set; }
        public double preco { get; set; }

        public Oferta()
        {

        }
        public Oferta(DateTime dataIda, DateTime dataVolta, double preco)
        {
            this.dataIda = dataIda;
            this.dataVolta = dataVolta;
            this.preco = preco;
        }
    }
}
