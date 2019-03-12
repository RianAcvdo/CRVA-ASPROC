using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Using necesarios
//Data para poder acceder a DataTable
using System.Data;
//Reflection para poder convertir un arreglo de linq a un DataTable
using System.Reflection;
//Using para convertir imagen a bytes
using System.IO;
using System.Windows.Media.Imaging;
//Usings del sistema CRVA para poder hacer metodos globales
using Agape.ReservacionVhl.EN;
using Agape.ReservacionVhl.BL;

namespace Agape.ReservacionVhl.WpfUI.Reportes
{
    public class MetodosBL
    {
        public CarBL CarM = new CarBL();
        public EmployeeBL EmployeeM = new EmployeeBL();
        public MessageBL MessageM = new MessageBL();
        public ReservationBL ReservationM = new ReservationBL();
        public UserBL User = new UserBL();
        public MarkBL MarkM = new MarkBL();
        public OfficeBL OffiM = new OfficeBL();

        //Metodo para cargar imagenes desde un arreglo de bytes[]
        public BitmapImage LoadImage(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0) return null;
            var image = new BitmapImage();
            using (var mem = new MemoryStream(imageData))
            {
                mem.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = mem;
                image.EndInit();
            }
            image.Freeze();
            return image;
        }

        //Convertir de IEnumerable a DataTable
        public DataTable LinqQueryToDataTable(IEnumerable<dynamic> v)
        {
            //Realmente queremos saber si hay algún dato en absoluto
           var firstRecord = v.FirstOrDefault();
            if (firstRecord == null)
                return null;

            /*De acuerdo, tenemos algunos datos. Tiempo de trabajar.*/

            //Así que querido disco, ¿qué tienes?
            PropertyInfo[] infos = firstRecord.GetType().GetProperties();

            //Nuestra tabla debe tener las columnas para apoyar las propiedades
            DataTable table = new DataTable();

            //agregamos las columnas
            foreach (var info in infos)
            {

                Type propType = info.PropertyType;

                if (propType.IsGenericType
                    && propType.GetGenericTypeDefinition() == typeof(Nullable<>)) //Los tipos anulables deben manejarse también
                {
                    table.Columns.Add(info.Name, Nullable.GetUnderlyingType(propType));
                }
                else
                {
                    table.Columns.Add(info.Name, info.PropertyType);
                }
            }

            // hemos terminado con las columnas.Comencemos con filas ahora
            DataRow row;

            foreach (var record in v)
            {
                row = table.NewRow();
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    row[i] = infos[i].GetValue(record) != null ? infos[i].GetValue(record) : DBNull.Value;
                }

                table.Rows.Add(row);
            }

            //La mesa está lista para servir.
            table.AcceptChanges();

            return table;
        }

    }
}
