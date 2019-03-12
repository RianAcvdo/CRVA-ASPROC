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
using Agape.ReservacionVhl.BL;
using Agape.ReservacionVhl.EN;
using Agape.ReservacionVhl.WpfUI.Properties;

namespace Agape.ReservacionVhl.WpfUI
{
    /// <summary>
    /// Lógica de interacción para NuevaContraseña.xaml
    /// </summary>
    public partial class NuevaContraseña : Window
    {
        MetodosBL _Accion = new MetodosBL();
        UserBL _UserBL = new UserBL();
        User _User = new User();
        public NuevaContraseña()
        {
            InitializeComponent();
        }
        private void lbRegresar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MainMenuAdministracion Window = new MainMenuAdministracion();
            this.Hide();
            Window.ShowDialog();
            Close();
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtCodigo.Text == "" || txtUserName.Text == "" || txtContraseña.Password =="" || txtContraseña2.Password =="")
                {
                    MessageBox.Show("No deje campos vacíos : ", "Error CRVA-UG", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                User _datoUsuario = DatosUsuarios();
                var verificar = from u in _Accion.User.GetAllData() where u.Password.ToUpper() == txtCodigo.Text.ToUpper() && u.UserName.ToUpper() == txtUserName.Text.ToUpper() select u;
                if (verificar.Count() > 0)
                {
                    if (txtContraseña2.Password != txtContraseña.Password)
                    {
                        MessageBox.Show("Las contraseñas no coinciden.", "Error CRVA-UG", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        txtCodigo.Focus();
                        _User.Password = txtContraseña2.Password;
                        _User.UserName = txtUserName.Text;
                        _UserBL.UpdatePassword(_User);
                        MessageBox.Show("Contraseña modificada correctamente.", "CRVA-UG", MessageBoxButton.OK, MessageBoxImage.Information);
                        MainMenuAdministracion Window = new MainMenuAdministracion();
                        this.Hide();
                        Window.ShowDialog();
                        Close();
                    }
                }
                else
                {
                    MessageBox.Show("Password y user name no coinciden.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Algo anda mal : " + ex.Message, "Error CRVA-UG", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private User DatosUsuarios()
        {
            User _User = new User();
            _User.Password = txtCodigo.Text;
            _User.UserName = txtUserName.Text.ToUpper();
            return _User;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtUserName.Text = Settings.Default["NombreUsuario"].ToString();
            txtUserName.IsEnabled = false;
        }
    }
}
