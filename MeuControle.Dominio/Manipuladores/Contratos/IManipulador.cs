using MeuControle.Dominio.Compartilhado.Contratos;

namespace MeuControle.Dominio.Manipuladores.Contratos
{
    public interface IManipulador<in T> where T : IComando
    {
        IComandoResultado Manipular(T comando);
    }
}
