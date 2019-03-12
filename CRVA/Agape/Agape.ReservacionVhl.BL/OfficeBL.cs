using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//librerias que se agregaron.
using Agape.ReservacionVhl.EN;
using Agape.ReservacionVhl.DAL;

namespace Agape.ReservacionVhl.BL
{
    public class OfficeBL
    {
        //Codigo hecho por Cesar Garcia.
        /// <summary>
        /// Codigo para base de datos
        /// hace una instancia de objeto
        /// </summary>
        /// 
        private OfficeDAL OfficeDal = new OfficeDAL();

        //metodo para obtener todos los datos de un oficina
        public List<Office> GetAll()
        {
            return OfficeDal.GetAll();
        }
        //metodo que crea una fila 
        //crea un registro de datos tipo oficina
        public Office Create(Office pOffice)
        {
            return OfficeDal.Create(pOffice);
        }

        //metodo para borrar un Oficina
        public Office Delete(Office pOffice)
        {
            return OfficeDal.Delete(pOffice);
        }

        //metodo para editar con un try y catch que devuelve un resultado.
        public int Edit(Office pOffice)
        {
            return OfficeDal.Edit(pOffice);
        }

        //metodo para buscar Office por ID
        public Office SearchID(int ID)
        {
            return OfficeDal.SearchID(ID);
        }

        //metodo para buscar Office un trabajador
        //por marca y por codigo.
        public List<Office> Search(Office pOffice)
        {
            return OfficeDal.Search(pOffice);
        }
        //metodo para obtener todos los datos eliminados
        public List<Office> GetAllDeleted()
        {
            return OfficeDal.GetAllDeleted();
        }
        public void UpdateItem(Office pItem)
        {
            OfficeDal.UpdateItem(pItem);
        }
    }
}
