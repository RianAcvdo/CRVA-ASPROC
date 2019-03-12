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
   public class MessageBL
    {
        //Codigo hecho por Cesar Garcia.
        /// <summary>
        /// Codigo para base de datos
        /// hace una instancia de objeto
        /// </summary>
        /// 
        private MessageDAL MessageDal = new MessageDAL();

        //metodo para obtener todos los datos de un Mensajes
        public List<Message> GetAll()
        {
            return MessageDal.GetAll();
        }
        //metodo que crea una fila 
        //crea un registro de datos tipo Mensajes
        public Message Create(Message pMessage)
        {
            return MessageDal.Create(pMessage);
        }

        //metodo para borrar un Mensajes
        public Message Delete(Message pMessage)
        {
            return MessageDal.Delete(pMessage);
        }

        //metodo para editar con un try y catch que devuelve un resultado.
        public int Edit(Message pMessage)
        {
            return MessageDal.Edit(pMessage);
        }

        //metodo para buscar Message por ID
        public Message SearchID(int ID)
        {
            return MessageDal.SearchID(ID);
        }

        //metodo para buscar Message un trabajador
        //por marca y por codigo.
        public List<Message> Search(Message pMessage)
        {
            return MessageDal.Search(pMessage);
        }
        //metodo para obtener todos los datos eliminados
        public List<Message> GetAllDeleted()
        {
            return MessageDal.GetAllDeleted();
        }
        public void UpdateItem(Message pItem)
        {
            MessageDal.UpdateItem(pItem);
        }
    }
}
