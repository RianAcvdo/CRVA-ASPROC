using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
//se agregaron librerias para modelar la base de datos.
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace Agape.ReservacionVhl.EN
{
    public class Reservation
    {
        //Codigo Hecho por Cesar Garcia
        public Int64 ReservationID { get; set; }

        //[Display(Name = "Kilometros")]
        //public string Km { get; set; }


        public string Origin { get; set; }
        public string Destination { get; set; }


        public string Kilometers { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }

        public string Action { get; set; }

        //[ForeignKey("CarID")]
        public Int64 CarID { get; set; }

        //[ForeignKey("EmployeeID")]
        public Int64 EmployeeID { get; set; }

        //[ForeignKey("PositionID")]
        //public Int64 PositionID { get; set; }

        public bool State { get; set; }

        //propiedades virtuales Ricardo Acevedo
        //public virtual ICollection<Car> Cars { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Car Car { get; set; }


        //propiedad me servira para enviar el mensaje de try y catch
        //public string MensajeError { get; set; }
        //public string MensajeErro2 { get; set; }
    }
}
