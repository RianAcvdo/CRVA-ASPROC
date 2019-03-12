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
//Usings a utilizar en la UI
using Agape.ReservacionVhl.BL;
using Agape.ReservacionVhl.EN;
using Agape.ReservacionVhl.WpfUI.Reportes;
using System.IO;
using PagedList;

namespace Agape.ReservacionVhl.WpfUI
{
    /// <summary>
    /// Lógica de interacción para RestauracionVehiculo.xaml
    /// </summary>
    public partial class RestauracionVehiculo : Window
    {
        MetodosBL _Accion = new MetodosBL();
        CarBL _CarBL = new CarBL();
        Car _Car = new Car();
        Int64 ID, Marca;
        DateTime Fecha;
        byte[] Img;
        string Detalle, Code;
        Image vehiculoImage = new Image();
        public RestauracionVehiculo()
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

        #region Llenar combobox de filtrado
        private void LlenarComboBoxFiltrado()
        {
            List<string> _listaOpciones = new List<string>();
            string Opcion0 = "Ninguno";//0
            string Opcion1 = "Mantenimiento planeado.";
            string Opcion2 = "Mantenimiento no planeado.";
            string Opcion3 = "Cambio de función.";
            string Opcion4 = "Venta.";
            string OpcionOtro = "Otro";//5
            _listaOpciones.Add(Opcion0);
            _listaOpciones.Add(Opcion1);
            _listaOpciones.Add(Opcion2);
            _listaOpciones.Add(Opcion3);
            _listaOpciones.Add(Opcion4);
            _listaOpciones.Add(OpcionOtro);
            txtFiltrado.ItemsSource = _listaOpciones;
            txtFiltrado.SelectedIndex = 0;
        }
        #endregion

        #region Imagen

        private static BitmapImage BytesToImage(byte[] bytes)
        {
            var Bitmap = new BitmapImage();
            using (MemoryStream stream = new MemoryStream(bytes))
            {
                stream.Position = 0;
                Bitmap.BeginInit();
                Bitmap.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                Bitmap.CacheOption = BitmapCacheOption.OnLoad;
                Bitmap.UriSource = null;
                Bitmap.StreamSource = stream;
                Bitmap.EndInit();
            }
            return Bitmap;
        }
        #endregion

        #region Evento de seleccion en grid
        private void dgvVehiculo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Car _Vehiculo = dgvVehiculo.SelectedItem as Car;
            if (_Vehiculo != null && _Vehiculo.CarID > 0)
            {
                //dgvVehiculo.ItemsSource = new List<Car>();
                Detalle = _Vehiculo.Details;

                Marca = _Vehiculo.MarkID;
                Fecha = _Vehiculo.Date;
                ID = _Vehiculo.CarID;
                Code = _Vehiculo.Code;
                Img = _Vehiculo.Image;
                imgVehiculo.Visibility = Visibility.Visible;
                imgVehiculo.Source = BytesToImage(_Vehiculo.Image);
                //dgvVehiculo.ItemsSource = _CarBL.GetAllDeleted();
            }
        }
        #endregion

