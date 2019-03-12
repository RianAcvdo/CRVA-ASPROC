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
    public class Employee
    {
        //Codigo Hecho por Cesar Garcia
       

        public Int64 EmployeeID { get; set; }

        //[Required(ErrorMessage = "Ingrese un nombre por favor.")]
        //[StringLength(50, ErrorMessage = "El nombre no puede contener más de 50 caracteres.")]
        public string Name { get; set; }

        //[Required(ErrorMessage = "Ingrese un apellido por favor.")]
        //[StringLength(50, ErrorMessage = "El apellido no puede contener más de 50 caracteres.")]
        public string LastName { get; set; }
     
        public string Code { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Creado En")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        public DateTime DateBirth { get; set; }

        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:0, 0.0}")]
        public decimal Salary { get; set; }

        //[ForeignKey("OfficeID")]
        public Int64 OfficeID { get; set; }

        public string StateDetail { get; set; }
        public bool State { get; set; }


        //propiedad me servira para enviar el mensaje de try y catch
        //public string MensajeError { get; set; }
        //public string MensajeErro2 { get; set; }


        //propiedades virtuales
        public virtual Office Office { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
