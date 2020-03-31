using MacroNutrientes.Domain.Entities;
using MacroNutrientes.Domain.Entities.Requests;
using MacroNutrientes.Domain.Entities.Views;
using MacroNutrientes.Domain.Interfaces.Notifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace MacroNutrientes.Domain.Interfaces.Services
{
    public interface IAlimentoService : INotificationService
    {
        IEnumerable<AlimentoView> ListarTodos();
        Alimentos ObterPorId(int id);
        Alimentos Incluir(AlimentoRequest alimento, string usuario);
        Alimentos Alterar(AlimentoRequest alimento, string usuario);
        void Inativar(int id, string usuarioCadastro);
    }
}
