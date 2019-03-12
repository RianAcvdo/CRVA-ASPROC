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
    public class UserBL
    {
        //Codigo hecho por Cesar Garcia.
        /// <summary>
        /// Codigo para base de datos
        /// hace una instancia de objeto
        /// </summary>
        /// 
        private UserDAL UserDal = new UserDAL();

        //metodo para obtener todos los datos de un Usuarios
        public List<User> GetAll()
        {
            return UserDal.GetAll();
        }
        public List<User> GetAllData()
        {
            return UserDal.GetAllData();
        }
        //metodo que crea una fila 
        //crea un registro de datos tipo Usuarios
        public User Create(User pUser)
        {
            return UserDal.Create(pUser);
        }

        //metodo para borrar un Usuarios
        public User Delete(User pUser)
        {
            return UserDal.Delete(pUser);
        }

        //metodo para editar con un try y catch que devuelve un resultado.
        public int Edit(User pUser)
        {
            return UserDal.Edit(pUser);
        }

        //metodo para buscar usuarios por ID
        public User SearchID(int ID)
        {
            return UserDal.SearchID(ID);
        }

        //metodo para buscar usuarios
        //por marca y por codigo.
        public List<User> Search(User pUser)
        {
            return UserDal.Search(pUser);
        }
        //comparar usuario segun contraseña
        public List<User> GetUserByEMail(User pUser)
        {
            return UserDal.GetUserByEMail(pUser);
        }
        //Actualizar contraseña
        //Opcion 1
        public int UpdatePassword2(User pItem)
        {
            return UserDal.UpdatePassword2(pItem);
        }
        //Opcion 2
        public int UpdatePassword(User pItem)
        {
            return UserDal.UpdatePassword(pItem);
        }

        //login
        public User Login(User pUser)
        {
            return UserDal.Login(pUser);
        }
        //metodo para obtener todos los datos eliminados
        public List<User> GetAllDeleted()
        {
            return UserDal.GetAllDeleted();
        }
        public void UpdateItem(User pItem)
        {
            UserDal.UpdateItem(pItem);
        }
    }
}
