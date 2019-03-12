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
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;
using System.IO;
using System.Data;
using Microsoft.Win32;
using Agape.ReservacionVhl.BL;

namespace Agape.ReservacionVhl.WpfUI
{
    /// <summary>
    /// Lógica de interacción para BackupOptions.xaml
    /// </summary>
    public partial class BackupOptions : Window
    {
        DataTable dtServers = SmoApplication.EnumAvailableSqlServers(true);
        private static Server srvr;


        public Restore rstDatabase = new Restore();
        public Backup bkpDatabase = new Backup();
        public BackUpBL backUpBL = new BackUpBL();

        public static string NombreServidor;
        public static string NombreDB;
        private string DBpath = AppDomain.CurrentDomain.BaseDirectory;

        public BackupOptions()
        {
            InitializeComponent();
        }
        private void lbRegresar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MainMenuAdministracion Window = new MainMenuAdministracion();
            this.Hide();
            Window.ShowDialog();
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            pbProgress.Visibility = Visibility.Hidden;
            WindowState = WindowState.Normal;
            //txt_username.IsEnabled = false;
            //txt_password.IsEnabled = false;
            Conexion();
            try
            {
                //chk_Insec.IsChecked = true;

                txtInfoServerDB.Text = NombreServidor + " " + NombreDB;
            }
            catch (Exception)
            {
                MessageBox.Show("ERROR: No hay servidores disponibles.\nU ocurrió mientras se caragaba el nombre del servidor.", "Error de servidor", MessageBoxButton.OK, MessageBoxImage.Error);
            }


        }


        private void db_change_Click(object sender, RoutedEventArgs e)
        {
            //cmbDataBase.IsEnabled = true;
        }

        #region BackUp

        #region btn_BackUp Click
        private async void Backup_Click(object sender, RoutedEventArgs e)
        {
            pbProgress.Visibility = Visibility.Visible;

            await Task.Run(() =>
            {

                BackUp();
            });
            pbProgress.Visibility = Visibility.Hidden;
        }
        #endregion

        #region BackUp Metodo
        public void BackUp()
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.FileName = "CRVA Data";
            saveFileDialog1.Filter = "Bak File|*.bak";
            saveFileDialog1.Title = "Guarde un archivo de BackUp.";
            saveFileDialog1.ShowDialog();
            Conexion();

            if (srvr != null)
            {
                try
                {

                    //Use this line if you have already created a bakup file.
                    File.Delete(DBpath + "\\backup.bak");
                   
                    // Date when Backup is created
                    string Backupdate = "Backup-" + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss").Replace("/", "-").Replace(":", " ") + ".bak";
                    // Set the backup device to a file
                    BackupDeviceItem bkpDevice1 = new BackupDeviceItem(saveFileDialog1.FileName.Replace(".", " ").Replace("bak", " ") + Backupdate, DeviceType.File);
                    // Add the backup device to the backup
                    bkpDatabase.Devices.Add(bkpDevice1);
                    // Perform the backup
                    bkpDatabase.SqlBackup(srvr);
                    bkpDatabase.Devices.Remove(bkpDevice1);
                    MessageBox.Show("Restauración de base de datos" + "CRVA-DB" + " creada correctamente", "Servidor", MessageBoxButton.OK, MessageBoxImage.Information);
                    //codigo en desarrollo
                    if (saveFileDialog1.FileName == bkpDevice1.Name)
                    {
                        // Saves the Image via a FileStream created by the OpenFile method.
                        FileStream fs = (FileStream)saveFileDialog1.OpenFile();
                        // Saves the Image in the appropriate ImageFormat based upon the
                        // File type selected in the dialog box.
                        // NOTE that the FilterIndex property is one-based.
                        fs.Close();
                    }
                    //codigo en desarrollo
                }
                catch (Exception x)
                {
                    MessageBox.Show("ERROR: Un error ocurrió mientras se intentaba restaurar la Base de Datos CRVA-DB." + x, "Error de servidor", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("ERROR: La conexión a SQL Server no fue establecida.", "Servidor", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Cursor = Cursors.Arrow;
            }
        }
        #endregion

        #endregion

        #region Restaurar

        #region btn_Restaurar Click
        private async void btn_restore_Click(object sender, RoutedEventArgs e)
        {
            pbProgress.Visibility = Visibility.Visible;

            await Task.Run(() =>
            {

                Restaurar();
            });
            
            pbProgress.Visibility = Visibility.Hidden;
            //this.Close();
        }
        #endregion

        #region Metodo Restaurar
        public void Restaurar()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.FileName = "*.bak";
            openFileDialog.Title = "Cargue un archivo Bak.";
            openFileDialog.ShowDialog();

            Conexion();
            if (srvr != null)
            {
                try
                {
                    // Set the backup device from which we want to restore, to a file
                    BackupDeviceItem bkpDevice = new BackupDeviceItem(openFileDialog.FileName, DeviceType.File);
                    // Add the backup device to the restore type
                    rstDatabase.Devices.Add(bkpDevice);

                    // If the database already exists, replace it

                    rstDatabase.ReplaceDatabase = true;
                    // Perform the restore
                    srvr.KillAllProcesses(rstDatabase.Database);
                    rstDatabase.SqlRestore(srvr);
                    rstDatabase.Devices.Remove(bkpDevice);
                    MessageBox.Show("La base de datos " + "CRVA-DB" + " se restauró con éxito.", "Servidor", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR: Ocurrió el siguiente error al restaurar la base de datos CRVA-DB: " + ex.Message, "Error en la aplicación", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("ERROR: La conexión con SQL Server no fué establecida.", "Servidor", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #endregion

        #region Conexion
        public void Conexion()
        {

            #region Servidor
            try
            {
                ServerConnection srvrConn = new ServerConnection(backUpBL.GetServer());
                srvrConn.LoginSecure = true;
                srvr = new Server(srvrConn);
                NombreServidor = backUpBL.GetServer();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: Un error ocurrió mientras se intentaba conectar al servidor." + ex, "Error de servidor", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            #endregion

            #region BaseDeDatos

            #region Restauración
            try
            {
                NombreDB = backUpBL.GetDataBase();
                // Create a new database restore operation
                // Set the restore type to a database restore
                rstDatabase.Action = RestoreActionType.Database;
                // Set the database that we want to perform the restore on
                rstDatabase.Database = backUpBL.GetDataBase();
            }
            catch (Exception x)
            {
                MessageBox.Show("ERROR: Un error ocurrió mientras se intentaba conectar a la base de Datos." + x, "Error de servidor", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            #endregion

            #region BackUp
            try
            {
                NombreDB = backUpBL.GetDataBase();
                bkpDatabase.Database = backUpBL.GetDataBase();
            }
            catch (Exception x)
            {
                MessageBox.Show("ERROR: Un error ocurrió mientras se intentaba conectar a la base de Datos." + x, "Error de servidor", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            #endregion

            #endregion
        }

        #endregion

        #region Seguridad integrada click

        //private void chk_Insec_Click(object sender, RoutedEventArgs e)
        //{
        //    if (chk_Insec.IsChecked == true)
        //    {
        //        txt_username.IsEnabled = false;
        //        txt_username.Text = string.Empty;

        //        txt_password.IsEnabled = false;
        //        txt_password.Password = string.Empty;
        //    }
        //    if (chk_Insec.IsChecked == false)
        //    {
        //        txt_username.IsEnabled = true;
        //        txt_password.IsEnabled = true;
        //    }
        //}
        #endregion
    }
}
