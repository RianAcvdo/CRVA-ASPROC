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
    public class Office
    {
        //Codigo Hecho por Cesar Garcia

        public Int64 OfficeID { get; set; }

        //[StringLength(maximumLength: 50,MinimumLength = 5,ErrorMessage = "Escriba un nombre de oficina por lo menos de 5 caracteres y no mayor a 50 caracteres")]
        public string Name { get; set; }

        public string Code { get; set; }

        //[Required(ErrorMessage = "Ingresar un departamento")]
        public string Department { get; set; }

        //[Required(ErrorMessage = "Ingresar un municipio.")]
        public string Town { get; set; }

        //[Required(AllowEmptyStrings = false, ErrorMessage = "Escriba un nombre de la calle donde se ubica la oficina.")]
        public string StreetOne { get; set; }

        [Required(AllowEmptyStrings = true)]
        public string StreetTwo { get; set; }

        [Required(AllowEmptyStrings = true)]
        public string StreetThree { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        //[ForeignKey("PositionID")]
       // public Int64 PositionID { get; set; }


        public bool State { get; set; }

        //propiedad me servira para enviar el mensaje de try y catch
        //public string MensajeError { get; set; }
        //public string MensajeErro2 { get; set; }
        //propiedades virtuales
       // public virtual Position Position { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
