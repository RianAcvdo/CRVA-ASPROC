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
//Using para paginacion
using PagedList;
//
using System.Text.RegularExpressions;

namespace Agape.ReservacionVhl.WpfUI
{
    /// <summary>
    /// Lógica de interacción para RestauracionMarca.xaml
    /// </summary>
    public partial class RestauracionMarca : Window
    {

        MetodosBL _Accion = new MetodosBL();
        MarkBL _MarkBL = new MarkBL();
        Mark _Mark = new Mark();
        Int64 ID, Marca;
        byte[] Img;
        string Nombre;
        Image marcaImage = new Image();
        public RestauracionMarca()
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

        private void dgvMarca_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Mark _marca = dgvMarca.SelectedItem as Mark;
            if (_marca != null && _marca.MarkID > 0)
            {
                //dgvMarca.ItemsSource = new List<Car>();
                Nombre = _marca.Name;
                Marca = _marca.MarkID;
                ID = _marca.MarkID;
                Img = _marca.Picture;
                //imgVehiculo.Visibility = Visibility.Visible;
                imgMarca.Source = BytesToImage(_marca.Picture);
                //dgvMarca.ItemsSource = _CarBL.GetAllDeleted();
            }
        }

        private async void btnRestaurar_Click(object sender, RoutedEventArgs e)
        {
            if (ID <= 0)
            {
                MessageBox.Show("Selecciona un Item");
            }
            else
            {
                if (MessageBox.Show("Seguro que desea restaurar la marca  " + Nombre, "Restaurar", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    _Mark.MarkID = Marca;
                    _Mark.Name = Nombre;
                    _Mark.Picture = Img;
                    _Mark.State = true;
                    _Mark.MarkID = ID;
                    _MarkBL.UpdateMarks(_Mark);
                    dgvMarca.Items.Refresh();
                    imgMarca.Source = marcaImage.Source;

                    //lista a paginar
                    list = await GetPagedListAsync();
                    //Determinamos el estado de los botones
                    btnPrevius.IsEnabled = list.HasPreviousPage;
                    btnSiguiente.IsEnabled = list.HasNextPage;
                    //Cargamos la lista al DataGrid
                    dgvMarca.ItemsSource = list.ToList();
                    //Establecemos el numero de paginas del DataGrid
                    tbPaginacion.Text = string.Format("Pagina {0}/{1}", pageNumber, list.PageCount);

                }
            }
        }

        //Paginacion DATAGRID
        //variables
        int pageNumber = 1;
        IPagedList<Mark> list;


        //Codigo botones
        private async void btnPrevius_Click(object sender, RoutedEventArgs e)
        {
            if (list.HasPreviousPage)
            {
                list = await GetPagedListAsync(--pageNumber);
                btnPrevius.IsEnabled = list.HasPreviousPage;
                btnSiguiente.IsEnabled = list.HasNextPage;
                dgvMarca.ItemsSource = list.ToList();
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
                dgvMarca.ItemsSource = list.ToList();
                tbPaginacion.Text = string.Format("Pagina {0}/{1}", pageNumber, list.PageCount);
            }
        }


        //metodo que llena el DataGrid

        public async Task<IPagedList<Mark>> GetPagedListAsync(int pageNumber = 1, int PageSize = 5)
        {
            return await Task.Factory.StartNew(() =>
            {
                return _Accion.MarkM.GetAllDeleted().OrderBy(u => u.MarkID).ToPagedList(pageNumber, PageSize);
            });
        }


        //Modificamos el evento loaded del window
        //Agregamos el atributo "async" al evento
        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //lista a paginar
            list = await GetPagedListAsync();
            //Determinamos el estado de los botones
            btnPrevius.IsEnabled = list.HasPreviousPage;
            btnSiguiente.IsEnabled = list.HasNextPage;
            //Cargamos la lista al DataGrid
            dgvMarca.ItemsSource = list.ToList();
            //Establecemos el numero de paginas del DataGrid
            tbPaginacion.Text = string.Format("Pagina {0}/{1}", pageNumber, list.PageCount);

            //Otros metodos
            marcaImage.Source = imgMarca.Source;
        }

        private void txtBuscar_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Regex.IsMatch(e.Text, "[^A-Za-z0-9]+");
        }

        //Fin Paginacion

        //private void Window_Loaded(object sender, RoutedEventArgs e)
        //{
        //    marcaImage.Source = imgMarca.Source;
        //    dgvMarca.ItemsSource = _Accion.MarkM.GetAllDeleted();
        //}

        private async void txtBuscar_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(txtBuscar.Text.Trim()))
                {
                    string txtBuscarMarca = txtBuscar.Text.ToUpper();
                    //lista a paginar
                    list = await GetPagedListAsync();
                    //Determinamos el estado de los botones
                    btnPrevius.IsEnabled = list.HasPreviousPage;
                    btnSiguiente.IsEnabled = list.HasNextPage;
                    //Cargamos la lista al DataGrid
                    dgvMarca.ItemsSource = list.Where(u => u.Name.ToUpper().Contains(txtBuscarMarca)).ToList();
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
                    dgvMarca.ItemsSource = list.ToList();
                    //Establecemos el numero de paginas del DataGrid
                    tbPaginacion.Text = string.Format("Pagina {0}/{1}", pageNumber, list.PageCount);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Algo anda mal : " + ex.Message, "Error CRVA-MR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
