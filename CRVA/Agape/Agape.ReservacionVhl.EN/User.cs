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
   public class User
    {
        //Codigo Hecho por Cesar Garcia
        public Int64 UserID { get; set; }

        //[Required(ErrorMessage ="Ingrese un nombre por favor.")]
        //[StringLength(50, ErrorMessage = "El nombre no puede contener más de 50 caracteres.")]
        public string Name { get; set; }

        //[Required(ErrorMessage = "Ingrese un apellido por favor.")]
        //[StringLength(50, ErrorMessage = "El apellido no puede contener más de 50 caracteres.")]
        public string LastName { get; set; }

        //[Required(ErrorMessage = "Ingrese un correo por favor.")]
        //clase solo para direcciones email.
        //[EmailAddress(ErrorMessage = "Por favor ingrese un correo valido.")]
        //es para que solo acepte correos
        //[RegularExpression("^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$", ErrorMessage = "Debe ser un correo valido")]
        public string Email { get; set; }

        //[StringLength(50, MinimumLength = 6,ErrorMessage = "Ingrese una contraseña de minimo 6 letras.")]  
        public string Password { get; set; }

        //[StringLength(25, MinimumLength = 6, ErrorMessage = "Ingrese un nombre de usuario que contenga 6 letras.")]
        public string UserName { get; set; }

        public bool State { get; set; }


        //propiedad me servira para enviar el mensaje de try y catch
        //public string MensajeError { get; set; }
        //public string MensajeErro2 { get; set; }
    }
}
