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
using System.Windows.Threading;
using Agape.ReservacionVhl.EN;
using Agape.ReservacionVhl.BL;
using Agape.ReservacionVhl.WpfUI.Reportes;
using System.Text.RegularExpressions;
using Agape.ReservacionVhl.WpfUI.Properties;

namespace Agape.ReservacionVhl.WpfUI
{
    /// <summary>
    /// Lógica de interacción para LogIn.xaml
    /// </summary>
    public partial class LogIn : Window
    {
        MetodosBL _Accion = new MetodosBL();
        public LogIn(User pUser=null)
        {
            InitializeComponent();
            txtEmail.Focus();
        }



        private void lbOlvidocontraseña_MouseDown(object sender, MouseButtonEventArgs e)
        {
            RecuperarContraseña Window = new RecuperarContraseña();
            this.Hide();
            Window.ShowDialog();
            Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtEmail.Text.Trim() != string.Empty && txtPassword.Password.ToString().Trim() != string.Empty)
                {
                    if (!Regex.IsMatch(txtEmail.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
                    {
                        msjEmail.Text = "Ingrese un correo electrónico valido";
                        msjEmail.Visibility = Visibility.Visible;
                        msjEmailLateral.Foreground = new SolidColorBrush(Colors.Red);
                        msjEmailLateral.Visibility = Visibility.Visible;
                        msjPassword.Visibility = Visibility.Hidden;
                        msjPasswordLateral.Visibility = Visibility.Hidden;
                    }
                    else
                    {
                        User _usuario = new User();
                        _usuario.Email = txtEmail.Text;
                        var verificacion = _Accion.User.Login(_usuario);
                        if (verificacion != null)
                        {
                            if (verificacion.Password == txtPassword.Password.ToString())
                            {
                                Settings.Default["UsuarioEmail"] = verificacion.Email.ToString().Trim();
                                Settings.Default["NombreUsuario"] = verificacion.UserName.ToString().Trim();
                                Settings.Default.Save();
                                MainMenuAdministracion _AdministradorWindow = new MainMenuAdministracion();
                                _AdministradorWindow.Show();
                                this.Close();
                            }
                            else //la contraseña no coincidio
                            {
                                //txtPassword.Foreground = new SolidColorBrush(Colors.DarkRed);
                                txtPassword.Focus();
                                msjEmail.Visibility = Visibility.Hidden;
                                //msjEmailLateral.Foreground = new SolidColorBrush(Colors.Red);
                                msjEmailLateral.Visibility = Visibility.Hidden;
                                msjPassword.Visibility = Visibility.Visible;
                                msjPasswordLateral.Foreground = new SolidColorBrush(Colors.Red);
                                msjPasswordLateral.Visibility = Visibility.Visible;
                            }
                        }
                        else //El usuario no existe
                        {
                            //txtEmail.Foreground = new SolidColorBrush(Colors.DarkRed);
                            msjEmail.Text = "El usuario no existe en el sistema";
                            msjEmail.Visibility = Visibility.Visible;
                            msjEmailLateral.Foreground = new SolidColorBrush(Colors.Red);
                            msjEmailLateral.Visibility = Visibility.Visible;
                            msjPassword.Visibility = Visibility.Hidden;
                            //msjPasswordLateral.Foreground = new SolidColorBrush(Colors.Red);
                            msjPasswordLateral.Visibility = Visibility.Hidden;
                            txtEmail.Focus();
                            txtPassword.Clear();
                        }
                    }
                }
                else if (txtEmail.Text.Trim() == string.Empty)
                {
                    //txtEmail.Foreground = new SolidColorBrush(Colors.DarkRed);
                    msjEmail.Visibility = Visibility.Hidden;
                    msjPassword.Visibility = Visibility.Hidden;

                    msjEmailLateral.Foreground = new SolidColorBrush(Colors.DarkOrange);
                    msjEmailLateral.Visibility = Visibility.Visible;
                    txtEmail.Focus();
                    if(txtPassword.Password == string.Empty)
                    {
                        //txtPassword.Foreground = new SolidColorBrush(Colors.DarkRed);
                        msjPasswordLateral.Foreground = new SolidColorBrush(Colors.DarkOrange);
                        msjPasswordLateral.Visibility = Visibility.Visible;
                    }
                }
                else
                {
                    //txtPassword.Foreground = new SolidColorBrush(Colors.DarkRed);
                    msjEmail.Visibility = Visibility.Hidden;
                    msjPassword.Visibility = Visibility.Hidden;
                    msjPasswordLateral.Foreground = new SolidColorBrush(Colors.DarkOrange);
                    msjPasswordLateral.Visibility = Visibility.Visible;
                    txtPassword.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al iniciar sesión" + ex.Message, "Error CRVA-Login");
            }
        }
        private void txtEmail_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = (txtPassword.Password.Length >= 8
                    && txtPassword.Password.Any(char.IsSymbol) || txtPassword.Password.Any(char.IsPunctuation)
                    && txtPassword.Password.Any(char.IsNumber)
                    && txtPassword.Password.Any(char.IsLetter));
        }

        private void frmLogIn_Loaded(object sender, RoutedEventArgs e)
        {
            txtEmail.Text = Settings.Default["UsuarioEmail"].ToString();
        }
    }
}

