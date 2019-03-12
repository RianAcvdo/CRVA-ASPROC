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
    public class EmployeeBL
    {
        //Codigo hecho por Cesar Garcia.
        /// <summary>
        /// Codigo para base de datos
        /// hace una instancia de objeto
        /// </summary>
        /// 
        private EmployeeDAL EmployeeDal = new EmployeeDAL();

        //metodo para obtener todos los datos de un Empleados
        public List<Employee> GetAll()
        {
            return EmployeeDal.GetAll();
        }
        //metodo que crea una fila 
        //crea un registro de datos tipo Empleados
        public Employee Create(Employee pEmployee)
        {
            return EmployeeDal.Create(pEmployee);
        }

        //metodo para borrar un Empleados
        public Employee Delete(Employee pEmployee)
        {
            return EmployeeDal.Delete(pEmployee);
        }

        //metodo para editar con un try y catch que devuelve un resultado.
        public int Edit(Employee pEmployee)
        {
            return EmployeeDal.Edit(pEmployee);
        }

        //metodo para buscar Employee por ID
        public Employee SearchID(int ID)
        {
            return EmployeeDal.SearchID(ID);
        }

        //metodo para buscar Employee un trabajador
        //por marca y por codigo.
        public List<Employee> Search(Employee pEmployee)
        {
            return EmployeeDal.Search(pEmployee);
        }
        //metodo para obtener todos los datos eliminados
        public List<Employee> GetAllDeleted()
        {
            return EmployeeDal.GetAllDeleted();
        }
        public void UpdateItem(Employee pItem)
        {
            EmployeeDal.UpdateItem(pItem);
        }

        //metodo para login por codigo de empleado
        public Employee loginCode(Employee pEmployee)
        {
           return EmployeeDal.loginCode(pEmployee);
        }
    }
}
