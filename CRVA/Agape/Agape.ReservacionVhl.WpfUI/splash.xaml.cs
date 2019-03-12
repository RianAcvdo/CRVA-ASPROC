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
using Agape.ReservacionVhl.WpfUI.Reportes;

namespace Agape.ReservacionVhl.WpfUI
{
    /// <summary>
    /// Lógica de interacción para splah.xaml
    /// </summary>
    public partial class splah : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        int porcentaje = 0;
        MetodosBL _Accion = new MetodosBL();
        public splah()
        { 
         InitializeComponent();

        //Configuracion  1seg (H,M,S)
        timer.Interval = new TimeSpan(0, 0, 1);

        timer.Tick += (s, a) =>
             {
                 // si a llegado 100%
                 if (porcentaje >= 100.00)
                 {
                     //menu
                     //MainMenuAdministracion _menu = new MainMenuAdministracion();
                     //Mostrar formulario menu 
                     //_menu.Show();
                     //Login
                     try
                     {
                         var _totalUsuarios = _Accion.User.GetAllData();
                         if (_totalUsuarios.Count == 0)
                         {
                             RegistroAdministrador _RegistroAdministrador = new RegistroAdministrador();
                             _RegistroAdministrador.Show();
                             Close();
                         }
                         else
                         {
                             LogIn _IniciarSesion = new LogIn();
                             _IniciarSesion.Show();
                             Close();
                         }
                     }
                     catch(Exception ex)
                     {
                         MessageBox.Show("Vuelva a intentar en otro momento" + ex.Message, "Error Configuración", MessageBoxButton.OK, MessageBoxImage.Stop);
                     }                     
                     //Detiene Timer
                     timer.Stop();
                     //Cerrar formulario Splah
                     this.Close();
                            }
                 else { //progreso sera de 25 
                     porcentaje += 25;
                 }
             };
            
            timer.Start();
            
        }
    }
}