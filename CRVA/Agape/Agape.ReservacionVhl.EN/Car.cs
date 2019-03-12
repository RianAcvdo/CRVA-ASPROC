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
   public class Car
    {
        //Codigo Hecho por Cesar Garcia
        public Int64 CarID { get; set; }

        //[Required(ErrorMessage = "Ingrese una marca de automovil.")]
        //[StringLength(maximumLength: 25,ErrorMessage = "Ingrese una marca de automovil", MinimumLength = 3)]
        //public string Mark { get; set; }

        public Int64 MarkID { get; set; }

        public byte[] Image { get; set; }

        public string Code { get; set; }

        public string Details { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Creado En")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        public decimal InitialKilometers { get; set; }

        public decimal Kilometers { get; set; }

        public string StateDetail { get; set; }
        //[ForeignKey("OfficeID")]
        //public Int64 OfficeID { get; set; }

        //propiedad me servira para enviar el mensaje de try y catch
        //public string MensajeError { get; set; }
        //public string MensajeErro2 { get; set; }

        public bool State { get; set; }

        //propiedades virtuales
        public virtual Mark Mark { get; set; }

        //public virtual Office Office { get; set; }
        //public virtual Reservation Reservation { get; set; }
    }
}
