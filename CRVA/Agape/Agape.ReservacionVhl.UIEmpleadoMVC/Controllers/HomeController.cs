
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//Librerias que se agregaron
using Agape.ReservacionVhl.EN;
using Agape.ReservacionVhl.BL;
using System.Web.Security;

namespace EmpleadoUI.Controllers
{
    public class HomeController : Controller
    {
        //retorna la vista de login por codigo de empleado
        public ActionResult Index()
        {
            if(Session["UserID"] != null)
                return RedirectToAction("Index", "Catalogo");
            if (ViewBag.MensajeLogin != null)
            {
                ViewBag.MensajeLogin = "No existe un ";
                
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult LogeoCodigo(Employee pEmployee)
        {
           
            EmployeeBL _employeeBl = new EmployeeBL();
            Employee _employee = new Employee();
            _employee = _employeeBl.loginCode(pEmployee);
         
            if (_employee != null)
                {
                if ((Request.Form["Code"] == pEmployee.Code))
                {
                    FormsAuthentication.SetAuthCookie(Request.Form["Code"],true);
                    Session["Name"] = _employee.Name.ToString();
                    Session["UserID"] = _employee.EmployeeID.ToString();
                    Session["LastName"] = _employee.LastName.ToString();
                    return RedirectToAction("Index", "Catalogo");
                }
               
            }
            else
            {
                ViewBag.MensajeLogin = "No user";
            }
            return View("Index");
        }
    }
}