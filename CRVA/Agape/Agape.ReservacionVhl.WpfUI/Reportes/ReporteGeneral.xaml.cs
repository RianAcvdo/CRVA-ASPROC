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
//referencias necesarias para generar reportes
using Agape.ReservacionVhl.EN;
using System.Data;
using Microsoft.Reporting.WinForms;
using MaterialDesignThemes.Wpf;

namespace Agape.ReservacionVhl.WpfUI.Reportes
{
    /// <summary>
    /// Lógica de interacción para ReporteGeneral.xaml
    /// </summary>
    public partial class ReporteGeneral : Page
    {
        MetodosBL _Accion = new MetodosBL();
        public ReporteGeneral()
        {
            InitializeComponent();
        }

        private void btnGenerar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dtpFechaInicial.SelectedDate.Value != null && dtpFechaFinal.SelectedDate.Value != null)
                {
                    Int64 MarkID, OfficeID;
                    MarkID = Convert.ToInt64(cmbMarca.SelectedValue);
                    OfficeID = Convert.ToInt64(cmbOficina.SelectedValue);
                    if (chkMarca.IsChecked ==true && chkOficina.IsChecked == true)
                    {
                        GenerarInformeGeneralAvanzado(MarkID,OfficeID);
                    }
                    else if(chkMarca.IsChecked == true && chkOficina.IsChecked == false)
                    {
                        GenerarInformeGeneralAvanzado(MarkID,0);
                    }
                    else if(chkMarca.IsChecked == false && chkOficina.IsChecked == true)
                    {
                        GenerarInformeGeneralAvanzado(0,OfficeID);
                    }
                    else
                    {
                        GenerarInformeGeneral();                        
                    }
                }
                else if (dtpFechaInicial.SelectedDate.Value == null)
                {
                    dtpFechaInicial.Focus();
                }
                else
                {
                    dtpFechaFinal.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Error CRVA-Informe",MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }

        #region Generar Informes de reservaciones

        public void GenerarInformeGeneral()
        {
            DateTime FechaInicial = new DateTime();
            DateTime FechaFinal = new DateTime();
            DatosInforme.Reset();
            if (dtpFechaInicial.SelectedDate.Value > dtpFechaFinal.SelectedDate.Value)
            {
                FechaInicial = dtpFechaFinal.SelectedDate.Value;
                FechaFinal = dtpFechaInicial.SelectedDate.Value;
                dtpFechaInicial.SelectedDate = FechaFinal;
                dtpFechaFinal.SelectedDate = FechaInicial;
            }
            else
            {
                FechaInicial = dtpFechaInicial.SelectedDate.Value;
                FechaFinal = dtpFechaFinal.SelectedDate.Value;
            }
            //Comienza Query de linq
            var ReservacionesRealizadas = from rr in _Accion.ReservationM.GetAll()
                                          where rr.Date >= FechaInicial && rr.Date <= FechaFinal
                                          select new
                                          {
                                              Empleado = rr.Employee.Name+" "+rr.Employee.LastName,
                                              Oficina = rr.Employee.Office.Name + " "+rr.Employee.Office.Department,
                                              Auto = rr.Car.Mark.Name,
                                              Fecha = rr.Date,
                                              Inicio = rr.Origin,
                                              Destino = rr.Destination,
                                              Matricula = rr.Car.Code
                                          };
            //Finaliza Query de linq
            DataTable dt = _Accion.LinqQueryToDataTable(ReservacionesRealizadas);
            ReportDataSource rds = new ReportDataSource("DataSetReservacion", dt);
            DatosInforme.LocalReport.DataSources.Add(rds);

            DatosInforme.LocalReport.ReportEmbeddedResource = "Agape.ReservacionVhl.WpfUI.Reportes.ReportGeneralReservacion.rdlc";
            DatosInforme.ZoomMode = ZoomMode.PageWidth;
            if (ReservacionesRealizadas.Count() == 0)
            {
                tbTotalRegistros.Text = "Total de registros : " + 0;
                //use la cola de mensajes para enviar un mensaje.
                var messageQueue = sbMensaje.MessageQueue;
                var message = "No existen registros bajo esos criterios";
                //la cola de mensajes se puede llamar desde cualquier subproceso
                Task.Factory.StartNew(() => messageQueue.Enqueue(message));

            }
            else
            {
                tbTotalRegistros.Text = "Total de registros : " + ReservacionesRealizadas.Count();
                DatosInforme.RefreshReport();
                //use la cola de mensajes para enviar un mensaje.
                var messageQueue = sbMensaje.MessageQueue;
                var message = "Reporte generado con exito!";
                //la cola de mensajes se puede llamar desde cualquier subproceso
                Task.Factory.StartNew(() => messageQueue.Enqueue(message));
            }
        }

        public void GenerarInformeGeneralAvanzado(Int64 MarkID=0,Int64 OfficeID=0)
        {
            DateTime FechaInicial = new DateTime();
            DateTime FechaFinal = new DateTime();
            int TotalRegistros =0;
            DatosInforme.Reset();

            if (dtpFechaInicial.SelectedDate.Value > dtpFechaFinal.SelectedDate.Value)
            {
                FechaInicial = dtpFechaFinal.SelectedDate.Value;
                FechaFinal = dtpFechaInicial.SelectedDate.Value;
                dtpFechaInicial.SelectedDate = FechaFinal;
                dtpFechaFinal.SelectedDate = FechaInicial;
            }
            else
            {
                FechaInicial = dtpFechaInicial.SelectedDate.Value;
                FechaFinal = dtpFechaFinal.SelectedDate.Value;
            }

            if(MarkID>0 && OfficeID > 0)
            {
                //Comienza Query de linq
                var ReservacionesRealizadas = from rr in _Accion.ReservationM.GetAll()
                                              where rr.Date >= FechaInicial && rr.Date <= FechaFinal && 
                                              rr.Car.Mark.MarkID ==MarkID && rr.Employee.Office.OfficeID==OfficeID
                                              select new
                                              {
                                                  Empleado = rr.Employee.Name + " " + rr.Employee.LastName,
                                                  Oficina = rr.Employee.Office.Name + " " + rr.Employee.Office.Department,
                                                  Auto = rr.Car.Mark.Name,
                                                  Fecha = rr.Date,
                                                  Inicio = rr.Origin,
                                                  Destino = rr.Destination,
                                                  Matricula = rr.Car.Code
                                              };
                //Finaliza Query de linq
                DataTable dt = _Accion.LinqQueryToDataTable(ReservacionesRealizadas);
                ReportDataSource rds = new ReportDataSource("DataSetReservacion", dt);
                DatosInforme.LocalReport.DataSources.Add(rds);

                TotalRegistros = ReservacionesRealizadas.Count();
            }
            else if  (MarkID>0 && OfficeID == 0)
            {
                //Comienza Query de linq
                var ReservacionesRealizadas = from rr in _Accion.ReservationM.GetAll()
                                              where rr.Date >= FechaInicial && rr.Date <= FechaFinal &&
                                              rr.Car.Mark.MarkID == MarkID
                                              select new
                                              {
                                                  Empleado = rr.Employee.Name + " " + rr.Employee.LastName,
                                                  Oficina = rr.Employee.Office.Name + " " + rr.Employee.Office.Department,
                                                  Auto = rr.Car.Mark.Name,
                                                  Fecha = rr.Date,
                                                  Inicio = rr.Origin,
                                                  Destino = rr.Destination,
                                                  Matricula = rr.Car.Code
                                              };
                //Finaliza Query de linq
                DataTable dt = _Accion.LinqQueryToDataTable(ReservacionesRealizadas);
                ReportDataSource rds = new ReportDataSource("DataSetReservacion", dt);
                DatosInforme.LocalReport.DataSources.Add(rds);

                TotalRegistros = ReservacionesRealizadas.Count();
            }
            else if  (MarkID==0 && OfficeID > 0)
            {
                //Comienza Query de linq
                var ReservacionesRealizadas = from rr in _Accion.ReservationM.GetAll()
                                              where rr.Date >= FechaInicial && rr.Date <= FechaFinal &&
                                              rr.Employee.Office.OfficeID == OfficeID
                                              select new
                                              {
                                                  Empleado = rr.Employee.Name + " " + rr.Employee.LastName,
                                                  Oficina = rr.Employee.Office.Name + " " + rr.Employee.Office.Department,
                                                  Auto = rr.Car.Mark.Name,
                                                  Fecha = rr.Date,
                                                  Inicio = rr.Origin,
                                                  Destino = rr.Destination,
                                                  Matricula = rr.Car.Code
                                              };
                //Finaliza Query de linq
                DataTable dt = _Accion.LinqQueryToDataTable(ReservacionesRealizadas);
                ReportDataSource rds = new ReportDataSource("DataSetReservacion", dt);
                DatosInforme.LocalReport.DataSources.Add(rds);

                TotalRegistros = ReservacionesRealizadas.Count();
            }

            DatosInforme.LocalReport.ReportEmbeddedResource = "Agape.ReservacionVhl.WpfUI.Reportes.ReportGeneralReservacion.rdlc";
            DatosInforme.ZoomMode = ZoomMode.PageWidth;
            if (TotalRegistros == 0)
            {
                tbTotalRegistros.Text = "Total de registros : " + 0;
                //use la cola de mensajes para enviar un mensaje.
                var messageQueue = sbMensaje.MessageQueue;
                var message = "No existen registros bajo esos criterios";
                //la cola de mensajes se puede llamar desde cualquier subproceso
                Task.Factory.StartNew(() => messageQueue.Enqueue(message));
            }
            else
            {
                tbTotalRegistros.Text = "Total de registros : " + TotalRegistros;
                DatosInforme.RefreshReport();
                //use la cola de mensajes para enviar un mensaje.
                var messageQueue = sbMensaje.MessageQueue;
                var message = "Reporte generado con exito!";
                //la cola de mensajes se puede llamar desde cualquier subproceso
                Task.Factory.StartNew(() => messageQueue.Enqueue(message));
            }
        }

        #endregion

        #region Combo Oficina Personalizado

        private void LLenarComboBoxOficina()
        {
            try
            {
                var listaOficinas = from o in _Accion.OffiM.GetAll()
                                    orderby o.Name ascending
                                    select o;
                cmbOficina.ItemsSource = listaOficinas;
                //cmbMarca.DisplayMemberPath = "Name";
                cmbOficina.SelectedValuePath = "OfficeID";
                cmbOficina.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos del formulario" + ex.Message, "Error CRVA", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        private void cmbOficina_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (chkOficina.IsChecked == true)
            {
                if (cmbOficina.SelectedIndex == 0)
                {
                    btnOficinaAnterior.IsEnabled = false;
                }
                else
                {
                    btnOficinaAnterior.IsEnabled = true;
                }

                if (cmbOficina.Items.Count - 1 == cmbOficina.SelectedIndex)
                {
                    btnOficinaSiguiente.IsEnabled = false;
                }
                else
                {
                    btnOficinaSiguiente.IsEnabled = true;
                }
            }
        }

        private void btnOficinaAnterior_Click(object sender, RoutedEventArgs e)
        {
            if (cmbOficina.SelectedIndex > 0)
                cmbOficina.SelectedIndex = cmbOficina.SelectedIndex - 1;
        }

        private void btnOficinaSiguiente_Click(object sender, RoutedEventArgs e)
        {
            if (cmbOficina.SelectedIndex < cmbOficina.Items.Count - 1)
                cmbOficina.SelectedIndex = cmbOficina.SelectedIndex + 1;
        }
        #endregion  

        #region Combo Marca Personalizado

        private void LLenarComboBoxMarca()
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
            if(chkMarca.IsChecked == true)
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
        }
        #endregion

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            MesActualReporte();
            LLenarComboBoxMarca();
            LLenarComboBoxOficina();
            DeshabilitarOpciones();
        }

