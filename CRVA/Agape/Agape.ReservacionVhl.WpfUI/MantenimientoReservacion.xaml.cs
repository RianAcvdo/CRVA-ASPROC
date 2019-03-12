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
using Agape.ReservacionVhl.BL;
using Agape.ReservacionVhl.EN;
using Agape.ReservacionVhl.WpfUI.Reportes;
using PagedList;

namespace Agape.ReservacionVhl.WpfUI
{
    
    /// <summary>
    /// Lógica de interacción para MantenimientoReservacion.xaml
    /// </summary>
    public partial class MantenimientoReservacion : Window
    {
        MetodosBL _Accion = new MetodosBL();
        ReservationBL _ReservationBL = new ReservationBL();
        Reservation _Reservation = new Reservation();
        CarBL _CarBL = new CarBL();
        EmployeeBL _EmployeeBL = new EmployeeBL();
        DateTime Date;
        string Origen,Destino,Accion;
        Int64 ID = 0, CarId=0,EmpleadoId=0;
        public MantenimientoReservacion()
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

        private async void frmMantenimientoReservacion_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                //dgvReservacion.ItemsSource = _ReservationBL.GetAll();
                //cmdIdEmpleado.ItemsSource = _EmployeeBL.GetAll();
                //cmdIdEmpleado.DisplayMemberPath = "Name";
                //cmdIdEmpleado.SelectedValuePath = "EmployeeID";
                //cmdIdAuto.ItemsSource = _CarBL.GetAll();
                //cmdIdAuto.DisplayMemberPath = "Details";
                //cmdIdAuto.SelectedValuePath = "CarID";

                //lista a paginar
                list = await GetPagedListAsync();
                //Determinamos el estado de los botones
                btnPrevius.IsEnabled = list.HasPreviousPage;
                btnSiguiente.IsEnabled = list.HasNextPage;
                //Cargamos la lista al DataGrid
                dgvReservacion.ItemsSource = list.ToList();
                //Establecemos el numero de paginas del DataGrid
                tbPaginacion.Text = string.Format("Pagina {0}/{1}", pageNumber, list.PageCount);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Algo anda mal : " + ex.Message, "Error CRVA-UG", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        //private void btnGuardar_Click(object sender, RoutedEventArgs e)
        //{

        //    if (txtAccion.Text == "" || txtDestino.Text == "" || txtOrigen.Text == "" || cmdIdAuto.Text == "" || cmdIdEmpleado.Text == "")
        //    {
        //        MessageBox.Show("No dejar campos vacíos");
        //    }
        //    else
        //    {
        //        //DateTime ThisDay = new DateTime();
        //        //ThisDay = Convert.ToDateTime(txtFecha.Text);
        //        _Reservation.ReservationID = ID;
        //        _Reservation.Origin = txtOrigen.Text.ToUpper();
        //        _Reservation.Destination = txtDestino.Text.ToUpper();
        //        _Reservation.Action = txtAccion.Text.ToUpper();
        //        _Reservation.Date = DateTime.Now;
        //        _Reservation.CarID = Convert.ToInt64(cmdIdAuto.SelectedValue);
        //        _Reservation.EmployeeID = Convert.ToInt64(cmdIdEmpleado.SelectedValue);
        //        _Reservation.State = true;
        //        _ReservationBL.Create(_Reservation);
        //        dgvReservacion.ItemsSource = _ReservationBL.GetAll();
        //        txtAccion.Text = ""; txtDestino.Text = ""; txtOrigen.Text = ""; cmdIdAuto.Text = ""; cmdIdEmpleado.Text = "";
        //    }
        //}

        //private void btnBuscar_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        if (txtAccion.Text == "" && txtDestino.Text == "" && txtOrigen.Text == "")
        //        {
        //            dgvReservacion.ItemsSource = _ReservationBL.GetAll();
        //        }
        //        else
        //        {
        //            _Reservation.Origin = txtOrigen.Text.ToUpper();
        //            _Reservation.Destination = txtDestino.Text.ToUpper();
        //            _Reservation.Action = txtAccion.Text.ToUpper();
        //            //_Reservation.Date = Convert.ToDateTime(txtFecha.Text);
        //            _Reservation.CarID = Convert.ToInt64(cmdIdAuto.SelectedValue);
        //            _Reservation.EmployeeID = Convert.ToInt64(cmdIdEmpleado.SelectedValue);
        //            dgvReservacion.ItemsSource = _ReservationBL.SearchByParameters(_Reservation);
        //            txtAccion.Text = ""; txtDestino.Text = ""; txtOrigen.Text = ""; cmdIdAuto.Text = ""; cmdIdEmpleado.Text = "";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Algo anda mal : " + ex.Message, "Error CRVA-UG", MessageBoxButton.OK, MessageBoxImage.Error);
        //    }
           
