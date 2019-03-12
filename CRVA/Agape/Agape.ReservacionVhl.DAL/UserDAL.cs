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
    public class UserDAL
    {
        //Codigo hecho por Cesar Garcia.
        /// <summary>
        /// Codigo para base de datos
        /// hace una instancia de objeto
        /// </summary>
        private DBCommon Db = new DBCommon();

        //trae todo los datos de una lista de Usuario
        public List<User> GetAll()
        {
            List<User> lista = new List<User>();
            try
            {

                var Users = from s in Db.Users
                            select s;
                Users = Db.Users.Where(
                    s => s.State == true);
                return Users.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: ", ex);
                return lista;
            }
        }
        public List<User> GetAllData()
        {
            List<User> lista = new List<User>();
            try
            {

                var Users = from s in Db.Users
                            select s;
                return Users.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: ", ex);
                return lista;
            }
        }

        //metodo que crea una fila 
        //crea un registro de datos tipo Usuario
        public User Create(User pUser)
        {
            User User = Db.Users.Add(pUser);
            Db.SaveChanges();
            return User;
        }

        //metodo para borrar un Usuario
        public User Delete(User pUser)
        {
            User User = Db.Users.Remove(pUser);
            Db.SaveChanges();
            return User;
        }

        //metodo para editar con un try y catch que devuelve un resultado.
        public int Edit(User pUser)
        {
            int resultado = 0;
            try
            {

                Db.Entry(pUser).State = EntityState.Modified;
                resultado = Db.SaveChanges();
                return resultado;
            }
            catch
            {
                return resultado;
            }
        }

        //metodo para buscar User por ID
        public User SearchID(int ID)
        {
            User User = Db.Users.Find(ID);
            return User;
        }

        //metodo para buscar User
        //busca por codigo , nombre y apellido
        public List<User> Search(User pUser)
        {
            var Users = from s in Db.Users
                          select s;
            Users = Db.Users.Where(
                s => s.Name.ToUpper().Contains(pUser.Name.ToUpper()) ||
                s.LastName.ToUpper().Contains(pUser.LastName.ToUpper()) ||
                s.Name.ToLower().Contains(pUser.Name.ToLower()) ||
                s.LastName.ToLower().Contains(pUser.LastName.ToLower()) ||
                s.Email.ToUpper().Contains(pUser.Email.ToUpper()) ||
                 s.Email.ToLower().Contains(pUser.Email.ToLower())
                );
            return Users.ToList();
        }

        //logeo para usuario
        public User Login(User pUser)
        {
            User user = new User();
         
            user = Db.Users.Where(x => x.Email == pUser.Email && x.State==true).FirstOrDefault();

            return user;
        }

        //Metodos para Eliminar logicamente y restaurar
        public void UpdateItem(User pItem)
        {
            using (var db = new DAL.DBCommon())
            {
                db.Entry(pItem).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        //Get all the "eliminated" Items
        public List<User> GetAllDeleted()
        {
            List<User> lista = new List<User>();
            try
            {

                var obj = from s in Db.Users
                          select s;
                obj = Db.Users.Where(
                    s => s.State == false);
                return obj.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: ", ex);
                return lista;
            }
        }
        // Metodos para ayudar on el login a la Kelly
        //comparar usuario segun su Email
        public List<User> GetUserByEMail(User pUser)
        {
            List<User> lista = new List<User>();
            try
            {
                var Users = from s in Db.Users
                            select s;
                Users = Db.Users.Where(
                    s => s.Email == pUser.Email);
                return Users.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: ", ex);
                return lista;
            }
        }
        //actualizar Contraseña para recuperarla (no estoy seguro si funciona)
        public int UpdatePassword(User pItem)
        {
            int resultado = 0;
            int count = 0;
            try
            {
                var query = (from a in Db.Users
                             where a.UserName == pItem.UserName
                             select a).FirstOrDefault();
                query.Password = pItem.Password;
                Db.SaveChanges();
            }
            catch
            {
                resultado = count;
            }
            return count;
        }

        //actualizar Contraseña para recuperarla opcion 2
        public int UpdatePassword2(User pItem)
        {
            int resultado = 0;
            int count = 0;
            try
            {
                var query = (from a in Db.Users
                             where a.Email == pItem.Email
                             select a).FirstOrDefault();
                query.Password = pItem.Password;
                Db.SaveChanges();
            }
            catch
            {
                resultado = count;
            }
            return count;
        }
    }
}
