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
using System.Windows.Navigation;
using System.Windows.Shapes;
//Using a utilizar
using Agape.ReservacionVhl.WpfUI.Reportes;
using Agape.ReservacionVhl.BL;
using Agape.ReservacionVhl.EN;
using PagedList;

namespace Agape.ReservacionVhl.WpfUI.Reportes
{
    /// <summary>
    /// Lógica de interacción para SeleccionEmpleadoVehiculo.xaml
    /// </summary>
    public partial class SeleccionEmpleadoVehiculo : Page
    {

        MetodosBL _Accion = new MetodosBL();
        Employee _employeDgv = new Employee();
        Car _carDgv = new Car();
        public SeleccionEmpleadoVehiculo()
        {
            InitializeComponent();
            _employeDgv = new Employee();
        }

        private async void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(txtBuscar.Text.Trim()))
                {
                    string txtBuscarObjeto = txtBuscar.Text.ToUpper();
                    //lista a paginar
                    list = await GetPagedListAsync();
                    //Determinamos el estado de los botones
                    btnPrevius.IsEnabled = list.HasPreviousPage;
                    btnSiguiente.IsEnabled = list.HasNextPage;
                    //Cargamos la lista al DataGrid
                    dgvContenedorEC.ItemsSource = list.Where(u => u.Code.ToUpper().Contains(txtBuscarObjeto) || u.Details.ToUpper().Contains(txtBuscarObjeto)|| u.Mark.Name.ToUpper().Contains(txtBuscarObjeto)).ToList();
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
                    dgvContenedorEC.ItemsSource = list.ToList();
                    //Establecemos el numero de paginas del DataGrid
                    tbPaginacion.Text = string.Format("Pagina {0}/{1}", pageNumber, list.PageCount);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Algo anda mal: " + ex.Message, "Error CRVA-EB", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void dgvEmpleado_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Employee _Item = dgvContenedorEC.SelectedItem as Employee;
                if (_Item != null && _Item.EmployeeID > 0)
                {
                    btnSeleccionar.IsEnabled = true;
                    _employeDgv = _Item;                    
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error CRVA", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnSeleccionar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_carDgv.CarID <= 0)
                {
                    MessageBox.Show("Selecciona un registro", "Seleccion de objeto",MessageBoxButton.OK,MessageBoxImage.Information);
                }
                else
                {
                    Employee _EmployeData = new Employee();
                    _EmployeData = _employeDgv;
                    this.NavigationService.Navigate(new ReporteEspecifico(_carDgv,1));
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error CRVA", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #region Paginacion
        //Paginacion DATAGRID
        //variables
        int pageNumber = 1;
        IPagedList<Car> list;


        //Codigo botones
        private async void btnPrevius_Click(object sender, RoutedEventArgs e)
        {
            if (list.HasPreviousPage)
            {
                list = await GetPagedListAsync(--pageNumber);
                btnPrevius.IsEnabled = list.HasPreviousPage;
                btnSiguiente.IsEnabled = list.HasNextPage;
                dgvContenedorEC.ItemsSource = list.ToList();
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
                dgvContenedorEC.ItemsSource = list.ToList();
                tbPaginacion.Text = string.Format("Pagina {0}/{1}", pageNumber, list.PageCount);
            }
        }


        //metodo que llena el DataGrid

        public async Task<IPagedList<Car>> GetPagedListAsync(int pageNumber = 1, int PageSize = 5)
        {
            return await Task.Factory.StartNew(() =>
            {
                return _Accion.CarM.GetAll().OrderBy(u => u.CarID).ToPagedList(pageNumber, PageSize);
            });
        }


        //Modificamos el evento loaded del window
        //Agregamos el atributo "async" al evento
        //Fin Paginacion
        #endregion

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Car _CarValue = new Car();
            this.NavigationService.Navigate(new ReporteEspecifico(_CarValue));
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            btnSeleccionar.IsEnabled = false;

            list = await GetPagedListAsync(pageNumber);
            btnPrevius.IsEnabled = list.HasPreviousPage;
            btnSiguiente.IsEnabled = list.HasNextPage;
            dgvContenedorEC.ItemsSource = list.ToList();
            tbPaginacion.Text = string.Format("Pagina {0}/{1}", pageNumber, list.PageCount);
        }

        private void dgvContenedorEC_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Car _Item = dgvContenedorEC.SelectedItem as Car;
                if (_Item != null && _Item.CarID > 0)
                {
                    btnSeleccionar.IsEnabled = true;
                    _carDgv = _Item;
                    imgVehiculo.Source = _Accion.LoadImage(_Item.Image);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error CRVA", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
