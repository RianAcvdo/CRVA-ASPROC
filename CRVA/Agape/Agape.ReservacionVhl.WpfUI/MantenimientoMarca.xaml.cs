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

namespace Agape.ReservacionVhl.WpfUI
{
    /// <summary>
    /// Lógica de interacción para MantenimientoMarca.xaml
    /// </summary>
    public partial class MantenimientoMarca : Window
    {
        private MetodosBL _Accion = new MetodosBL();
        Mark _Mark = new Mark();
        Int64 ID = 0;
        byte[] img;
        Image marcaImage = new Image();
        public MantenimientoMarca()
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

        private async void txtBuscar_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(txtBuscar.Text.Trim()))
                {
                    string txtBuscarMarca = txtBuscar.Text.ToUpper();
                    //dgvMarca.Items.Refresh();
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
                MessageBox.Show("Algo anda mal : " + ex.Message, "Error CRVA-MB", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void dgvMarca_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Mark _Marca = dgvMarca.SelectedItem as Mark;
                if (_Marca != null && _Marca.MarkID > 0)
                {
                    //dgvMarca.ItemsSource = new List<Mark>();
                    txtNombre.Text = _Marca.Name;
                    ID = _Marca.MarkID;
                    img = _Marca.Picture;
                    imgMarca.Visibility = Visibility.Visible;
                    imgMarca.Source = _Accion.LoadImage(img);
                    EstadoBotones(1);
                    //LlenarDGV();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Algo anda mal : " + ex.Message, "Error CRVA-MS", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ID <= 0)
                {
                    MessageBox.Show("Selecciona un Item","Alerta CRVA-ME",MessageBoxButton.OK,MessageBoxImage.Information);
                }
                else
                {
                    if (MessageBox.Show("Seguro que desea eliminar?", "Eliminar", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    {
                        _Mark.Picture = img;
                        _Mark.State = false;
                        _Mark.Name = txtNombre.Text.ToUpper();
                        _Mark.MarkID = ID;
                        _Accion.MarkM.UpdateMarks(_Mark);
                        imgMarca.Source = null;
                        LimpiarWindow();
                        MessageBox.Show("Registro eliminado exitosamente", "Exito CRVA-ME", MessageBoxButton.OK, MessageBoxImage.Information);
                        EstadoBotones(0);

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
            catch (Exception ex)
            {
                MessageBox.Show("Algo anda mal : " + ex.Message, "Error CRVA-ME", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtNombre.Text.Trim() != string.Empty)
                {
                    Mark _datoMarca = DatosMarca();
                    _datoMarca.MarkID = ID;
                    var verificar = from u in _Accion.MarkM.GetAllData() where u.Name.ToUpper() == txtNombre.Text.ToUpper() select u;
                    if (verificar.Count() > 0)
                    {

                        if (verificar.FirstOrDefault().MarkID == _datoMarca.MarkID)
                        {
                            _Accion.MarkM.UpdateMarks(_datoMarca);
                            LimpiarWindow();
                            MessageBox.Show("Registro guardado exitosamente", "Exito CRVA-MM", MessageBoxButton.OK, MessageBoxImage.Information);
                            //EstadoBotones(0);
                            //LlenarDGV();
                        }
                        else
                        {
                            MessageBox.Show("Ya existe una marca con ese nombre", "Alerta CRVA-MM", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                            txtNombre.Focus();
                        }
                    }
                    else
                    {
                        _Accion.MarkM.UpdateMarks(_datoMarca);
                        LimpiarWindow();
                        MessageBox.Show("Registro guardado exitosamente", "Exito CRVA-MM", MessageBoxButton.OK, MessageBoxImage.Information);
                        //LlenarDGV();
                        //EstadoBotones(0);
                    }

                    //Canfiguracion de los botones
                    EstadoBotones(0);
                    MantenimientoMarca _window = new MantenimientoMarca();
                    _window.Show();
                    this.Close();
                }
                else
                {
                    txtNombre.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Algo anda mal : " + ex.Message, "Error CRVA-MM", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtNombre.Text.Trim() != string.Empty && txtFileName.Text.Trim() !=string.Empty)
                {
                    Mark _datoMarca = DatosMarca();
                    var verificar = from u in _Accion.MarkM.GetAllData() where u.Name.ToUpper() == txtNombre.Text.ToUpper() select u;
                    if (verificar.Count() > 0)
                    {

                        MessageBox.Show("Ya existe una marca con ese nombre", "Alerta CRVA-MG", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        txtNombre.Focus();
                    }
                    else
                    {
                       _Accion.MarkM.Create(_datoMarca);
                        LimpiarWindow();
                        MessageBox.Show("Registro guardado exitosamente", "Exito CRVA-MG", MessageBoxButton.OK, MessageBoxImage.Information);
                        //LlenarDGV();
                        EstadoBotones(0);
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
                else if (txtNombre.Text.Trim() == string.Empty)
                {
                    txtNombre.Focus();
                }
                else
                {
                    btnLoadFile.Focus();
                    MessageBox.Show("Carga una imagen", "Alerta CRVA-MG", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Algo anda mal : " + ex.Message, "Error CRVA-MG", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnLoadFile_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog op = new OpenFileDialog();
                op.Title = "Seleccione una imagen";
                op.Filter = "Archivos de imagen|*.jpg;*.jpeg;*.png|" +
                  "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                  "Portable Network Graphic (*.png)|*.png";
                if (op.ShowDialog() == true)
                {
                    imgMarca.Source = new BitmapImage(new Uri(op.FileName));
                    txtFileName.Text = op.FileName;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Algo anda mal : " + ex.Message, "Error CRVA-ML", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private Mark DatosMarca()
        {
            Mark _marca = new Mark();
            _marca.Name = txtNombre.Text.ToUpper();
            _marca.State = true;
            if (txtFileName.Text != string.Empty)
            {
                FileStream fstream = new FileStream(txtFileName.Text, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fstream);
                _marca.Picture = br.ReadBytes((int)fstream.Length);
            }
            else
            {
                _marca.Picture = img;
            }
            return _marca;
        }

        private void LimpiarWindow()
        {
            txtNombre.Clear();
            txtFileName.Clear();
            imgMarca.Source = marcaImage.Source;
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            LimpiarWindow();
            EstadoBotones(0);
        }

        /// <summary>
        /// recibe valores del tipo int, ya sea un 1 o 0, 
        /// 2 para habilitar la accion de restaurar, 
        /// 1 para habilitar las acciones modificar y eliminar, 
        /// 0 para deshabilitar las acciones modificar y eliminar
        /// </summary>
        /// <param name="estado"></param>
        private void EstadoBotones(int estado)
        {
            if(estado==1)
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

        private void LlenarDGV()
        {
            try
            {
                dgvMarca.Items.Refresh();
                dgvMarca.Items.Clear();
                //dgvMarca.ItemsSource = new List<Mark>();
                foreach (var item in _Accion.MarkM.GetAll())
                {
                    dgvMarca.Items.Add(item);
                }
                
            }
            catch(Exception ex)
            {
                MessageBox.Show("Algo anda mal : " + ex.Message, "Error CRVA-MLl", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private async void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.F5)
            {
                txtBuscar.Clear();
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
            else if(e.Key == Key.Delete)
            {
                LimpiarWindow();
            }
        }


        #region Paginacion
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
                return _Accion.MarkM.GetAll().OrderBy(u => u.MarkID).ToPagedList(pageNumber, PageSize);
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
            EstadoBotones(0);
        }
        //Fin Paginacion
       #endregion

        private void txtNombre_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Regex.IsMatch(e.Text, "[^A-Za-z0-9]+");
        }
    }
}
