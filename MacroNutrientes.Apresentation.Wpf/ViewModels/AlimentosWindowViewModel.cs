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
    public class AlimentosWindowViewModel : BindableBase
    {
        private IDialogCoordinator dialog;
        private readonly IAlimentoService alimentoService;
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

        private bool _editarAlimento = true;
        public bool EditarAlimento
        {
            get { return _editarAlimento; }
            set
            {
                SetProperty(ref _editarAlimento, value);
            }
        }

        private AlimentoRequest _request = new AlimentoRequest();
        public AlimentoRequest Request
        {
            get { return _request; }
            set
            {
                SetProperty(ref _request, value);
            }
        }

        private List<AlimentoView> _listaAlimento;
        public List<AlimentoView> ListaAlimento
        {
            get { return _listaAlimento; }
            set
            {
                SetProperty(ref _listaAlimento, value);
            }
        }

        private AlimentoView _view;
        public AlimentoView View
        {
            get { return _view; }
            set
            {
                SetProperty(ref _view, value);
                ModoEdicao = _view?.Id > 0;
                EditarAlimento = !_modoEdicao;
            }
        }

        public AlimentosWindowViewModel(IDialogCoordinator dialog)
        {
            this.dialog = dialog;
            alimentoService = new AlimentoService(new AlimentoRepository());
            IncluirCommand = new DelegateCommand(Incluir, () => !ModoEdicao).ObservesProperty(() => ModoEdicao);
            AlterarCommand = new DelegateCommand(Alterar, () => ModoEdicao).ObservesProperty(() => ModoEdicao);
            InativarCommand = new DelegateCommand(Inativar, () => ModoEdicao).ObservesProperty(() => ModoEdicao);
            LimparTelaCommand = new DelegateCommand(Limpar);
            BuscarAlimentos();
        }

        private async void Incluir()
        {
            Request.Alimento = View.Alimento;
            Request.Peso = View.Peso;
            Request.Proteina = View.Proteina;
            Request.Carboidrato = View.Carboidrato;
            Request.Gordura = View.Gordura;
            Request.Caloria = View.Caloria;
            var alimentoCriado = alimentoService.Incluir(Request, "Carlosg");
            if (!alimentoService.Validar)
            {
                var linq = alimentoService.Notificacoes.Select(msg => msg.Mensagem);
                await this.dialog.ShowMessageAsync(this, "Atenção", string.Join("\r\n", linq));
                alimentoService.LimparNotificacoes();
            }
            if (alimentoCriado != null)
            {
                Progresso = await dialog.ShowProgressAsync(this, "Progresso", "Incluindo dados do alimento. Aguarde...");
                Progresso.SetIndeterminate();
                var t = Task.Factory.StartNew(() => { BuscarAlimentos(); });
                await t;
                await Progresso?.CloseAsync();
                await this.dialog.ShowMessageAsync(this, "Atenção", "Alimento cadastrado com sucesso !!!");
                Limpar();
            }
        }

        private async void Alterar()
        {
            if (View != null && View.Id > 0)
            {
                var alimentoExistente = alimentoService.ObterPorId(View.Id);
                if (alimentoExistente != null)
                {
                    Request.Id = View.Id;
                    Request.Peso = View.Peso;
                    Request.Proteina = View.Proteina;
                    Request.Carboidrato = View.Carboidrato;
                    Request.Gordura = View.Gordura;
                    Request.Caloria = View.Caloria;
                    alimentoExistente = alimentoService.Alterar(Request, "Carlosg");
                    if (alimentoService.Validar)
                    {
                        Progresso = await dialog.ShowProgressAsync(this, "Progresso", "Alterando dados do alimento. Aguarde...");
                        Progresso.SetIndeterminate();
                        await Progresso?.CloseAsync();
                        await this.dialog.ShowMessageAsync(this, "Atenção", "Alimento alterado com sucesso !!!");
                        Limpar();
                    }
                    else
                    {
                        await this.dialog.ShowMessageAsync(this, "Atenção", string.Join("\r\n", alimentoService.Notificacoes.Select(s => s.Mensagem)));
                        alimentoService.LimparNotificacoes();
                    }
                    BuscarAlimentos();
                }
            }
        }

        private async void Inativar()
        {
            if (View != null && View.Id > 0)
            {
                var alimentoExistente = alimentoService.ObterPorId(View.Id);
                if (alimentoExistente != null)
                {
                    Progresso = await dialog.ShowProgressAsync(this, "Progresso", "Inativando alimento. Aguarde...");
                    Progresso.SetIndeterminate();
                    var t = Task.Factory.StartNew(() => { alimentoService.Inativar(View.Id, "Carlosg"); });
                    await t;
                    await Progresso?.CloseAsync();
                    await this.dialog.ShowMessageAsync(this, "Atenção", "Alimento inativado com sucesso !!!");
                    Limpar();
                    BuscarAlimentos();
                }
                else
                {
                    await this.dialog.ShowMessageAsync(this, "Atenção", string.Join("\r\n", alimentoExistente.Notificacoes.Select(s => s.Mensagem)));
                    alimentoService.LimparNotificacoes();
                }
            }
        }

        private void BuscarAlimentos()
        {
            ListaAlimento = alimentoService.ListarTodos().ToList();
        }

        private void Limpar()
        {
            View = new AlimentoView();
        }
    }
}
