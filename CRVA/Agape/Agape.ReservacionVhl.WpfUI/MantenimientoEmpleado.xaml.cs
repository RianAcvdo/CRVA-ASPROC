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
using System.Text.RegularExpressions;
using PagedList;

namespace Agape.ReservacionVhl.WpfUI
{
    /// <summary>
    /// Lógica de interacción para MantenimientoEmpleado.xaml
    /// </summary>
    /// 


    public partial class MantenimientoEmpleado : Window
    {
        EmployeeBL _EmployeeBL = new EmployeeBL();
        Employee _Employee = new Employee();
        OfficeBL _OfficeBL = new OfficeBL();
        Office _Office = new Office();
        MetodosBL _Accion = new MetodosBL();
        Int64 ID = 0;
        string Code;
        DateTime Date;
        public MantenimientoEmpleado()
        {
            InitializeComponent();
        }

        #region Navegabilidad
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

        public string CrearCode(int longitud)
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

        private async void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtNombre.Text == "" || txtApellido.Text == "" || txtFecha.Text == "" || txtSalario.Text == "" || cmbOficina.Text == "")
                {
                    MessageBox.Show("No dejar campos vacíos");
                }
                else if (txtNombre.Text == string.Empty)
                {
                    txtNombre.Focus();
                }
                else if (txtApellido.Text == string.Empty)
                {
                    txtApellido.Focus();
                }
                else if (txtApellido.Text == string.Empty)
                {
                    txtApellido.Focus();
                }
                else if (txtSalario.Text == string.Empty)
                {
                    txtSalario.Focus();
                }
                else
                {
                    DateTime ThisDay = new DateTime();
                    ThisDay = Convert.ToDateTime(txtFecha.Text);
                    _Employee.Code = CrearCode(10);
                    _Employee.Name = txtNombre.Text.ToUpper();
                    _Employee.LastName = txtApellido.Text.ToUpper();
                    _Employee.DateBirth = Convert.ToDateTime(ThisDay.ToString("d"));
                    _Employee.Date = DateTime.Now;
                    _Employee.Salary = Convert.ToDecimal(txtSalario.Text);
                    _Employee.OfficeID = Convert.ToInt64(cmbOficina.SelectedValue);
                    _Employee.State = true;
                    _EmployeeBL.Create(_Employee);
                    //dgvEmpleado.ItemsSource = _EmployeeBL.GetAll();
                    Limpiar();
                    EstadoBotones(0);
                    MessageBox.Show("Registro guardado exitosamente", "Exito CRVA-EG", MessageBoxButton.OK, MessageBoxImage.Information);
                    //lista a paginar
                    list = await GetPagedListAsync();
                    //Determinamos el estado de los botones
                    btnPrevius.IsEnabled = list.HasPreviousPage;
                    btnSiguiente.IsEnabled = list.HasNextPage;
                    //Cargamos la lista al DataGrid
                    dgvEmpleado.ItemsSource = list.ToList();
                    //Establecemos el numero de paginas del DataGrid
                    tbPaginacion.Text = string.Format("Pagina {0}/{1}", pageNumber, list.PageCount);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Algo anda mal: " + ex.Message, "Error CRVA-EG", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtNombre.Text == "" || txtApellido.Text == "" || txtFecha.Text == "" || txtSalario.Text == "" || ID <= 0)
                {
                    MessageBox.Show("Selecciona un Item");
                }
                else if (txtNombre.Text == string.Empty)
                {
                    txtNombre.Focus();
                }
                else if (txtApellido.Text == string.Empty)
                {
                    txtApellido.Focus();
                }
                else if (txtApellido.Text == string.Empty)
                {
                    txtApellido.Focus();
                }
                else if (txtSalario.Text == string.Empty)
                {
                    txtSalario.Focus();
                }
                if (txtNombre.Text != "" && txtApellido.Text != "" && txtFecha.Text != "" && txtSalario.Text != "" && cmbOficina.SelectedValue == null && ID > 0)
                {
                    DateTime ThisDay = new DateTime();
                    ThisDay = Convert.ToDateTime(txtFecha.Text);
                    _Employee.Code = Code;
                    _Employee.Name = txtNombre.Text.ToUpper();
                    _Employee.LastName = txtApellido.Text.ToUpper();
                    _Employee.DateBirth = Convert.ToDateTime(ThisDay.ToString("d"));
                    _Employee.Date = Date;
                    _Employee.Salary = Convert.ToDecimal(txtSalario.Text);
                    _Employee.OfficeID = Convert.ToInt64(cmbOficina.SelectedValue);
                    _Employee.State = true;
                    _Employee.EmployeeID = ID;
                    _EmployeeBL.UpdateItem(_Employee);
                    dgvEmpleado.ItemsSource = _EmployeeBL.GetAll();
                    Limpiar();
                    EstadoBotones(0);
                    MantenimientoEmpleado _window = new MantenimientoEmpleado();
                    _window.Show();
                    Close();
                }
                else if (txtNombre.Text != "" && txtApellido.Text != "" && txtFecha.Text != "" && txtSalario.Text != "" && cmbOficina.SelectedValue != null && ID > 0)
                {
                    DateTime ThisDay = new DateTime();
                    ThisDay = Convert.ToDateTime(txtFecha.Text);
                    _Employee.Code = Code;
                    _Employee.Name = txtNombre.Text.ToUpper();
                    _Employee.LastName = txtApellido.Text.ToUpper();
                    _Employee.DateBirth = Convert.ToDateTime(ThisDay.ToString("d"));
                    _Employee.Date = Date;
                    _Employee.Salary = Convert.ToDecimal(txtSalario.Text);
                    _Employee.OfficeID = Convert.ToInt64(cmbOficina.SelectedValue);
                    _Employee.State = true;
                    _Employee.EmployeeID = ID;
                    _EmployeeBL.UpdateItem(_Employee);
                    dgvEmpleado.ItemsSource = _EmployeeBL.GetAll();
                    Limpiar();
                    EstadoBotones(0);
                    MantenimientoEmpleado _window = new MantenimientoEmpleado();
                    _window.Show();
                    Close();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Algo anda mal: " + ex.Message, "Error CRVA-EM", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private async void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtNombre.Text.ToUpper().Trim() != "" || txtApellido.Text != "" || txtFecha.Text != "" || txtSalario.Text != "")
                {
                    //if (MessageBox.Show("Seguro que desea eliminar?", "Eliminar", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    //{
                    DateTime ThisDay = new DateTime();
                    ThisDay = Convert.ToDateTime(txtFecha.Text);
                    _Employee.Code = Code;
                    _Employee.Name = txtNombre.Text.ToUpper();
                    _Employee.LastName = txtApellido.Text.ToUpper();
                    _Employee.DateBirth = Convert.ToDateTime(ThisDay.ToString("d"));
                    _Employee.Date = Date;
                    _Employee.Salary = Convert.ToDecimal(txtSalario.Text);
                    _Employee.OfficeID = Convert.ToInt64(cmbOficina.SelectedValue);
                    _Employee.EmployeeID = ID;

                    DialogoEliminacion dialogo = new DialogoEliminacion(_Employee);
                    dialogo.ShowDialog();

                    Limpiar();
                    EstadoBotones(0);

                    //lista a paginar
                    list = await GetPagedListAsync();
                    //Determinamos el estado de los botones
                    btnPrevius.IsEnabled = list.HasPreviousPage;
                    btnSiguiente.IsEnabled = list.HasNextPage;
                    //Cargamos la lista al DataGrid
                    dgvEmpleado.ItemsSource = list.ToList();
                    //Establecemos el numero de paginas del DataGrid
                    tbPaginacion.Text = string.Format("Pagina {0}/{1}", pageNumber, list.PageCount);
                    //}
                }
                else if (txtNombre.Text.ToUpper().Trim() == "" || txtApellido.Text == "" || txtFecha.Text == "" || txtSalario.Text == "" || ID <= 0)
                {
                    MessageBox.Show("Selecciona un Item");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Algo anda mal: " + ex.Message, "Error CRVA-EE", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void dgvEmpleado_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Employee _Employee = dgvEmpleado.SelectedItem as Employee;
                if (_Employee != null && _Employee.EmployeeID > 0)
                {
                    //dgvEmpleado.ItemsSource = new List<Employee>();
                    ID = _Employee.EmployeeID;
                    txtNombre.Text = _Employee.Name;
                    txtApellido.Text = _Employee.LastName;
                    Code = _Employee.Code;
                    Date = _Employee.Date;
                    txtFecha.Text = Convert.ToString(_Employee.DateBirth);
                    txtSalario.Text = Convert.ToString(_Employee.Salary);
                    cmbOficina.SelectedValue = _Employee.OfficeID;
                    EstadoBotones(1);
                    //dgvEmpleado.ItemsSource = _EmployeeBL.GetAll();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Algo anda mal: " + ex.Message, "Error CRVA-ES", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void txtSalario_TextInput(object sender, TextCompositionEventArgs e)
        {
            int ascci = Convert.ToInt32(Convert.ToChar(e.Text));
            if (ascci >= 48 && ascci <= 57) e.Handled = false;
            else e.Handled = true;
            bool approvedDecimalPoint = false;

            if (e.Text == ".")
            {
                if (!((TextBox)sender).Text.Contains("."))
                    approvedDecimalPoint = true;
            }

            if (!(char.IsDigit(e.Text, e.Text.Length - 1) || approvedDecimalPoint)) e.Handled = true;
        }

        private void txtSalario_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        //Metodos y Validaciones Empleado

        private Employee DatosEmpleados()
        {
            Employee _Employee = new Employee();
            _Employee.Name = txtNombre.Text.ToUpper();
            _Employee.LastName = txtApellido.Text.ToUpper();
            _Employee.State = true;
            return _Employee;
        }

        private void txtNombre_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int ascci = Convert.ToInt32(Convert.ToChar(e.Text));
            if (ascci >= 65 && ascci <= 90 || ascci >= 97 && ascci <= 122)
                e.Handled = false;
            else e.Handled = true;
        }

        private void txtApellido_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int ascci = Convert.ToInt32(Convert.ToChar(e.Text));
            if (ascci >= 65 && ascci <= 90 || ascci >= 97 && ascci <= 122)
                e.Handled = false;
            else e.Handled = true;
        }
        private void EstadoBotones(int estado)
        {
            if (estado == 1)
            {
                btnModificar.IsEnabled = true;
                btnEliminar.IsEnabled = true;
                btnCancelar.IsEnabled = true;
                btnGuardar.IsEnabled = false;
            }
            else
            {
                btnCancelar.IsEnabled = false;
                btnModificar.IsEnabled = false;
                btnEliminar.IsEnabled = false;
                btnGuardar.IsEnabled = true;
            }
        }
        private void Limpiar()
        {
            txtNombre.Text = string.Empty;
            txtApellido.Text = string.Empty;
            txtFecha.Text = string.Empty;
            txtSalario.Text = string.Empty;
            cmbOficina.SelectedIndex = 0;
        }
        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
            EstadoBotones(0);
        }

        //Paginacion
        private async void txtBuscar_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(txtBuscar.Text.Trim()))
                {
                    string txtBuscarEmpleado = txtBuscar.Text.ToUpper();
                    //lista a paginar
                    list = await GetPagedListAsync();
                    //Cargamos la lista al DataGrid
                    dgvEmpleado.ItemsSource = list.Where(u => u.Name.ToUpper().Contains(txtBuscarEmpleado) || u.LastName.ToUpper().Contains(txtBuscarEmpleado)).ToList();
                    //Determinamos el estado de los botones
                    btnPrevius.IsEnabled = list.HasPreviousPage;
                    btnSiguiente.IsEnabled = list.HasNextPage;
                    //Establecemos el numero de paginas del DataGrid
                    tbPaginacion.Text = string.Format("Pagina {0}/{1}", pageNumber, list.PageCount);
                    //EstadoBotones(0);
                }
                else
                {
                    //lista a paginar
                    list = await GetPagedListAsync();
                    //Determinamos el estado de los botones
                    btnPrevius.IsEnabled = list.HasPreviousPage;
                    btnSiguiente.IsEnabled = list.HasNextPage;
                    //Cargamos la lista al DataGrid
                    dgvEmpleado.ItemsSource = list.ToList();
                    //Establecemos el numero de paginas del DataGrid
                    tbPaginacion.Text = string.Format("Pagina {0}/{1}", pageNumber, list.PageCount);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Algo anda mal: " + ex.Message, "Error CRVA-EB", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #region Paginacion
        //Paginacion DATAGRID
        //variables
        int pageNumber = 1;
        IPagedList<Employee> list;


        //Codigo botones
        private async void btnPrevius_Click(object sender, RoutedEventArgs e)
        {
            if (list.HasPreviousPage)
            {
                list = await GetPagedListAsync(--pageNumber);
                btnPrevius.IsEnabled = list.HasPreviousPage;
                btnSiguiente.IsEnabled = list.HasNextPage;
                dgvEmpleado.ItemsSource = list.ToList();
                tbPaginacion.Text = string.Format("Pagina {0}/{1}", pageNumber, list.PageCount);
            }
        }

        private async void btnSiguiente_Click(object sender, RoutedEventArgs e)
        {
            if (list.HasNextPage)
            {
                list = await GetPagedListAsync(++pageNumber);
                btnPrevius.IsEnabled = list.HasPreviousPage;
                btnSiguiente.IsEnabled = list.HasNextPage;
                dgvEmpleado.ItemsSource = list.ToList();
                tbPaginacion.Text = string.Format("Pagina {0}/{1}", pageNumber, list.PageCount);
            }
        }


        //metodo que llena el DataGrid

        public async Task<IPagedList<Employee>> GetPagedListAsync(int pageNumber = 1, int PageSize = 5)
        {
            return await Task.Factory.StartNew(() =>
            {
                return _Accion.EmployeeM.GetAll().OrderBy(u => u.EmployeeID).ToPagedList(pageNumber, PageSize);
            });
        }


        //Modificamos el evento loaded del window
        //Agregamos el atributo "async" al evento
        private async void frmMantenimientoEmpleado_Loaded(object sender, RoutedEventArgs e)
        {
            dgvEmpleado.ItemsSource = _EmployeeBL.GetAll();

            cmbOficina.ItemsSource = _OfficeBL.GetAll();
            //cmbOficina.DisplayMemberPath = "Name";
            cmbOficina.SelectedValuePath = "OfficeID";
            cmbOficina.SelectedIndex = 0;
            EstadoBotones(0);

            //lista a paginar
            list = await GetPagedListAsync();
            //Determinamos el estado de los botones
            btnPrevius.IsEnabled = list.HasPreviousPage;
            btnSiguiente.IsEnabled = list.HasNextPage;
            //Cargamos la lista al DataGrid
            dgvEmpleado.ItemsSource = list.ToList();
            //Establecemos el numero de paginas del DataGrid
            tbPaginacion.Text = string.Format("Pagina {0}/{1}", pageNumber, list.PageCount);
        }
        #endregion
    }
}
