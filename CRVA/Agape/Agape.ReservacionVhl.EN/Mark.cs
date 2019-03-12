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
    public class Mark
    {
        public Int64 MarkID { get; set; }
        public string Name { get; set; }
        public byte[] Picture { get; set; }
        public bool State { get; set; }
    }
}
