using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//se agregaron librerias para modelado de datos
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Agape.ReservacionVhl.EN
{
    public class Message
    {
        //Codigo Hecho por Cesar Garcia
        public Int64 MessageID { get; set; }

        public string Details { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy - MM - dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        
        //[ForeignKey("EmployeeID")]
        public Int64 EmployeeID { get; set; }

        public bool State { get; set; }


        //propiedad me servira para enviar el mensaje de try y catch
        //public string MensajeError { get; set; }
        //public string MensajeErro2 { get; set; }

        //propiedades virtuales
        public virtual ICollection<Employee> Employees { get; set; }
        public virtual Employee Employee { get; set; }
        
    }
}
