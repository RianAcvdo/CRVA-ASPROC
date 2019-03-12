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
    public class OfficeDAL
    {
        //Codigo hecho por Cesar Garcia.
        /// <summary>
        /// Codigo para base de datos
        /// hace una instancia de objeto
        /// </summary>
        private DBCommon Db = new DBCommon();

        //trae todo los datos de una lista de Oficina
        public List<Office> GetAll()
        {
            List<Office> lista = new List<Office>();
            try
            {
                var Offices = from s in Db.Offices
                              select s;
                Offices = Db.Offices.Where(
                    s => s.State == true);
                return Offices.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: ", ex);
                return lista;
            }
        }

        //metodo que crea una fila 
        //antes de crear una oficina tiene que aver antes un lugar es decir una posicion.
        //crea un registro de datos tipo Oficina
        public Office Create(Office pOffice)
        {
            Office Office = Db.Offices.Add(pOffice);
            Db.SaveChanges();
            return Office;
        }

        //metodo para borrar un Oficina
        public Office Delete(Office pOffice)
        {
            Office Office = Db.Offices.Remove(pOffice);
            Db.SaveChanges();
            return Office;
        }

        //metodo para editar con un try y catch que devuelve un resultado.
        public int Edit(Office pOffice)
        {
            int resultado = 0;
            try
            {

                Db.Entry(pOffice).State = EntityState.Modified;
                resultado = Db.SaveChanges();
                return resultado;
            }
            catch
            {
                return resultado;
            }
        }

        //metodo para buscar Office por ID
        public Office SearchID(int ID)
        {
            Office Office = Db.Offices.Find(ID);
            return Office;
        }

        //metodo para buscar Office un trabajador
        //busca por codigo , nombre y apellido
        public List<Office> Search(Office pOffice)
        {
            var Offices = from s in Db.Offices
                            select s;
            Offices = Db.Offices.Where(
                s => s.Name.ToUpper().Contains(pOffice.Name.ToUpper()) ||
                s.Department.ToUpper().Contains(pOffice.Department.ToUpper()) ||
                s.Name.ToLower().Contains(pOffice.Name.ToLower()) ||
                s.Department.ToLower().Contains(pOffice.Department.ToLower()) ||
                s.Code.ToUpper().Contains(pOffice.Code.ToUpper()) ||
                 s.Code.ToLower().Contains(pOffice.Code.ToLower())
                );
            return Offices.ToList();
        }

        //metodo para traer oficinas pero con la posicion en la que se encuentran
        //public List<Office> GetAllOffice(Office pOffice,Position pPosition)
        //{
        //    Db.Offices.FirstAsync(s => 
        //    s.PositionID.Equals(pPosition.PositionID) && s.OfficeID.Equals(pOffice.OfficeID)
        //    );

        //    return Db.Offices.ToList();
        //}
        //Metodos para Eliminar logicamente y restaurar
        public void UpdateItem(Office pItem)
        {
            using (var db = new DAL.DBCommon())
            {
                db.Entry(pItem).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        //Get all the "eliminated" Items
        public List<Office> GetAllDeleted()
        {
            List<Office> lista = new List<Office>();
            try
            {
                var objs = from s in Db.Offices
                           select s;
                objs = Db.Offices.Where(
                    s => s.State == false);
                return objs.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: ", ex);
                return lista;
            }
        }
    }
}
