namespace MeuControle.Dominio.Compartilhado.Contratos
{
    public class GenericoComandoResultado : IComandoResultado
    {
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }
        public object Dado { get; set; }

        public GenericoComandoResultado() { }

        public GenericoComandoResultado(bool sucesso, string mensagem, object dado)
        {
            Sucesso = sucesso;
            Mensagem = mensagem;
            Dado = dado;
        }
    }
}
