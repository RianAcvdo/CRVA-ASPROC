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
    public class CarBL
    {
        //Codigo hecho por Cesar Garcia.
        /// <summary>
        /// Codigo para base de datos
        /// hace una instancia de objeto
        /// </summary>
        /// 
        private CarDAL carDal = new CarDAL();

        //metodo para obtener todos los datos de un carro
        public List<Car> GetAll()
        {
            return carDal.GetAll();
        }
        public List<Car> GetAllData()
        {
            return carDal.GetAllData();
        }

        //metodo para obtener todos los datos de un carro
        public List<Car> GetAllDeleted()
        {
            return carDal.GetAllDeleted();
        }
        //metodo que crea una fila 
        //crea un registro de datos tipo carro
        public Car Create(Car pCar)
        {
            return carDal.Create(pCar);
        }

        //metodo para borrar un auto
        public Car Delete(Car pCar)
        {
            return carDal.Delete(pCar);
        }

        //metodo para editar con un try y catch que devuelve un resutaldo.
        public int Edit(Car pCar)
        {
            return carDal.Edit(pCar);
        }

        //metodo para buscar por ID
        public Car SearchID(int ID)
        {
            return carDal.SearchID(ID);
        }

        public void UpdateCar(Car pCar)
        {
            carDal.UpdateCars(pCar);
        }

        //metodo para buscar un auto
        //por marca y por codigo.
        public List<Car> Search(Car pCar)
        {
            return carDal.Search(pCar);
        }

        //metodo para buscar un auto
        //por marca, descripcion y fecha.
        public List<Car> SearchCar(Car pCar)
        {
            return carDal.SearchCar(pCar);
        }

    }
}
