using Dapper;
using MacroNutrientes.Domain.Entities;
using MacroNutrientes.Domain.Entities.Views;
using MacroNutrientes.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MacroNutrientes.Infra.Data.Repositories
{
    public class RefeicoesRepository : RepositoryBase<Refeicoes, RefeicoesView>, IRefeicoesRepository
    {
        public IEnumerable<RefeicoesView> ListarRefeicoesPorDataAlimentoRefeicao(DateTime data, string refeicao)
        {
            return cn.Query<RefeicoesView>($"SELECT RFC.Id,RFC.DataRefeicao,RFC.Refeicao,ALI.Alimento,RFC.Peso,RFC.Proteina,RFC.Carboidrato,RFC.Gordura,RFC.Caloria FROM Refeicoes AS RFC INNER JOIN Alimentos AS ALI ON RFC.IdAlimento = ALI.Id WHERE RFC.DataRefeicao = '{data}' AND RFC.Refeicao = '{refeicao}' AND RFC.Ativo = 1").ToList();
        }

        public IEnumerable<AlimentoView> ListarTodosAlimentos()
        {
            return cn.Query<AlimentoView>("SELECT * FROM Alimentos WHERE Ativo = 1 ORDER BY Alimento").ToList();
        }

        public bool VerificarRefeicaoExistente(DateTime data, string refeicao, int idAlimento)
        {
            return cn.Query<int>($"SELECT TOP 1 1 FROM Refeicoes WHERE DataRefeicao = '{data}' AND Refeicao = '{refeicao}' AND IdAlimento = {idAlimento} AND Ativo = 1").Any();
        }
    }
}