        private void chkMarca_Click(object sender, RoutedEventArgs e)
        {
            if(chkMarca.IsChecked == true)
            {
                cmbMarca.IsEnabled = true;
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
            else
            {
                cmbMarca.IsEnabled = false;
                btnMarcaAnterior.IsEnabled = false;
                btnMarcaSiguiente.IsEnabled = false;
            }
        }

        private void chkOficina_Click(object sender, RoutedEventArgs e)
        {
            if (chkOficina.IsChecked == true)
            {
                cmbOficina.IsEnabled = true;
                if (cmbOficina.SelectedIndex == 0)
                {
                    btnOficinaAnterior.IsEnabled = false;
                }
                else
                {
                    btnOficinaAnterior.IsEnabled = true;
                }

                if (cmbOficina.Items.Count - 1 == cmbOficina.SelectedIndex)
                {
                    btnOficinaSiguiente.IsEnabled = false;
                }
                else
                {
                    btnOficinaSiguiente.IsEnabled = true;
                }
            }
            else
            {
                cmbOficina.IsEnabled = false;
                btnOficinaAnterior.IsEnabled = false;
                btnOficinaSiguiente.IsEnabled = false;
            }
        }
        
        private void DeshabilitarOpciones()
        {
            cmbMarca.IsEnabled = false;
            btnMarcaAnterior.IsEnabled = false;
            btnMarcaSiguiente.IsEnabled = false;
            cmbOficina.IsEnabled = false;
            btnOficinaAnterior.IsEnabled = false;
            btnOficinaSiguiente.IsEnabled = false;
        }

        private void MesActualReporte()
        {
            int MonthDays, Year, Month, FirstDay, EndDay;
            Year = DateTime.Now.Year;
            Month = DateTime.Now.Month;
            MonthDays = DateTime.DaysInMonth(Year,Month);
            FirstDay = (MonthDays + 1) - MonthDays;
            EndDay = MonthDays;
            DateTime inicio = new DateTime(Year,Month,FirstDay);
            DateTime final = new DateTime(Year, Month,EndDay);            
            dtpFechaInicial.SelectedDate = inicio;
            dtpFechaFinal.SelectedDate = final;
        }
    }
}