        //}

        //private void btnModificar_Click(object sender, RoutedEventArgs e)
        //{
        //    if (txtAccion.Text != "" && txtDestino.Text != "" && txtOrigen.Text != "" || cmdIdAuto.SelectedValue == null || cmdIdEmpleado.SelectedValue == null && ID > 0)
        //    {
        //        //DateTime ThisDay = new DateTime();
        //        //ThisDay = Convert.ToDateTime(txtFecha.Text);
        //        _Reservation.ReservationID = ID;
        //        _Reservation.Origin = txtOrigen.Text.ToUpper();
        //        _Reservation.Destination = txtDestino.Text.ToUpper();
        //        _Reservation.Action = txtAccion.Text.ToUpper();
        //        _Reservation.Date = Date;
        //        _Reservation.CarID = CarId;
        //        _Reservation.EmployeeID = EmpleadoId;
        //        _Reservation.State = true;
        //        _ReservationBL.UpdateItem(_Reservation);
        //        dgvReservacion.ItemsSource = _ReservationBL.GetAll();
        //        txtAccion.Text = ""; txtDestino.Text = ""; txtOrigen.Text = ""; cmdIdAuto.Text = ""; cmdIdEmpleado.Text = "";
        //    }
        //    else if (txtAccion.Text != "" && txtDestino.Text != "" && txtOrigen.Text != "" || cmdIdAuto.SelectedValue != null || cmdIdEmpleado.SelectedValue != null && ID > 0)
        //    {
        //        _Reservation.ReservationID = ID;
        //        _Reservation.Origin = txtOrigen.Text.ToUpper();
        //        _Reservation.Destination = txtDestino.Text.ToUpper();
        //        _Reservation.Action = txtAccion.Text.ToUpper();
        //        _Reservation.Date = Date;
        //        _Reservation.CarID = Convert.ToInt64(cmdIdAuto.SelectedValue);
        //        _Reservation.EmployeeID = Convert.ToInt64(cmdIdEmpleado.SelectedValue);
        //        _Reservation.State = true;
        //        _ReservationBL.UpdateItem(_Reservation);
        //        dgvReservacion.ItemsSource = _ReservationBL.GetAll();
        //        txtAccion.Text = ""; txtDestino.Text = ""; txtOrigen.Text = ""; cmdIdAuto.Text = ""; cmdIdEmpleado.Text = "";
        //    }
        //    else if (txtAccion.Text == "" || txtDestino.Text == "" || txtOrigen.Text == "" && ID <= 0)
        //    {
        //        MessageBox.Show("Selecciona un Item");
        //    }
        //}

