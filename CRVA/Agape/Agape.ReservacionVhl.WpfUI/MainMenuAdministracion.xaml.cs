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
using System.Text.RegularExpressions;
using Agape.ReservacionVhl.WpfUI.Reportes;

namespace Agape.ReservacionVhl.WpfUI
{
    /// <summary>
    /// Lógica de interacción para MainMenuAdministracion.xaml
    /// </summary>
    public partial class MainMenuAdministracion : Window
    {
        public MainMenuAdministracion()
        {
            InitializeComponent();
        }

        #region Navegabilidad
        private void btnBackup_Click(object sender, RoutedEventArgs e)
        {
            BackupOptions Window = new BackupOptions();
            //this.Hide();
            Window.ShowDialog();
            //Close();
        }
        private void btnMenuPrincipalAdministracion_Click(object sender, RoutedEventArgs e)
        {
            MainMenuAdministracion Window = new MainMenuAdministracion();
            this.Hide();
            Window.ShowDialog();
            Close();
        }

        private void btnMantUsuario_Click(object sender, RoutedEventArgs e)
        {
            MantenimientoUsuario Window = new MantenimientoUsuario();
            this.Hide();
            Window.ShowDialog();
            Close();
        }

        private void btnMantReservacion_Click(object sender, RoutedEventArgs e)
        {
            MantenimientoReservacion Window = new MantenimientoReservacion();
            this.Hide();
            Window.ShowDialog();
            Close();
        }

        private void btnMantOficinas_Click(object sender, RoutedEventArgs e)
        {
            MantenimientoOficinas Window = new MantenimientoOficinas();
            this.Hide();
            Window.ShowDialog();
            Close();
        }

        private void btnMantEmpleados_Click(object sender, RoutedEventArgs e)
        {
            MantenimientoEmpleado Window = new MantenimientoEmpleado();
            this.Hide();
            Window.ShowDialog();
            Close();
        }

        private void btnMantVehiculos_Click(object sender, RoutedEventArgs e)
        {
            MantenimientoVehiculo Window = new MantenimientoVehiculo();
            this.Hide();
            Window.ShowDialog();
            Close();
        }

        private void btnRestUsuario_Click(object sender, RoutedEventArgs e)
        {
            RestauracionUsuario Window = new RestauracionUsuario();
            this.Hide();
            Window.ShowDialog();
            Close();
        }

        private void btnRestReservacion_Click(object sender, RoutedEventArgs e)
        {
            RestauracionReservacion Window = new RestauracionReservacion();
            this.Hide();
            Window.ShowDialog();
            Close();
        }

        private void btnRestOficinas_Click(object sender, RoutedEventArgs e)
        {
            RestauracionOficina Window = new RestauracionOficina();
            this.Hide();
            Window.ShowDialog();
            Close();
        }

        private void btnRestEmpleados_Click(object sender, RoutedEventArgs e)
        {
            RestauracionEmpleado Window = new RestauracionEmpleado();
            this.Hide();
            Window.ShowDialog();
            Close();
        }

        private void btnRestVehiculos_Click(object sender, RoutedEventArgs e)
        {
            RestauracionVehiculo Window = new RestauracionVehiculo();
            this.Hide();
            Window.ShowDialog();
            Close();
        }

        private void btnGenerarReportes_Click(object sender, RoutedEventArgs e)
        {
            Reportes.WindowReports window = new WindowReports(1);
            window.Show();
            //ReportesWindow _window = new ReportesWindow();
            //_window.Show();
            Close();
        }

        private void btnMantMarcas_Click(object sender, RoutedEventArgs e)
        {
            MantenimientoMarca Window = new MantenimientoMarca();
            this.Hide();
            Window.ShowDialog();
            Close();
        }

        private void btnRestMarcas_Click(object sender, RoutedEventArgs e)
        {
            RestauracionMarca Window = new RestauracionMarca();
            this.Hide();
            Window.ShowDialog();
            Close();
        }
        #endregion

        private void btnLogOut_Click(object sender, RoutedEventArgs e)
        {
            LogIn _window = new LogIn();
            _window.Show();
            Close();
        }

        private void btnCambiarPassword_Click(object sender, RoutedEventArgs e)
        {
            NuevaContraseña Window = new NuevaContraseña();
            this.Hide();
            Window.ShowDialog();
            Close();
        }

        private void btnReportCase_Click(object sender, RoutedEventArgs e)
        {
            Reportes.WindowReports window = new WindowReports(2);
            window.Show();
            //ReportesWindow _window = new ReportesWindow();
            //_window.Show();
            Close();
        }
    }
}
