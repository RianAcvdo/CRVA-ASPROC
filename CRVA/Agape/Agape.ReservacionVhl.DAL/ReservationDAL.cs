using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//librerias que se agregaron.
using Agape.ReservacionVhl.EN;
using System.Data.Entity;

namespace Agape.ReservacionVhl.DAL
{
    public class ReservationDAL
    {
        //Codigo hecho por Cesar Garcia.
        /// <summary>
        /// Codigo para base de datos
        /// hace una instancia de objeto
        /// </summary>
        private DBCommon Db = new DBCommon();

        //trae todo los datos de una lista de Reservacion
        public List<Reservation> GetAll()
        {
            List<Reservation> lista = new List<Reservation>();
            try
            {
                var Reservations = from s in Db.Reservations
                                   select s;
                Reservations = Db.Reservations.Include(r => r.Car).Include(r => r.Employee).Include(r => r.Employee.Office).Include(r => r.Car.Mark).Where(r => r.State == true);
                return Reservations.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: ", ex);
                return lista;
            }
            
        }

        //metodo que crea una fila 
        //crea un registro de datos tipo Reservacion
        public Reservation Create(Reservation pReservation)
        {
            Reservation _identidad = new Reservation();
            try
            {
                _identidad = Db.Reservations.Add(pReservation);
                Db.SaveChanges();
                return _identidad;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: ",ex);
                return _identidad;
            }
        }

        //metodo para borrar un Reservacion
        public Reservation Delete(Reservation pReservation)
        {
            Reservation Reservation = Db.Reservations.Remove(pReservation);
            Db.SaveChanges();
            return Reservation;
        }

        //metodo para editar con un try y catch que devuelve un resultado.
        public int Edit(Reservation pReservation)
        {
            int resultado = 0;
            int count = 0;
            try
            {
                var query = (from a in Db.Reservations
                             where a.ReservationID == pReservation.ReservationID
                             select a).FirstOrDefault();
                query.Action = pReservation.Action;
                Db.SaveChanges();
            }
            catch
            {
                resultado = count;
            }
            return resultado;
        }

        //metodo para buscar Reservation por ID
        public Reservation SearchID(int ID)
        {
            Reservation Reservation = Db.Reservations.Find(ID);
            return Reservation;
        }


        //buscador por fecha
        public List<Reservation> SearchDate(Reservation pReservation)
        {
            Db.Reservations.Find(pReservation.Date);
            return Db.Reservations.ToList();
        }

        //buscador por fecha, km, accion, Id empleado y auto
        public List<Reservation> SearchParemeters(Reservation pReservation)
        {
            var reser = from s in Db.Reservations
                        select s;
            reser = Db.Reservations.Where(
                s => s.Origin.ToUpper().Contains(pReservation.Origin.ToUpper()) ||
                s.Destination.ToUpper().Contains(pReservation.Destination.ToUpper()) ||
                s.Action.ToUpper().Contains(pReservation.Action.ToUpper()) ||
                s.Origin.ToLower().Contains(pReservation.Origin.ToLower()) ||
                s.Destination.ToLower().Contains(pReservation.Destination.ToLower()) ||
                s.Action.ToLower().Contains(pReservation.Action.ToLower()));
            return reser.ToList();
        }
        //metodo para buscar Reservation un trabajador
        //busca por codigo , nombre y apellido
        //public List<Reservation> Search(Reservation pReservation)
        //{
        //    var Reservations = from s in Db.Reservations
        //                  select s;
        //    Reservations = Db.Reservations.Where(
        //        s => s.Date.ToUpper().Contains(pReservation.Name.ToUpper()) ||
        //        s.Department.ToUpper().Contains(pReservation.Department.ToUpper()) ||
        //        s.Name.ToLower().Contains(pReservation.Name.ToLower()) ||
        //        s.Department.ToLower().Contains(pReservation.Department.ToLower()) ||
        //        s.Code.ToUpper().Contains(pReservation.Code.ToUpper()) ||
        //         s.Code.ToLower().Contains(pReservation.Code.ToLower())
        //        );
        //    return Reservations.ToList();
        //}

        //Metodos para Eliminar logicamente y restaurar
        public void UpdateItem(Reservation pItem)
        {
            try
            {
                using (var db = new DAL.DBCommon())
                {
                    db.Entry(pItem).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine("Exception: {0}", ex);
            }
            
        }

        //Get all the "eliminated" Items
        public List<Reservation> GetAllDeleted()
        {
            List<Reservation> lista = new List<Reservation>();
            try
            {

                var objs = from s in Db.Reservations
                           select s;
                objs = Db.Reservations.Where(
                    s => s.State == false);
                return objs.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: ", ex);
                return lista;
            }
        }

        //metodo para reservacion
        public Reservation GetReservation(Reservation pReseravation)
        {       
            Reservation Reservations;

            Reservations = Db.Reservations.Find(pReseravation.CarID);
            Reservations = Db.Reservations.Find(pReseravation.Action == "Reservado");
            return Reservations;
        }

        public List<Reservation> GetReservationEmployee(Int64 pemployee)
        {
            List<Reservation> lista = new List<Reservation>();
            try
            {
                var Reservations = from s in Db.Reservations
                                   select s;
                Reservations = Db.Reservations.Where(s => s.EmployeeID == pemployee && s.Action != "Libre");
                return Reservations.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: ", ex);
                return lista;
            }
        }

    }
}
