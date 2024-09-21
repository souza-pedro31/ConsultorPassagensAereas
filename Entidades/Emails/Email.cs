namespace ConsultorPassagensAereas.Entidades.Emails
{
    abstract class Email
    {
        public string provedor { get; set; }
        public string usuario { get; set; } = "emailoutlook@outlook.com";
        public string senha { get; set; } = "senhaoutlook";

        public Email()
        {
        }

        public Email(string provedor, string usuario, string senha)
        {
            this.provedor = null;
            this.usuario = usuario;
            this.senha = usuario;
        }
    }
}