        #region Evento restaurar
        private async void btnRestaurar_Click(object sender, RoutedEventArgs e)
        {
            if (ID <= 0)
            {
                MessageBox.Show("Selecciona un Item");
            }
            else
            {
                if (MessageBox.Show("Seguro que desea restaurar vehículo numero de placa: " + Code, "Restaurar", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    _Car.MarkID = Marca;
                    _Car.Date = Fecha;
                    _Car.Details = Detalle;
                    _Car.Image = Img;
                    _Car.State = true;
                    _Car.Code = Code;
                    _Car.CarID = ID;
                    _CarBL.UpdateCar(_Car);
                    imgVehiculo.Source = vehiculoImage.Source;
                    //lista a paginar
                    list = await GetPagedListAsync();
                    //Determinamos el estado de los botones
                    btnPrevius.IsEnabled = list.HasPreviousPage;
                    btnSiguiente.IsEnabled = list.HasNextPage;
                    //Cargamos la lista al DataGrid
                    dgvVehiculo.ItemsSource = list.ToList();
                    //Establecemos el numero de paginas del DataGrid
                    tbPaginacion.Text = string.Format("Pagina {0}/{1}", pageNumber, list.PageCount);
                }
            }
        }
        #endregion

        #region evenmto buscar
        private async void txtBuscar_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(txtBuscar.Text.Trim()))
                {
                    string txtBuscarVehiculo = txtBuscar.Text.ToUpper();
                    //lista a paginar
                    list = await GetPagedListAsync();
                    //Determinamos el estado de los botones
                    btnPrevius.IsEnabled = list.HasPreviousPage;
                    btnSiguiente.IsEnabled = list.HasNextPage;
                    //Cargamos la lista al DataGrid
                    dgvVehiculo.ItemsSource = list.Where(u => u.Code.ToUpper().Contains(txtBuscarVehiculo) || u.Mark.Name.ToUpper().Contains(txtBuscarVehiculo)).ToList();
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
                    dgvVehiculo.ItemsSource = list.ToList();
                    //Establecemos el numero de paginas del DataGrid
                    tbPaginacion.Text = string.Format("Pagina {0}/{1}", pageNumber, list.PageCount);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Algo anda mal : " + ex.Message, "Error CRVA-UB", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

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
                dgvVehiculo.ItemsSource = list.ToList();
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
                dgvVehiculo.ItemsSource = list.ToList();
                tbPaginacion.Text = string.Format("Pagina {0}/{1}", pageNumber, list.PageCount);
            }
        }


        //metodo que llena el DataGrid

        public async Task<IPagedList<Car>> GetPagedListAsync(int pageNumber = 1, int PageSize = 5)
        {
            string Valor = LlenarGridPaginacion(txtFiltrado.SelectedValue.ToString().ToUpper());
            if (Valor == "NINGUNO")
            {
                Valor = "";
                return await Task.Factory.StartNew(() =>
            {
                return _Accion.CarM.GetAllDeleted()
                .Where(u => u.StateDetail.Contains(Valor))
                .OrderBy(u => u.CarID)
                .ToPagedList(pageNumber, PageSize);
            });
            }
            else if (Valor == "OTRO")
            {
                Valor = "";
                return await Task.Factory.StartNew(() =>
                {
                    return _Accion.CarM.GetAllDeleted()
                    .Where(u => u.StateDetail.Contains(Valor))
                    .OrderBy(u => u.MarkID)
                    .ToPagedList(pageNumber, PageSize);
                });
            }
            else
            {
                return await Task.Factory.StartNew(() =>
                {
                    return _Accion.CarM.GetAllDeleted()
                    .Where(u => u.StateDetail.Contains(Valor))
                    .OrderBy(u => u.MarkID)
                    .ToPagedList(pageNumber, PageSize);
                });
            }
        }

        private async void txtFiltrado_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(txtBuscar.Text.Trim()))
                {
                    string txtBuscarVehiculo = txtBuscar.Text.ToUpper();
                    //lista a paginar
                    list = await GetPagedListAsync();
                    //Determinamos el estado de los botones
                    btnPrevius.IsEnabled = list.HasPreviousPage;
                    btnSiguiente.IsEnabled = list.HasNextPage;
                    string Valor = txtFiltrado.SelectedValue.ToString().ToUpper();
                    if (Valor == "NINGUNO")
                    {
                        Valor = "";
                    }
                    //Cargamos la lista al DataGrid
                    dgvVehiculo.ItemsSource = list.Where(u => u.Code.ToUpper().Contains(txtBuscarVehiculo) || u.Mark.Name.ToUpper().Contains(txtBuscarVehiculo)).ToList();
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
                    dgvVehiculo.ItemsSource = list.ToList();
                    //Establecemos el numero de paginas del DataGrid
                    tbPaginacion.Text = string.Format("Pagina {0}/{1}", pageNumber, list.PageCount);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Algo anda mal : " + ex.Message, "Error CRVA-UB", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public string LlenarGridPaginacion(string valor)
        {
            return valor;
        }
        //Fin Paginacion
        #endregion

        #region Evento load
        //Modificamos el evento loaded del window
        //Agregamos el atributo "async" al evento
        private async void frmRestauracionVehiculo_Loaded(object sender, RoutedEventArgs e)
        {
            LlenarComboBoxFiltrado();
            //lista a paginar
            list = await GetPagedListAsync();
            //Determinamos el estado de los botones
            btnPrevius.IsEnabled = list.HasPreviousPage;
            btnSiguiente.IsEnabled = list.HasNextPage;
            //Cargamos la lista al DataGrid
            dgvVehiculo.ItemsSource = list.ToList();
            //Establecemos el numero de paginas del DataGrid
            tbPaginacion.Text = string.Format("Pagina {0}/{1}", pageNumber, list.PageCount);

            //Otros metodos
            vehiculoImage.Source = imgVehiculo.Source;
        }
        #endregion
    }
}
