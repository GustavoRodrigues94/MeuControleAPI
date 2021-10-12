using System.Threading.Tasks;
using Flunt.Notifications;
using MeuControle.Dominio.Comandos.LancamentoContaComando;
using MeuControle.Dominio.Compartilhado.Contratos;
using MeuControle.Dominio.Entidades;
using MeuControle.Dominio.Manipuladores.Contratos;
using MeuControle.Dominio.Repositorios;

namespace MeuControle.Dominio.Manipuladores
{
    public class LancamentoContaManipulador : Notifiable,
       IManipulador<CriarLancamentoContaComando>
    {
        private readonly ILancamentoContaRepositorio _repositorio;

        public LancamentoContaManipulador(ILancamentoContaRepositorio repositorio) => _repositorio = repositorio;

        public async Task<GenericoComandoResultado> Manipular(CriarLancamentoContaComando comando)
        {
            comando.Validate();

            if (comando.Invalid)
                return new GenericoComandoResultado(false, "Ops, parece que seu lançamento de conta está errado.", comando.Notifications);

            var lancamentoConta = new LancamentoConta(comando.Usuario, comando.Operacao, comando.CodigoPlanoConta, comando.DataLancamento, comando.Valor);

            _repositorio.Criar(lancamentoConta);

            return new GenericoComandoResultado(true, "Lançamento de conta salvo", lancamentoConta);
        }
    }
}
