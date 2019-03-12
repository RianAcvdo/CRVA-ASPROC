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
//Using a utilizar
using Agape.ReservacionVhl.BL;
using Agape.ReservacionVhl.EN;
using Microsoft.Win32;
using System.IO;
using Agape.ReservacionVhl.WpfUI.Reportes;
using PagedList;

namespace Agape.ReservacionVhl.WpfUI
{
    /// <summary>
    /// Lógica de interacción para RestauracionReservacion.xaml
    /// </summary>
    public partial class RestauracionReservacion : Window
    {
        MetodosBL _Accion = new MetodosBL();
        ReservationBL _dataObjBL = new ReservationBL();
        Reservation _dataObj = new Reservation();
        Int64 ID,CarID,EmployeeID;
        DateTime Fecha;
        string Origen, Destino, Action;
        bool State;
        public RestauracionReservacion()
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

        private void btnRestVehiculos_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                RestauracionVehiculo Window = new RestauracionVehiculo();
                this.Hide();
                Window.ShowDialog();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Algo anda mal : " + ex.Message, "Error CRVA-UG", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }
        #endregion
        private void dgvRestaurar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Reservation _Item = dgvRestaurar.SelectedItem as Reservation;
                if (_Item != null && _Item.ReservationID > 0)
                {
                    //dgvRestaurar.ItemsSource = new List<Reservation>();

                    Origen = _Item.Origin;
                    Destino = _Item.Destination;
                    Fecha = _Item.Date;
                    Action = _Item.Action;
                    CarID = _Item.CarID;
                    EmployeeID = _Item.EmployeeID;
                    State = _Item.State;
                    ID = _Item.ReservationID;
                    //dgvRestaurar.ItemsSource = _dataObjBL.GetAllDeleted();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Algo anda mal : " + ex.Message, "Error CRVA-UG", MessageBoxButton.OK, MessageBoxImage.Error);

            }

        }

        private async void btnRestaurar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ID <= 0)
                {
                    MessageBox.Show("Selecciona un Item");
                }
                else
                {
                    if (MessageBox.Show("Seguro que desea restaurar?", "Restaurar", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        _dataObj.Destination = Destino;
                        _dataObj.Origin = Origen;
                        _dataObj.Date = Fecha;
                        _dataObj.Action = Action;
                        _dataObj.CarID = CarID;
                        _dataObj.EmployeeID = EmployeeID;
                        _dataObj.ReservationID = ID;
                        _dataObj.State = true;
                        _dataObjBL.UpdateItem(_dataObj);
                        //dgvRestaurar.Items.Refresh();
                        //dgvRestaurar.ItemsSource = _dataObjBL.GetAllDeleted();

                        //lista a paginar
                        list = await GetPagedListAsync();
                        //Determinamos el estado de los botones
                        btnPrevius.IsEnabled = list.HasPreviousPage;
                        btnSiguiente.IsEnabled = list.HasNextPage;
                        //Cargamos la lista al DataGrid
                        dgvRestaurar.ItemsSource = list.ToList();
                        //Establecemos el numero de paginas del DataGrid
                        tbPaginacion.Text = string.Format("Pagina {0}/{1}", pageNumber, list.PageCount);
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
                    dgvRestaurar.ItemsSource = list.Where(u => u.Action.ToUpper().Contains(txtBuscarReservacion) || u.Origin.ToUpper().Contains(txtBuscarReservacion) || u.Destination.ToUpper().Contains(txtBuscarReservacion) || u.Car.Code.ToUpper().Contains(txtBuscarReservacion) || u.Employee.Name.ToUpper().Contains(txtBuscarReservacion) || u.Employee.LastName.ToUpper().Contains(txtBuscarReservacion)).ToList();
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
                    dgvRestaurar.ItemsSource = list.ToList();
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
                dgvRestaurar.ItemsSource = list.ToList();
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
                dgvRestaurar.ItemsSource = list.ToList();
                tbPaginacion.Text = string.Format("Pagina {0}/{1}", pageNumber, list.PageCount);
            }
        }


        //metodo que llena el DataGrid

        public async Task<IPagedList<Reservation>> GetPagedListAsync(int pageNumber = 1, int PageSize = 5)
        {
            return await Task.Factory.StartNew(() =>
            {
                return _Accion.ReservationM.GetAllDeleted().OrderBy(u => u.ReservationID).ToPagedList(pageNumber, PageSize);
            });
        }


        //Modificamos el evento loaded del window
        //Agregamos el atributo "async" al evento
        private async void frmRestauracionReservacion_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                //lista a paginar
                list = await GetPagedListAsync();
                //Determinamos el estado de los botones
                btnPrevius.IsEnabled = list.HasPreviousPage;
                btnSiguiente.IsEnabled = list.HasNextPage;
                //Cargamos la lista al DataGrid
                dgvRestaurar.ItemsSource = list.ToList();
                //Establecemos el numero de paginas del DataGrid
                tbPaginacion.Text = string.Format("Pagina {0}/{1}", pageNumber, list.PageCount);
            }
            catch (Exception ex)
            {

                MessageBox.Show("Algo anda mal : " + ex.Message, "Error CRVA-UG", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        //Fin Paginacion
        #endregion
    }
}
