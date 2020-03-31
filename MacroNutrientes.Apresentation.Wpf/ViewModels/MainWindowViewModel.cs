using MacroNutrientes.Apresentation.Wpf.Views;
using MahApps.Metro.Controls.Dialogs;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Windows;

namespace MacroNutrientes.Apresentation.Wpf.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        IDialogCoordinator dialog;
        public DelegateCommand SairCommand { get; set; }
        public DelegateCommand FecharMenuCommand { get; set; }
        public DelegateCommand AbrirMenuCommand { get; set; }
        public DelegateCommand CadastrarAlimentosCommand { get; set; }
        public DelegateCommand RegistrarRefeicoesCommand { get; set; }
            
        private bool _visivel = true;
        public bool Visivel
        {
            get { return _visivel; }
            set
            {
                SetProperty(ref _visivel, value);
            }
        }

        private string _visibilidadeAbrir = "Visible";
        public string VisibilidadeAbrir
        {
            get { return _visibilidadeAbrir; }
            set
            {
                SetProperty(ref _visibilidadeAbrir, value);
            }
        }

        private string _visibilidadeFechar = "Collapsed";
        public string VisibilidadeFechar
        {
            get { return _visibilidadeFechar; }
            set
            {
                SetProperty(ref _visibilidadeFechar, value);
            }
        }

        public MainWindowViewModel(IDialogCoordinator dialog)
        {
            this.dialog = dialog;
            AbrirMenuCommand = new DelegateCommand(AbrirFecharMenu, () => Visivel).ObservesProperty(() => Visivel);
            FecharMenuCommand = new DelegateCommand(AbrirFecharMenu, () => !Visivel).ObservesProperty(() => Visivel);
            CadastrarAlimentosCommand = new DelegateCommand(ChamarCadastroAlimentos);
            RegistrarRefeicoesCommand = new DelegateCommand(ChamarRegistroRefeicoes);
            SairCommand = new DelegateCommand(Sair);
        }

        private void AbrirFecharMenu()
        {
            VisibilidadeAbrir = Visivel ? "Collapsed" : "Visible";
            VisibilidadeFechar = Visivel ? "Visible" : "Collapsed";
            Visivel = !Visivel;
        }

        private void ChamarCadastroAlimentos()
        {   
            AlimentosWindow novaJanela = new AlimentosWindow();
            novaJanela.ShowDialog();
        }

        private void ChamarRegistroRefeicoes()
        {
            RefeicoesWindow novaJanela = new RefeicoesWindow();
            novaJanela.ShowDialog();
        }

        private void Sair()
        {
            Application.Current.Shutdown();
        }
    }
}
