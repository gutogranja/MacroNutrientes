using MacroNutrientes.Domain.Entities;
using MacroNutrientes.Domain.Entities.Requests;
using MacroNutrientes.Domain.Entities.Views;
using MacroNutrientes.Domain.Interfaces.Repositories;
using MacroNutrientes.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace MacroNutrientes.Domain.Services
{
    public class RefeicoesService : BaseService, IRefeicoesService
    {
        private readonly IRefeicoesRepository repositorio;

        public RefeicoesService(IRefeicoesRepository repositorio)
        {
            this.repositorio = repositorio;
        }

        public IEnumerable<RefeicoesView> ListarRefeicoesPorDataAlimentoRefeicao(DateTime data, string refeicao)
        {
            return repositorio.ListarRefeicoesPorDataAlimentoRefeicao(data,refeicao);
        }

        public IEnumerable<AlimentoView> ListarTodosAlimentos()
        {
            return repositorio.ListarTodosAlimentos();
        }

        public Refeicoes ObterPorId(int id)
        {
            return repositorio.ObterPorId(id);
        }

        public Refeicoes Incluir(RefeicoesRequest request, string usuario)
        {
            var novaRefeicao= new Refeicoes(request.DataRefeicao, request.Refeicao, request.IdAlimento, request.Peso, usuario);
            ValidarRefeicao(novaRefeicao);  
            if (Validar)
            {
                bool refeicaoExistente = repositorio.VerificarRefeicaoExistente(request.DataRefeicao,request.Refeicao,request.IdAlimento);
                if (!refeicaoExistente)
                    return repositorio.Incluir(novaRefeicao);
                else
                    AdicionarNotificacao("Refeicao", "Refeição já existe, nesta data");
            }
            return null;
        }

        public Refeicoes Alterar(RefeicoesRequest request, string usuario)  
        {
            var refeicaoExistente = repositorio.ObterPorId(request.Id);
            if (refeicaoExistente != null)
            {
                refeicaoExistente.AlterarPeso(request.Peso,refeicaoExistente);
                ValidarRefeicao(refeicaoExistente);
                if (Validar)
                    return repositorio.Alterar(refeicaoExistente);
            }
            else
                AdicionarNotificacao("Refeicao", "Refeição não existe.");
            return null;
        }

        public void Inativar(int id, string usuario)
        {
            if (id <= 0)
                AdicionarNotificacao("Refeicao", "Refeição não existe.");
            var refeicaoExistente = repositorio.ObterPorId(id);
            if (refeicaoExistente != null)
            {
                refeicaoExistente.Inativar(usuario);
                repositorio.Alterar(refeicaoExistente);
            }
            else
                AdicionarNotificacao("Refeicao", "Não é possível inativar. Pois refeição não existe");
        }

        private void ValidarRefeicao(Refeicoes refeicao)
        {   
            if (!refeicao.Validar)  
                AdicionarNotificacao(refeicao.Notificacoes);
        }
    }
}
