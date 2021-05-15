using System;
using System.Collections.Generic;
using System.Linq;
using MeuControle.Dominio.Consultas;
using MeuControle.Dominio.Entidades;
using MeuControle.Dominio.Repositorios;
using MeuControle.Infra.Contextos;
using Microsoft.EntityFrameworkCore;

namespace MeuControle.Infra.Repositorios
{
    public class PlanoContaRepositorio : IPlanoContaRepositorio
    {
        private readonly ContextoDados _contexto;

        public PlanoContaRepositorio(ContextoDados contexto) => _contexto = contexto;

        public void Atualizar(PlanoConta planoConta)
        {
            _contexto.Entry(planoConta).State = EntityState.Modified;
            _contexto.SaveChanges();
        }

        public void Criar(PlanoConta produto)
        {
            _contexto.PlanosContas.Add(produto);
            _contexto.SaveChanges();
        }

        public void Deletar(PlanoConta planoConta)
        {
            _contexto.PlanosContas.Remove(planoConta);
            _contexto.SaveChanges();
        }

        public PlanoConta ObterPorCodigo(Guid codigo, Guid usuario) => _contexto.PlanosContas
                                                                        .FirstOrDefault(PlanoContaConsultas
                                                                        .ObterPorCodigo(codigo, usuario));

        public IEnumerable<PlanoConta> ObterPorOperacao(Guid usuario, string operacao) => _contexto.PlanosContas
                                                                    .AsNoTracking()
                                                                    .Where(PlanoContaConsultas
                                                                    .ObterPorOperacao(usuario, operacao))
                                                                    .OrderBy(p => p.Nome);

        public IEnumerable<PlanoConta> ObterTodos(Guid usuario) => _contexto.PlanosContas
                                                                    .AsNoTracking()
                                                                    .Where(PlanoContaConsultas
                                                                    .ObterTodos(usuario))
                                                                    .OrderBy(p => p.Nome);
    }
}
