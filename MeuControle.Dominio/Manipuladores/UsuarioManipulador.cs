using System.Threading.Tasks;
using Flunt.Notifications;
using MeuControle.Dominio.Comandos.UsuarioComando;
using MeuControle.Dominio.Compartilhado.Contratos;
using MeuControle.Dominio.Entidades;
using MeuControle.Dominio.Manipuladores.Contratos;
using MeuControle.Dominio.Repositorios;

namespace MeuControle.Dominio.Manipuladores
{
    public class UsuarioManipulador : Notifiable,
        IManipulador<CriarUsuarioComando>,
        IManipulador<AutenticarUsuarioComando>
    {
        private readonly IUsuarioRepositorio _repositorio;

        public UsuarioManipulador(IUsuarioRepositorio repositorio) => _repositorio = repositorio;

        public async Task<GenericoComandoResultado> Manipular(CriarUsuarioComando comando)
        {
            comando.Validate();

            if (comando.Invalid)
                return new GenericoComandoResultado(false, "Ops, parece que o usuário está incorreto.", comando.Notifications);

            var usuario = new Usuario(comando.Nome, comando.Email, comando.Senha);

            _repositorio.Criar(usuario);

            return new GenericoComandoResultado(true, "Usuário salvo", usuario);
        }

        public async Task<GenericoComandoResultado> Manipular(AutenticarUsuarioComando comando)
        {       
            comando.Validate();

            if (comando.Invalid)
                return new GenericoComandoResultado(false, "Ops, parece que o usuário está incorreto.", comando.Notifications);

            var usuario = await _repositorio.ObterUsuarioPorEmailSenha(comando.Email, comando.Senha);

            return usuario is null 
                ? new GenericoComandoResultado(false, "Usuário ou senha inválidos.", null) 
                : new GenericoComandoResultado(true, "Usuário existente", usuario);
        }
    }
}
