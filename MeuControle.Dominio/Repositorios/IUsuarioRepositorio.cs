using System.Threading.Tasks;
using MeuControle.Dominio.Entidades;

namespace MeuControle.Dominio.Repositorios
{
    public interface IUsuarioRepositorio
    {
        void Criar(Usuario usuario);
        Task<Usuario> ObterUsuarioPorEmailSenha(string email, string senha);
    }
}
