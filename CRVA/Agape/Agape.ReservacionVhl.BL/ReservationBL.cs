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
    public class ReservationBL
    {
        //Codigo hecho por Cesar Garcia.
        /// <summary>
        /// Codigo para base de datos
        /// hace una instancia de objeto
        /// </summary>
        /// 
        private ReservationDAL ReservationDal = new ReservationDAL();

        //metodo para obtener todos los datos de un Reservacion
        public List<Reservation> GetAll()
        {
            return ReservationDal.GetAll();
        }
        //metodo que crea una fila 
        //crea un registro de datos tipo Reservacion
        public Reservation Create(Reservation pReservation)
        {
            return ReservationDal.Create(pReservation);
        }

        //metodo para borrar un Reservacion
        public Reservation Delete(Reservation pReservation)
        {
            return ReservationDal.Delete(pReservation);
        }

        //metodo para editar con un try y catch que devuelve un resultado.
        public int Edit(Reservation pReservation)
        {
            return ReservationDal.Edit(pReservation);
        }

        //metodo para buscar Reservation por ID
        public Reservation SearchID(int ID)
        {
            return ReservationDal.SearchID(ID);
        }

        public List<Reservation> SearchByParameters(Reservation pReservation)
        {
            return ReservationDal.SearchParemeters(pReservation);
        }

        //metodo para buscar Reservation un trabajador
        //por marca y por codigo.
        //public List<Reservation> Search(Reservation pReservation)
        //{
        //    return ReservationDal.Search(pReservation);
        //}
        //metodo para obtener todos los datos eliminados
        public List<Reservation> GetAllDeleted()
        {
            return ReservationDal.GetAllDeleted();
        }
        //metodo de actualizar
        public void UpdateItem(Reservation pItem)
        {
            ReservationDal.UpdateItem(pItem);
        }
        //metodo para comprobar reservacion
        public Reservation GetReservation(Reservation pReseravation)
        {
           return ReservationDal.GetReservation(pReseravation);
        }

        //metodo para mostrar reservaciones por empleado

        public List<Reservation> GetReservationEmployee(Int64 pReservation)
        {
            return ReservationDal.GetReservationEmployee(pReservation);
        }
    }
}
