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
    /// Lógica de interacción para RestauracionOficina.xaml
    /// </summary>
    public partial class RestauracionOficina : Window
    {
        MetodosBL _Accion = new MetodosBL();
        OfficeBL _dataObjBL = new OfficeBL();
        Office _dataObj = new Office();
        Int64 ID;
        DateTime Fecha;
        string OfficeName, Code, Department, Town,Str1,Str2,str3;
        bool State;
        public RestauracionOficina()
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
      
        private void dgvRestaurar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Office _Item = dgvRestaurar.SelectedItem as Office;
                if (_Item != null && _Item.OfficeID > 0)
                {
                    //dgvRestaurar.ItemsSource = new List<Office>();
                    OfficeName = _Item.Name;
                    Code = _Item.Code;
                    Fecha = _Item.Date;
                    Department = _Item.Department;
                    Town = _Item.Town;
                    Str1 = _Item.StreetOne;
                    Str2 = _Item.StreetTwo;
                    str3 = _Item.StreetThree;
                    State = _Item.State;
                    ID = _Item.OfficeID;
                    Code = _Item.Code;
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
                        _dataObj.Name = OfficeName;
                        _dataObj.Department = Department;
                        _dataObj.Code = Code;
                        _dataObj.Date = Fecha;
                        _dataObj.Town = Town;
                        _dataObj.StreetOne = Str1;
                        _dataObj.StreetTwo = Str2;
                        _dataObj.StreetThree = str3;
                        _dataObj.OfficeID = ID;
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
                    string txtBuscarOficina = txtBuscar.Text.ToUpper();
                    //lista a paginar
                    list = await GetPagedListAsync();
                    //Determinamos el estado de los botones
                    btnPrevius.IsEnabled = list.HasPreviousPage;
                    btnSiguiente.IsEnabled = list.HasNextPage;
                    //Cargamos la lista al DataGrid
                    dgvRestaurar.ItemsSource = list.Where(u => u.Name.ToUpper().Contains(txtBuscarOficina) || u.Department.ToUpper().Contains(txtBuscarOficina) || u.Town.ToUpper().Contains(txtBuscarOficina) || u.StreetOne.ToUpper().Contains(txtBuscarOficina) || u.StreetTwo.ToUpper().Contains(txtBuscarOficina) || u.StreetThree.ToUpper().Contains(txtBuscarOficina)).ToList();
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
                MessageBox.Show("Algo anda mal: " + ex.Message, "Error CRVA-EB", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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

        public async Task<IPagedList<Office>> GetPagedListAsync(int pageNumber = 1, int PageSize = 5)
        {
            return await Task.Factory.StartNew(() =>
            {
                return _Accion.OffiM.GetAllDeleted().OrderBy(u => u.OfficeID).ToPagedList(pageNumber, PageSize);
            });
        }


        //Modificamos el evento loaded del window
        //Agregamos el atributo "async" al evento
        private async void frmRestauracionOficinas_Loaded(object sender, RoutedEventArgs e)
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
        private void frmRestauracionOficinas_Loaded_1(object sender, RoutedEventArgs e)
        {
            dgvRestaurar.ItemsSource = _dataObjBL.GetAllDeleted();
        }
        //Fin Paginacion
        #endregion
    }
}
