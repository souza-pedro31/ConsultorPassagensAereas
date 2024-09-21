using ConsultorPassagensAereas.Entidades.Passagens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultorPassagensAereas.Services
{
    interface IMelhorPassagem
    {
        public Oferta melhorOferta(DadosPassagens dadosPassagens);
    }
}
