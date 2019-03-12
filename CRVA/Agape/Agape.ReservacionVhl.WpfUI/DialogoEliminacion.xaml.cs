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
using Agape.ReservacionVhl.EN;
using Agape.ReservacionVhl.BL;

namespace Agape.ReservacionVhl.WpfUI
{
    /// <summary>
    /// Lógica de interacción para DialogoEliminacion.xaml
    /// </summary>
    public partial class DialogoEliminacion : Window
    {
        #region Variables para funcionamiento de formulario
        //carro a eliminar para el primer constructor
        public CarBL _CarBL = new CarBL();

        public Car _car = new Car();
        //empleado a eliminar para el segundo constructor
        public EmployeeBL _EmployeeBL = new EmployeeBL();

        public Employee _employee = new Employee();
        #endregion

        #region Constructores
        //Constructor 1 para vehículos
        public DialogoEliminacion(Car pCar)
        {
            _car = pCar;
            InitializeComponent();
        }
        //Constructor 1 para empleados
        public DialogoEliminacion(Employee pEmployee)
        {
            _employee = pEmployee;
            InitializeComponent();
        }
        #endregion

        #region Cargar Opciones y Verificar ventana

        #region Cargar Opciones
        public List<string> CargarOpciones()
        {
            //lista y opciones vacías
            List<string> _listaOpciones = new List<string>();
            string Opcion1 = "";
            string Opcion2 = "";
            string Opcion3 = "";
            string Opcion4 = "";

            //Si es eliminación de un vehículo
            if (SiVentanaAbierta<MantenimientoVehiculo>())
            {
                Opcion1 = "Mantenimiento planeado.";
                Opcion2 = "Mantenimiento no planeado.";
                Opcion3 = "Cambio de función.";
                Opcion4 = "Venta.";
            }
            //Si es eliminación de un empleado
            else if (SiVentanaAbierta<MantenimientoEmpleado>())
            {
                Opcion1 = "Despido.";
                Opcion2 = "Renuncia.";
                Opcion3 = "Muerte.";
                Opcion4 = "Jubilación.";
            }

            //llenamos y devolvemos la lista
            _listaOpciones.Add(Opcion1);
            _listaOpciones.Add(Opcion2);
            _listaOpciones.Add(Opcion3);
            _listaOpciones.Add(Opcion4);
            return _listaOpciones;
        }
        #endregion

        #region Verificar ventana
        public static bool SiVentanaAbierta<T>(string nombre = "") where T : Window
        {
            return string.IsNullOrEmpty(nombre)
               ? Application.Current.Windows.OfType<T>().Any()
               : Application.Current.Windows.OfType<T>().Any(w => w.Name.Equals(nombre));
        }
        #endregion

        #endregion

        #region Eventos

        #region load
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                List<string> ListaOpciones = CargarOpciones();
                rbOpción1.Content = ListaOpciones.ElementAt(0);
                rbOpción2.Content = ListaOpciones.ElementAt(1);
                rbOpción3.Content = ListaOpciones.ElementAt(2);
                rbOpción4.Content = ListaOpciones.ElementAt(3);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Algo anda mal : " + ex.Message, "Error CRVA-EL", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region aceptar
        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            try
            {          
                #region si es vehículo
                if (SiVentanaAbierta<MantenimientoVehiculo>())
                {
                    #region validación Radio butons 

                    if (rbOpción1.IsChecked == true)
                    {
                        _car.StateDetail = rbOpción1.Content.ToString().ToUpper();
                        _car.State = false;
                        _CarBL.UpdateCar(_car);
                        this.Close();
                    }
                    else if (rbOpción2.IsChecked == true)
                    {
                        _car.StateDetail = rbOpción2.Content.ToString().ToUpper();
                        _car.State = false;
                        _CarBL.UpdateCar(_car);
                        this.Close();
                    }
                    else if (rbOpción3.IsChecked == true)
                    {
                        _car.StateDetail = rbOpción3.Content.ToString().ToUpper();
                        _car.State = false;
                        _CarBL.UpdateCar(_car);
                        this.Close();
                    }
                    else if (rbOpción4.IsChecked == true)
                    {
                        _car.StateDetail = rbOpción4.Content.ToString().ToUpper();
                        _car.State = false;
                        _CarBL.UpdateCar(_car);
                        this.Close();
                    }
                    else if (rbOpciónOtro.IsChecked == true)
                    {
                        #region validación opción otro
                        if (txtDetalle.Text.Replace(" ","").All(char.IsLetter) && txtDetalle.Text.Length > 1)
                        {
                            _car.StateDetail = txtDetalle.Text.ToUpper();
                            _car.State = false;
                            _CarBL.UpdateCar(_car);
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Caracteres inválidos. ", "Alerta CRVA-EAE", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        #endregion
                    }
                    else
                    {
                        MessageBox.Show("Selecciona un Item.", "Alerta CRVA-EAE", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    #endregion
                }
                #endregion

                #region si es empleado
                else if (SiVentanaAbierta<MantenimientoEmpleado>())
                {
                    #region validación radio buttons
                    if (rbOpción1.IsChecked == true)
                    {
                        _employee.StateDetail = rbOpción1.Content.ToString().ToUpper();
                        _employee.State = false;
                        _EmployeeBL.UpdateItem(_employee);
                        
                        this.Close();
                    }
                    else if (rbOpción2.IsChecked == true)
                    {
                        _employee.StateDetail = rbOpción2.Content.ToString().ToUpper();
                        _employee.State = false;
                        _EmployeeBL.UpdateItem(_employee);
                        this.Close();
                    }
                    else if (rbOpción3.IsChecked == true)
                    {
                        _employee.StateDetail = rbOpción3.Content.ToString().ToUpper();
                        _employee.State = false;
                        _EmployeeBL.UpdateItem(_employee);
                        this.Close();
                    }
                    else if (rbOpción4.IsChecked == true)
                    {
                        _employee.StateDetail = rbOpción4.Content.ToString().ToUpper();
                        _employee.State = false;
                        _EmployeeBL.UpdateItem(_employee);
                        this.Close();
                    }
                    else if (rbOpciónOtro.IsChecked == true)
                    {
                        #region validación opción otro
                        if (txtDetalle.Text.Replace(" ","").All(char.IsLetter) && txtDetalle.Text.Length > 1)
                        {
                            _employee.StateDetail = txtDetalle.Text.ToUpper();
                            _employee.State = false;
                            _EmployeeBL.UpdateItem(_employee);
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Caracteres inválidos. ", "Alerta CRVA-EAE", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        #endregion
                    }
                    #endregion
                }
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show("Algo anda mal : " + ex.Message, "Error CRVA-EAE", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region Regresar
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #endregion

        #endregion
    }
}
