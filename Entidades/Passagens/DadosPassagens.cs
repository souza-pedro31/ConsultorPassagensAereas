namespace ConsultorPassagensAereas.Entidades.Passagens
{
    class DadosPassagens
    {
        public string message { get; set; }
        public List<Oferta> ofertas { get; set; } = new List<Oferta>();

        public DadosPassagens()
        {

        }

        public void addOfertas(Oferta oferta)
        {
            ofertas.Add(oferta);
        }
    }
}