        private void dgvReservacion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Reservation _Reservation = dgvReservacion.SelectedItem as Reservation;
                if (_Reservation != null && _Reservation.CarID > 0)
                {
                    //dgvReservacion.ItemsSource = new List<Reservation>();
                    Accion = _Reservation.Action;
                    Destino = _Reservation.Destination;
                    Origen = _Reservation.Origin;
                    //txtFecha.Text = Convert.ToString(_Reservation.Date);
                    Date = _Reservation.Date;
                    CarId = _Reservation.CarID;
                    EmpleadoId = _Reservation.EmployeeID;
                    ID = _Reservation.ReservationID;
                    //dgvReservacion.ItemsSource = _ReservationBL.GetAll();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Algo anda mal : " + ex.Message, "Error CRVA-UG", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        private async void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ID <= 0)
                {
                    MessageBox.Show("Selecciona un Item");
                }
                else
                {
                    if (MessageBox.Show("Seguro que desea eliminar?", "Eliminar", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    {
                        //DateTime ThisDay = new DateTime();
                        //ThisDay = Convert.ToDateTime(txtFecha.Text);
                        _Reservation.ReservationID = ID;
                        _Reservation.Origin = Origen;
                        _Reservation.Destination = Destino;
                        _Reservation.Action = Accion;
                        _Reservation.Date = Date;
                        _Reservation.CarID = CarId;
                        _Reservation.EmployeeID = EmpleadoId;
                        _Reservation.State = false;
                        _ReservationBL.UpdateItem(_Reservation);
                        //dgvReservacion.ItemsSource = _ReservationBL.GetAll();
                        //lista a paginar
                        list = await GetPagedListAsync();
                        //Determinamos el estado de los botones
                        btnPrevius.IsEnabled = list.HasPreviousPage;
                        btnSiguiente.IsEnabled = list.HasNextPage;
                        //Cargamos la lista al DataGrid
                        dgvReservacion.ItemsSource = list.ToList();
                        //Establecemos el numero de paginas del DataGrid
                        tbPaginacion.Text = string.Format("Pagina {0}/{1}", pageNumber, list.PageCount);
                        //txtAccion.Text = ""; txtDestino.Text = ""; txtOrigen.Text = ""; cmdIdAuto.Text = ""; cmdIdEmpleado.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Algo anda mal : " + ex.Message, "Error CRVA-UG", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            
        }

        private async void txtBuscar_KeyUp(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(txtBuscar.Text.Trim()))
                {
                    string txtBuscarReservacion = txtBuscar.Text.ToUpper();
                    //lista a paginar
                    list = await GetPagedListAsync();
                    //Determinamos el estado de los botones
                    btnPrevius.IsEnabled = list.HasPreviousPage;
                    btnSiguiente.IsEnabled = list.HasNextPage;
                    //Cargamos la lista al DataGrid
                    dgvReservacion.ItemsSource = list.Where(u => u.Action.ToUpper().Contains(txtBuscarReservacion) || u.Origin.ToUpper().Contains(txtBuscarReservacion) || u.Destination.ToUpper().Contains(txtBuscarReservacion) || u.Car.Code.ToUpper().Contains(txtBuscarReservacion) || u.Employee.Name.ToUpper().Contains(txtBuscarReservacion) || u.Employee.LastName.ToUpper().Contains(txtBuscarReservacion)).ToList();
                    //Establecemos el numero de paginas del DataGrid
                    tbPaginacion.Text = string.Format("Pagina {0}/{1}", pageNumber, list.PageCount);
                }
                else
                {
                    //lista a paginar
                    list = await GetPagedListAsync();
                    //Determinamos el estado de los botones
                    btnPrevius.IsEnabled = list.HasPreviousPage;
                    btnSiguiente.IsEnabled = list.HasNextPage;
                    //Cargamos la lista al DataGrid
                    dgvReservacion.ItemsSource = list.ToList();
                    //Establecemos el numero de paginas del DataGrid
                    tbPaginacion.Text = string.Format("Pagina {0}/{1}", pageNumber, list.PageCount);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error de busqueda : " + ex.Message, "Error CRVA-RB", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #region Paginacion
        //Paginacion DATAGRID
        //variables
        int pageNumber = 1;
        IPagedList<Reservation> list;


        //Codigo botones
        private async void btnPrevius_Click(object sender, RoutedEventArgs e)
        {
            if (list.HasPreviousPage)
            {
                list = await GetPagedListAsync(--pageNumber);
                btnPrevius.IsEnabled = list.HasPreviousPage;
                btnSiguiente.IsEnabled = list.HasNextPage;
                dgvReservacion.ItemsSource = list.ToList();
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
                dgvReservacion.ItemsSource = list.ToList();
                tbPaginacion.Text = string.Format("Pagina {0}/{1}", pageNumber, list.PageCount);
            }
        }


        //metodo que llena el DataGrid

        public async Task<IPagedList<Reservation>> GetPagedListAsync(int pageNumber = 1, int PageSize = 5)
        {
            return await Task.Factory.StartNew(() =>
            {
                return _Accion.ReservationM.GetAll().OrderBy(u => u.EmployeeID).ToPagedList(pageNumber, PageSize);
            });
        }


        //Modificamos el evento loaded del window
        //Agregamos el atributo "async" al evento
        //Fin Paginacion
        #endregion

        //private void txtKilometro_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.Key>=Key.D0&& e.Key <= Key.D9|| e.Key >= Key.NumPad0&& e.Key <= Key.NumPad9)
        //    {
        //        e.Handled = false;
        //    }
        //    else
        //    {
        //        e.Handled = true;
        //    }
        //}
    }
}
