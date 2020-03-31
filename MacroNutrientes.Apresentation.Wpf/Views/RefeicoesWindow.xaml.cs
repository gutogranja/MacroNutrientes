using MacroNutrientes.Apresentation.Wpf.ViewModels;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace MacroNutrientes.Apresentation.Wpf.Views
{
    /// <summary>
    /// Lógica interna para RefeicoesWindow.xaml
    /// </summary>
    public partial class RefeicoesWindow : MetroWindow
    {
        public RefeicoesWindow()
        {
            InitializeComponent();
            this.DataContext = new RefeicoesWindowViewModel(DialogCoordinator.Instance);
        }
    }
}
