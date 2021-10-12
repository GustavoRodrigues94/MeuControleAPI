using System.Linq;
using System.Threading.Tasks;
using MeuControle.Dominio.Entidades;
using MeuControle.Dominio.Repositorios;
using MeuControle.Infra.Contextos;
using Microsoft.EntityFrameworkCore;

namespace MeuControle.Infra.Repositorios
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly ContextoDados _contexto;

        public UsuarioRepositorio(ContextoDados contexto) => _contexto = contexto;

        public void Criar(Usuario usuario)
        {
            _contexto.Usuarios.Add(usuario);
            _contexto.SaveChanges();
        }

        public async Task<Usuario> ObterUsuarioPorEmailSenha(string email, string senha) => await _contexto.Usuarios
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Email == email && x.Senha == senha);
    }
}
