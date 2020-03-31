using MacroNutrientes.Domain.Entities;
using MacroNutrientes.Domain.Entities.Views;
using System;
using System.Collections.Generic;

namespace MacroNutrientes.Domain.Interfaces.Repositories
{
    public interface IRefeicoesRepository : IRepositoryBase<Refeicoes, RefeicoesView>
    {
        IEnumerable<RefeicoesView> ListarRefeicoesPorDataAlimentoRefeicao(DateTime data,string refeicao);
        IEnumerable<AlimentoView> ListarTodosAlimentos();
        bool VerificarRefeicaoExistente(DateTime data,string refeicao,int idAlimento);
    }
}
