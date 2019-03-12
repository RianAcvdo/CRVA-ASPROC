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
using Microsoft.Win32;
using System.IO;
using Agape.ReservacionVhl.WpfUI.Reportes;
using PagedList;
using System.Text.RegularExpressions;
//using System.Drawing;
//using System.Drawing.Imaging;

namespace Agape.ReservacionVhl.WpfUI
{
    /// <summary>
    /// Lógica de interacción para MantenimientoVehiculo.xaml
    /// </summary>
    public partial class MantenimientoVehiculo : Window
    {
        MetodosBL _Accion = new MetodosBL();
        OpenFileDialog openFileDialog1 = new OpenFileDialog();
        CarBL _CarBL = new CarBL();
        Car _Car = new Car();
        Int64 ID = 0;
        byte[] img;
        string Code;
        Image vehiculoImage = new Image();

        public MantenimientoVehiculo()
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
        private void btnMantMarcas_Click(object sender, RoutedEventArgs e)
        {
            MantenimientoMarca Window = new MantenimientoMarca();
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

        private void btnRestMarcas_Click(object sender, RoutedEventArgs e)
        {
            RestauracionMarca Window = new RestauracionMarca();
            this.Hide();
            Window.ShowDialog();
            Close();
        }
        #endregion

        #region MetodosExtras
        //public string CrearCode(int longitud)
        //{
        //    string caracteres = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        //    StringBuilder res = new StringBuilder();
        //    Random rnd = new Random();
        //    while (0 < longitud--)
        //    {
        //        res.Append(caracteres[rnd.Next(caracteres.Length)]);
        //    }
        //    return res.ToString();
        //}
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
        private void btnLoadFile_Click(object sender, RoutedEventArgs e)
        {
            openFileDialog1.Title = "Seleccione una imagen";
            openFileDialog1.Filter = "Archivos de imagen|*.jpg;*.jpeg;*.png|" +
                  "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                  "Portable Network Graphic (*.png)|*.png";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.Multiselect = false;
            bool? checarOK = openFileDialog1.ShowDialog();
            if (checarOK == true)
            {

                //imagen.source = openFileDialog1.FileName.ToString;
                //(System.Windows.Media.ImageSource)
                txtFileName.Text = openFileDialog1.FileName;
                //imgVehiculo.Visibility = Visibility.Visible;
                imgVehiculo.Source = new BitmapImage(new Uri(openFileDialog1.FileName));
            }
        }
        #endregion

        private void dgvVehiculo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Car _Vehiculo = dgvVehiculo.SelectedItem as Car;
                if (_Vehiculo != null && _Vehiculo.CarID > 0)
                {
                    //dgvVehiculo.ItemsSource = new List<Car>();
                    txtDetalle.Text = _Vehiculo.Details;
                    cmbMarca.SelectedValue = _Vehiculo.Mark.MarkID;
                    txtFecha.Text = Convert.ToString(_Vehiculo.Date);
                    ID = _Vehiculo.CarID;
                    Code = _Vehiculo.Code;
                    txtMatricula.Text = _Vehiculo.Code;
                    img = _Vehiculo.Image;
                    //imgVehiculo.Visibility = Visibility.Visible;
                    imgVehiculo.Source = BytesToImage(_Vehiculo.Image);
                    EstadoBotones(1);
                    //LlenarDGV();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Algo anda mal : " + ex.Message, "Error CRVA-VS", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private async void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtFecha.Text != string.Empty && txtDetalle.Text.Trim() != string.Empty && txtFileName.Text != string.Empty && txtMatricula.Text.Trim() != string.Empty)
                {
                    Car _datoVehiculo = DatosVehiculo();
                    var verificar = from u in _Accion.CarM.GetAllData() where u.Code.ToUpper() == txtMatricula.Text.ToUpper() select u;
                    if (verificar.Count() > 0)
                    {
                        MessageBox.Show("Ya existe un vehículo con esa matrícula", "Alerta CRVA-UG", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        txtMatricula.Focus();
                    }
                    else
                    {
                        _CarBL.Create(_datoVehiculo);
                        MessageBox.Show("Registro guardado con exito", "Exito CRVA-VG", MessageBoxButton.OK, MessageBoxImage.Information);
                        LimpiarWindow();
                        EstadoBotones(0);
                    }
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
                else if (txtFecha.Text.Trim() == string.Empty)
                {
                    txtFecha.Focus();
                }
                else if (txtDetalle.Text.Trim() == string.Empty)
                {
                    txtDetalle.Focus();
                }
                else if (txtMatricula.Text.Trim() == string.Empty)
                {
                    txtMatricula.Focus();
                }
                else if (txtFileName.Text == string.Empty)
                {
                    MessageBox.Show("Cargue una imagen para guardar el registro", "Alerta CRVA-VC", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                else
                {
                    MessageBox.Show("No se controlo el codigo fuente", "Alerta CRVA-VC", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Algo anda mal : " + ex.Message, "Error CRVA-VG", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtFecha.Text.Trim() != string.Empty && txtDetalle.Text.Trim() != string.Empty && txtMatricula.Text.Trim() != string.Empty)
                {
                    Car _datoVehiculo = DatosVehiculo();
                    _datoVehiculo.CarID = ID;
                    var verificar = from u in _Accion.CarM.GetAllData() where u.Code.ToUpper() == txtMatricula.Text.ToUpper() select u;
                    if (verificar.Count() > 0)
                    {
                        if (verificar.FirstOrDefault().CarID == _datoVehiculo.CarID)
                        {
                            _CarBL.UpdateCar(_datoVehiculo);
                            MessageBox.Show("Registro guardado exitosamente", "Exito CRVA-VM", MessageBoxButton.OK, MessageBoxImage.Information);
                            //Volvemos a cargar la ventana
                            MantenimientoVehiculo _window = new MantenimientoVehiculo();
                            _window.Show();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Ya existe un vehículo con esa matrícula", "Alerta CRVA-VM", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                            txtMatricula.Focus();
                        }
                    }
                    else
                    {
                        _CarBL.UpdateCar(_datoVehiculo);
                        MessageBox.Show("Registro guardado exitosamente", "Exito CRVA-VM", MessageBoxButton.OK, MessageBoxImage.Information);
                        //Volvemos a cargar la ventana
                        MantenimientoVehiculo _window = new MantenimientoVehiculo();
                        _window.Show();
                        this.Close();
                    }
                    //Reconfiguracion de la ventana
                    LimpiarWindow();
                    EstadoBotones(0);

                }
                else if (txtFecha.Text.Trim() == string.Empty)
                {
                    txtFecha.Focus();
                }
                else if (txtDetalle.Text.Trim() == string.Empty)
                {
                    txtDetalle.Focus();
                }
                else if (txtMatricula.Text.Trim() == string.Empty)
                {
                    txtMatricula.Focus();
                }
                else
                {
                    MessageBox.Show("Cargue una imagen para guardar el registro", "Alerta CRVA-VC", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Algo anda mal : " + ex.Message, "Error CRVA-VG", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ID <= 0)
                {
                    MessageBox.Show("Selecciona un Item", "Alerta CRVA-VE", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    DateTime ThisDay = new DateTime();
                    ThisDay = Convert.ToDateTime(txtFecha.Text);
                    _Car.MarkID = Convert.ToInt64(cmbMarca.SelectedValue);
                    _Car.Date = Convert.ToDateTime(ThisDay.ToString("d"));
                    _Car.Details = txtDetalle.Text.ToUpper();
                    _Car.Image = img;
                    _Car.Code = txtMatricula.Text.ToUpper();
                    _Car.CarID = ID;

                    //Modificación para eliminar mediante el cuadro de eliminación.
                    DialogoEliminacion dialogoEliminacion = new DialogoEliminacion(_Car);
                    dialogoEliminacion.ShowDialog();
                    dgvVehiculo.UnselectAllCells();
                    EstadoBotones(0);
                    LimpiarWindow();
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
                MessageBox.Show("Algo anda mal : " + ex.Message, "Error CRVA-VE", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void txtMarca_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int ascci = Convert.ToInt32(Convert.ToChar(e.Text));
            if (ascci >= 65 && ascci <= 90 || ascci >= 97 && ascci <= 122)
                e.Handled = false;
            else e.Handled = true;
        }
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

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            LimpiarWindow();
            EstadoBotones(0);
        }

        #region Configuracion de los botones
        /// <summary>
        /// recibe valores del tipo int, ya sea un 1 o 0, 
        /// 2 para habilitar la accion de restaurar, 
        /// 1 para habilitar las acciones modificar y eliminar, 
        /// 0 para deshabilitar las acciones modificar y eliminar
        /// </summary>
        /// <param name="estado"></param>
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

        #endregion Configuracion de los botones

        private void LlenarDGV()
        {
            try
            {
                dgvVehiculo.Items.Refresh();
                dgvVehiculo.ItemsSource = new List<Car>();
                dgvVehiculo.ItemsSource = _Accion.CarM.GetAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Algo anda mal : " + ex.Message, "Error CRVA-MLl", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private async void frmMantenimientoVehiculo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F5)
            {
                txtBuscar.Clear();
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
            else if (e.Key == Key.Delete)
            {
                LimpiarWindow();
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
            return await Task.Factory.StartNew(() =>
            {
                return _Accion.CarM.GetAll().OrderBy(u => u.CarID).ToPagedList(pageNumber, PageSize);
            });
        }


        //Modificamos el evento loaded del window
        //Agregamos el atributo "async" al evento
        private async void frmMantenimientoVehiculo_Loaded(object sender, RoutedEventArgs e)
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

            //configuracion de la ventana
            EstadoBotones(0);
            vehiculoImage.Source = imgVehiculo.Source;
            LLenarComboBox();
        }
        //Fin Paginacion
        #endregion Paginacion


        //Metodos de mantenimiento de vehiculo
        private Car DatosVehiculo()
        {
            Car _Car = new Car();
            _Car.Code = txtMatricula.Text.ToUpper();
            _Car.MarkID = Convert.ToInt64(cmbMarca.SelectedValue);
            _Car.Date = Convert.ToDateTime(txtFecha.Text);
            _Car.Details = txtDetalle.Text.ToLower(); ;
            _Car.State = true;
            if (txtFileName.Text != string.Empty)
            {
                byte[] imageBT = null;
                FileStream fstream = new FileStream(txtFileName.Text, FileMode.Open, FileAccess.Read);
                BinaryReader Br = new BinaryReader(fstream);
                imageBT = Br.ReadBytes((int)fstream.Length);
                _Car.Image = imageBT;
            }
            else
            {
                _Car.Image = img;

            }
            return _Car;
        }
        private void LimpiarWindow()
        {
            txtMatricula.Clear();
            txtDetalle.Clear();
            txtFileName.Clear();
            cmbMarca.SelectedIndex = 0;
            imgVehiculo.Source = vehiculoImage.Source;
        }

        #region Combo Personalizado

        private void LLenarComboBox()
        {
            try
            {
                var listaMarcas = from m in _Accion.MarkM.GetAll()
                                  orderby m.Name ascending
                                  select new
                                  {
                                      MarkID = m.MarkID,
                                      Name = m.Name,
                                      Picture = _Accion.LoadImage(m.Picture),
                                      State = m.State
                                  };
                cmbMarca.ItemsSource = listaMarcas;
                //cmbMarca.DisplayMemberPath = "Name";
                cmbMarca.SelectedValuePath = "MarkID";
                cmbMarca.SelectedIndex = 0;
                btnMarcaAnterior.IsEnabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos del formulario" + ex.Message, "Error CRVA", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void btnMarcaAnterior_Click(object sender, RoutedEventArgs e)
        {
            if (cmbMarca.SelectedIndex > 0)
                cmbMarca.SelectedIndex = cmbMarca.SelectedIndex - 1;
        }

        private void btnMarcaSiguiente_Click(object sender, RoutedEventArgs e)
        {
            if (cmbMarca.SelectedIndex < cmbMarca.Items.Count - 1)
                cmbMarca.SelectedIndex = cmbMarca.SelectedIndex + 1;
        }
        private void cmbMarca_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbMarca.SelectedIndex == 0)
            {
                btnMarcaAnterior.IsEnabled = false;
            }
            else
            {
                btnMarcaAnterior.IsEnabled = true;
            }

            if (cmbMarca.Items.Count - 1 == cmbMarca.SelectedIndex)
            {
                btnMarcaSiguiente.IsEnabled = false;
            }
            else
            {
                btnMarcaSiguiente.IsEnabled = true;
            }
        }
        #endregion

        private void txtMatricula_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Regex.IsMatch(e.Text, "[^A-Za-z0-9]+");
        }

        private void txtDetalle_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Regex.IsMatch(e.Text, "[^A-Za-z0-9,.-]+");
        }
    }
}

