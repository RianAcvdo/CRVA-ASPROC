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
    public class MarkBL
    {
        private MarkDAL markDal = new MarkDAL();
        public List<Mark> GetAll()
        {
            return markDal.GetAll();
        }
        public List<Mark> GetAllDeleted()
        {
            return markDal.GetAllDeleted();
        }
        public List<Mark> GetAllData()
        {
            return markDal.GetAllData();
        }

        //metodo que crea una fila 
        //crea un registro de datos tipo Markro
        public Mark Create(Mark pMark)
        {
            return markDal.Create(pMark);
        }

        //metodo para borrar un auto
        public Mark Delete(Mark pMark)
        {
            return markDal.Delete(pMark);
        }

        //metodo para editar con un try y catch que devuelve un resutaldo.
        public int Edit(Mark pMark)
        {
            return markDal.Edit(pMark);
        }

        public Mark SearchID(int ID)
        {
            return markDal.SearchID(ID);
        }

        public void UpdateMarks(Mark pMark)
        {
            markDal.UpdateMarks(pMark);
        }
    }
}
