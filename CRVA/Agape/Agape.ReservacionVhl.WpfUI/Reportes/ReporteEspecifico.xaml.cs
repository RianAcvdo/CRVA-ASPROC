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
    /// Lógica de interacción para ReporteEspecifico.xaml
    /// </summary>
    public partial class ReporteEspecifico : Page
    {
        MetodosBL _Accion = new MetodosBL();
        Car m_Car = new Car();
        Employee m_Employee = new Employee();
        Reservation m_Reservation = new Reservation();
        Image imageCarSheet = new Image();
        int CarIsValid;
        public ReporteEspecifico(Car pCar, int ContentCar=0)
        {
            InitializeComponent();
           m_Car = pCar;
            CarIsValid = ContentCar;
            
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            imageCarSheet.Source = imgSeleccion.Source;

            MesActualReporte();

            dtpFechaInicial.IsEnabled = false;
            dtpFechaFinal.IsEnabled = false;

            if (CarIsValid != 0)
            {
                tbMarca.Text = "Marca: " + m_Car.Mark.Name;
                tbMatricula.Text = "Matrícula: " + m_Car.Code;
                tbFechaAdquisicion.Text = "Adquirido en : " + m_Car.Date.Year;
                imgSeleccion.Source = _Accion.LoadImage(m_Car.Image);
            }
        }

        private void btnGenerar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CarIsValid != 0)
                {
                    GenerarInformeGeneral();
                }
                else
                {
                    //use la cola de mensajes para enviar un mensaje.
                    var messageQueue = sbMensaje.MessageQueue;
                    var message = "Debe seleccionar un vehículo primero";
                    //la cola de mensajes se puede llamar desde cualquier subproceso
                    Task.Factory.StartNew(() => messageQueue.Enqueue(message));
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error - " + ex.Message, "Error Reporte", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void chkFecha_Click(object sender, RoutedEventArgs e)
        {
            if (chkFecha.IsChecked == true)
            {
                dtpFechaInicial.IsEnabled = true;
                dtpFechaFinal.IsEnabled = true;
            }
            else
            {
                dtpFechaInicial.IsEnabled = false;
                dtpFechaFinal.IsEnabled = false;
            }
        }

        private void btnLoadCar_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new SeleccionEmpleadoVehiculo());
        }


        #region generar reporte
        public void GenerarInformeGeneral()
        {
            int totalRegistros = 0;
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

            if (chkFecha.IsChecked == true)
            {
                //Comienza Query de linq
                var ReservacionesRealizadas = from rr in _Accion.ReservationM.GetAll()
                                              where rr.Date >= FechaInicial && rr.Date <= FechaFinal && rr.CarID == m_Car.CarID
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
                totalRegistros = ReservacionesRealizadas.Count();
            }
            else
            {
                //Comienza Query de linq
                var ReservacionesRealizadas = from rr in _Accion.ReservationM.GetAll()
                                              where rr.CarID == m_Car.CarID
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
                totalRegistros = ReservacionesRealizadas.Count();
            }           

            DatosInforme.LocalReport.ReportEmbeddedResource = "Agape.ReservacionVhl.WpfUI.Reportes.ReportGeneralReservacion.rdlc";
            DatosInforme.ZoomMode = ZoomMode.PageWidth;
            if (totalRegistros == 0)
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
                tbTotalRegistros.Text = "Total de registros : " + totalRegistros;
                DatosInforme.RefreshReport();
                //use la cola de mensajes para enviar un mensaje.
                var messageQueue = sbMensaje.MessageQueue;
                var message = "Reporte generado con exito!";
                //la cola de mensajes se puede llamar desde cualquier subproceso
                Task.Factory.StartNew(() => messageQueue.Enqueue(message));
            }
        }

        private void MesActualReporte()
        {
            int MonthDays, Year, Month, FirstDay, EndDay;
            Year = DateTime.Now.Year;
            Month = DateTime.Now.Month;
            MonthDays = DateTime.DaysInMonth(Year, Month);
            FirstDay = (MonthDays + 1) - MonthDays;
            EndDay = MonthDays;
            DateTime inicio = new DateTime(Year, Month, FirstDay);
            DateTime final = new DateTime(Year, Month, EndDay);
            dtpFechaInicial.SelectedDate = inicio;
            dtpFechaFinal.SelectedDate = final;
        }
        #endregion
    }
}
