using MacroNutrientes.Domain.Entities;
using MacroNutrientes.Domain.Entities.Requests;
using MacroNutrientes.Domain.Entities.Views;
using MacroNutrientes.Domain.Interfaces.Repositories;
using MacroNutrientes.Domain.Interfaces.Services;
using System.Collections.Generic;


namespace MacroNutrientes.Domain.Services
{
    public class AlimentoService : BaseService, IAlimentoService
    {
        private readonly IAlimentoRepository repositorio;

        public AlimentoService(IAlimentoRepository repositorio)
        {
            this.repositorio = repositorio;
        }

        public IEnumerable<AlimentoView> ListarTodos()
        {
            return repositorio.ListarTodos();
        }

        public Alimentos ObterPorId(int id)
        {
            return repositorio.ObterPorId(id);
        }

        public Alimentos Incluir(AlimentoRequest request, string usuario)
        {
            var novoAlimento = new Alimentos(request.Alimento, request.Peso, request.Proteina, request.Carboidrato, request.Gordura, request.Caloria, usuario);
            ValidarAlimento(novoAlimento);
            if (Validar)
            {
                bool alimentoExistente = repositorio.VerificarAlimentoExistente(request.Alimento);
                if (!alimentoExistente)
                    return repositorio.Incluir(novoAlimento);
                else
                    AdicionarNotificacao("Alimento", "Alimento já existe na base de dados");
            }
            return null;
        }

        public Alimentos Alterar(AlimentoRequest request, string usuario)
        {
            var alimentoExistente = repositorio.ObterPorId(request.Id);
            if (alimentoExistente != null)
            {
                alimentoExistente.AlterarPeso(request.Peso);
                alimentoExistente.AlterarProteina(request.Proteina);
                alimentoExistente.AlterarCarboidrato(request.Carboidrato);
                alimentoExistente.AlterarGordura(request.Gordura);
                alimentoExistente.AlterarCaloria(request.Caloria);
                ValidarAlimento(alimentoExistente);
                if (Validar)
                    return repositorio.Alterar(alimentoExistente);
            }
            else
                AdicionarNotificacao("Alimento", "Alimento não existe.");
            return null;
        }

        public void Inativar(int id, string usuario)
        {
            if (id <= 0)
                AdicionarNotificacao("Alimento", "Alimento não existe");
            var alimentoExistente = repositorio.ObterPorId(id);
            if (alimentoExistente != null)
            {
                alimentoExistente.Inativar(usuario);
                repositorio.Alterar(alimentoExistente);
            }
            else
                AdicionarNotificacao("Alimento", "Não é possível inativar. Pois alimento não existe");
        }

        private void ValidarAlimento(Alimentos alimento)
        {   
            if (!alimento.Validar)
                AdicionarNotificacao(alimento.Notificacoes);
        }
    }
}
