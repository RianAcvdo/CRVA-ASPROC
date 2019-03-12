using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Agape.ReservacionVhl.WpfUI.Reportes;
using Agape.ReservacionVhl.WpfUI.Properties;

namespace Agape.ReservacionVhl.WpfUI
{
    /// <summary>
    /// Lógica de interacción para ReportesWindow.xaml
    /// </summary>
    public partial class ReportesWindow : Window
    {
        public ReportesWindow()
        {
            InitializeComponent();
            Loaded += ReportesWindow_Loaded;
        }

        private void ReportesWindow_Loaded(object sender, RoutedEventArgs e)
        {
            PagerContainer.NavigationService.Navigate(new Reportes.ReporteGeneral());
        }

        private void btnMenuPrincipal_Click(object sender, RoutedEventArgs e)
        {
            MainMenuAdministracion _window = new MainMenuAdministracion();
            _window.Show();
            Close();
        }
    }
}
