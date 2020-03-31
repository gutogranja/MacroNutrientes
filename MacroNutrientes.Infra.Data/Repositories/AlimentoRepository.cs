using Dapper;
using MacroNutrientes.Domain.Entities;
using MacroNutrientes.Domain.Entities.Views;
using MacroNutrientes.Domain.Interfaces.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace MacroNutrientes.Infra.Data.Repositories
{
    public class AlimentoRepository : RepositoryBase<Alimentos,AlimentoView> , IAlimentoRepository 
    {
        //public override IEnumerable<AlimentoView> ListarTodos()
        public IEnumerable<AlimentoView> ListarTodos()
        {
            return cn.Query<AlimentoView>("SELECT * FROM Alimentos WHERE Ativo = 1 ORDER BY Alimento").ToList();
        }

        public bool VerificarAlimentoExistente(string alimento)
        {
            return cn.Query<int>($"SELECT TOP 1 1 FROM Alimentos WHERE Alimento = '{alimento}' AND Ativo = 1").Any();
        }
    }
}
