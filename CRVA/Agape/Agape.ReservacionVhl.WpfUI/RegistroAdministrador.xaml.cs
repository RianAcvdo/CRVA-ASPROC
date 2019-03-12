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
using Agape.ReservacionVhl.EN;
using System.Text.RegularExpressions;
using Agape.ReservacionVhl.WpfUI.Properties;

namespace Agape.ReservacionVhl.WpfUI
{
    /// <summary>
    /// Lógica de interacción para RegistroAdministrador.xaml
    /// </summary>
    public partial class RegistroAdministrador : Window
    {
        #region Variables y Constructor
        MetodosBL _Accion = new MetodosBL();
        public RegistroAdministrador()
        {
            InitializeComponent();
        }
        #endregion

        #region Evento Registrar
        private void btnRegistrar_Click(object sender, RoutedEventArgs e)
        {
            txtNombre.Focus();
            try
            {
                #region validación general
                if (txtNombre.Text.Trim() != string.Empty && txtApellido.Text.Trim() != string.Empty && txtPassword.Password.Trim() != string.Empty && txtPasswordConfirmar.Password.Trim() != string.Empty && txtEmail.Text.Trim() != string.Empty)
                {

                    #region Validaciones
                    if (ValidarUsusario() == 6)
                    {
                        User _datoUsuario = DatosUsuarios();
                        Settings.Default["UsuarioEmail"] = txtEmail.Text.Trim();
                        Settings.Default["NombreUsuario"] = "ADMINISTRADOR";
                        Settings.Default.Save();

                        _Accion.User.Create(_datoUsuario);
                        MainMenuAdministracion _window = new MainMenuAdministracion();
                        _window.Show();
                        Close();
                    }
                    else
                    {
                        ValidarUsusario();
                    }
                    #endregion

                }
                else
                {
                    ValidarUsusario();
                }
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se logro registrar el administrador : " + ex.Message + ", Vuelva a intentarlo en otro momemnto", "Error CRVA-AG", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region Evento load
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtNombre.Focus();
        }
        #endregion


        #region Metodo DatosUsuario()
        private User DatosUsuarios()
        {
            User _User = new User();
            _User.Name = txtNombre.Text.ToUpper();
            _User.LastName = txtApellido.Text.ToUpper();
            _User.UserName = "ADMINISTRADOR";
            _User.Password = txtPasswordConfirmar.Password;
            _User.Email = txtEmail.Text.ToLower();
            _User.State = true;
            return _User;
        }
        #endregion

        #region Validar Usuario
        private int ValidarUsusario()
        {
            int Contador = 1;

            Brush rojo = new SolidColorBrush( Colors.Red);
            Brush verde = new SolidColorBrush(Colors.DarkGreen);
            Brush naranja = new SolidColorBrush(Colors.DarkOrange);

            #region msj Nombre
            if (txtNombre.Text.Trim() != string.Empty)
            {

                if (!txtNombre.Text.Replace(" ", "").All(char.IsLetter))
                {
                    msjNombre.Foreground = new SolidColorBrush(Colors.Red);
                    msjNombre.Visibility = Visibility.Visible;
                    txtNombre.Focus();
                }
                else
                {
                    msjNombre.Foreground = new SolidColorBrush(Colors.DarkGreen);
                    msjNombre.Visibility = Visibility.Visible;
                    Contador = Contador + 1;
                }
            }
            else
            {
                msjNombre.Foreground = new SolidColorBrush(Colors.DarkOrange);
                msjNombre.Visibility = Visibility.Visible;
                txtNombre.Focus();
            }
            #endregion

            #region msj apellido
            if (txtApellido.Text.Trim() != string.Empty)
            {
                if (!txtApellido.Text.Replace(" ", "").All(char.IsLetter))
                {
                    msjApellido.Foreground = new SolidColorBrush(Colors.Red);
                    msjApellido.Visibility = Visibility.Visible;
                    #region Focus donde debe
                    if (Brush.Equals(msjNombre.Foreground, rojo) || Brush.Equals(msjNombre.Foreground,naranja))
                    {
                        txtNombre.Focus();
                    }
                    else
                    {
                        txtApellido.Focus();
                    }
                    #endregion

                }
                else
                {
                    msjApellido.Foreground = new SolidColorBrush(Colors.DarkGreen);
                    msjApellido.Visibility = Visibility.Visible;
                    Contador = Contador + 1;
                }
            }
            else
            {
                msjApellido.Foreground = new SolidColorBrush(Colors.DarkOrange);
                msjApellido.Visibility = Visibility.Visible;
                if (Brush.Equals(msjNombre.Foreground, rojo) || Brush.Equals(msjNombre.Foreground, naranja))
                {
                    txtNombre.Focus();
                }
                else
                {
                    txtApellido.Focus();
                }
            }
            #endregion

            #region msj email
            if (txtEmail.Text.Trim() != string.Empty)
            {
                if (!Regex.IsMatch(txtEmail.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
                {
                    msjEmail.Foreground = new SolidColorBrush(Colors.Red);
                    msjEmail.Visibility = Visibility.Visible;
                    txtEmail.Focus();
                }
                else
                {
                    msjEmail.Foreground = new SolidColorBrush(Colors.DarkGreen);
                    msjEmail.Visibility = Visibility.Visible;
                    Contador = Contador + 1;
                }
            }
            else
            {
                msjEmail.Foreground = new SolidColorBrush(Colors.DarkOrange);
                msjEmail.Visibility = Visibility.Visible;
                txtEmail.Focus();
            }
            #endregion

            #region msjPassword

            if (txtPassword.Password.Trim() == string.Empty)
            {
                msjPassword.Foreground = new SolidColorBrush(Colors.DarkOrange);
                msjPassword.Visibility = Visibility.Visible;
                txtPassword.Focus();
            }
            else
            {
                if (txtPassword.Password.Length >= 8
                    && txtPassword.Password.Any(char.IsSymbol) || txtPassword.Password.Any(char.IsPunctuation) 
                    && txtPassword.Password.Any(char.IsNumber)
                    && txtPassword.Password.Any(char.IsLetter))
                {
                    msjPassword.Foreground = new SolidColorBrush(Colors.DarkGreen);
                    msjPassword.Visibility = Visibility.Visible;
                    Contador = Contador + 1;
                }
                else
                {
                    msjPassword.Foreground = new SolidColorBrush(Colors.Red);
                    msjPassword.Visibility = Visibility.Visible;
                    txtPassword.Focus();
                    MessageBox.Show("Su contraseña debe contener almenos 8 caracteres, una letra, un número y un simbolo","Contraseña incorrecta",MessageBoxButton.OK,MessageBoxImage.Exclamation);
                }
            }
            #endregion

            #region msjPasswordConfirmar
            if (txtPasswordConfirmar.Password != string.Empty)
            {
                if (txtPasswordConfirmar.Password != txtPassword.Password)
                {

                    msjPasswordConfirmar.Foreground = new SolidColorBrush(Colors.Red);
                    msjPasswordConfirmar.Visibility = Visibility.Visible;
                    msjPassword.Foreground = new SolidColorBrush(Colors.Red);
                    msjPassword.Visibility = Visibility.Visible;
                    txtPassword.Focus();
                }
                else
                {
                    if (txtPassword.Password.Length >= 8
                    && txtPassword.Password.Any(char.IsSymbol) || txtPassword.Password.Any(char.IsPunctuation)
                    && txtPassword.Password.Any(char.IsNumber)
                    && txtPassword.Password.Any(char.IsLetter))
                    {
                        msjPasswordConfirmar.Foreground = new SolidColorBrush(Colors.DarkGreen);
                        msjPasswordConfirmar.Visibility = Visibility.Visible;
                        msjPassword.Foreground = new SolidColorBrush(Colors.DarkGreen);
                        msjPassword.Visibility = Visibility.Visible;
                        Contador = Contador + 1;
                    }
                    else
                    {
                        msjPasswordConfirmar.Foreground = new SolidColorBrush(Colors.Red);
                        msjPasswordConfirmar.Visibility = Visibility.Visible;
                        msjPassword.Foreground = new SolidColorBrush(Colors.Red);
                        msjPassword.Visibility = Visibility.Visible;
                        txtPassword.Focus();
                        
                    }
                }
            }
            else
            {
                msjPasswordConfirmar.Foreground = new SolidColorBrush(Colors.DarkOrange);
                msjPasswordConfirmar.Visibility = Visibility.Visible;
                txtPassword.Focus();
            }

            #endregion

            return Contador;
        }
        #endregion

        //Metodo que valida y muestra icono de error los textboxes de contraseña

        #region metodo PasswordInvalids()

        private void PasswordInvalids()
        {
            txtPassword.Focus();
            txtPassword.Clear();
            txtPasswordConfirmar.Clear();
            msjPassword.Visibility = Visibility.Visible;
            msjPassword.Foreground = new SolidColorBrush(Colors.DarkRed);
            msjPasswordConfirmar.Visibility = Visibility.Visible;
            msjPasswordConfirmar.Foreground = new SolidColorBrush(Colors.DarkRed);
        }
        #endregion

        //Metodo que valida y muestra icono de error el textbox de email
        #region metodo EmailInvalid()
        private void EmailInvalid()
        {
            txtEmail.Focus();
            msjEmail.Foreground = new SolidColorBrush(Colors.DarkRed);
            msjEmail.Visibility = Visibility.Visible;
            msjEmail.ToolTip = "Ingrese un correo electrónico valido";
        }
        #endregion  

    }
}
