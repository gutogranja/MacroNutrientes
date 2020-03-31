using MahApps.Metro.Controls.Dialogs;
using Prism.Commands;
using Prism.Mvvm;
using System.Collections.Generic;
using MacroNutrientes.Domain.Entities.Views;
using MacroNutrientes.Domain.Interfaces.Services;
using MacroNutrientes.Domain.Services;
using MacroNutrientes.Infra.Data.Repositories;
using System.Linq;
using System.Threading.Tasks;
using MacroNutrientes.Domain.Entities.Requests;

namespace MacroNutrientes.Apresentation.Wpf.ViewModels
{
    public class RefeicoesWindowViewModel : BindableBase
    {
        private IDialogCoordinator dialog;
        private readonly IRefeicoesService refeicaoService;
        public DelegateCommand IncluirCommand { get; set; }
        public DelegateCommand AlterarCommand { get; set; }
        public DelegateCommand InativarCommand { get; set; }
        public DelegateCommand LimparTelaCommand { get; set; }
        public ProgressDialogController Progresso { get; set; }

        private bool _modoEdicao = false;
        public bool ModoEdicao
        {
            get { return _modoEdicao; }
            set
            {
                SetProperty(ref _modoEdicao, value);
            }
        }

        private bool _editarRefeicao = true;
        public bool EditarRefeicao
        {
            get { return _editarRefeicao; }
            set
            {
                SetProperty(ref _editarRefeicao, value);
            }
        }

        private RefeicoesRequest _request = new RefeicoesRequest();
        public RefeicoesRequest Request
        {
            get { return _request; }
            set
            {
                SetProperty(ref _request, value);
            }
        }

        private List<AlimentoView> _listaAlimentos;
        public List<AlimentoView> ListaAlimentos
        {
            get { return _listaAlimentos; }
            set
            {
                SetProperty(ref _listaAlimentos, value);
            }
        }

        private List<RefeicoesView> _listaRefeicao;
        public List<RefeicoesView> ListaRefeicao
        {
            get { return _listaRefeicao; }
            set
            {
                SetProperty(ref _listaRefeicao, value);
            }
        }

        private RefeicoesView _view;
        public RefeicoesView View
        {
            get { return _view; }
            set
            {
                SetProperty(ref _view, value);
                ModoEdicao = _view?.Id > 0;
                EditarRefeicao = !_modoEdicao;
            }
        }

        public RefeicoesWindowViewModel(IDialogCoordinator dialog)
        {
            this.dialog = dialog;
            refeicaoService = new RefeicoesService(new RefeicoesRepository());
            IncluirCommand = new DelegateCommand(Incluir, () => !ModoEdicao).ObservesProperty(() => ModoEdicao);
            AlterarCommand = new DelegateCommand(Alterar, () => ModoEdicao).ObservesProperty(() => ModoEdicao);
            InativarCommand = new DelegateCommand(Inativar, () => ModoEdicao).ObservesProperty(() => ModoEdicao);
            LimparTelaCommand = new DelegateCommand(Limpar);
            BuscarTodosAlimentos();
        }

        private async void Incluir()
        {
            Request.DataRefeicao = View.DataRefeicao;
            Request.Refeicao = View.Refeicao;
            Request.IdAlimento = View.IdAlimento;
            Request.Peso = View.Peso;
            Request.Proteina = View.Proteina;
            Request.Carboidrato = View.Carboidrato;
            Request.Gordura = View.Gordura;
            Request.Caloria = View.Caloria;
            var refeicaoCriada = refeicaoService.Incluir(Request, "Carlosg");
            if (!refeicaoService.Validar)
            {
                var linq = refeicaoService.Notificacoes.Select(msg => msg.Mensagem);
                await this.dialog.ShowMessageAsync(this, "Atenção", string.Join("\r\n", linq));
                refeicaoService.LimparNotificacoes();
            }
            if (refeicaoCriada != null)
            {
                Progresso = await dialog.ShowProgressAsync(this, "Progresso", "Incluindo dados da refeição. Aguarde...");
                Progresso.SetIndeterminate();
                var t = Task.Factory.StartNew(() => { BuscarTodosAlimentos(); });
                await t;
                await Progresso?.CloseAsync();
                await this.dialog.ShowMessageAsync(this, "Atenção", "Refeição cadastrada com sucesso !!!");
                Limpar();
            }
        }

        private async void Alterar()
        {
            if (View != null && View.Id > 0)
            {
                var refeicaoExistente = refeicaoService.ObterPorId(View.Id);
                if (refeicaoExistente != null)
                {
                    Request.Id = View.Id;
                    Request.DataRefeicao = View.DataRefeicao;
                    Request.Refeicao = View.Refeicao;
                    Request.IdAlimento = View.IdAlimento;
                    Request.Peso = View.Peso;
                    Request.Proteina = View.Proteina;
                    Request.Carboidrato = View.Carboidrato;
                    Request.Gordura = View.Gordura;
                    Request.Caloria = View.Caloria;
                    refeicaoExistente = refeicaoService.Alterar(Request, "Carlosg");
                    if (refeicaoService.Validar)
                    {
                        Progresso = await dialog.ShowProgressAsync(this, "Progresso", "Alterando dados da refeição. Aguarde...");
                        Progresso.SetIndeterminate();
                        await Progresso?.CloseAsync();
                        await this.dialog.ShowMessageAsync(this, "Atenção", "Refeição alterada com sucesso !!!");
                        Limpar();
                    }
                    else
                    {
                        await this.dialog.ShowMessageAsync(this, "Atenção", string.Join("\r\n", refeicaoService.Notificacoes.Select(s => s.Mensagem)));
                        refeicaoService.LimparNotificacoes();
                    }
                    BuscarTodosAlimentos();
                }
            }
        }

        private async void Inativar()
        {
            if (View != null && View.Id > 0)
            {
                var refeicaoExistente = refeicaoService.ObterPorId(View.Id);
                if (refeicaoExistente != null)
                {
                    Progresso = await dialog.ShowProgressAsync(this, "Progresso", "Inativando refeição. Aguarde...");
                    Progresso.SetIndeterminate();
                    var t = Task.Factory.StartNew(() => { refeicaoService.Inativar(View.Id, "Carlosg"); });
                    await t;
                    await Progresso?.CloseAsync();
                    await this.dialog.ShowMessageAsync(this, "Atenção", "Refeição inativada com sucesso !!!");
                    Limpar();
                    BuscarTodosAlimentos();
                }
                else
                {
                    await this.dialog.ShowMessageAsync(this, "Atenção", string.Join("\r\n", refeicaoExistente.Notificacoes.Select(s => s.Mensagem)));
                    refeicaoService.LimparNotificacoes();
                }
            }
        }

        private void BuscarTodosAlimentos()
        {
            // Implementar a parte de buscar todos os alimentos
        }

        private void Limpar()
        {
            View = new RefeicoesView();
        }
    }
}
