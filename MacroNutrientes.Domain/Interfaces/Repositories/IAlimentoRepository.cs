using MacroNutrientes.Domain.Entities;
using MacroNutrientes.Domain.Entities.Views;
using System.Collections.Generic;

namespace MacroNutrientes.Domain.Interfaces.Repositories
{
    public interface IAlimentoRepository : IRepositoryBase<Alimentos,AlimentoView>
    {
        IEnumerable<AlimentoView> ListarTodos();
        bool VerificarAlimentoExistente(string alimento);
    }
}
