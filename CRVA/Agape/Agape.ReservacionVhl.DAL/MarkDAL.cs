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
    public class MarkDAL
    {
        /// Codigo para base de datos
        /// hace una instancia de objeto
        /// </summary>
        private DBCommon Db = new DBCommon();

        //trae todo los datos de una lista de Markros
        public List<Mark> GetAll()
        {
            List<Mark> lista = new List<Mark>();
            try
            {
                var Marks = from s in Db.Marks
                            select s;
                Marks = Db.Marks.Where(s => s.State == true);
                return Marks.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: ", ex);
                return lista;
            }
        }
        public List<Mark> GetAllDeleted()
        {
            List<Mark> lista = new List<Mark>();
            try
            {
                var Marks = from s in Db.Marks
                            select s;
                Marks = Db.Marks.Where(
                    s => s.State == false);
                return Marks.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: ", ex);
                return lista;
            }
        }
        public List<Mark> GetAllData()
        {
            List<Mark> lista = new List<Mark>();
            try
            {
                var Marks = from s in Db.Marks
                            select s;
                return Marks.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: ", ex);
                return lista;
            }
        }

        //metodo que crea una fila 
        //crea un registro de datos tipo Markro
        public Mark Create(Mark pMark)
        {
            Mark Mark = Db.Marks.Add(pMark);
            Db.SaveChanges();
            return Mark;
        }

        //metodo para borrar un auto
        public Mark Delete(Mark pMark)
        {
            Mark Mark = Db.Marks.Remove(pMark);
            Db.SaveChanges();
            return Mark;
        }

        //metodo para editar con un try y catch que devuelve un resutaldo.
        public int Edit(Mark pMark)
        {
            int resultado = 0;
            try
            {

                Db.Entry(pMark).State = EntityState.Modified;
                resultado = Db.SaveChanges();
                return resultado;
            }
            catch
            {
                return resultado;
            }
        }

        public Mark SearchID(int ID)
        {
            Mark Mark = Db.Marks.Find(ID);
            return Mark;
        }

        //Metodo para modifiMark vehiculo
        public void UpdateMarks(Mark pMark)
        {
            using (var db = new DAL.DBCommon())
            {
                db.Entry(pMark).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
    }
}
