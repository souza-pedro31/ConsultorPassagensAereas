using ConsultorPassagensAereas.Entidades.Passagens;
using ConsultorPassagensAereas.Entidades.Emails;
using ConsultorPassagensAereas.Services;
using System.Text;
using System.Text.Json.Nodes;

namespace ConsultorPassagensAereas.Entities
{
    internal class Consultor
    {
        public async Task consultarPassagens(SolicitacaoConsulta solicitacao, IMelhorPassagem melhorPassagem)
        {
            var client = new HttpClient();

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://booking-com15.p.rapidapi.com/api/v1/flights/getMinPrice?fromId={solicitacao.AeroportoPartida.ToUpper().Trim()}.AIRPORT&toId={solicitacao.AeroportoChegada.ToUpper().Trim()}.AIRPORT&departDate={solicitacao.dataIda.ToString("yyyy-MM-dd")}&returnDate={solicitacao.dataVolta.ToString("yyyy-MM-dd")}&cabinClass=ECONOMY&currency_code=BRL"),
                Headers =
                    {
                        { "x-rapidapi-key", "<apikey>" },
                        { "x-rapidapi-host", "booking-com15.p.rapidapi.com" },
                    },
            };

            using (var response = client.Send(request))
            {
                response.EnsureSuccessStatusCode();

                var json = JsonNode.Parse(await response.Content.ReadAsStringAsync());
                DadosPassagens dados = PopularDadosPassagens(json);

                if (solicitacao.dispararEmail == true && dados.message == "Success")
                    DispararEmail(dados, solicitacao.emails);
            }
        }
        private DadosPassagens PopularDadosPassagens(JsonNode json)
        {
            DadosPassagens dp = new DadosPassagens();
            var dadosPassagensJson = json["data"];
            dp.message = json["message"].ToString();
            for (int i = 0; i < dadosPassagensJson.AsArray().Count; i++)
            {
                var passagem = dadosPassagensJson[i];
                DateTime dataIdaJson = DateTime.Parse(passagem["departureDate"].ToString());
                DateTime dataVoltaJson = DateTime.Parse(passagem["returnDate"].ToString());
                double preco = double.Parse(passagem["price"]["units"].ToString());

                Oferta oferta = new Oferta(dataIdaJson, dataVoltaJson, preco);
                dp.addOfertas(oferta);
            }
            return dp;
        }

        private void DispararEmail(DadosPassagens dados, List<string> emails)
        {
            var outlook = new Outlook();
            IMelhorPassagem melhorPassagem = new MelhorPrecoPassagem();

            string assunto = $"Relatório valor das passagens aéreas data: {DateTime.Now.ToString("dd/MM/yyyy")}";

            StringBuilder corpo = new StringBuilder();
            corpo.Append($"<h1>Olá, segue relatório das passagens de hoje! {DateTime.Now.ToString("dd/MM/yyyy")}</h1><br>");
            corpo.Append($"<h2>O melhor preço hoje foi: R$ {melhorPassagem.melhorOferta(dados).preco.ToString("N2")}</h2><br><br>");
            corpo.Append("<p>Lista de ofertas:</p>");

            foreach (Oferta oferta in dados.ofertas)
            {
                corpo.Append($"<p><strong>Data de ida:</strong> {oferta.dataIda.ToShortDateString()}<br>" +
                            $"<strong>Data de volta:</strong> {oferta.dataVolta.ToShortDateString()}<br>" +
                            $"<strong>Quantidade de dias:</strong> {(oferta.dataVolta - oferta.dataIda).TotalDays}<br>" +
                            $"<strong>Valor:</strong> R$ {oferta.preco.ToString("N2")}</p><br>");
            }

            corpo.Append("Atenciosamente, Robô do Pedro.");

            outlook.EnviaEmail(emails, assunto, corpo.ToString());
        }
    }
}
