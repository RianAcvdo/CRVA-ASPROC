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
    public class CarDAL
    {
        //Codigo hecho por Cesar Garcia.
        /// <summary>
        /// Codigo para base de datos
        /// hace una instancia de objeto
        /// </summary>
       private DBCommon Db = new DBCommon();

        //trae todo los datos de una lista de carros
        public List<Car> GetAll ()
        {
            List<Car> lista = new List<Car>();
            try
            {
                var cars = from s in Db.Cars
                           select s;
                cars = Db.Cars.Include(m => m.Mark).Where(s => s.State == true);
                return cars.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: ", ex);
                return lista;
            }
        }
        public List<Car> GetAllData()
        {
            List<Car> lista = new List<Car>();
            try
            {
                var cars = from s in Db.Cars
                           select s;
                return cars.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: ", ex);
                return lista;
            }
        }

        public Reservation BuscadorReservacionID(Reservation pReservation)
        {
            
            IQueryable<Reservation> _reservacion = Db.Reservations.Where(r => r.CarID == pReservation.CarID && r.Action == "Reservado");

            return _reservacion.FirstOrDefault() ;
        }

        //metodo que crea una fila 
        //crea un registro de datos tipo carro
        public Car Create(Car pCar)
        {
            Car car = Db.Cars.Add(pCar);
            Db.SaveChanges();
            return car;
        }

        //metodo para borrar un auto
        public Car Delete(Car pCar)
        {
            Car car = Db.Cars.Remove(pCar);
            Db.SaveChanges();
            return car;
        }

        //metodo para editar con un try y catch que devuelve un resutaldo.
        public int Edit(Car pCar)
        {
            int resultado = 0;
            try
            {
                
                Db.Entry(pCar).State = EntityState.Modified;
                resultado = Db.SaveChanges();
                return resultado;
            }
            catch 
            {
                return resultado;
            }
        }



        //metodo para buscar por ID
        public Car SearchID(int ID)
        {
            Car car = Db.Cars.Find(ID);
            return car;
        }

        //metodo para buscar un auto
        //por marca y por codigo.
        public List<Car> Search(Car pCar)
        {
            var cars = from s in Db.Cars
                           select s;
           cars = Db.Cars.Where(s =>
               s.Code.ToUpper().Contains(pCar.Code.ToUpper()) || 
               s.Code.ToLower().Contains(pCar.Code.ToLower()));
            return cars.ToList();
        }

        //Metodo para modificar vehiculo
        public void UpdateCars(Car pCar)
        {
            using (var db = new DAL.DBCommon())
            {
                
                db.Entry(pCar).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        //Get all the eliminated Items
        public List<Car> GetAllDeleted()
        {
            List<Car> lista = new List<Car>();
            try
            {
                var cars = from s in Db.Cars
                           select s;
                cars = Db.Cars.Include(cm => cm.Mark).Where(
                    s => s.State == false);
                return cars.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: ", ex);
                return lista;
            }
        }
        //metodo para buscar un auto
        //por marca y por codigo y detalles.
        public List<Car> SearchCar(Car pCar)
        {
            var listaCars = Db.Cars.Include(m => m.Mark);
            var cars = from s in listaCars
                       select s;
            cars = Db.Cars.Where(
                s => s.Mark.Name.ToUpper().Contains(pCar.Mark.Name.ToUpper()) ||
                s.Code.ToUpper().Contains(pCar.Code.ToUpper()) ||
                s.Details.ToUpper().Contains(pCar.Mark.Name.ToUpper()) ||
                s.Details.ToLower().Contains(pCar.Mark.Name.ToLower()) ||
                s.Mark.Name.ToLower().Contains(pCar.Mark.Name.ToLower()) ||
                s.Code.ToLower().Contains(pCar.Code.ToLower()));
            return cars.ToList();
        }
    }
}
