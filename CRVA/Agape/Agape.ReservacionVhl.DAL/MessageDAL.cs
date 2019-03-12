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
     public class MessageDAL
    {
        //Codigo hecho por Cesar Garcia.
        /// <summary>
        /// Codigo para base de datos
        /// hace una instancia de objeto
        /// </summary>
        private DBCommon Db = new DBCommon();

        //trae todo los datos de una lista de Mensaje
        public List<Message> GetAll()
        {
            var Messages = from s in Db.Messages
                            select s;
            Messages = Db.Messages.Where(
                s => s.State == true);
            return Messages.ToList();
        }

        //metodo que crea una fila 
        //crea un registro de datos tipo Mensajes
        public Message Create(Message pMessage)
        {
            Message Message = Db.Messages.Add(pMessage);
            Db.SaveChanges();
            return Message;
        }

        //metodo para borrar un Mensajes
        public Message Delete(Message pMessage)
        {
            Message Message = Db.Messages.Remove(pMessage);
            Db.SaveChanges();
            return Message;
        }

        //metodo para editar con un try y catch que devuelve un resutaldo.
        public int Edit(Message pMessage)
        {
            int resultado = 0;
            try
            {

                Db.Entry(pMessage).State = EntityState.Modified;
                resultado = Db.SaveChanges();
                return resultado;
            }
            catch
            {
                return resultado;
            }
        }

        //metodo para buscar Message por ID
        public Message SearchID(int ID)
        {
            Message Message = Db.Messages.Find(ID);
            return Message;
        }

        //metodo para buscar Message un trabajador
        //busca por codigo , nombre y apellido
        public List<Message> Search(Message pMessage)
        {
            var Messages = from s in Db.Messages
                            select s;
            Messages = Db.Messages.Where(
                s => s.Employee.Name.ToUpper().Contains(pMessage.Employee.Name.ToUpper()) ||
                s.Employee.Name.ToLower().Contains(pMessage.Employee.Name.ToLower()) ||
                s.Employee.Code.ToUpper().Contains(pMessage.Employee.Code.ToUpper()) ||
                s.Employee.Code.ToLower().Contains(pMessage.Employee.Code.ToLower())
                );
            return Messages.ToList();
        }
        //Metodos para Eliminar logicamente y restaurar
        public void UpdateItem(Message pItem)
        {
            using (var db = new DAL.DBCommon())
            {
                db.Entry(pItem).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        //Get all the "eliminated" Items
        public List<Message> GetAllDeleted()
        {
            var objs = from s in Db.Messages
                       select s;
            objs = Db.Messages.Where(
                s => s.State == false);
            return objs.ToList();
        }
    }
}
