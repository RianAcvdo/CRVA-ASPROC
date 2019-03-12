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
using System.Net;
//using System.Net.Mail;
using System.Data;
using EASendMail;
using System.Data.SqlClient;
using Agape.ReservacionVhl.BL;
using Agape.ReservacionVhl.EN;
using Agape.ReservacionVhl.WpfUI.Reportes;



namespace Agape.ReservacionVhl.WpfUI
{
    /// <summary>
    /// Lógica de interacción para RecuperarContraseña.xaml
    /// </summary>
    public partial class RecuperarContraseña : Window
    {
        MetodosBL _Accion = new MetodosBL();
        UserBL _UserBL = new UserBL();
        User _User = new User();
        public RecuperarContraseña()
        {
            InitializeComponent();
        }
        public string ContrasenaNueva(int longitud)
        {
            string caracteres = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < longitud--)
            {
                res.Append(caracteres[rnd.Next(caracteres.Length)]);
            }
            return res.ToString();
        }

        private void SendEmailPasword(string contrasenaNueva, string correo)
        {
            SmtpMail oMail = new SmtpMail("TryIt");
            SmtpClient oSmtp = new SmtpClient();
            oMail.From = "crva.dev@gmail.com";
            oMail.To = correo;
            oMail.Subject = "Recuperación de contraseña CRVA";
            oMail.TextBody = "Su solicitud de recuperación de contraseña de la cuenta: "+ correo +" se realizó correctamente. Su nueva contraseña es: " +contrasenaNueva;
            SmtpServer oServer = new SmtpServer("smtp.gmail.com");
            oServer.Port = 587;
            oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;
            #region credenciales
            oServer.User = "crva.dev@gmail.com";
            oServer.Password = "CRVA-12345";
            #endregion
            try
            {
                oSmtp.SendMail(oServer, oMail);
                MessageBox.Show("Su solicitud fue enviada a: " + correo);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al enviar correo electrónico: " + ex.Message);
            }
        }
        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                User _datoUsuario = DatosUsuarios();
                var verificar = from u in _Accion.User.GetAllData() where u.Email.ToUpper() == txtCorreoElectronico.Text.ToUpper() select u;
                if (verificar.Count() > 0)
                {
                    txtCorreoElectronico.Focus();
                    string Pass = ContrasenaNueva(8);
                    string email = txtCorreoElectronico.Text;
                    _User.Password = Pass;
                    _User.Email = email;
                    _UserBL.UpdatePassword2(_User);
                    SendEmailPasword(Pass, email);
                    LogIn Window = new LogIn();
                    this.Hide();
                    Window.ShowDialog();
                    Close();
                }
                else if (txtCorreoElectronico.Text == "")
                {
                    MessageBox.Show("Incerte un correo electrónico valido: ");
                }
                else
                {
                    MessageBox.Show("Incerte un correo electrónico valido: ");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al enviar correo electrónico: " + ex.Message);
            }
        }
        private User DatosUsuarios()
        {
            User _User = new User();
            _User.Email = txtCorreoElectronico.Text.ToUpper();
            return _User;
        }
        private void lbRegresar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            LogIn Window = new LogIn();
            this.Hide();
            Window.ShowDialog();
            Close();
        }
    }
}
