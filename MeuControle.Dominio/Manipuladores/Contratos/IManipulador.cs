using System.Threading.Tasks;
using MeuControle.Dominio.Compartilhado.Contratos;

namespace MeuControle.Dominio.Manipuladores.Contratos
{
    public interface IManipulador<in T> where T : IComando
    {
        Task<GenericoComandoResultado> Manipular(T comando);
    }
}
