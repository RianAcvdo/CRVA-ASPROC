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
    /// Lógica de interacción para MantenimientoOficinas.xaml
    /// </summary>
    public partial class MantenimientoOficinas : Window
    {
        MetodosBL _Accion = new MetodosBL();
        Office _Office = new Office();
        OfficeBL _OfficeBL = new OfficeBL();
        String Code;
        Int64 ID = 0;
        DateTime Date;
        bool State;

        public MantenimientoOficinas()
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

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Office _Office = dgvOficinas.SelectedItem as Office;
                if (_Office != null && _Office.OfficeID > 0)
                {
                    //dgvOficinas.ItemsSource = new List<Office>();
                    txtName.Text = _Office.Name;
                    txtDepartment.SelectedItem = _Office.Department;
                    txtTown.Text = _Office.Town;
                    txtStreet1.Text = _Office.StreetOne;
                    txtStreet2.Text = _Office.StreetTwo;
                    txtStreet3.Text = _Office.StreetThree;
                    Date = _Office.Date;
                    Code = _Office.Code;
                    State = _Office.State;
                    ID = _Office.OfficeID;
                    EstadoBotones(1);
                    //dgvOficinas.ItemsSource = _OfficeBL.GetAll();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Algo anda mal : " + ex.Message, "Error CRVA-UG", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        private async void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtName.Text == ""
                || txtDepartment.Text == ""
                || txtStreet2.Text == ""
                || txtTown.Text == ""
                || txtStreet3.Text == ""
                || txtStreet1.Text == "")

                {
                    MessageBox.Show("No dejar campos vacíos");
                }
                else if (txtName.Text.Trim() == string.Empty)
                {
                    txtName.Focus();
                }
                else if (txtDepartment.Text.Trim() == string.Empty)
                {
                    txtDepartment.Focus();
                }
                else if (txtTown.Text.Trim() == string.Empty)
                {
                    txtTown.Focus();
                }
                else if (txtStreet1.Text.Trim() == string.Empty)
                {
                    txtStreet1.Focus();
                }
                else
                {
                    //DateTime ThisDay = new DateTime();
                    //ThisDay = Convert.ToDateTime(txtDate.Text);
                    _Office.Code = CrearCode(8);
                    _Office.Name = txtName.Text.ToUpper();
                    _Office.Department = txtDepartment.Text.ToUpper();
                    _Office.Date = DateTime.Now;
                    _Office.Town = txtTown.Text.ToUpper();
                    _Office.StreetOne = txtStreet1.Text.ToUpper();
                    _Office.StreetTwo = txtStreet2.Text.ToUpper();
                    _Office.StreetThree = txtStreet3.Text.ToUpper();
                    _Office.State = true;
                    _OfficeBL.Create(_Office);
                    //dgvOficinas.ItemsSource = _OfficeBL.GetAll();
                    Limpiar();
                    EstadoBotones(0);
                    MessageBox.Show("Registro guardado exitosamente", "Exito CRVA-VG", MessageBoxButton.OK, MessageBoxImage.Information);
                    
                    //lista a paginar
                    list = await GetPagedListAsync();
                    //Determinamos el estado de los botones
                    btnPrevius.IsEnabled = list.HasPreviousPage;
                    btnSiguiente.IsEnabled = list.HasNextPage;
                    //Cargamos la lista al DataGrid
                    dgvOficinas.ItemsSource = list.ToList();
                    //Establecemos el numero de paginas del DataGrid
                    tbPaginacion.Text = string.Format("Pagina {0}/{1}", pageNumber, list.PageCount);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Algo anda mal : " + ex.Message, "Error CRVA-UG", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtName.Text == ""
                && txtDepartment.Text == ""
                && txtStreet2.Text == ""
                && txtTown.Text == ""
                && txtStreet3.Text == ""
                && txtStreet1.Text == "")
                {
                    dgvOficinas.ItemsSource = _OfficeBL.GetAll();
                }
                else
                {
                    _Office.Name = txtName.Text.ToUpper();
                    _Office.Department = txtDepartment.Text.ToUpper();
                    _Office.Town = txtTown.Text.ToUpper();
                    _Office.StreetOne = txtStreet1.Text.ToUpper();
                    _Office.StreetTwo = txtStreet2.Text.ToUpper();
                    _Office.StreetThree = txtStreet3.Text.ToUpper();
                    _Office.State = true;
                    //dgvOficinas.ItemsSource = _OfficeBL.Search(_Office);
                    Limpiar();
                    EstadoBotones(0);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Algo anda mal : " + ex.Message, "Error CRVA-UG", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtName.Text == ""
                || txtDepartment.Text == ""
                || txtStreet2.Text == ""
                || txtTown.Text == ""
                || txtStreet3.Text == ""
                || txtStreet1.Text == ""
                || ID <= 0)

                {
                    MessageBox.Show("Selecciona un Item");
                }
                else if (txtName.Text.Trim() == string.Empty)
                {
                    txtName.Focus();
                }
                else if (txtDepartment.Text.Trim() == string.Empty)
                {
                    txtDepartment.Focus();
                }
                else if (txtTown.Text.Trim() == string.Empty)
                {
                    txtTown.Focus();
                }
                else if (txtStreet1.Text.Trim() == string.Empty)
                {
                    txtStreet1.Focus();
                }
                else
                {
                    //DateTime ThisDay = new DateTime();
                    //ThisDay = Convert.ToDateTime(txtDate.Text);
                    _Office.OfficeID = ID;
                    _Office.Code = Code;
                    _Office.Name = txtName.Text.ToUpper();
                    _Office.Department = txtDepartment.Text.ToUpper();
                    _Office.Date = DateTime.Now;
                    _Office.Town = txtTown.Text.ToUpper();
                    _Office.StreetOne = txtStreet1.Text.ToUpper();
                    _Office.StreetTwo = txtStreet2.Text.ToUpper();
                    _Office.StreetThree = txtStreet3.Text.ToUpper();
                    _Office.State = true;
                    _OfficeBL.UpdateItem(_Office);
                    //dgvOficinas.ItemsSource = _OfficeBL.GetAll();
                    Limpiar();
                    EstadoBotones(0);
                    MessageBox.Show("Registro guardado exitosamente", "Exito CRVA-MG", MessageBoxButton.OK, MessageBoxImage.Information);
                    MantenimientoOficinas _window = new MantenimientoOficinas();
                    _window.Show();
                    Close();
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
                if (txtName.Text == ""
                || txtDepartment.Text == ""
                || txtStreet2.Text == ""
                || txtTown.Text == ""
                || txtStreet3.Text == ""
                || txtStreet1.Text == ""
                || ID <= 0)
                {
                    MessageBox.Show("Selecciona un Item");
                }
                else
                {
                    if (MessageBox.Show("Seguro que desea eliminar?", "Eliminar", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    {
                        //DateTime ThisDay = new DateTime();
                        //ThisDay = Convert.ToDateTime(txtDate.Text);
                        _Office.Code = Code;
                        _Office.Name = txtName.Text.ToUpper();
                        _Office.Department = txtDepartment.Text.ToUpper();
                        _Office.Date = DateTime.Now;
                        _Office.Town = txtTown.Text.ToUpper();
                        _Office.StreetOne = txtStreet1.Text.ToUpper();
                        _Office.StreetTwo = txtStreet2.Text.ToUpper();
                        _Office.StreetThree = txtStreet3.Text.ToUpper();
                        _Office.State = false;
                        _Office.OfficeID = ID;
                        _OfficeBL.UpdateItem(_Office);
                        //dgvOficinas.ItemsSource = _OfficeBL.GetAll();
                        Limpiar();
                        EstadoBotones(0);

                        //lista a paginar
                        list = await GetPagedListAsync();
                        //Determinamos el estado de los botones
                        btnPrevius.IsEnabled = list.HasPreviousPage;
                        btnSiguiente.IsEnabled = list.HasNextPage;
                        //Cargamos la lista al DataGrid
                        dgvOficinas.ItemsSource = list.ToList();
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

        private async void frmMantenimientoOficinas_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                //dgvOficinas.ItemsSource = _OfficeBL.GetAll();
                EstadoBotones(0);
                LlenarComboBoxDepartment();

                //lista a paginar
                list = await GetPagedListAsync();
                //Determinamos el estado de los botones
                btnPrevius.IsEnabled = list.HasPreviousPage;
                btnSiguiente.IsEnabled = list.HasNextPage;
                //Cargamos la lista al DataGrid
                dgvOficinas.ItemsSource = list.ToList();
                //Establecemos el numero de paginas del DataGrid
                tbPaginacion.Text = string.Format("Pagina {0}/{1}", pageNumber, list.PageCount);
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
                    string txtBuscarOficina = txtBuscar.Text.ToUpper();
                    //lista a paginar
                    list = await GetPagedListAsync();
                    //Determinamos el estado de los botones
                    btnPrevius.IsEnabled = list.HasPreviousPage;
                    btnSiguiente.IsEnabled = list.HasNextPage;
                    //Cargamos la lista al DataGrid
                    dgvOficinas.ItemsSource = list.Where(u => u.Name.ToUpper().Contains(txtBuscarOficina) || u.Department.ToUpper().Contains(txtBuscarOficina) || u.Town.ToUpper().Contains(txtBuscarOficina) || u.StreetOne.ToUpper().Contains(txtBuscarOficina) || u.StreetTwo.ToUpper().Contains(txtBuscarOficina) || u.StreetThree.ToUpper().Contains(txtBuscarOficina)).ToList();
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
                    dgvOficinas.ItemsSource = list.ToList();
                    //Establecemos el numero de paginas del DataGrid
                    tbPaginacion.Text = string.Format("Pagina {0}/{1}", pageNumber, list.PageCount);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Error CRVA-OB",MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }

        private void txtName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

            int ascci = Convert.ToInt32(Convert.ToChar(e.Text));
            if (ascci >= 65 && ascci <= 90 || ascci >= 97 && ascci <= 122)
                e.Handled = false;
            else e.Handled = true;
        }

        private void txtDepartment_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int ascci = Convert.ToInt32(Convert.ToChar(e.Text));
            if (ascci >= 65 && ascci <= 90 || ascci >= 97 && ascci <= 122)
                e.Handled = false;
            else e.Handled = true;
        }

        private void txtTown_PreviewTextInput(object sender, TextCompositionEventArgs e)
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
            txtName.Text = string.Empty;
            txtDepartment.SelectedIndex = 0;
            txtTown.Text = string.Empty;
            txtStreet1.Text = string.Empty;
            txtStreet2.Text = string.Empty;
            txtStreet3.Text = string.Empty;
        }
        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
            EstadoBotones(0);
        }

        private void LlenarComboBoxDepartment()
        {
            List<string> DepartamentoLista = new List<string>();
            DepartamentoLista.Add("AHUACHAPÁN");//1
            DepartamentoLista.Add("CABAÑAS");//2
            DepartamentoLista.Add("CHALATENANGO");//3
            DepartamentoLista.Add("CUSCATLÁN");//4
            DepartamentoLista.Add("LA LIBERTAD");//5
            DepartamentoLista.Add("LA PAZ");//6
            DepartamentoLista.Add("LA UNIÓN");//7
            DepartamentoLista.Add("MORAZÁN");//8
            DepartamentoLista.Add("SAN MIGUEL");//9
            DepartamentoLista.Add("SAN SALVADOR");//10
            DepartamentoLista.Add("SAN VICENTE");//11
            DepartamentoLista.Add("SANTA ANA");//12
            DepartamentoLista.Add("SONSONATE");//13
            DepartamentoLista.Add("USULUTÁN");//14
            txtDepartment.ItemsSource = DepartamentoLista;
            txtDepartment.SelectedIndex = 0;
        }

        #region Paginacion
        //Paginacion DATAGRID
        //variables
        int pageNumber = 1;
        IPagedList<Office> list;


        //Codigo botones
        private async void btnPrevius_Click(object sender, RoutedEventArgs e)
        {
            if (list.HasPreviousPage)
            {
                list = await GetPagedListAsync(--pageNumber);
                btnPrevius.IsEnabled = list.HasPreviousPage;
                btnSiguiente.IsEnabled = list.HasNextPage;
                dgvOficinas.ItemsSource = list.ToList();
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
                dgvOficinas.ItemsSource = list.ToList();
                tbPaginacion.Text = string.Format("Pagina {0}/{1}", pageNumber, list.PageCount);
            }
        }


        //metodo que llena el DataGrid

        public async Task<IPagedList<Office>> GetPagedListAsync(int pageNumber = 1, int PageSize = 5)
        {
            return await Task.Factory.StartNew(() =>
            {
                return _Accion.OffiM.GetAll().OrderBy(u => u.OfficeID).ToPagedList(pageNumber, PageSize);
            });
        }


        //Modificamos el evento loaded del window
        //Agregamos el atributo "async" al evento

        #endregion
    }
}
