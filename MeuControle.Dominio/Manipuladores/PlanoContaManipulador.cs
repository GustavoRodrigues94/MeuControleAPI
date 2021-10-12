using System.Threading.Tasks;
using Flunt.Notifications;
using MeuControle.Dominio.Comandos.PlanoContaComando;
using MeuControle.Dominio.Compartilhado.Contratos;
using MeuControle.Dominio.Entidades;
using MeuControle.Dominio.Manipuladores.Contratos;
using MeuControle.Dominio.Repositorios;

namespace MeuControle.Dominio.Manipuladores
{
    public class PlanoContaManipulador : 
        Notifiable,
        IManipulador<CriarPlanoContaComando>,
        IManipulador<DeletarPlanoContaComando>,
        IManipulador<AtualizarPlanoContaComando>
    {
        private readonly IPlanoContaRepositorio _repositorio;

        public PlanoContaManipulador(IPlanoContaRepositorio repositorio) => _repositorio = repositorio;

        public async Task<GenericoComandoResultado> Manipular(CriarPlanoContaComando comando)
        {
            comando.Validate();

            if (comando.Invalid)
                return new GenericoComandoResultado(false, "Ops, parece que seu plano de conta está errado.", comando.Notifications);

            var planoConta = new PlanoConta(comando.Nome, comando.Operacao, comando.Usuario);

            _repositorio.Criar(planoConta);

            return new GenericoComandoResultado(true, "Plano de conta salvo", planoConta);
        }

        public async Task<GenericoComandoResultado> Manipular(DeletarPlanoContaComando comando)
        {
            comando.Validate();

            if (comando.Invalid)
                return new GenericoComandoResultado(false, "Ops, parece que seu plano de conta está errado.", comando.Notifications);

            var planoConta = _repositorio.ObterPorCodigo(comando.CodigoPlanoConta, comando.CodigoUsuario);

            if (planoConta is null)
                return new GenericoComandoResultado(false, "Plano de conta não encontrado", null);

            _repositorio.Deletar(planoConta);

            return new GenericoComandoResultado(true, "Plano de conta deletado", planoConta);
        }

        public async Task<GenericoComandoResultado> Manipular(AtualizarPlanoContaComando comando)
        {
            comando.Validate();

            if (comando.Invalid)
                return new GenericoComandoResultado(false, "Ops, parece que seu plano de conta está errado.", comando.Notifications);

            var planoConta = _repositorio.ObterPorCodigo(comando.Codigo, comando.Usuario);

            if (planoConta is null)
                return new GenericoComandoResultado(false, "Plano de conta não encontrado", null);

            planoConta.Atualizar(comando.Nome, comando.Operacao);

            _repositorio.Atualizar(planoConta);

            return new GenericoComandoResultado(true, "Plano de conta atualizado", planoConta);
        }
    }
}
