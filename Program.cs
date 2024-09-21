using ConsultorPassagensAereas.Entities;
using ConsultorPassagensAereas.Services;

namespace ConsultorPassagensAereas
{
    class Program
    {
        static void Main(string[] args)
        {
            SolicitacaoConsulta solicitacao = new SolicitacaoConsulta();
            Consultor consultor = new Consultor();

            solicitacao.AeroportoPartida = "SDU";
            solicitacao.AeroportoChegada = "GOI";
            solicitacao.dataIda = DateTime.Parse("10/03/2025");
            solicitacao.dataVolta = DateTime.Parse("25/03/2025");
            solicitacao.adicionarEmail("exemploemail@gmail.com");

            consultor.consultarPassagens(solicitacao, new MelhorPrecoPassagem());
        }       
    }
}