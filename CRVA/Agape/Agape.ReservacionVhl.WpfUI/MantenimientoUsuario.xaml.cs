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
    /// Lógica de interacción para MantenimientoUsuario.xaml
    /// </summary>
    public partial class MantenimientoUsuario : Window
    {
        //UserBL _UserBL = new UserBL();
        //User _User = new User();
        MetodosBL _Accion = new MetodosBL();
        //String Code 
        Int64 ID = 0;
        public MantenimientoUsuario()
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

        private void btnGenerarReportes_Click(object sender, RoutedEventArgs e)
        {
            Reportes.WindowReports window = new WindowReports(1);
            window.Show();
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

        private async void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (txtNombre.Text.Trim() != string.Empty && txtApellido.Text.Trim() != string.Empty && txtUserName.Text.Trim() != string.Empty && Password.Password.Trim() != string.Empty && PasswordConfirmar.Password.Trim() != string.Empty && txtEmail.Text.Trim() != string.Empty)
                {
                    User _datoUsuario = DatosUsuarios();
                    var verificar = from u in _Accion.User.GetAllData() where u.Email.ToUpper() == txtEmail.Text.ToUpper() || u.UserName.ToUpper() == txtUserName.Text.ToUpper() select u;
                    if (verificar.Count() > 0)
                    {

                        MessageBox.Show("Ya existe un usuario con ese e-mail y/o nombre de usuario", "Alerta CRVA-UG", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        txtEmail.Focus();
                    }
                    else
                    {
                        if (Password.Password == PasswordConfirmar.Password)
                        {
                            if (!Regex.IsMatch(txtEmail.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
                            {
                                MessageBox.Show("Ingrese un correo electrónico valido", "Alerta CRVA-UG", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                                txtEmail.Focus();
                            }
                            else
                            {
                                _Accion.User.Create(_datoUsuario);
                                MessageBox.Show("Registro guardado exitosamente", "Exito CRVA-UG", MessageBoxButton.OK, MessageBoxImage.Information);
                                Limpiar();
                            }                            
                        }
                        else
                        {
                            MessageBox.Show("Las contraseñas no coinciden", "Alerta CRVA-UX", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                            Password.Focus();
                        }
                    }
                    EstadoBotones(0);
                    //lista a paginar
                    list = await GetPagedListAsync();
                    //Determinamos el estado de los botones
                    btnPrevius.IsEnabled = list.HasPreviousPage;
                    btnSiguiente.IsEnabled = list.HasNextPage;
                    //Cargamos la lista al DataGrid
                    dgvUsuario.ItemsSource = list.ToList();
                    //Establecemos el numero de paginas del DataGrid
                    tbPaginacion.Text = string.Format("Pagina {0}/{1}", pageNumber, list.PageCount);
                }
                else if (txtNombre.Text.Trim() == string.Empty)
                {
                    txtNombre.Focus();
                }
                else if (txtApellido.Text.Trim() == string.Empty)
                {
                    txtApellido.Focus();
                }
                else if (txtUserName.Text.Trim() == string.Empty)
                {
                    txtUserName.Focus();
                }
                else if(txtEmail.Text.Trim() == string.Empty)
                {
                    txtEmail.Focus();
                }
                else if (Password.Password.Trim() == string.Empty)
                {
                    Password.Focus();
                }
                else
                {
                    PasswordConfirmar.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Algo anda mal : " + ex.Message, "Error CRVA-UG", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private async void txtBuscar_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if(!String.IsNullOrEmpty(txtBuscar.Text.Trim()))
                {
                    string txtBuscarUsuario = txtBuscar.Text.ToUpper();
                    //lista a paginar
                    list = await GetPagedListAsync();
                    //Determinamos el estado de los botones
                    btnPrevius.IsEnabled = list.HasPreviousPage;
                    btnSiguiente.IsEnabled = list.HasNextPage;
                    //Cargamos la lista al DataGrid
                    dgvUsuario.ItemsSource = list.Where(u => u.Name.ToUpper().Contains(txtBuscarUsuario) || u.LastName.ToUpper().Contains(txtBuscarUsuario) || u.UserName.ToUpper().Contains(txtBuscarUsuario) || u.Email.ToUpper().Contains(txtBuscarUsuario)).ToList();
                    //Establecemos el numero de paginas del DataGrid
                    tbPaginacion.Text = string.Format("Pagina {0}/{1}", pageNumber, list.PageCount);
                    //Otros metodos
                    EstadoBotones(0);
                }
                else
                {
                    //lista a paginar
                    list = await GetPagedListAsync();
                    //Determinamos el estado de los botones
                    btnPrevius.IsEnabled = list.HasPreviousPage;
                    btnSiguiente.IsEnabled = list.HasNextPage;
                    //Cargamos la lista al DataGrid
                    dgvUsuario.ItemsSource = list.ToList();
                    //Establecemos el numero de paginas del DataGrid
                    tbPaginacion.Text = string.Format("Pagina {0}/{1}", pageNumber, list.PageCount);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Algo anda mal : " + ex.Message, "Error CRVA-UB", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                User _datoUsuario = DatosUsuarios();
                _datoUsuario.UserID = ID;
                if (txtNombre.Text != string.Empty && txtApellido.Text != string.Empty && txtUserName.Text != string.Empty && Password.Password != string.Empty && PasswordConfirmar.Password != string.Empty && txtEmail.Text != string.Empty)
                {
                    var verificar = from u in _Accion.User.GetAll() where u.Email.ToUpper() == txtEmail.Text.ToUpper() || u.UserName.ToUpper() == txtUserName.Text.ToUpper() select u;
                    if (verificar.Count() > 0)
                    {

                        if (verificar.FirstOrDefault().UserID == _datoUsuario.UserID)
                        {
                            if (Password.Password == PasswordConfirmar.Password)
                            {
                                if (!Regex.IsMatch(txtEmail.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
                                {
                                    MessageBox.Show("Ingrese un correo electrónico valido", "Alerta CRVA-UM", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                                    txtEmail.Focus();
                                }
                                else
                                {
                                    _Accion.User.UpdateItem(_datoUsuario);
                                    //abrir y cerrar ventana
                                    MantenimientoUsuario _window = new MantenimientoUsuario();
                                    _window.Show();
                                    Close();
                                    MessageBox.Show("Registro guardado exitosamente", "Exito CRVA-UM", MessageBoxButton.OK, MessageBoxImage.Information);
                                }                               
                            }
                            else
                            {
                                MessageBox.Show("Las contraseñas no coinciden", "Alerta CRVA-UM", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                                Password.Focus();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Ya existe un usuario con ese e-mail y/o user name", "Alerta CRVA-UM", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                            txtEmail.Focus();
                        }
                    }
                    else
                    {
                        if (Password.Password == Password.Password)
                        {
                            if (!Regex.IsMatch(txtEmail.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
                            {
                                MessageBox.Show("Ingrese un correo electrónico valido", "Alerta CRVA-UM", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                                txtEmail.Focus();
                            }
                            else
                            {
                                _Accion.User.UpdateItem(_datoUsuario);
                                //abrir y cerrar ventana
                                MantenimientoUsuario _window = new MantenimientoUsuario();
                                _window.Show();
                                MessageBox.Show("Registro guardado exitosamente", "Exito CRVA-UM", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Las contraseñas no coinciden", "Alerta HM-1001", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                            Password.Focus();
                        }
                    }
                    //EstadoBotones(0);                    
                }
                else if (txtNombre.Text == string.Empty)
                {
                    txtNombre.Focus();
                }
                else if (txtApellido.Text == string.Empty)
                {
                    txtApellido.Focus();
                }
                else if (txtUserName.Text == string.Empty)
                {
                    txtUserName.Focus();
                }
                else if (txtEmail.Text == string.Empty)
                {
                    txtEmail.Focus();
                }
                else if (Password.Password == string.Empty)
                {
                    Password.Focus();
                }
                else
                {
                    PasswordConfirmar.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Algo anda mal : " + ex.Message, "Error CRVA-UM", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                User _datoUsuario = DatosUsuarios();
                _datoUsuario.UserID = ID;
                if (ID <= 0)
                {
                    MessageBox.Show("Selecciona el registro que desea eliminar");
                }
                else
                {
                    if (MessageBox.Show("Seguro que desea eliminar?", "Eliminar", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    {
                        _datoUsuario.State = false;
                        _Accion.User.UpdateItem(_datoUsuario);
                        //lista a paginar
                        list = await GetPagedListAsync();
                        //Determinamos el estado de los botones
                        btnPrevius.IsEnabled = list.HasPreviousPage;
                        btnSiguiente.IsEnabled = list.HasNextPage;
                        //Cargamos la lista al DataGrid
                        dgvUsuario.ItemsSource = list.ToList();
                        //Establecemos el numero de paginas del DataGrid
                        tbPaginacion.Text = string.Format("Pagina {0}/{1}", pageNumber, list.PageCount);
                        Limpiar();
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Algo anda mal : " + ex.Message, "Error CRVA-UE", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
            EstadoBotones(0);
        }

        private void dgvUsuario_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                User _User = dgvUsuario.SelectedItem as User;
                if (_User != null && _User.UserID > 0)
                {
                    //dgvUsuario.ItemsSource = new List<User>();
                    txtNombre.Text = _User.Name;
                    ID = _User.UserID;
                    txtApellido.Text = _User.LastName;
                    txtUserName.Text = _User.UserName;
                    txtEmail.Text = _User.Email;
                    Password.Password = _User.Password;
                    PasswordConfirmar.Password = _User.Password;
                    EstadoBotones(1);
                    //dgvUsuario.ItemsSource = _Accion.User.GetAll();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Algo anda mal : " + ex.Message, "Error CRVA-US", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private Boolean ValidacionEmail(String email)
        {
            String expresion;
            expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(email, expresion))
            {
                if (Regex.Replace(email, expresion, String.Empty).Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        //Metodos de mantenimiento de usuario
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
        private User DatosUsuarios()
        {
            User _User = new User();
            _User.Name =     txtNombre.Text.ToUpper();
            _User.LastName = txtApellido.Text.ToUpper();
            _User.UserName = txtUserName.Text.ToUpper();
            _User.Password = PasswordConfirmar.Password;
            _User.Email =    txtEmail.Text.ToLower();
            _User.State = true;
            return _User;
        }

        private void Limpiar()
        {
            txtNombre.Text = string.Empty;
            txtApellido.Text = string.Empty;
            txtUserName.Text = string.Empty;
            txtEmail.Text = string.Empty;
            Password.Password = string.Empty;
            PasswordConfirmar.Password = string.Empty;
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

        private void txtUserName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int ascci = Convert.ToInt32(Convert.ToChar(e.Text));
            if (ascci >= 65 && ascci <= 90 || ascci >= 97 && ascci <= 122)
                e.Handled = false;
            else e.Handled = true;
        }

        private void Password_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Regex.IsMatch(e.Text, "[^A-Za-z0-9]+");
        }

        private void PasswordConfirmar_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Regex.IsMatch(e.Text, "[^A-Za-z0-9]+");
        }

        private void txtEmail_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Regex.IsMatch(e.Text, "[^A-Za-z0-9_@.-]+");
            //ValidacionEmail(txtEmail.Text);
        }

        #region 
        //Paginacion DATAGRID
        //variables
        int pageNumber = 1;
        IPagedList<User> list;


        //Codigo botones
        private async void btnPrevius_Click(object sender, RoutedEventArgs e)
        {
            if (list.HasPreviousPage)
            {
                list = await GetPagedListAsync(--pageNumber);
                btnPrevius.IsEnabled = list.HasPreviousPage;
                btnSiguiente.IsEnabled = list.HasNextPage;
                dgvUsuario.ItemsSource = list.ToList();
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
                dgvUsuario.ItemsSource = list.ToList();
                tbPaginacion.Text = string.Format("Pagina {0}/{1}", pageNumber, list.PageCount);
            }
        }


        //metodo que llena el DataGrid

        public async Task<IPagedList<User>> GetPagedListAsync(int pageNumber = 1, int PageSize = 5)
        {
            return await Task.Factory.StartNew(() =>
            {
                return _Accion.User.GetAll().OrderBy(u => u.UserID).ToPagedList(pageNumber, PageSize);
            });
        }


        //Modificamos el evento loaded del window
        //Agregamos el atributo "async" al evento
        private async void frmMantenimientoUsuario_Loaded(object sender, RoutedEventArgs e)
        {
            //lista a paginar
            list = await GetPagedListAsync();
            //Determinamos el estado de los botones
            btnPrevius.IsEnabled = list.HasPreviousPage;
            btnSiguiente.IsEnabled = list.HasNextPage;
            //Cargamos la lista al DataGrid
            dgvUsuario.ItemsSource = list.ToList();
            //Establecemos el numero de paginas del DataGrid
            tbPaginacion.Text = string.Format("Pagina {0}/{1}", pageNumber, list.PageCount);

            //Otros metodos
            EstadoBotones(0);
        }
        //Fin Paginacion
        #endregion
    }
}
