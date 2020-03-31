using MacroNutrientes.Apresentation.Wpf.ViewModels;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace MacroNutrientes.Apresentation.Wpf.Views
{
    /// <summary>
    /// Lógica interna para alimentosWindow.xaml
    /// </summary>
    public partial class AlimentosWindow : MetroWindow  
    {
        public AlimentosWindow()    
        {
            InitializeComponent();
            this.DataContext = new AlimentosWindowViewModel(DialogCoordinator.Instance);
        }
    }
}
