using ConsultorPassagensAereas.Entidades.Passagens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultorPassagensAereas.Services
{
    internal class MelhorPrecoPassagem : IMelhorPassagem
    {
        public Oferta melhorOferta(DadosPassagens dadosPassagens)
        {
            Oferta menorOferta = new Oferta();

            for(int i=0; i < dadosPassagens.ofertas.Count; i++)
            {
                if (i == 0)
                {
                    menorOferta = dadosPassagens.ofertas[0];
                }
                else if (dadosPassagens.ofertas[i].preco < menorOferta.preco)
                {
                    menorOferta = dadosPassagens.ofertas[i];
                }
            }
            return menorOferta;
        }
    }
}
