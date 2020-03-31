using MacroNutrientes.Domain.Entities;
using System.Collections.Generic;

namespace MacroNutrientes.Domain.Interfaces.Repositories
{
    public interface IRepositoryBase<T,TView> where T : Entity where TView : class 
    {
        //IEnumerable<TView> ListarTodos();
        T ObterPorId(int id);
        T Incluir(T alimento);
        T Alterar(T alimento);
    }
}
