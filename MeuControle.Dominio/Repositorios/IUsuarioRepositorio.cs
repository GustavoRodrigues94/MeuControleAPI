using MeuControle.Dominio.Entidades;

namespace MeuControle.Dominio.Repositorios
{
    public interface IUsuarioRepositorio
    {
        void Criar(Usuario usuario);
        Usuario ObterUsuarioPorEmailSenha(string email, string senha);
    }
}
