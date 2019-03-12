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
    public class EmployeeDAL
    {
        //Codigo hecho por Cesar Garcia.
        /// <summary>
        /// Codigo para base de datos
        /// hace una instancia de objeto
        /// </summary>
        private DBCommon Db = new DBCommon();

        //trae todo los datos de una lista de trabajadores
        public List<Employee> GetAll()
        {
            List<Employee> lista = new List<Employee>();
            try
            {
                var employees = from s in Db.Employees
                                select s;
                employees = Db.Employees.Where(
                    s => s.State == true);
                return employees.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: ", ex);
                return lista;
            }
        }

        //metodo que crea una fila 
        //crea un registro de datos tipo trabajadores
        public Employee Create(Employee pEmployee)
        {
            Employee Employee = Db.Employees.Add(pEmployee);
            Db.SaveChanges();
            return Employee;
        }

        //metodo para borrar un trabajadores
        public Employee Delete(Employee pEmployee)
        {
            Employee Employee = Db.Employees.Remove(pEmployee);
            Db.SaveChanges();
            return Employee;
        }

        //metodo para editar con un try y catch que devuelve un resutaldo.
        public int Edit(Employee pEmployee)
        {
            int resultado = 0;
            try
            {

                Db.Entry(pEmployee).State = EntityState.Modified;
                resultado = Db.SaveChanges();
                return resultado;
            }
            catch
            {
                return resultado;
            }
        }

        //metodo para buscar Employee por ID
        public Employee SearchID(int ID)
        {
            Employee Employee = Db.Employees.Find(ID);
            return Employee;
        }

        //metodo para buscar Employee un trabajador
        //busca por codigo , nombre y apellido
        public List<Employee> Search(Employee pEmployee)
        {
            var Employees = from s in Db.Employees
                            select s;
            Employees = Db.Employees.Where(
                s => s.Name.ToUpper().Contains(pEmployee.Name.ToUpper()) ||
                s.LastName.ToUpper().Contains(pEmployee.LastName.ToUpper()) ||
                s.Name.ToLower().Contains(pEmployee.Name.ToLower()) ||
                s.LastName.ToLower().Contains(pEmployee.LastName.ToLower()) ||
                s.Code.ToUpper().Contains(pEmployee.Code.ToUpper()) ||
                 s.Code.ToLower().Contains(pEmployee.Code.ToLower()) 
                );
            return Employees.ToList();

        }
        //Metodos para Eliminar logicamente y restaurar
        public void UpdateItem(Employee pItem)
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
        public List<Employee> GetAllDeleted()
        {
            List<Employee> lista = new List<Employee>();
            try
            {
                var objs = from s in Db.Employees
                           select s;
                objs = Db.Employees.Where(
                    s => s.State == false);
                return objs.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: ", ex);
                return lista;
            }
        }

        //metodo para login por codigo
        public Employee loginCode(Employee pEmployee)
        {
            Employee employee = new Employee();

            employee = Db.Employees.FirstOrDefault(s => s.Code.Equals(pEmployee.Code));

            return employee;
        }
    }
}
