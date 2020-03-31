using MacroNutrientes.Domain.Entities;
using MacroNutrientes.Domain.Entities.Requests;
using MacroNutrientes.Domain.Entities.Views;
using MacroNutrientes.Domain.Interfaces.Notifications;
using System;
using System.Collections.Generic;

namespace MacroNutrientes.Domain.Interfaces.Services
{
    public interface IRefeicoesService : INotificationService   
    {
        IEnumerable<RefeicoesView> ListarRefeicoesPorDataAlimentoRefeicao(DateTime data,string refeicao);
        IEnumerable<AlimentoView> ListarTodosAlimentos();
        Refeicoes ObterPorId(int id);
        Refeicoes Incluir(RefeicoesRequest refeicao, string usuario);
        Refeicoes Alterar(RefeicoesRequest refeicao, string usuario);
        void Inativar(int id, string usuarioCadastro);
    }   
}
